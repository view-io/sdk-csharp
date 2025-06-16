namespace View.Sdk.Assistant.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Assistant.Interfaces;

    /// <summary>
    /// Assistant chat thread methods implementation.
    /// </summary>
    public class ChatThreadMethods : IChatThreadMethods
    {
        #region Public-Members

        #endregion

        #region Private-Members

        private ViewSdkBase _Sdk = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Assistant chat thread methods.
        /// </summary>
        /// <param name="sdk">View SDK.</param>
        public ChatThreadMethods(ViewSdkBase sdk)
        {
            _Sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        #endregion

        #region Public-Methods

        /// <inheritdoc />
        public async Task<ChatThreadsResponse> RetrieveMany(CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/assistant/threads";
            return await _Sdk.Retrieve<ChatThreadsResponse>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<ChatThread> Retrieve(Guid threadGuid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/assistant/threads/" + threadGuid;
            return await _Sdk.Retrieve<ChatThread>(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Exists(Guid threadGuid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/assistant/threads/" + threadGuid;
            return await _Sdk.Exists(url, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<ChatThread> Create(ChatThread chatThread, CancellationToken token = default)
        {
            if (chatThread == null) throw new ArgumentNullException(nameof(chatThread));

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/assistant/threads";
            return await _Sdk.Post<ChatThread, ChatThread>(url, chatThread, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<ChatThread> Append(Guid threadGuid, ChatMessage message, CancellationToken token = default)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/assistant/threads/" + threadGuid + "/messages";
            return await _Sdk.Post<ChatMessage, ChatThread>(url, message, token).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<bool> Delete(Guid threadGuid, CancellationToken token = default)
        {
            string url = _Sdk.Endpoint + "v1.0/tenants/" + _Sdk.TenantGUID + "/assistant/threads/" + threadGuid;
            return await _Sdk.Delete(url, token).ConfigureAwait(false);
        }

        #endregion
    }
}