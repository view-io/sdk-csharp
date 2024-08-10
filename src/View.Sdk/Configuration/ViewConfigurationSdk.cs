namespace View.Sdk.Configuration
{
    using RestWrapper;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Serialization;
    using View.Sdk;

    /// <summary>
    /// View Configuration SDK.
    /// </summary>
    public class ViewConfigurationSdk : ViewSdkBase
    {
        #region Public-Members

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="tenantGuid">Tenant GUID.</param>
        /// <param name="accessKey">Access key.</param>
        /// <param name="endpoint">Endpoint URL.</param>
        public ViewConfigurationSdk(string tenantGuid, string accessKey, string endpoint = "http://localhost:8601/") : base(tenantGuid, accessKey, endpoint)
        { 
            Header = "[ViewConfigurationSdk] ";
        }

        #endregion

        #region Public-Methods

        #region Nodes

        /// <summary>
        /// Create a node.
        /// </summary>
        /// <param name="node">Node.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Node.</returns>
        public async Task<Node> CreateNode(Node node, CancellationToken token = default)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            string url = Endpoint + "v1.0/nodes";
            return await Create<Node>(url, node, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if a node exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> ExistsNode(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/nodes/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a node.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Node.</returns>
        public async Task<Node> RetrieveNode(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/nodes/" + guid;
            return await Retrieve<Node>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read nodes.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Nodes.</returns>
        public async Task<List<Node>> RetrieveNodes(CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/nodes";
            return await RetrieveMany<Node>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a node.
        /// </summary>
        /// <param name="node">Node.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Node.</returns>
        public async Task<Node> UpdateNode(Node node, CancellationToken token = default)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            string url = Endpoint + "v1.0/nodes/" + node.GUID;
            return await Update<Node>(url, node, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a node.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteNode(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/nodes/" + guid;
            await Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Tenants

        /// <summary>
        /// Read a tenant.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Tenant.</returns>
        public async Task<TenantMetadata> RetrieveTenant(CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID;
            return await Retrieve<TenantMetadata>(url, token).ConfigureAwait(false);
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
            string url = Endpoint + "v1.0/tenants/" + TenantGUID;
            return await Update<TenantMetadata>(url, tenant, token).ConfigureAwait(false);
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
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/users";
            return await Create<UserMaster>(url, user, token).ConfigureAwait(false);
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
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/users/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
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
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/users/" + guid;
            return await Retrieve<UserMaster>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read users.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Users.</returns>
        public async Task<List<UserMaster>> RetrieveUsers(CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/users";
            return await RetrieveMany<UserMaster>(url, token).ConfigureAwait(false);
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
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/users/" + user.GUID;
            return await Update<UserMaster>(url, user, token).ConfigureAwait(false);
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
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/users/" + guid;
            await Delete(url, token).ConfigureAwait(false);
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
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/credentials";
            return await Create<Credential>(url, cred, token).ConfigureAwait(false);
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
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/credentials/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
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
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/credentials/" + guid;
            return await Retrieve<Credential>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read nodes.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Nodes.</returns>
        public async Task<List<Credential>> RetrieveCredentials(CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/credentials";
            return await RetrieveMany<Credential>(url, token).ConfigureAwait(false);
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
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/credentials/" + cred.GUID;
            return await Update<Credential>(url, cred, token).ConfigureAwait(false);
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
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/credentials/" + guid;
            await Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Pools

        /// <summary>
        /// Create a pool.
        /// </summary>
        /// <param name="pool">Pool.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Pool.</returns>
        public async Task<StoragePool> CreatePool(StoragePool pool, CancellationToken token = default)
        {
            if (pool == null) throw new ArgumentNullException(nameof(pool));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/pools";
            return await Create<StoragePool>(url, pool, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if a pool exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> ExistsPool(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/pools/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a pool.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Pool.</returns>
        public async Task<StoragePool> RetrievePool(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/pools/" + guid;
            return await Retrieve<StoragePool>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read pools.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Pools.</returns>
        public async Task<List<StoragePool>> RetrievePools(CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/pools";
            return await RetrieveMany<StoragePool>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a pool.
        /// </summary>
        /// <param name="pool">Pool.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Pool.</returns>
        public async Task<StoragePool> UpdatePool(StoragePool pool, CancellationToken token = default)
        {
            if (pool == null) throw new ArgumentNullException(nameof(pool));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/pools/" + pool.GUID;
            return await Update<StoragePool>(url, pool, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a pool.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeletePool(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/pools/" + guid;
            await Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Buckets

        /// <summary>
        /// Create a bucket.
        /// </summary>
        /// <param name="bucket">Bucket.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Bucket.</returns>
        public async Task<BucketMetadata> CreateBucket(BucketMetadata bucket, CancellationToken token = default)
        {
            if (bucket == null) throw new ArgumentNullException(nameof(bucket));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/buckets";
            return await Create<BucketMetadata>(url, bucket, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if a bucket exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> ExistsBucket(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/buckets/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a bucket.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Bucket.</returns>
        public async Task<BucketMetadata> RetrieveBucket(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/buckets/" + guid;
            return await Retrieve<BucketMetadata>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read buckets.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Buckets.</returns>
        public async Task<List<BucketMetadata>> RetrieveBuckets(CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/buckets";
            return await RetrieveMany<BucketMetadata>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a bucket.
        /// </summary>
        /// <param name="bucket">Bucket.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Bucket.</returns>
        public async Task<BucketMetadata> UpdateBucket(BucketMetadata bucket, CancellationToken token = default)
        {
            if (bucket == null) throw new ArgumentNullException(nameof(bucket));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/buckets/" + bucket.GUID;
            return await Update<BucketMetadata>(url, bucket, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a bucket.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteBucket(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/buckets/" + guid;
            await Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Encryption-Keys

        /// <summary>
        /// Create an encryption key.
        /// </summary>
        /// <param name="key">Encryption key.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Encryption key.</returns>
        public async Task<EncryptionKey> CreateEncryptionKey(EncryptionKey key, CancellationToken token = default)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/encryptionkeys";
            return await Create<EncryptionKey>(url, key, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if an encryption key exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> ExistsEncryptionKey(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/encryptionkeys/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read an encryption key.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Encryption key.</returns>
        public async Task<EncryptionKey> RetrieveEncryptionKey(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/encryptionkeys/" + guid;
            return await Retrieve<EncryptionKey>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read encryption keys.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Encryption keys.</returns>
        public async Task<List<EncryptionKey>> RetrieveEncryptionKeys(CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/encryptionkeys";
            return await RetrieveMany<EncryptionKey>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Update an encryption key.
        /// </summary>
        /// <param name="key">Encryption key.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Encryption key.</returns>
        public async Task<EncryptionKey> UpdateEncryptionKey(EncryptionKey key, CancellationToken token = default)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/encryptionkeys/" + key.GUID;
            return await Update<EncryptionKey>(url, key, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete an encryption key.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteEncryptionKey(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/encryptionkeys/" + guid;
            await Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Metadata-Rules

        /// <summary>
        /// Create a metadata rule.
        /// </summary>
        /// <param name="rule">Metadata rule.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Metadata rule.</returns>
        public async Task<MetadataRule> CreateMetadataRule(MetadataRule rule, CancellationToken token = default)
        {
            if (rule == null) throw new ArgumentNullException(nameof(rule));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/metadatarules";
            return await Create<MetadataRule>(url, rule, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if a metadata rule exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> ExistsMetadataRule(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/metadatarules/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a metadata rule.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Metadata rule.</returns>
        public async Task<MetadataRule> RetrieveMetadataRule(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/metadatarules/" + guid;
            return await Retrieve<MetadataRule>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read metadata rules.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Metadata rules.</returns>
        public async Task<List<MetadataRule>> RetrieveMetadataRules(CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/metadatarules";
            return await RetrieveMany<MetadataRule>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a metadata rule.
        /// </summary>
        /// <param name="rule">Metadata rule.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Metadata rule.</returns>
        public async Task<MetadataRule> UpdateMetadataRule(MetadataRule rule, CancellationToken token = default)
        {
            if (rule == null) throw new ArgumentNullException(nameof(rule));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/metadatarules/" + rule.GUID;
            return await Update<MetadataRule>(url, rule, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a metadata rule.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteMetadataRule(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/metadatarules/" + guid;
            await Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Embeddings-Rules

        /// <summary>
        /// Create a embeddings rule.
        /// </summary>
        /// <param name="rule">Embeddings rule.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Embeddings rule.</returns>
        public async Task<EmbeddingsRule> CreateEmbeddingsRule(EmbeddingsRule rule, CancellationToken token = default)
        {
            if (rule == null) throw new ArgumentNullException(nameof(rule));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/embeddingsrules";
            return await Create<EmbeddingsRule>(url, rule, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if a embeddings rule exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> ExistsEmbeddingsRule(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/embeddingsrules/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a embeddings rule.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Embeddings rule.</returns>
        public async Task<EmbeddingsRule> RetrieveEmbeddingsRule(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/embeddingsrules/" + guid;
            return await Retrieve<EmbeddingsRule>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read embeddings rules.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Embeddings rules.</returns>
        public async Task<List<EmbeddingsRule>> RetrieveEmbeddingsRules(CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/embeddingsrules";
            return await RetrieveMany<EmbeddingsRule>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a embeddings rule.
        /// </summary>
        /// <param name="rule">Embeddings rule.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Embeddings rule.</returns>
        public async Task<EmbeddingsRule> UpdateEmbeddingsRule(EmbeddingsRule rule, CancellationToken token = default)
        {
            if (rule == null) throw new ArgumentNullException(nameof(rule));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/embeddingsrules/" + rule.GUID;
            return await Update<EmbeddingsRule>(url, rule, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a embeddings rule.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteEmbeddingsRule(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/embeddingsrules/" + guid;
            await Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Webhook-Events

        /// <summary>
        /// Check if a webhook event exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> ExistsWebhookEvent(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/webhookevents/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a webhook event.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Embeddings rule.</returns>
        public async Task<WebhookEvent> RetrieveWebhookEvent(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/webhookevents/" + guid;
            return await Retrieve<WebhookEvent>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read webhook events.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Webhook events.</returns>
        public async Task<List<WebhookEvent>> RetrieveWebhookEvents(CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/webhookevents";
            return await RetrieveMany<WebhookEvent>(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Webhook-Rules

        /// <summary>
        /// Create a webhook rule.
        /// </summary>
        /// <param name="rule">Webhook rule.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Webhook rule.</returns>
        public async Task<WebhookRule> CreateWebhookRule(WebhookRule rule, CancellationToken token = default)
        {
            if (rule == null) throw new ArgumentNullException(nameof(rule));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/webhookrules";
            return await Create<WebhookRule>(url, rule, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if a webhook rule exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> ExistsWebhookRule(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/webhookrules/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a webhook rule.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Webhook rule.</returns>
        public async Task<WebhookRule> RetrieveWebhookRule(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/webhookrules/" + guid;
            return await Retrieve<WebhookRule>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read webhook rules.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Webhook rules.</returns>
        public async Task<List<WebhookRule>> RetrieveWebhookRules(CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/webhookrules";
            return await RetrieveMany<WebhookRule>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a webhook rule.
        /// </summary>
        /// <param name="rule">Webhook rule.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Webhook rule.</returns>
        public async Task<WebhookRule> UpdateWebhookRule(WebhookRule rule, CancellationToken token = default)
        {
            if (rule == null) throw new ArgumentNullException(nameof(rule));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/webhookrules/" + rule.GUID;
            return await Update<WebhookRule>(url, rule, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a webhook rule.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteWebhookRule(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/webhookrules/" + guid;
            await Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Webhook-Targets

        /// <summary>
        /// Create a webhook target.
        /// </summary>
        /// <param name="target">Webhook target.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Webhook target.</returns>
        public async Task<WebhookTarget> CreateWebhookTarget(WebhookTarget target, CancellationToken token = default)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/webhooktargets";
            return await Create<WebhookTarget>(url, target, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if a webhook target exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> ExistsWebhookTarget(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/webhooktargets/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a webhook target.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Webhook target.</returns>
        public async Task<WebhookTarget> RetrieveWebhookTarget(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/webhooktargets/" + guid;
            return await Retrieve<WebhookTarget>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read webhook targets.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Webhook targets.</returns>
        public async Task<List<WebhookTarget>> RetrieveWebhookTargets(CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/webhooktargets";
            return await RetrieveMany<WebhookTarget>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a webhook target.
        /// </summary>
        /// <param name="target">Webhook target.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Webhook target.</returns>
        public async Task<WebhookTarget> UpdateWebhookTarget(WebhookTarget target, CancellationToken token = default)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/webhooktargets/" + target.GUID;
            return await Update<WebhookTarget>(url, target, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a webhook target.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteWebhookTarget(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/webhooktargets/" + guid;
            await Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region View-Endpoints

        /// <summary>
        /// Create a View endpoint.
        /// </summary>
        /// <param name="endpoint">View endpoint.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>View endpoint.</returns>
        public async Task<ViewEndpoint> CreateViewEndpoint(ViewEndpoint endpoint, CancellationToken token = default)
        {
            if (endpoint == null) throw new ArgumentNullException(nameof(endpoint));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/viewendpoints";
            return await Create<ViewEndpoint>(url, endpoint, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if a View endpoint exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> ExistsViewEndpoint(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/viewendpoints/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a View endpoint.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>View endpoint.</returns>
        public async Task<ViewEndpoint> RetrieveViewEndpoint(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/viewendpoints/" + guid;
            return await Retrieve<ViewEndpoint>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read View endpoints.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>View endpoints.</returns>
        public async Task<List<ViewEndpoint>> RetrieveViewEndpoints(CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/viewendpoints";
            return await RetrieveMany<ViewEndpoint>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a View endpoint.
        /// </summary>
        /// <param name="endpoint">View endpoint.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>View endpoint.</returns>
        public async Task<ViewEndpoint> UpdateViewEndpoint(ViewEndpoint endpoint, CancellationToken token = default)
        {
            if (endpoint == null) throw new ArgumentNullException(nameof(endpoint));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/viewendpoints/" + endpoint.GUID;
            return await Update<ViewEndpoint>(url, endpoint, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a View endpoint.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteViewEndpoint(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/viewendpoints/" + guid;
            await Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region View-Endpoints

        /// <summary>
        /// Create an object lock.
        /// </summary>
        /// <param name="endpoint">Object lock.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Object lock.</returns>
        public async Task<ObjectLock> CreateObjectLock(ObjectLock endpoint, CancellationToken token = default)
        {
            if (endpoint == null) throw new ArgumentNullException(nameof(endpoint));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/objectlocks";
            return await Create<ObjectLock>(url, endpoint, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if an object lock exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> ExistsObjectLock(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/objectlocks/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read an object lock.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Object lock.</returns>
        public async Task<ObjectLock> RetrieveObjectLock(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/objectlocks/" + guid;
            return await Retrieve<ObjectLock>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read Object locks.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Object locks.</returns>
        public async Task<List<ObjectLock>> RetrieveObjectLocks(CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/objectlocks";
            return await RetrieveMany<ObjectLock>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Update an object lock.
        /// </summary>
        /// <param name="endpoint">Object lock.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Object lock.</returns>
        public async Task<ObjectLock> UpdateObjectLock(ObjectLock endpoint, CancellationToken token = default)
        {
            if (endpoint == null) throw new ArgumentNullException(nameof(endpoint));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/objectlocks/" + endpoint.GUID;
            return await Update<ObjectLock>(url, endpoint, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete an object lock.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteObjectLock(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/objectlocks/" + guid;
            await Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #endregion

        #region Private-Methods

        #endregion
    }
}
