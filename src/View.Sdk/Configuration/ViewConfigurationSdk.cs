﻿namespace View.Sdk.Configuration
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
    public class ViewConfigurationSdk : ViewSdkBase, IDisposable
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
        /// <param name="endpoint">Endpoint URL, i.e. http://localhost:8000/.</param>
        public ViewConfigurationSdk(Guid tenantGuid, string accessKey, string endpoint = "http://localhost:8000/") : base(tenantGuid, accessKey, endpoint)
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
        public async Task<bool> ExistsNode(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/nodes/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a node.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Node.</returns>
        public async Task<Node> RetrieveNode(Guid guid, CancellationToken token = default)
        {
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
        /// <returns>True if successful.</returns>
        public async Task<bool> DeleteNode(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/nodes/" + guid;
            return await Delete(url, token).ConfigureAwait(false);
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
        public async Task<bool> ExistsUser(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/users/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a user.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>User.</returns>
        public async Task<UserMaster> RetrieveUser(Guid guid, CancellationToken token = default)
        {
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
        /// <returns>True if successful.</returns>
        public async Task<bool> DeleteUser(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/users/" + guid;
            return await Delete(url, token).ConfigureAwait(false);
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
        public async Task<bool> ExistsCredential(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/credentials/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a credential.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Credential.</returns>
        public async Task<Credential> RetrieveCredential(Guid guid, CancellationToken token = default)
        {
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
        /// <returns>True if successful.</returns>
        public async Task<bool> DeleteCredential(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/credentials/" + guid;
            return await Delete(url, token).ConfigureAwait(false);
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
        public async Task<bool> ExistsPool(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/pools/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a pool.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Pool.</returns>
        public async Task<StoragePool> RetrievePool(Guid guid, CancellationToken token = default)
        {
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
        /// <returns>True if successful.</returns>
        public async Task<bool> DeletePool(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/pools/" + guid;
            return await Delete(url, token).ConfigureAwait(false);
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
        public async Task<bool> ExistsBucket(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/buckets/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a bucket.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Bucket.</returns>
        public async Task<BucketMetadata> RetrieveBucket(Guid guid, CancellationToken token = default)
        {
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
        /// <returns>True if successful.</returns>
        public async Task<bool> DeleteBucket(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/buckets/" + guid;
            return await Delete(url, token).ConfigureAwait(false);
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
        public async Task<bool> ExistsEncryptionKey(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/encryptionkeys/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read an encryption key.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Encryption key.</returns>
        public async Task<EncryptionKey> RetrieveEncryptionKey(Guid guid, CancellationToken token = default)
        {
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
        /// <returns>True if successful.</returns>
        public async Task<bool> DeleteEncryptionKey(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/encryptionkeys/" + guid;
            return await Delete(url, token).ConfigureAwait(false);
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
        public async Task<bool> ExistsMetadataRule(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/metadatarules/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a metadata rule.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Metadata rule.</returns>
        public async Task<MetadataRule> RetrieveMetadataRule(Guid guid, CancellationToken token = default)
        {
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
        /// <returns>True if successful.</returns>
        public async Task<bool> DeleteMetadataRule(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/metadatarules/" + guid;
            return await Delete(url, token).ConfigureAwait(false);
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
        public async Task<bool> ExistsEmbeddingsRule(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/embeddingsrules/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a embeddings rule.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Embeddings rule.</returns>
        public async Task<EmbeddingsRule> RetrieveEmbeddingsRule(Guid guid, CancellationToken token = default)
        {
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
        /// <returns>True if successful.</returns>
        public async Task<bool> DeleteEmbeddingsRule(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/embeddingsrules/" + guid;
            return await Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Vector-Repositories

        /// <summary>
        /// Create a vector repository.
        /// </summary>
        /// <param name="repo">Vector repository.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Vector repository.</returns>
        public async Task<VectorRepository> CreateVectorRepository(VectorRepository repo, CancellationToken token = default)
        {
            if (repo == null) throw new ArgumentNullException(nameof(repo));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/vectorrepositories";
            return await Create<VectorRepository>(url, repo, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if a vector repository exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> ExistsVectorRepository(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/vectorrepositories/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a vector repository.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>vector repository.</returns>
        public async Task<VectorRepository> RetrieveVectorRepository(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/vectorrepositories/" + guid;
            return await Retrieve<VectorRepository>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read vector repositories.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Vector repositories.</returns>
        public async Task<List<VectorRepository>> RetrieveVectorRepositories(CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/vectorrepositories";
            return await RetrieveMany<VectorRepository>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a vector repository.
        /// </summary>
        /// <param name="repo">Vector repository.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Vector repository.</returns>
        public async Task<VectorRepository> UpdateVectorRepository(VectorRepository repo, CancellationToken token = default)
        {
            if (repo == null) throw new ArgumentNullException(nameof(repo));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/vectorrepositories/" + repo.GUID;
            return await Update<VectorRepository>(url, repo, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a vector repository.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public async Task<bool> DeleteVectorRepository(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/vectorrepositories/" + guid;
            return await Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Graph-Repositories

        /// <summary>
        /// Create a graph repository.
        /// </summary>
        /// <param name="repo">Graph repository.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph repository.</returns>
        public async Task<GraphRepository> CreateGraphRepository(GraphRepository repo, CancellationToken token = default)
        {
            if (repo == null) throw new ArgumentNullException(nameof(repo));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/graphrepositories";
            return await Create<GraphRepository>(url, repo, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if a graph repository exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> ExistsGraphRepository(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/graphrepositories/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a graph repository.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph repository.</returns>
        public async Task<GraphRepository> RetrieveGraphRepository(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/graphrepositories/" + guid;
            return await Retrieve<GraphRepository>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read graph repositories.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph repositories.</returns>
        public async Task<List<GraphRepository>> RetrieveGraphRepositories(CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/graphrepositories";
            return await RetrieveMany<GraphRepository>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a graph repository.
        /// </summary>
        /// <param name="repo">graph repository.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph repository.</returns>
        public async Task<GraphRepository> UpdateGraphRepository(GraphRepository repo, CancellationToken token = default)
        {
            if (repo == null) throw new ArgumentNullException(nameof(repo));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/graphrepositories/" + repo.GUID;
            return await Update<GraphRepository>(url, repo, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a graph repository.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public async Task<bool> DeleteGraphRepository(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/graphrepositories/" + guid;
            return await Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Collections

        /// <summary>
        /// Retrieve collections.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of collection.</returns>
        public async Task<List<Collection>> RetrieveCollections(CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/collections";
            return await RetrieveMany<Collection>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve a collection.
        /// </summary>
        /// <param name="collectionGuid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Collection.</returns>
        public async Task<Collection> RetrieveCollection(string collectionGuid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(collectionGuid)) throw new ArgumentNullException(nameof(collectionGuid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/collections/" + collectionGuid;
            return await Retrieve<Collection>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve collection statistics.
        /// </summary>
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Collection statistics.</returns>
        public async Task<CollectionStatistics> RetrieveCollectionStatistics(string collectionGuid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(collectionGuid)) throw new ArgumentNullException(nameof(collectionGuid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/collections/" + collectionGuid + "?stats";
            return await Retrieve<CollectionStatistics>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Create collection.
        /// </summary>
        /// <param name="collection">Collection.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Collection.</returns>
        public async Task<Collection> CreateCollection(Collection collection, CancellationToken token = default)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/collections";
            return await Create<Collection>(url, collection, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete collection.
        /// </summary>
        /// <param name="collectionGuid">Collection GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public async Task<bool> DeleteCollection(string collectionGuid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(collectionGuid)) throw new ArgumentNullException(nameof(collectionGuid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/collections/" + collectionGuid;
            return await Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Data-Repositories

        /// <summary>
        /// Retrieve data repositories.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of data repository.</returns>
        public async Task<List<DataRepository>> RetrieveDataRepositories(CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/datarepositories";
            return await RetrieveMany<DataRepository>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve a data repository.
        /// </summary>
        /// <param name="repositoryGuid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Data repository.</returns>
        public async Task<DataRepository> RetrieveDataRepository(string repositoryGuid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(repositoryGuid)) throw new ArgumentNullException(nameof(repositoryGuid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/datarepositories/" + repositoryGuid;
            return await Retrieve<DataRepository>(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Create data repository.
        /// </summary>
        /// <param name="repository">Data repository.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Data repository.</returns>
        public async Task<DataRepository> CreateDataRepository(DataRepository repository, CancellationToken token = default)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/datarepositories";
            return await Create<DataRepository>(url, repository, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete data repository.
        /// </summary>
        /// <param name="repositoryGuid">Data repository GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if successful.</returns>
        public async Task<bool> DeleteDataRepository(string repositoryGuid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(repositoryGuid)) throw new ArgumentNullException(nameof(repositoryGuid));
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/datarepositories/" + repositoryGuid;
            return await Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Webhook-Events

        /// <summary>
        /// Check if a webhook event exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> ExistsWebhookEvent(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/webhookevents/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a webhook event.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Embeddings rule.</returns>
        public async Task<WebhookEvent> RetrieveWebhookEvent(Guid guid, CancellationToken token = default)
        {
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
        public async Task<bool> ExistsWebhookRule(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/webhookrules/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a webhook rule.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Webhook rule.</returns>
        public async Task<WebhookRule> RetrieveWebhookRule(Guid guid, CancellationToken token = default)
        {
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
        /// <returns>True if successful.</returns>
        public async Task<bool> DeleteWebhookRule(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/webhookrules/" + guid;
            return await Delete(url, token).ConfigureAwait(false);
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
        public async Task<bool> ExistsWebhookTarget(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/webhooktargets/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a webhook target.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Webhook target.</returns>
        public async Task<WebhookTarget> RetrieveWebhookTarget(Guid guid, CancellationToken token = default)
        {
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
        /// <returns>True if successful.</returns>
        public async Task<bool> DeleteWebhookTarget(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/webhooktargets/" + guid;
            return await Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #region Object-Locks

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
        public async Task<bool> ExistsObjectLock(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/objectlocks/" + guid;
            return await Exists(url, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read an object lock.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Object lock.</returns>
        public async Task<ObjectLock> RetrieveObjectLock(Guid guid, CancellationToken token = default)
        {
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
        /// <returns>True if successful.</returns>
        public async Task<bool> DeleteObjectLock(Guid guid, CancellationToken token = default)
        {
            string url = Endpoint + "v1.0/tenants/" + TenantGUID + "/objectlocks/" + guid;
            return await Delete(url, token).ConfigureAwait(false);
        }

        #endregion

        #endregion

        #region Private-Methods

        #endregion
    }
}
