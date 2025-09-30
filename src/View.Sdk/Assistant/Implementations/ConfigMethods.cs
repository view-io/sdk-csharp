namespace View.Sdk.Assistant.Implementations
{
    using RestWrapper;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Assistant.Interfaces;

    /// <summary>
    /// Assistant configuration methods.
    /// </summary>
    public class ConfigMethods : IConfigMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Assistant configuration methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public ConfigMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<AssistantConfigurationResponse> RetrieveMany(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/assistant/configs";
            return await _Sdk.Retrieve<AssistantConfigurationResponse>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<AssistantConfig> Retrieve(Guid configGuid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/assistant/configs/" + configGuid;
            return await _Sdk.Retrieve<AssistantConfig>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Exists(Guid configGuid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/assistant/configs/" + configGuid;
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<AssistantConfiguration> CreateRag(AssistantConfig config, CancellationToken token = default)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/assistant/configs";
            return await _Sdk.Post<AssistantConfig, AssistantConfiguration>(url, config, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<AssistantConfiguration> CreateChat(AssistantConfig config, CancellationToken token = default)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            config.ChatOnly = true;
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/assistant/configs";
            return await _Sdk.Post<AssistantConfig, AssistantConfiguration>(url, config, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<AssistantConfiguration> Update(AssistantConfig config, CancellationToken token = default)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/assistant/configs/" + config.GUID;
            using (RestRequest req = new RestRequest(url, HttpMethod.Put))
            {
                req.TimeoutMilliseconds = _Sdk.TimeoutMs;
                req.Authorization.BearerToken = _Sdk.AccessKey;
                req.ContentType = "application/json";

                string json = _Sdk.Serializer.SerializeJson(config, true);
                if (_Sdk.LogRequests) _Sdk.Log(SeverityEnum.Debug, "request: " + Environment.NewLine + json);

                using (RestResponse resp = await req.SendAsync(json, token).ConfigureAwait(false))
                {
                    if (resp != null)
                    {
                        string responseData = await _Sdk.ReadResponseDataAsync(resp, url, token).ConfigureAwait(false);

                        if (_Sdk.LogResponses) _Sdk.Log(SeverityEnum.Debug, "response (status " + resp.StatusCode + "): " + Environment.NewLine + responseData);

                        if (resp.StatusCode >= 200 && resp.StatusCode <= 299)
                        {
                            _Sdk.Log(SeverityEnum.Debug, "success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            if (!String.IsNullOrEmpty(responseData))
                            {
                                _Sdk.Log(SeverityEnum.Debug, "deserializing response body");
                                return _Sdk.Serializer.DeserializeJson<AssistantConfiguration>(responseData);
                            }
                            else
                            {
                                _Sdk.Log(SeverityEnum.Debug, "empty response body, returning null");
                                return null;
                            }
                        }
                        else
                        {
                            _Sdk.Log(SeverityEnum.Warn, "non-success reported from " + url + ": " + resp.StatusCode + ", " + resp.ContentLength + " bytes");
                            return null;
                        }
                    }
                    else
                    {
                        _Sdk.Log(SeverityEnum.Warn, "no response from " + url);
                        return null;
                    }
                }
            }
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid configGuid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/assistant/configs/" + configGuid;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Private-Methods

        #endregion
    }    
}