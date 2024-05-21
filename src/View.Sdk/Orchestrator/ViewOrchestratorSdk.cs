﻿namespace View.Sdk.Orchestrator
{
    using RestWrapper;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Serializer;
    using View.Sdk.Shared.Orchestrator;

    /// <summary>
    /// View Orchestrator SDK.
    /// </summary>
    public class ViewOrchestratorSdk
    {
        #region Public-Members

        /// <summary>
        /// Method to invoke to send log messages.
        /// </summary>
        public Action<string> Logger { get; set; } = null;
         
        #endregion

        #region Private-Members

        private string _Header = "[ViewOrchestratorSdk] ";
        private Uri _Uri = null;
        private string _TenantGuid = Guid.NewGuid().ToString();
        private string _AccessKey = null;
        private string _Endpoint = "http://localhost:8501/";
        private SerializationHelper _Serializer = new SerializationHelper();
        
        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="tenantGuid">Tenant GUID.</param>
        /// <param name="accessKey">Access key.</param>
        /// <param name="endpoint">Endpoint URL.</param>
        public ViewOrchestratorSdk(string tenantGuid, string accessKey, string endpoint = "http://localhost:8501/")
        { 
            if (string.IsNullOrEmpty(tenantGuid)) throw new ArgumentNullException(nameof(tenantGuid));
            if (string.IsNullOrEmpty(accessKey)) throw new ArgumentNullException(nameof(accessKey));
            if (string.IsNullOrEmpty(endpoint)) throw new ArgumentNullException(nameof(endpoint));

            _TenantGuid = tenantGuid;
            _AccessKey = accessKey;
            _Uri = new Uri(endpoint);
            _Endpoint = _Uri.ToString();
            if (!_Endpoint.EndsWith("/")) _Endpoint += "/";
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// Validate connectivity.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Boolean indicating success.</returns>
        public async Task<bool> ValidateConnectivity(CancellationToken token = default)
        {
            string url = _Endpoint;

            using (RestRequest req = new RestRequest(url))
            {
                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        Log("success reported from " + url);
                        return true;
                    }
                    else if (resp != null)
                    {
                        Log("non-success reported from " + url + ": " + resp.StatusCode);
                        return false;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return false;
                    }
                }
            }
        }

        #region Tenants

        /// <summary>
        /// Create a tenant.
        /// </summary>
        /// <param name="tenant">Tenant.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Tenant.</returns>
        public async Task<TenantMetadata> CreateTenant(TenantMetadata tenant, CancellationToken token = default)
        {
            if (tenant == null) throw new ArgumentNullException(nameof(tenant));

            string url = _Endpoint + "v1.0/tenants";

            using (RestRequest req = new RestRequest(url, HttpMethod.Put))
            {
                req.ContentType = Constants.JsonContentType;
                req.Authorization.BearerToken = _AccessKey;

                using (RestResponse resp = await req.SendAsync(_Serializer.SerializeJson(tenant, true), token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        return _Serializer.DeserializeJson<TenantMetadata>(resp.DataAsString);
                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                        return null;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Read a tenant.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Tenant.</returns>
        public async Task<TenantMetadata> RetrieveTenant(CancellationToken token = default)
        {
            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid;

            using (RestRequest req = new RestRequest(url))
            {
                req.Authorization.BearerToken = _AccessKey;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        return _Serializer.DeserializeJson<TenantMetadata>(resp.DataAsString);
                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                        return null;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Update a tenant.
        /// </summary>
        /// <param name="tenant">Tenant.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Tenant.</returns>
        public async Task<TenantMetadata> UpdateTenant(TenantMetadata tenant, CancellationToken token = default)
        {
            if (tenant == null) throw new ArgumentNullException(nameof(tenant));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid;

            using (RestRequest req = new RestRequest(url, HttpMethod.Put))
            {
                req.Authorization.BearerToken = _AccessKey;
                req.ContentType = Constants.JsonContentType;

                using (RestResponse resp = await req.SendAsync(_Serializer.SerializeJson(tenant, true), token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        return _Serializer.DeserializeJson<TenantMetadata>(resp.DataAsString);
                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                        return null;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return null;
                    }
                }
            }
        }

        #endregion

        #region Users

        /// <summary>
        /// Create a user.
        /// </summary>
        /// <param name="user">User.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>User.</returns>
        public async Task<UserMaster> CreateUser(UserMaster user, CancellationToken token = default)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/users";

            using (RestRequest req = new RestRequest(url, HttpMethod.Put))
            {
                req.Authorization.BearerToken = _AccessKey;
                req.ContentType = Constants.JsonContentType;

                using (RestResponse resp = await req.SendAsync(_Serializer.SerializeJson(user, true), token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        return _Serializer.DeserializeJson<UserMaster>(resp.DataAsString);
                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                        return null;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Check if a user exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> ExistsUser(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/users/" + guid;

            using (RestRequest req = new RestRequest(url, HttpMethod.Head))
            {
                req.Authorization.BearerToken = _AccessKey;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        return true;
                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                        return false;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Read a user.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>User.</returns>
        public async Task<UserMaster> RetrieveUser(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/users/" + guid;

            using (RestRequest req = new RestRequest(url))
            {
                req.Authorization.BearerToken = _AccessKey;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        return _Serializer.DeserializeJson<UserMaster>(resp.DataAsString);
                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                        return null;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Read users.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Users.</returns>
        public async Task<List<UserMaster>> RetrieveUsers(CancellationToken token = default)
        {
            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/users";

            using (RestRequest req = new RestRequest(url))
            {
                req.Authorization.BearerToken = _AccessKey;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        return _Serializer.DeserializeJson<List<UserMaster>>(resp.DataAsString);
                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                        return null;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Update a user.
        /// </summary>
        /// <param name="user">User.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>User.</returns>
        public async Task<UserMaster> UpdateUser(UserMaster user, CancellationToken token = default)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/users/" + user.GUID;

            using (RestRequest req = new RestRequest(url, HttpMethod.Put))
            {
                req.Authorization.BearerToken = _AccessKey;
                req.ContentType = Constants.JsonContentType;

                using (RestResponse resp = await req.SendAsync(_Serializer.SerializeJson(user, true), token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        return _Serializer.DeserializeJson<UserMaster>(resp.DataAsString);
                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                        return null;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Delete a user.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteUser(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/users/" + guid;

            using (RestRequest req = new RestRequest(url, HttpMethod.Delete))
            {
                req.Authorization.BearerToken = _AccessKey;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode >= 200 && resp.StatusCode <= 204)
                    {

                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                    }
                    else
                    {
                        Log("no response from " + url);
                    }

                    return;
                }
            }
        }

        #endregion

        #region Credentials

        /// <summary>
        /// Create a credential.
        /// </summary>
        /// <param name="cred">Credential.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Credential.</returns>
        public async Task<Credential> CreateCredential(Credential cred, CancellationToken token = default)
        {
            if (cred == null) throw new ArgumentNullException(nameof(cred));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/credentials";

            using (RestRequest req = new RestRequest(url, HttpMethod.Put))
            {
                req.Authorization.BearerToken = _AccessKey;
                req.ContentType = Constants.JsonContentType;

                using (RestResponse resp = await req.SendAsync(_Serializer.SerializeJson(cred, true), token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        return _Serializer.DeserializeJson<Credential>(resp.DataAsString);
                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                        return null;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Check if a credential exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> ExistsCredential(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/credentials/" + guid;

            using (RestRequest req = new RestRequest(url, HttpMethod.Head))
            {
                req.Authorization.BearerToken = _AccessKey;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        return true;
                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                        return false;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Read a credential.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Credential.</returns>
        public async Task<Credential> RetrieveCredential(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/credentials/" + guid;

            using (RestRequest req = new RestRequest(url))
            {
                req.Authorization.BearerToken = _AccessKey;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        return _Serializer.DeserializeJson<Credential>(resp.DataAsString);
                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                        return null;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Read credentials.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Credentials.</returns>
        public async Task<List<Credential>> RetrieveCredentials(CancellationToken token = default)
        {
            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/credentials";

            using (RestRequest req = new RestRequest(url))
            {
                req.Authorization.BearerToken = _AccessKey;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        return _Serializer.DeserializeJson<List<Credential>>(resp.DataAsString);
                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                        return null;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Update a credential.
        /// </summary>
        /// <param name="cred">Credential.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Credential.</returns>
        public async Task<Credential> UpdateCredential(Credential cred, CancellationToken token = default)
        {
            if (cred == null) throw new ArgumentNullException(nameof(cred));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/credentials/" + cred.GUID;

            using (RestRequest req = new RestRequest(url, HttpMethod.Put))
            {
                req.Authorization.BearerToken = _AccessKey;
                req.ContentType = Constants.JsonContentType;

                using (RestResponse resp = await req.SendAsync(_Serializer.SerializeJson(cred, true), token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        return _Serializer.DeserializeJson<Credential>(resp.DataAsString);
                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                        return null;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Delete a credential.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteCredential(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/credentials/" + guid;

            using (RestRequest req = new RestRequest(url, HttpMethod.Delete))
            {
                req.Authorization.BearerToken = _AccessKey;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode >= 200 && resp.StatusCode <= 204)
                    {

                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                    }
                    else
                    {
                        Log("no response from " + url);
                    }

                    return;
                }
            }
        }

        #endregion

        #region Triggers

        /// <summary>
        /// Create a trigger.
        /// </summary>
        /// <param name="trigger">Trigger.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Trigger.</returns>
        public async Task<Trigger> CreateTrigger(Trigger trigger, CancellationToken token = default)
        {
            if (trigger == null) throw new ArgumentNullException(nameof(trigger));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/triggers";

            using (RestRequest req = new RestRequest(url, HttpMethod.Put))
            {
                req.Authorization.BearerToken = _AccessKey;
                req.ContentType = Constants.JsonContentType;

                using (RestResponse resp = await req.SendAsync(_Serializer.SerializeJson(trigger, true), token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        return _Serializer.DeserializeJson<Trigger>(resp.DataAsString);
                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                        return null;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Check if a trigger exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> ExistsTrigger(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/triggers/" + guid;

            using (RestRequest req = new RestRequest(url, HttpMethod.Head))
            {
                req.Authorization.BearerToken = _AccessKey;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        return true;
                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                        return false;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Read a trigger.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Trigger.</returns>
        public async Task<Trigger> RetrieveTrigger(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/triggers/" + guid;

            using (RestRequest req = new RestRequest(url))
            {
                req.Authorization.BearerToken = _AccessKey;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        return _Serializer.DeserializeJson<Trigger>(resp.DataAsString);
                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                        return null;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Read triggers.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Triggers.</returns>
        public async Task<List<Trigger>> RetrieveTriggers(CancellationToken token = default)
        {
            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/triggers";

            using (RestRequest req = new RestRequest(url))
            {
                req.Authorization.BearerToken = _AccessKey;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        return _Serializer.DeserializeJson<List<Trigger>>(resp.DataAsString);
                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                        return null;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Update a trigger.
        /// </summary>
        /// <param name="trigger">Trigger.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Trigger.</returns>
        public async Task<Trigger> UpdateTrigger(Trigger trigger, CancellationToken token = default)
        {
            if (trigger == null) throw new ArgumentNullException(nameof(trigger));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/triggers/" + trigger.GUID;

            using (RestRequest req = new RestRequest(url, HttpMethod.Put))
            {
                req.Authorization.BearerToken = _AccessKey;
                req.ContentType = Constants.JsonContentType;

                using (RestResponse resp = await req.SendAsync(_Serializer.SerializeJson(trigger, true), token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        return _Serializer.DeserializeJson<Trigger>(resp.DataAsString);
                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                        return null;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Delete a trigger.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteTrigger(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/triggers/" + guid;

            using (RestRequest req = new RestRequest(url, HttpMethod.Delete))
            {
                req.Authorization.BearerToken = _AccessKey;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode >= 200 && resp.StatusCode <= 204)
                    {

                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                    }
                    else
                    {
                        Log("no response from " + url);
                    }

                    return;
                }
            }
        }

        #endregion

        #region Steps

        /// <summary>
        /// Create a step.
        /// </summary>
        /// <param name="step">Step.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Step.</returns>
        public async Task<StepMetadata> CreateStep(StepMetadata step, CancellationToken token = default)
        {
            if (step == null) throw new ArgumentNullException(nameof(step));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/steps";

            using (RestRequest req = new RestRequest(url, HttpMethod.Put))
            {
                req.Authorization.BearerToken = _AccessKey;
                req.ContentType = Constants.JsonContentType;

                using (RestResponse resp = await req.SendAsync(_Serializer.SerializeJson(step, true), token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        return _Serializer.DeserializeJson<StepMetadata>(resp.DataAsString);
                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                        return null;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Check if a step exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> ExistsStep(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/steps/" + guid;

            using (RestRequest req = new RestRequest(url, HttpMethod.Head))
            {
                req.Authorization.BearerToken = _AccessKey;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        return true;
                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                        return false;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Read a step.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Step.</returns>
        public async Task<StepMetadata> RetrieveStep(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/steps/" + guid;

            using (RestRequest req = new RestRequest(url))
            {
                req.Authorization.BearerToken = _AccessKey;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        return _Serializer.DeserializeJson<StepMetadata>(resp.DataAsString);
                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                        return null;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Read steps.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Steps.</returns>
        public async Task<List<StepMetadata>> RetrieveSteps(CancellationToken token = default)
        {
            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/steps";

            using (RestRequest req = new RestRequest(url))
            {
                req.Authorization.BearerToken = _AccessKey;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        return _Serializer.DeserializeJson<List<StepMetadata>>(resp.DataAsString);
                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                        return null;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Update a step.
        /// </summary>
        /// <param name="step">Step.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Step.</returns>
        public async Task<StepMetadata> UpdateStep(StepMetadata step, CancellationToken token = default)
        {
            if (step == null) throw new ArgumentNullException(nameof(step));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/steps/" + step.GUID;

            using (RestRequest req = new RestRequest(url, HttpMethod.Put))
            {
                req.Authorization.BearerToken = _AccessKey;
                req.ContentType = Constants.JsonContentType;

                using (RestResponse resp = await req.SendAsync(_Serializer.SerializeJson(step, true), token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        return _Serializer.DeserializeJson<StepMetadata>(resp.DataAsString);
                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                        return null;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Delete a step.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteStep(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/steps/" + guid;

            using (RestRequest req = new RestRequest(url, HttpMethod.Delete))
            {
                req.Authorization.BearerToken = _AccessKey;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode >= 200 && resp.StatusCode <= 204)
                    {

                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                    }
                    else
                    {
                        Log("no response from " + url);
                    }

                    return;
                }
            }
        }

        #endregion

        #region DataFlows

        /// <summary>
        /// Create a data flow.
        /// </summary>
        /// <param name="flow">DataFlow.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>DataFlow.</returns>
        public async Task<DataFlow> CreateDataFlow(DataFlow flow, CancellationToken token = default)
        {
            if (flow == null) throw new ArgumentNullException(nameof(flow));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/dataflows";

            using (RestRequest req = new RestRequest(url, HttpMethod.Put))
            {
                req.Authorization.BearerToken = _AccessKey;
                req.ContentType = Constants.JsonContentType;

                using (RestResponse resp = await req.SendAsync(_Serializer.SerializeJson(flow, true), token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        return _Serializer.DeserializeJson<DataFlow>(resp.DataAsString);
                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                        return null;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Check if a data flow exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> ExistsDataFlow(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/dataflows/" + guid;

            using (RestRequest req = new RestRequest(url, HttpMethod.Head))
            {
                req.Authorization.BearerToken = _AccessKey;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        return true;
                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                        return false;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Read a data flow.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>DataFlow.</returns>
        public async Task<DataFlow> RetrieveDataFlow(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/dataflows/" + guid;

            using (RestRequest req = new RestRequest(url))
            {
                req.Authorization.BearerToken = _AccessKey;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        return _Serializer.DeserializeJson<DataFlow>(resp.DataAsString);
                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                        return null;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Read data flows.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>DataFlows.</returns>
        public async Task<List<DataFlow>> RetrieveDataFlows(CancellationToken token = default)
        {
            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/dataflows";

            using (RestRequest req = new RestRequest(url))
            {
                req.Authorization.BearerToken = _AccessKey;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        return _Serializer.DeserializeJson<List<DataFlow>>(resp.DataAsString);
                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                        return null;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Update a data flow.
        /// </summary>
        /// <param name="flow">DataFlow.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>DataFlow.</returns>
        public async Task<DataFlow> UpdateDataFlow(DataFlow flow, CancellationToken token = default)
        {
            if (flow == null) throw new ArgumentNullException(nameof(flow));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/dataflows/" + flow.GUID;

            using (RestRequest req = new RestRequest(url, HttpMethod.Put))
            {
                req.Authorization.BearerToken = _AccessKey;
                req.ContentType = Constants.JsonContentType;

                using (RestResponse resp = await req.SendAsync(_Serializer.SerializeJson(flow, true), token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        return _Serializer.DeserializeJson<DataFlow>(resp.DataAsString);
                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                        return null;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Delete a data flow.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteDataFlow(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/dataflows/" + guid;

            using (RestRequest req = new RestRequest(url, HttpMethod.Delete))
            {
                req.Authorization.BearerToken = _AccessKey;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode >= 200 && resp.StatusCode <= 204)
                    {

                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                    }
                    else
                    {
                        Log("no response from " + url);
                    }

                    return;
                }
            }
        }

        #endregion

        #region DataFlow-Logs

        /// <summary>
        /// Read data flow logs.
        /// </summary>
        /// <param name="dataFlowGuid">Data flow GUID.</param>
        /// <param name="requestGuid">Request GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of DataFlowLog.</returns>
        public async Task<List<DataFlowLog>> RetrieveDataFlowLogs(string dataFlowGuid, string requestGuid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(dataFlowGuid)) throw new ArgumentNullException(nameof(dataFlowGuid));
            if (String.IsNullOrEmpty(requestGuid)) throw new ArgumentNullException(nameof(requestGuid));

            string url = _Endpoint + "v1.0/tenants/" + _TenantGuid + "/dataflows/" + dataFlowGuid + "/logs?request=" + requestGuid;

            using (RestRequest req = new RestRequest(url))
            {
                req.Authorization.BearerToken = _AccessKey;

                using (RestResponse resp = await req.SendAsync(token).ConfigureAwait(false))
                {
                    if (resp != null && resp.StatusCode == 200)
                    {
                        return _Serializer.DeserializeJson<List<DataFlowLog>>(resp.DataAsString);
                    }
                    else if (resp != null)
                    {
                        Log("non-success status from " + url + ": " + resp.StatusCode);
                        return null;
                    }
                    else
                    {
                        Log("no response from " + url);
                        return null;
                    }
                }
            }
        }

        #endregion

        #endregion

        #region Private-Methods

        private void Log(string msg)
        {
            if (String.IsNullOrEmpty(msg)) return;
            Logger?.Invoke(_Header + msg);
        }

        #endregion
    }
}