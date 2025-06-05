namespace View.Sdk.Configuration
{
    using System;
    using View.Sdk;
    using View.Sdk.Configuration.Implementations;
    using View.Sdk.Configuration.Interfaces;

    /// <summary>
    /// View Configuration SDK.
    /// </summary>
    public class ViewConfigurationSdk : ViewSdkBase, IDisposable
    {
        #region Public-Members

        /// <summary>
        /// Bucket methods.
        /// </summary>
        public IBucketMethods Bucket { get; set; }

        /// <summary>
        /// Node methods.
        /// </summary>
        public INodeMethods Node { get; set; }

        /// <summary>
        /// Tenant methods.
        /// </summary>
        public ITenantMethods Tenant { get; set; }

        /// <summary>
        /// User methods.
        /// </summary>
        public IUserMethods User { get; set; }

        /// <summary>
        /// Credential methods.
        /// </summary>
        public ICredentialMethods Credential { get; set; }

        /// <summary>
        /// Pool methods.
        /// </summary>
        public IPoolMethods Pool { get; set; }

        /// <summary>
        /// Encryption key methods.
        /// </summary>
        public IEncryptionKeyMethods EncryptionKey { get; set; }

        /// <summary>
        /// Webhook rule methods.
        /// </summary>
        public IWebhookRuleMethods WebhookRule { get; set; }

        /// <summary>
        /// Webhook target methods.
        /// </summary>
        public IWebhookTargetMethods WebhookTarget { get; set; }

        /// <summary>
        /// Webhook event methods.
        /// </summary>
        public IWebhookEventMethods WebhookEvent { get; set; }

        /// <summary>
        /// Metadata rule methods.
        /// </summary>
        public IMetadataRuleMethods MetadataRule { get; set; }

        /// <summary>
        /// Collection methods.
        /// </summary>
        public ICollectionMethods Collection { get; set; }

        /// <summary>
        /// Authentication methods.
        /// </summary>
        public IAuthenticationMethods Authentication { get; set; }

        /// <summary>
        /// Graph repository methods.
        /// </summary>
        public IGraphRepositoryMethods GraphRepository { get; set; }

        /// <summary>
        /// Vector repository methods.
        /// </summary>
        public IVectorRepositoryMethods VectorRepository { get; set; }

        /// <summary>
        /// Object lock methods.
        /// </summary>
        public IObjectLockMethods ObjectLock { get; set; }

        /// <summary>
        /// Embeddings rule methods.
        /// </summary>
        public IEmbeddingsRuleMethods EmbeddingsRule { get; set; }

        /// <summary>
        /// Blob methods.
        /// </summary>
        public IBlobMethods Blob { get; set; }

        /// <summary>
        /// Permission methods.
        /// </summary>
        public IPermissionMethods Permission { get; set; }

        /// <summary>
        /// Role methods.
        /// </summary>
        public IRoleMethods Role { get; set; }

        /// <summary>
        /// Role permission map methods.
        /// </summary>
        public IRolePermissionMapMethods RolePermissionMap { get; set; }

        /// <summary>
        /// User role map methods.
        /// </summary>
        public IUserRoleMapMethods UserRoleMap { get; set; }

        /// <summary>
        /// Deployment type methods.
        /// </summary>
        public IDeploymentTypeMethods DeploymentType { get; set; }

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="tenantGuid">Tenant GUID.</param>
        /// <param name="accessKey">Access key.</param>
        /// <param name="endpoint">Endpoint URL, i.e. http://localhost:8000/.</param>
        /// <param name="xToken">
        /// Optional token to be included as the <c>x-token</c> header in API requests that require additional authorization.
        /// This is used in cases where certain endpoints require an extra level of authentication.
        /// </param>
        public ViewConfigurationSdk(Guid tenantGuid, string accessKey, string endpoint = "http://localhost:8000/", string xToken = null) : base(tenantGuid, accessKey, endpoint)
        {
            Header = "[ViewConfigurationSdk] ";
            XToken = xToken;
            Bucket = new BucketMethods(this);
            Node = new NodeMethods(this);
            Tenant = new TenantMethods(this);
            User = new UserMethods(this);
            Credential = new CredentialMethods(this);
            Pool = new PoolMethods(this);
            EncryptionKey = new EncryptionKeyMethods(this);
            WebhookRule = new WebhookRuleMethods(this);
            WebhookTarget = new WebhookTargetMethods(this);
            WebhookEvent = new WebhookEventMethods(this);
            MetadataRule = new MetadataRuleMethods(this);
            Collection = new CollectionMethods(this);
            Authentication = new AuthenticationMethods(this);
            GraphRepository = new GraphRepositoryMethods(this);
            VectorRepository = new VectorRepositoryMethods(this);
            ObjectLock = new ObjectLockMethods(this);
            EmbeddingsRule = new EmbeddingsRuleMethods(this);
            Blob = new BlobMethods(this);
            Permission = new PermissionMethods(this);
            Role = new RoleMethods(this);
            RolePermissionMap = new RolePermissionMapMethods(this);
            UserRoleMap = new UserRoleMapMethods(this);
            DeploymentType = new DeploymentTypeMethods(this);
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
