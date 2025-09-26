namespace View.Sdk.Completions.Providers.OpenAI
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using RestWrapper;
    using View.Sdk;
    using View.Sdk.Completions;
    using View.Sdk.Embeddings.Providers;
    using View.Sdk.Serialization;

    /// <summary>
    /// View OpenAI completions SDK for interacting with the OpenAI Chat Completions API to generate text completions.
    /// Supports both streaming and non-streaming responses with Server-Sent Events (SSE) for streaming.
    /// </summary>
    public class ViewOpenAiCompletionsSdk : CompletionsProviderSdkBase, IDisposable
    {
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

        #region Public-Members

        #endregion

        #region Private-Members

        private Serializer _Serializer = new Serializer();
        private string _DefaultModel = "gpt-3.5-turbo";

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate a new View OpenAI completions SDK.
        /// </summary>
        /// <param name="tenantGuid">Tenant GUID for multi-tenancy support.</param>
        /// <param name="baseUrl">Base URL for the OpenAI API. Default is "https://api.openai.com/".</param>
        /// <param name="apiKey">API key for authentication. Required for OpenAI API access. Default is null.</param>
        public ViewOpenAiCompletionsSdk(
            Guid tenantGuid,
            string baseUrl = "https://api.openai.com/",
            string apiKey = null) : base(
                tenantGuid,
                CompletionsProviderEnum.OpenAI,
                baseUrl,
                apiKey)
        {
            Header = "[OpenAiCompletionsSdk] ";
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// Validate connectivity to the OpenAI API endpoint.
        /// Tests connectivity by fetching the list of available models.
        /// </summary>
        /// <param name="token">Cancellation token for the operation.</param>
        /// <returns>True if connectivity is successful, false otherwise.</returns>
        public override async Task<bool> ValidateConnectivity(CancellationToken token = default)
        {
            string url = BaseUrl + "v1/models";

            try
            {
                using (RestRequest req = new RestRequest(url, HttpMethod.Get))
                {
                    req.Authorization.BearerToken = ApiKey;

                    using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                    {
                        if (resp != null)
                        {
                            if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                            {
                                return true;
                            }
                            else
                            {
                                Log(SeverityEnum.Warn, "non-success response " + resp.StatusCode + " from " + url);
                                return false;
                            }
                        }
                        else
                        {
                            Log(SeverityEnum.Warn, "no response from " + url);
                            return false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log(SeverityEnum.Warn, "exception while connecting to " + url + Environment.NewLine + e.ToString());
                return false;
            }
        }

        /// <summary>
        /// Generate a completion using the OpenAI Chat Completions API.
        /// Supports both streaming (SSE) and non-streaming modes based on the request configuration.
        /// </summary>
        /// <param name="req">Completion request containing model, messages, and generation parameters.</param>
        /// <param name="timeoutMs">Timeout in milliseconds. Range: 1 to int.MaxValue. Default is 30000.</param>
        /// <param name="token">Cancellation token for the operation.</param>
        /// <returns>Completion result containing the generated tokens.</returns>
        public override async Task<GenerateCompletionResult> GenerateCompletionAsync(
            GenerateCompletionRequest req,
            int timeoutMs = 30000,
            CancellationToken token = default)
        {
            if (req == null) throw new ArgumentNullException(nameof(req));
            if (timeoutMs < 1) throw new ArgumentOutOfRangeException(nameof(timeoutMs));
            if (string.IsNullOrEmpty(req.Model)) req.Model = _DefaultModel;

            string url = BaseUrl + "v1/chat/completions";

            GenerateCompletionResult result = new GenerateCompletionResult
            {
                Provider = CompletionsProviderEnum.OpenAI,
                Model = req.Model,
                StartUtc = DateTime.UtcNow
            };

            OpenAiCompletionsRequest openAiReq = OpenAiCompletionsRequest.FromCompletionRequest(req);

            if (!req.Stream)
            {
                // Non-streaming request
                using (RestRequest restReq = new RestRequest(url, HttpMethod.Post))
                {
                    restReq.ContentType = "application/json";
                    restReq.TimeoutMilliseconds = timeoutMs;
                    restReq.Authorization.BearerToken = ApiKey;

                    string json = Serializer.SerializeJson(openAiReq, true);
                    if (LogRequests) Log(SeverityEnum.Debug, "request:" + Environment.NewLine + json);

                    using (RestResponse resp = await restReq.SendAsync(json, token).ConfigureAwait(false))
                    {
                        if (resp == null)
                        {
                            Log(SeverityEnum.Warn, "no response from " + url);
                            result.Tokens = GenerateErrorTokenStream("No connectivity to completions provider at " + url + ".");
                            return result;
                        }
                        else
                        {
                            if (LogResponses) Log(SeverityEnum.Debug, "response (status " + resp.StatusCode + "): " + Environment.NewLine + resp.DataAsString);

                            if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                            {
                                if (!string.IsNullOrEmpty(resp.DataAsString))
                                {
                                    Log(SeverityEnum.Debug, "deserializing response body");
                                    OpenAiCompletionsResult openAiResult = Serializer.DeserializeJson<OpenAiCompletionsResult>(resp.DataAsString);
                                    GenerateCompletionResult completionResult = openAiResult.ToCompletionResult(req, true, resp.StatusCode, null);

                                    // Update timing information
                                    result.FirstTokenUtc = DateTime.UtcNow;
                                    result.LastTokenUtc = DateTime.UtcNow;
                                    result.Tokens = completionResult.Tokens;

                                    return result;
                                }
                                else
                                {
                                    Log(SeverityEnum.Warn, "no data received from " + url);
                                    result.Tokens = GenerateErrorTokenStream("No completions returned from the provider.");
                                    return result;
                                }
                            }
                            else
                            {
                                Log(SeverityEnum.Warn, "status " + resp.StatusCode + " received from " + url + ": " + Environment.NewLine + resp.DataAsString);
                                result.Tokens = GenerateErrorTokenStream("Failure reported by the completions provider.");
                                return result;
                            }
                        }
                    }
                }
            }
            else
            {
                // Streaming request
                result.Tokens = StreamCompletionAsync(url, openAiReq, result, timeoutMs, token);
                return result;
            }
        }

        #endregion

        #region Private-Methods

        private async IAsyncEnumerable<CompletionToken> StreamCompletionAsync(
            string url,
            OpenAiCompletionsRequest openAiReq,
            GenerateCompletionResult result,
            int timeoutMs,
            [EnumeratorCancellation] CancellationToken token = default)
        {
            HttpClient client = null;
            HttpResponseMessage response = null;
            Stream stream = null;
            StreamReader reader = null;

            try
            {
                client = new HttpClient();
                client.Timeout = TimeSpan.FromMilliseconds(timeoutMs);
                if (!string.IsNullOrEmpty(ApiKey))
                {
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ApiKey);
                }

                string json = Serializer.SerializeJson(openAiReq, true);
                if (LogRequests) Log(SeverityEnum.Debug, "streaming request:" + Environment.NewLine + json);

                using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url) { Content = content })
                {
                    response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, token);
                }
            }
            catch (HttpRequestException ex)
            {
                Log(SeverityEnum.Warn, "HTTP request exception during streaming: " + ex.ToString());
                yield break;
            }
            catch (TaskCanceledException)
            {
                Log(SeverityEnum.Debug, "streaming request cancelled or timed out");
                yield break;
            }
            finally
            {
                client?.Dispose();
            }

            if (response == null)
            {
                yield break;
            }

            try
            {
                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    Log(SeverityEnum.Warn, "streaming error response: " + errorContent);
                    yield break;
                }

                stream = await response.Content.ReadAsStreamAsync();
                reader = new StreamReader(stream);

                string line;
                int index = 0;
                bool firstToken = true;

                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (token.IsCancellationRequested)
                        break;

                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // OpenAI sends SSE format with "data: " prefix
                    if (line.StartsWith("data: "))
                    {
                        string dataContent = line.Substring(6);

                        // Check for end of stream
                        if (dataContent == "[DONE]")
                        {
                            break;
                        }

                        OpenAiCompletionsResult chunk = null;
                        try
                        {
                            if (LogResponses) Log(SeverityEnum.Debug, "streaming chunk: " + dataContent);
                            chunk = Serializer.DeserializeJson<OpenAiCompletionsResult>(dataContent);
                        }
                        catch (JsonException ex)
                        {
                            Log(SeverityEnum.Warn, "failed to parse streaming chunk: " + ex.Message);
                            continue;
                        }

                        if (chunk != null)
                        {
                            CompletionToken completionToken = chunk.ToCompletionToken(index++);

                            // Only yield tokens with content or finish reason
                            if (!string.IsNullOrEmpty(completionToken.Content) || completionToken.IsComplete)
                            {
                                if (firstToken && !string.IsNullOrEmpty(completionToken.Content))
                                {
                                    result.FirstTokenUtc = completionToken.TimestampUtc;
                                    firstToken = false;
                                }

                                if (completionToken.IsComplete)
                                {
                                    result.LastTokenUtc = completionToken.TimestampUtc;
                                }

                                yield return completionToken;
                            }
                        }
                    }
                }
            }
            finally
            {
                reader?.Dispose();
                stream?.Dispose();
                response?.Dispose();
            }
        }

        private async IAsyncEnumerable<CompletionToken> GenerateErrorTokenStream(string errorMessage)
        {
            await Task.CompletedTask;
            yield break;
        }

        #endregion

#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    }
}