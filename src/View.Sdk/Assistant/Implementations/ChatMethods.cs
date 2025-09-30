namespace View.Sdk.Assistant.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Runtime.CompilerServices;
    using System.Text.Json;
    using System.Threading;
    using RestWrapper;
    using View.Sdk.Assistant.Interfaces;

    /// <summary>
    /// Assistant chat methods implementation.
    /// </summary>
    public class ChatMethods : IChatMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Assistant chat methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public ChatMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async IAsyncEnumerable<string> ProcessConfigChat(Guid configGuid, AssistantRequest request, [EnumeratorCancellation] CancellationToken token = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/assistant/chat/" + configGuid;
            string json = _Sdk.Serializer.SerializeJson(request, true);

            using (RestRequest req = new RestRequest(url, HttpMethod.Post, "application/json"))
            {
                req.TimeoutMilliseconds = _Sdk.TimeoutMs;
                req.Authorization.BearerToken = _Sdk.AccessKey;
                req.ContentType = "application/json";

                if (_Sdk.LogRequests) _Sdk.Log(SeverityEnum.Debug, "request body: " + Environment.NewLine + json);

                using (RestResponse resp = await req.SendAsync(json, token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            if (request.Stream)
                            {
                                while (true)
                                {
                                    ServerSentEvent sse = await resp.ReadEventAsync();
                                    if (sse != null) yield return ExtractToken(sse.Data);
                                    else
                                        yield break;
                                }
                            }
                            else
                            {
                                string responseData = await _Sdk.ReadResponse(resp, url, token).ConfigureAwait(false);
                                if (_Sdk.LogResponses) _Sdk.Log(SeverityEnum.Debug, "response: " + responseData);
                                yield return responseData;
                            }
                        }
                        else
                        {
                            _Sdk.Log(SeverityEnum.Warn, "non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            yield break;
                        }
                    }
                    else
                    {
                        _Sdk.Log(SeverityEnum.Warn, "no response from " + url);
                        yield break;
                    }
                }
            }
        }

        /// <inheritdoc/>
        public async IAsyncEnumerable<string> ProcessRagQuestion(AssistantRequest request, [EnumeratorCancellation] CancellationToken token = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/assistant/rag";
            string json = _Sdk.Serializer.SerializeJson(request, true);

            using (RestRequest req = new RestRequest(url, HttpMethod.Post, "application/json"))
            {
                req.TimeoutMilliseconds = _Sdk.TimeoutMs;
                req.Authorization.BearerToken = _Sdk.AccessKey;
                req.ContentType = "application/json";

                if (_Sdk.LogRequests) _Sdk.Log(SeverityEnum.Debug, "request body: " + Environment.NewLine + json);

                using (RestResponse resp = await req.SendAsync(json, token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            if (request.Stream)
                            {
                                while (true)
                                {
                                    ServerSentEvent sse = await resp.ReadEventAsync();
                                    if (sse != null) yield return ExtractToken(sse.Data);
                                    else
                                        yield break;
                                }
                            }
                            else
                            {
                                string responseData = await _Sdk.ReadResponse(resp, url, token).ConfigureAwait(false);
                                if (_Sdk.LogResponses) _Sdk.Log(SeverityEnum.Debug, "response: " + responseData);
                                yield return responseData;
                            }
                        }
                        else
                        {
                            _Sdk.Log(SeverityEnum.Warn, "non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            yield break;
                        }
                    }
                    else
                    {
                        _Sdk.Log(SeverityEnum.Warn, "no response from " + url);
                        yield break;
                    }
                }
            }
        }

        /// <inheritdoc />
        public async IAsyncEnumerable<string> ProcessRagMessage(AssistantRequest request, [EnumeratorCancellation] CancellationToken token = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/assistant/rag/chat";
            string json = _Sdk.Serializer.SerializeJson(request, true);

            using (RestRequest req = new RestRequest(url, HttpMethod.Post, "application/json"))
            {
                req.TimeoutMilliseconds = _Sdk.TimeoutMs;
                req.Authorization.BearerToken = _Sdk.AccessKey;
                req.ContentType = "application/json";

                if (_Sdk.LogRequests) _Sdk.Log(SeverityEnum.Debug, "request body: " + Environment.NewLine + json);

                using (RestResponse resp = await req.SendAsync(json, token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            if (request.Stream)
                            {
                                while (true)
                                {
                                    ServerSentEvent sse = await resp.ReadEventAsync();
                                    if (sse != null) yield return ExtractToken(sse.Data);
                                    else
                                        yield break;
                                }
                            }
                            else
                            {
                                string responseData = await _Sdk.ReadResponse(resp, url, token).ConfigureAwait(false);
                                if (_Sdk.LogResponses) _Sdk.Log(SeverityEnum.Debug, "response: " + responseData);
                                yield return responseData;
                            }
                        }
                        else
                        {
                            _Sdk.Log(SeverityEnum.Warn, "non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            yield break;
                        }
                    }
                    else
                    {
                        _Sdk.Log(SeverityEnum.Warn, "no response from " + url);
                        yield break;
                    }
                }
            }
        }

        /// <inheritdoc />
        public async IAsyncEnumerable<string> ProcessChatOnlyQuestion(AssistantRequest request, [EnumeratorCancellation] CancellationToken token = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/assistant/chat/completions";
            string json = _Sdk.Serializer.SerializeJson(request, true);

            using (RestRequest req = new RestRequest(url, HttpMethod.Post, "application/json"))
            {
                req.TimeoutMilliseconds = _Sdk.TimeoutMs;
                req.Authorization.BearerToken = _Sdk.AccessKey;
                req.ContentType = "application/json";

                if (_Sdk.LogRequests) _Sdk.Log(SeverityEnum.Debug, "request body: " + Environment.NewLine + json);

                using (RestResponse resp = await req.SendAsync(json, token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            if (request.Stream)
                            {
                                while (true)
                                {
                                    ServerSentEvent sse = await resp.ReadEventAsync();
                                    if (sse != null) yield return ExtractToken(sse.Data);
                                    else
                                        yield break;
                                }
                            }
                            else
                            {
                                string responseData = await _Sdk.ReadResponse(resp, url, token).ConfigureAwait(false);
                                if (_Sdk.LogResponses) _Sdk.Log(SeverityEnum.Debug, "response: " + responseData);
                                yield return responseData;
                            }
                        }
                        else
                        {
                            _Sdk.Log(SeverityEnum.Warn, "non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            yield break;
                        }
                    }
                    else
                    {
                        _Sdk.Log(SeverityEnum.Warn, "no response from " + url);
                        yield break;
                    }
                }
            }
        }

        /// <inheritdoc />
        public async IAsyncEnumerable<string> ProcessChatOnlyMessage(AssistantRequest request, [EnumeratorCancellation] CancellationToken token = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/assistant/chat/completions";
            string json = _Sdk.Serializer.SerializeJson(request, true);

            using (RestRequest req = new RestRequest(url, HttpMethod.Post, "application/json"))
            {
                req.TimeoutMilliseconds = _Sdk.TimeoutMs;
                req.Authorization.BearerToken = _Sdk.AccessKey;
                req.ContentType = "application/json";

                if (_Sdk.LogRequests) _Sdk.Log(SeverityEnum.Debug, "request body: " + Environment.NewLine + json);

                using (RestResponse resp = await req.SendAsync(json, token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            if (request.Stream)
                            {
                                while (true)
                                {
                                    ServerSentEvent sse = await resp.ReadEventAsync();
                                    if (sse != null) yield return ExtractToken(sse.Data);
                                    else
                                        yield break;
                                }
                            }
                            else
                            {
                                string responseData = await _Sdk.ReadResponse(resp, url, token).ConfigureAwait(false);
                                if (_Sdk.LogResponses) _Sdk.Log(SeverityEnum.Debug, "response: " + responseData);
                                yield return responseData;
                            }
                        }
                        else
                        {
                            _Sdk.Log(SeverityEnum.Warn, "non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            yield break;
                        }
                    }
                    else
                    {
                        _Sdk.Log(SeverityEnum.Warn, "no response from " + url);
                        yield break;
                    }
                }
            }
        }

        /// <inheritdoc />
        public async IAsyncEnumerable<string> ProcessChatOnlyMessageOpenAI(AssistantRequest request, [EnumeratorCancellation] CancellationToken token = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/assistant/chat/completions";
            string json = _Sdk.Serializer.SerializeJson(request, true);

            using (RestRequest req = new RestRequest(url, HttpMethod.Post, "application/json"))
            {
                req.TimeoutMilliseconds = _Sdk.TimeoutMs;
                req.Authorization.BearerToken = _Sdk.AccessKey;
                req.ContentType = "application/json";

                if (_Sdk.LogRequests) _Sdk.Log(SeverityEnum.Debug, "request body: " + Environment.NewLine + json);

                using (RestResponse resp = await req.SendAsync(json, token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            if (request.Stream)
                            {
                                while (true)
                                {
                                    ServerSentEvent sse = await resp.ReadEventAsync();
                                    if (sse != null) yield return ExtractToken(sse.Data);
                                    else
                                        yield break;
                                }
                            }
                            else
                            {
                                string responseData = await _Sdk.ReadResponse(resp, url, token).ConfigureAwait(false);
                                if (_Sdk.LogResponses) _Sdk.Log(SeverityEnum.Debug, "response: " + responseData);
                                yield return responseData;
                            }
                        }
                        else
                        {
                            _Sdk.Log(SeverityEnum.Warn, "non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            yield break;
                        }
                    }
                    else
                    {
                        _Sdk.Log(SeverityEnum.Warn, "no response from " + url);
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
                        : doc.RootElement.TryGetProperty("detail", out var detailElement)
                            ? detailElement.GetString()
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