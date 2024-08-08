namespace View.Sdk.Graph
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ExpressionTree;

    /// <summary>
    /// Graph SDK.
    /// </summary>
    public class GraphSdk
    {
        #region Public-Members

        /// <summary>
        /// Graph repository.
        /// </summary>
        public GraphRepository GraphRepository
        {
            get
            {
                return _GraphRepository;
            }
        }

        #endregion

        #region Private-Members

        private GraphRepository _GraphRepository = null;
        private IGraphDriver _GraphDriver = null;

        private readonly string TENANT_TYPE = "Tenant";
        private readonly string TENANT_GUID = "Tenant.GUID";

        private readonly string POOL_TYPE = "StoragePool";
        private readonly string POOL_GUID = "StoragePool.GUID";

        private readonly string BUCKET_TYPE = "Bucket";
        private readonly string BUCKET_GUID = "Bucket.GUID";

        private readonly string OBJECT_TYPE = "Object";
        private readonly string OBJECT_GUID = "Object.GUID";

        private readonly string COLLECTION_TYPE = "Collection";
        private readonly string COLLECTION_GUID = "Collection.GUID";

        private readonly string SOURCEDOC_TYPE = "SourceDocument";
        private readonly string SOURCEDOC_GUID = "SourceDocument.GUID";

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="repo">Graph repository.</param>
        public GraphSdk(GraphRepository repo)
        {
            _GraphRepository = repo ?? throw new ArgumentNullException(nameof(repo));

            InitializeGraphDriver();
        }

        #endregion

        #region Public-Methods

        #region Exists

        /// <summary>
        /// Check if a tenant exists.
        /// </summary>
        /// <param name="tenant">Tenant.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> TenantExists(TenantMetadata tenant)
        {
            if (tenant == null) throw new ArgumentNullException(nameof(tenant));
            return await ExistsInternal(TENANT_TYPE, TENANT_GUID, tenant.GUID);
        }

        /// <summary>
        /// Check if a storage pool exists.
        /// </summary>
        /// <param name="pool">Storage pool.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> StoragePoolExists(StoragePool pool)
        {
            if (pool == null) throw new ArgumentNullException(nameof(pool));
            return await ExistsInternal(POOL_TYPE, POOL_GUID, pool.GUID);
        }

        /// <summary>
        /// Check if a bucket exists.
        /// </summary>
        /// <param name="bucket">Bucket.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> BucketExists(BucketMetadata bucket)
        {
            if (bucket == null) throw new ArgumentNullException(nameof(bucket));
            return await ExistsInternal(BUCKET_TYPE, BUCKET_GUID, bucket.GUID);
        }

        /// <summary>
        /// Check if an object exists.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> ObjectMetadataExists(ObjectMetadata obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            return await ExistsInternal(OBJECT_TYPE, OBJECT_GUID, obj.GUID);
        }

        /// <summary>
        /// Check if a collection exists.
        /// </summary>
        /// <param name="coll">Collection.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> CollectionExists(Collection coll)
        {
            if (coll == null) throw new ArgumentNullException(nameof(coll));
            return await ExistsInternal(COLLECTION_TYPE, COLLECTION_GUID, coll.GUID);
        }

        /// <summary>
        /// Check if a source document exists.
        /// </summary>
        /// <param name="doc">Source document.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> SourceDocumentExists(SourceDocument doc)
        {
            if (doc == null) throw new ArgumentNullException(nameof(doc));
            return await ExistsInternal(SOURCEDOC_TYPE, SOURCEDOC_GUID, doc.GUID);
        }

        #endregion

        #region Create

        /// <summary>
        /// Create a tenant.
        /// </summary>
        /// <param name="tenant">Tenant.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> CreateTenant(TenantMetadata tenant)
        {
            if (tenant == null) throw new ArgumentNullException(nameof(tenant));
            GraphNode node = new GraphNode
            {
                GraphGUID = Guid.Parse(GraphRepository.GraphIdentifier),
                Name = "Tenant " + tenant.Name,
                Data = new GraphData
                {
                    Type = GraphNodeTypeEnum.Tenant,
                    Tenant = tenant
                }
            };
            return await _GraphDriver.CreateNode(node).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a storage pool.
        /// </summary>
        /// <param name="pool">Storage pool.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> CreateStoragePool(StoragePool pool)
        {
            if (pool == null) throw new ArgumentNullException(nameof(pool));
            GraphNode node = new GraphNode
            {
                GraphGUID = Guid.Parse(GraphRepository.GraphIdentifier),
                Name = "Storage pool " + pool.Name,
                Data = new GraphData
                {
                    Type = GraphNodeTypeEnum.StoragePool,
                    StoragePool = pool
                }
            };
            return await _GraphDriver.CreateNode(node).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a bucket.
        /// </summary>
        /// <param name="bucket">Bucket.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> CreateBucket(BucketMetadata bucket)
        {
            if (bucket == null) throw new ArgumentNullException(nameof(bucket));
            GraphNode node = new GraphNode
            {
                GraphGUID = Guid.Parse(GraphRepository.GraphIdentifier),
                Name = "Bucket " + bucket.Name,
                Data = new GraphData
                {
                    Type = GraphNodeTypeEnum.Bucket,
                    Bucket = bucket
                }
            };
            return await _GraphDriver.CreateNode(node).ConfigureAwait(false);
        }

        /// <summary>
        /// Create an object.
        /// </summary>
        /// <param name="obj">Object metadata.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> CreateObjectMetadata(ObjectMetadata obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            GraphNode node = new GraphNode
            {
                GraphGUID = Guid.Parse(GraphRepository.GraphIdentifier),
                Name = "Object " + obj.Key + ":" + obj.Version,
                Data = new GraphData
                {
                    Type = GraphNodeTypeEnum.Object,
                    Object = obj
                }
            };
            return await _GraphDriver.CreateNode(node).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a collection.
        /// </summary>
        /// <param name="coll">Collection.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> CreateCollection(Collection coll)
        {
            if (coll == null) throw new ArgumentNullException(nameof(coll));
            GraphNode node = new GraphNode
            {
                GraphGUID = Guid.Parse(GraphRepository.GraphIdentifier),
                Name = "Collection " + coll.Name,
                Data = new GraphData
                {
                    Type = GraphNodeTypeEnum.Collection,
                    Collection = coll
                }
            };
            return await _GraphDriver.CreateNode(node).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a source document.
        /// </summary>
        /// <param name="doc">Source document.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> CreateSourceDocument(SourceDocument doc)
        {
            if (doc == null) throw new ArgumentNullException(nameof(doc));
            GraphNode node = new GraphNode
            {
                GraphGUID = Guid.Parse(GraphRepository.GraphIdentifier),
                Name = "Source document " + doc.ObjectKey + ":" + doc.ObjectVersion,
                Data = new GraphData
                {
                    Type = GraphNodeTypeEnum.SourceDocument,
                    SourceDocument = doc
                }
            };
            return await _GraphDriver.CreateNode(node).ConfigureAwait(false);
        }

        #endregion

        #region Read

        /// <summary>
        /// Read a tenant graph node.
        /// </summary>
        /// <param name="tenant">Tenant.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> ReadTenant(TenantMetadata tenant)
        {
            if (tenant == null) throw new ArgumentNullException(nameof(tenant));
            List<GraphNode> nodes = await SearchInternal(TENANT_TYPE, TENANT_GUID, tenant.GUID);
            if (nodes != null && nodes.Count == 1) return nodes[0];
            return null;
        }

        /// <summary>
        /// Read a storage pool graph node.
        /// </summary>
        /// <param name="pool">Storage pool.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> ReadStoragePool(StoragePool pool)
        {
            if (pool == null) throw new ArgumentNullException(nameof(pool));
            List<GraphNode> nodes = await SearchInternal(POOL_TYPE, POOL_GUID, pool.GUID);
            if (nodes != null && nodes.Count == 1) return nodes[0];
            return null;
        }

        /// <summary>
        /// Read a bucket graph node.
        /// </summary>
        /// <param name="bucket">Bucket.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> ReadBucket(BucketMetadata bucket)
        {
            if (bucket == null) throw new ArgumentNullException(nameof(bucket));
            List<GraphNode> nodes = await SearchInternal(BUCKET_TYPE, BUCKET_GUID, bucket.GUID);
            if (nodes != null && nodes.Count == 1) return nodes[0];
            return null;
        }

        /// <summary>
        /// Read an object node.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> ReadObjectMetadata(ObjectMetadata obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            List<GraphNode> nodes = await SearchInternal(OBJECT_TYPE, OBJECT_GUID, obj.GUID);
            if (nodes != null && nodes.Count == 1) return nodes[0];
            return null;
        }

        /// <summary>
        /// Read a collection node.
        /// </summary>
        /// <param name="coll">Collection.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> ReadCollection(Collection coll)
        {
            if (coll == null) throw new ArgumentNullException(nameof(coll));
            List<GraphNode> nodes = await SearchInternal(COLLECTION_TYPE, COLLECTION_GUID, coll.GUID);
            if (nodes != null && nodes.Count == 1) return nodes[0];
            return null;
        }

        /// <summary>
        /// Read a source document node.
        /// </summary>
        /// <param name="doc">Source document.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> ReadSourceDocument(SourceDocument doc)
        {
            if (doc == null) throw new ArgumentNullException(nameof(doc));
            List<GraphNode> nodes = await SearchInternal(SOURCEDOC_TYPE, SOURCEDOC_GUID, doc.GUID);
            if (nodes != null && nodes.Count == 1) return nodes[0];
            return null;
        }

        #endregion

        #region Read-Many

        // TBD

        #endregion

        #region Search

        // TBD

        #endregion

        #region Update

        // TBD

        #endregion

        #region Delete

        /// <summary>
        /// Delete a tenant.
        /// </summary>
        /// <param name="tenant">Tenant.</param>
        /// <returns>Task.</returns>
        public async Task DeleteTenant(TenantMetadata tenant)
        {
            if (tenant == null) throw new ArgumentNullException(nameof(tenant));
            List<GraphNode> nodes = await SearchInternal(TENANT_TYPE, TENANT_GUID, tenant.GUID);
            if (nodes != null && nodes.Count > 0)
            {
                foreach (GraphNode node in nodes)
                {
                    await _GraphDriver.DeleteNode(node.GraphGUID, node.GUID).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// Delete a storage pool.
        /// </summary>
        /// <param name="pool">Storage pool.</param>
        /// <returns>Task.</returns>
        public async Task DeleteStoragePool(StoragePool pool)
        {
            if (pool == null) throw new ArgumentNullException(nameof(pool));
            List<GraphNode> nodes = await SearchInternal(POOL_TYPE, POOL_GUID, pool.GUID);
            if (nodes != null && nodes.Count > 0)
            {
                foreach (GraphNode node in nodes)
                {
                    await _GraphDriver.DeleteNode(node.GraphGUID, node.GUID).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// Delete a bucket.
        /// </summary>
        /// <param name="bucket">Bucket.</param>
        /// <returns>Task.</returns>
        public async Task DeleteBucket(BucketMetadata bucket)
        {
            if (bucket == null) throw new ArgumentNullException(nameof(bucket));
            List<GraphNode> nodes = await SearchInternal(BUCKET_TYPE, BUCKET_GUID, bucket.GUID);
            if (nodes != null && nodes.Count > 0)
            {
                foreach (GraphNode node in nodes)
                {
                    await _GraphDriver.DeleteNode(node.GraphGUID, node.GUID).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// Delete an object.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <returns>Task.</returns>
        public async Task DeleteObjectMetadata(ObjectMetadata obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            List<GraphNode> nodes = await SearchInternal(OBJECT_TYPE, OBJECT_GUID, obj.GUID);
            if (nodes != null && nodes.Count > 0)
            {
                foreach (GraphNode node in nodes)
                {
                    await _GraphDriver.DeleteNode(node.GraphGUID, node.GUID).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// Delete a collection.
        /// </summary>
        /// <param name="coll">Collection.</param>
        /// <returns>Task.</returns>
        public async Task DeleteCollection(Collection coll)
        {
            if (coll == null) throw new ArgumentNullException(nameof(coll));
            List<GraphNode> nodes = await SearchInternal(COLLECTION_TYPE, COLLECTION_GUID, coll.GUID);
            if (nodes != null && nodes.Count > 0)
            {
                foreach (GraphNode node in nodes)
                {
                    await _GraphDriver.DeleteNode(node.GraphGUID, node.GUID).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// Delete a source document.
        /// </summary>
        /// <param name="doc">Source document.</param>
        /// <returns>Task.</returns>
        public async Task DeleteSourceDocument(SourceDocument doc)
        {
            if (doc == null) throw new ArgumentNullException(nameof(doc));
            List<GraphNode> nodes = await SearchInternal(SOURCEDOC_TYPE, SOURCEDOC_GUID, doc.GUID);
            if (nodes != null && nodes.Count > 0)
            {
                foreach (GraphNode node in nodes)
                {
                    await _GraphDriver.DeleteNode(node.GraphGUID, node.GUID).ConfigureAwait(false);
                }
            }
        }

        #endregion

        #endregion

        #region Private-Methods

        private void InitializeGraphDriver()
        {
            switch (GraphRepository.RepositoryType)
            {
                case GraphRepositoryTypeEnum.LiteGraph:
                    _GraphDriver = new LiteGraphDriver(GraphRepository.EndpointUrl);
                    break;
                default:
                    throw new ArgumentException("Unknown graph repository type '" + GraphRepository.RepositoryType.ToString() + "'.");
            }
        }

        private async Task<bool> ExistsInternal(string typeVal, string dataKey, string dataVal)
        {
            List<GraphNode> nodes = await SearchInternal(typeVal, dataKey, dataVal);
            if (nodes != null && nodes.Count > 0) return true;
            return false;
        }

        private async Task<List<GraphNode>> SearchInternal(string typeVal, string dataKey, string dataVal)
        {
            Expr e = new Expr("Type", OperatorEnum.Equals, typeVal);
            e = e.PrependAnd(dataKey, OperatorEnum.Equals, dataVal);
            List<GraphNode> nodes = await _GraphDriver.SearchNodes(
                Guid.Parse(GraphRepository.GraphIdentifier),
                e).ConfigureAwait(false);
            if (nodes == null) nodes = new List<GraphNode>();
            return nodes;
        }

        #endregion
    }
}
