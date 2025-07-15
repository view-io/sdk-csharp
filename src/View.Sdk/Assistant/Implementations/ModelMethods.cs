namespace View.Sdk.Assistant.Implementations
{
    using RestWrapper;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Runtime.CompilerServices;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Assistant.Interfaces;

    /// <summary>
    /// Assistant model methods implementation.
    /// </summary>
    public class ModelMethods : IModelMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Assistant model methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public ModelMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<List<Model>> RetrieveMany(ModelRequest request, CancellationToken token = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/assistant/models";
            return await _Sdk.Post<ModelRequest, List<Model>>(url, request, token).ConfigureAwait(false) ?? new List<Model>();
        }

        /// <inheritdoc />
        public async IAsyncEnumerable<string> Retrieve(ModelRequest request, [EnumeratorCancellation] CancellationToken token = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrEmpty(request.ModelName)) throw new ArgumentNullException(nameof(request.ModelName));

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/assistant/models/pull";
            string json = _Sdk.Serializer.SerializeJson(request, true);

            using (RestRequest req = new RestRequest(url, HttpMethod.Post, "application/json"))
            {
                req.TimeoutMilliseconds = _Sdk.TimeoutMs;
                req.Authorization.BearerToken = _Sdk.AccessKey;
                req.ContentType = "application/json";

                if (_Sdk.LogRequests)
                    _Sdk.Log(SeverityEnum.Debug, "request body: " + Environment.NewLine + json);

                using (RestResponse resp = await req.SendAsync(json, token).ConfigureAwait(false))
                {
                    if (resp == null || resp.StatusCode < 200 || resp.StatusCode > 299)
                    {
                        _Sdk.Log(SeverityEnum.Warn, $"No or failed response from {url}");
                        yield break;
                    }

                    while (!token.IsCancellationRequested)
                    {
                        ServerSentEvent sse = await resp.ReadEventAsync();

                        if (sse == null || string.IsNullOrWhiteSpace(sse.Data))
                            yield break;

                        yield return sse.Data;
                    }
                }
            }
        }


        /// <inheritdoc />
        public async Task<ModelResponse> Delete(ModelRequest request, CancellationToken token = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrEmpty(request.ModelName)) throw new ArgumentNullException(nameof(request.ModelName));

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/assistant/models/delete";
            return await _Sdk.Post<ModelRequest, ModelResponse>(url, request, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<ModelResponse> PreLoad(ModelRequest request, CancellationToken token = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrEmpty(request.ModelName)) throw new ArgumentNullException(nameof(request.ModelName));

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/assistant/models/load";
            return await _Sdk.Post<ModelRequest, ModelResponse>(url, request, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<ModelResponse> Unload(ModelRequest request, CancellationToken token = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrEmpty(request.ModelName)) throw new ArgumentNullException(nameof(request.ModelName));

            request.Unload = true;
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/assistant/models/load";
            return await _Sdk.Post<ModelRequest, ModelResponse>(url, request, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}