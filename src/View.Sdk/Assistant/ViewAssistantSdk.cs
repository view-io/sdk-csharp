namespace View.Sdk.Assistant
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using RestWrapper;
    using View.Sdk;

    /// <summary>
    /// View Assistant SDK.
    /// </summary>
    public class ViewAssistantSdk : ViewSdkBase, IDisposable
    {
        #region Public-Members

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="endpoint">Endpoint URL.</param>
        public ViewAssistantSdk(string endpoint = "http://localhost:8331/") : base(endpoint)
        {
            Header = "[ViewAssistantSdk] ";
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// Process a RAG request.
        /// </summary>
        /// <returns>Enumerable of tokens.</returns>
        public async IAsyncEnumerable<string> ProcessRag(AssistantRagRequest ragRequest, [EnumeratorCancellation] CancellationToken token = default)
        {
            if (ragRequest == null) throw new ArgumentNullException(nameof(ragRequest));

            string url = Endpoint + "v1.0/rag";
            string json = Serializer.SerializeJson(ragRequest, true);

            using (RestRequest req = new RestRequest(url, HttpMethod.Post, "application/json"))
            {
                if (LogRequests) Log(SeverityEnum.Debug, "request body: " + Environment.NewLine + json);

                using (RestResponse resp = await req.SendAsync(Serializer.SerializeJson(ragRequest, true), token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            while (true)
                            {
                                ServerSentEvent sse = await resp.ReadEventAsync();
                                if (sse != null) yield return ExtractToken(sse.Data);
                                else
                                {
                                    Console.WriteLine("ServerSentEvent is null");
                                    yield break;
                                }
                            }
                        }
                    }
                    else
                    {
                        Log(SeverityEnum.Warn, "no response from " + url);
                        yield break;
                    }
                }
            }
        }

        /// <summary>
        /// Process a chat request.
        /// </summary>
        /// <returns>Enumerable of tokens.</returns>
        public async IAsyncEnumerable<string> ProcessChat(AssistantChatRequest chatRequest, [EnumeratorCancellation] CancellationToken token = default)
        {
            if (chatRequest == null) throw new ArgumentNullException(nameof(chatRequest));

            string url = Endpoint + "v1.0/chat";
            string json = Serializer.SerializeJson(chatRequest, true);

            using (RestRequest req = new RestRequest(url, HttpMethod.Post, "application/json"))
            {
                if (LogRequests) Log(SeverityEnum.Debug, "request body: " + Environment.NewLine + json);

                using (RestResponse resp = await req.SendAsync(Serializer.SerializeJson(chatRequest, true), token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            while (true)
                            {
                                ServerSentEvent sse = await resp.ReadEventAsync();
                                if (sse != null) yield return ExtractToken(sse.Data);
                                else
                                {
                                    Console.WriteLine("ServerSentEvent is null");
                                    yield break;
                                }
                            }
                        }
                    }
                    else
                    {
                        Log(SeverityEnum.Warn, "no response from " + url);
                        yield break;
                    }
                }
            }
        }

        #endregion

        #region Private-Methods
        private string ExtractToken(string json)
        {
            try
            {
                using (JsonDocument doc = System.Text.Json.JsonDocument.Parse(json))
                {
                    return doc.RootElement.TryGetProperty("token", out var tokenElement)
                        ? tokenElement.GetString()
                        : null;
                }
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}
