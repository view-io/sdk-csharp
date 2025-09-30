namespace View.Sdk.Completions.Providers.Ollama
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
    /// View Ollama completions SDK for interacting with the Ollama API to generate text completions.
    /// Supports both streaming and non-streaming responses.
    /// </summary>
    public class ViewOllamaCompletionsSdk : CompletionsProviderSdkBase, IDisposable
    {
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

        #region Public-Members

        #endregion

        #region Private-Members

        private Serializer _Serializer = new Serializer();
        private string _DefaultModel = "llama3";

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate a new View Ollama completions SDK.
        /// </summary>
        /// <param name="tenantGuid">Tenant GUID for multi-tenancy support.</param>
        /// <param name="baseUrl">Base URL for the Ollama API. Default is "http://localhost:11434/".</param>
        /// <param name="apiKey">API key for authentication. Default is null.</param>
        public ViewOllamaCompletionsSdk(
            Guid tenantGuid,
            string baseUrl = "http://localhost:11434/",
            string apiKey = null) : base(
                tenantGuid,
                CompletionsProviderEnum.Ollama,
                baseUrl,
                apiKey)
        {
            Header = "[OllamaCompletionsSdk] ";
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// Validate connectivity to the Ollama API endpoint.
        /// </summary>
        /// <param name="token">Cancellation token for the operation.</param>
        /// <returns>True if connectivity is successful, false otherwise.</returns>
        public override async Task<bool> ValidateConnectivity(CancellationToken token = default)
        {
            string url = BaseUrl;

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
        /// Generate a completion using the Ollama API.
        /// Supports both streaming and non-streaming modes based on the request configuration.
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

            // Determine endpoint based on whether we have messages or prompt
            string endpoint = (req.Messages != null && req.Messages.Count > 0) ? "api/chat" : "api/generate";
            string url = BaseUrl + endpoint;

            GenerateCompletionResult result = new GenerateCompletionResult
            {
                Provider = CompletionsProviderEnum.Ollama,
                Model = req.Model,
                StartUtc = DateTime.UtcNow
            };

            OllamaCompletionsRequest ollamaReq = OllamaCompletionsRequest.FromCompletionRequest(req);

            if (!req.Stream)
            {
                // Non-streaming request
                using (RestRequest restReq = new RestRequest(url, HttpMethod.Post))
                {
                    restReq.ContentType = "application/json";
                    restReq.TimeoutMilliseconds = timeoutMs;
                    restReq.Authorization.BearerToken = ApiKey;

                    string json = Serializer.SerializeJson(ollamaReq, true);
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
                            string responseData = await ReadResponseDataAsync(resp, url, token).ConfigureAwait(false);

                            if (LogResponses) Log(SeverityEnum.Debug, "response (status " + resp.StatusCode + "): " + Environment.NewLine + responseData);

                            if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                            {
                                if (!string.IsNullOrEmpty(responseData))
                                {
                                    Log(SeverityEnum.Debug, "deserializing response body");
                                    OllamaCompletionsResult ollamaResult = Serializer.DeserializeJson<OllamaCompletionsResult>(responseData);
                                    GenerateCompletionResult completionResult = ollamaResult.ToCompletionResult(req, true, resp.StatusCode, null);

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
                                Log(SeverityEnum.Warn, "status " + resp.StatusCode + " received from " + url + ": " + Environment.NewLine + responseData);
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
                result.Tokens = StreamCompletionAsync(url, ollamaReq, result, timeoutMs, token);
                return result;
            }
        }

        #endregion

        #region Private-Methods

        private async IAsyncEnumerable<CompletionToken> StreamCompletionAsync(
            string url,
            OllamaCompletionsRequest ollamaReq,
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

                string json = Serializer.SerializeJson(ollamaReq, true);
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

                    OllamaCompletionsResult chunk = null;
                    try
                    {
                        if (LogResponses) Log(SeverityEnum.Debug, "streaming chunk: " + line);
                        chunk = Serializer.DeserializeJson<OllamaCompletionsResult>(line);
                    }
                    catch (JsonException ex)
                    {
                        Log(SeverityEnum.Warn, "failed to parse streaming chunk: " + ex.Message);
                        continue;
                    }

                    if (chunk != null)
                    {
                        CompletionToken completionToken = chunk.ToCompletionToken(index++);

                        if (firstToken)
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