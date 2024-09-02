namespace View.Sdk.Graph
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using ExpressionTree;

    /// <summary>
    /// Graph SDK.
    /// </summary>
    public class GraphSdk : IDisposable
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

        /// <summary>
        /// Timestamp format.
        /// </summary>
        public string TimestampFormat
        {
            get
            {
                return _TimestampFormat;
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(TimestampFormat));
                DateTime dt = DateTime.Parse(DateTime.UtcNow.ToString(value));
                _TimestampFormat = value;
            }
        }

        #endregion

        #region Private-Members

        private GraphRepository _GraphRepository = null;
        private IGraphDriver _GraphDriver = null;

        private string _TimestampFormat = "yyyy-MM-dd HH:mm:ss.ffffffz";

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

        private readonly string SEMCELL_TYPE = "SemanticCell";
        private readonly string SEMCELL_GUID = "SemanticCell.GUID";

        private readonly string SEMCHUNK_TYPE = "SemanticChunk";
        private readonly string SEMCHUNK_GUID = "SemanticChunk.GUID";

        private readonly string DATAREPOSITORY_TYPE = "DataRepository";
        private readonly string DATAREPOSITORY_GUID = "DataRepository.GUID";

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

        #region General

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            _GraphDriver.Dispose();
        }

        /// <summary>
        /// Validate connectivity to the graph repository.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if connected.</returns>
        public async Task<bool> ValidateConnectivity(CancellationToken token = default)
        {
            return await _GraphDriver.ValidateConnectivity(token).ConfigureAwait(false);
        }

        #endregion

        #region Processing

        /// <summary>
        /// Insert object metadata and its associated objects from storage server.
        /// </summary>
        /// <param name="tenant">Tenant.</param>
        /// <param name="pool">Storage pool.</param>
        /// <param name="bucket">Bucket.</param>
        /// <param name="obj">Object.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph result.</returns>
        public async Task<GraphResult> InsertObjectMetadata(
            TenantMetadata tenant,
            StoragePool pool,
            BucketMetadata bucket,
            ObjectMetadata obj,
            CancellationToken token = default)
        {
            if (tenant == null) throw new ArgumentNullException(nameof(tenant));
            if (pool == null) throw new ArgumentNullException(nameof(pool));
            if (bucket == null) throw new ArgumentNullException(nameof(bucket));
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            GraphResult result = new GraphResult();

            #region Create-Graph

            result.Graph = await ReadGraph(Guid.Parse(_GraphRepository.GraphIdentifier)).ConfigureAwait(false);
            if (result.Graph == null)
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms creating graph " + _GraphRepository.GraphIdentifier);
                result.Graph = await CreateGraph(
                    Guid.Parse(_GraphRepository.GraphIdentifier), 
                    _GraphRepository.Name, token).ConfigureAwait(false);

                if (result.Graph == null)
                {
                    result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms failed to create graph " + _GraphRepository.GraphIdentifier);
                    result.Success = false;
                    return result;
                }
            }
            else
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms attached to graph " + result.Graph.GUID);
            }

            #endregion

            #region Insert-Tenant

            result.Tenant = await ReadTenant(tenant, token).ConfigureAwait(false);
            if (result.Tenant == null)
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms creating node for tenant " + tenant.GUID);
                result.Tenant = await CreateTenant(tenant, token).ConfigureAwait(false);
                if (result.Tenant == null)
                {
                    result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms failed to create node for tenant " + tenant.GUID);
                    result.Success = false;
                    return result;
                }
            }
            else
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms re-using tenant " + tenant.GUID + " node " + result.Tenant.GUID);
            }

            #endregion

            #region Insert-Storage-Pool

            result.StoragePool = await ReadStoragePool(pool, token).ConfigureAwait(false);
            if (result.StoragePool == null)
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms creating node for storage pool " + pool.GUID);
                result.StoragePool = await CreateStoragePool(pool, token).ConfigureAwait(false);
                if (result.StoragePool == null)
                {
                    result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms failed to create node for storage pool " + pool.GUID);
                    result.Success = false;
                    return result;
                }
            }
            else
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms re-using storage pool " + pool.GUID + " node " + result.StoragePool.GUID);
            }

            #endregion

            #region Storage-Pool-to-Tenant

            List<GraphEdge> edgesPoolTenant = await _GraphDriver.EdgesBetweenNodes(
                result.Graph.GUID,
                result.StoragePool.GUID,
                result.Tenant.GUID,
                token).ConfigureAwait(false);

            if (edgesPoolTenant == null || edgesPoolTenant.Count < 1)
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms creating edge from storage pool to tenant");
                GraphEdge edgePoolTenant = await _GraphDriver.CreateEdge(
                    new GraphEdge
                    {
                        GraphGUID = result.Graph.GUID,
                        Name = "Storage pool to tenant",
                        From = result.StoragePool.GUID,
                        To = result.Tenant.GUID
                    }, token).ConfigureAwait(false);

                if (edgePoolTenant == null)
                {
                    result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms failed to create edge from storage pool to tenant");
                    result.Success = false;
                    return result;
                }

                result.Edges.Add(edgePoolTenant);
            }
            else
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms re-using edge(s) between storage pool and tenant");
                result.Edges.AddRange(edgesPoolTenant);
            }

            #endregion

            #region Insert-Bucket

            result.Bucket = await ReadBucket(bucket, token).ConfigureAwait(false);
            if (result.Bucket == null)
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms creating node for bucket " + bucket.GUID);
                result.Bucket = await CreateBucket(bucket, token).ConfigureAwait(false);
                if (result.Bucket == null)
                {
                    result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms failed to create node for bucket " + bucket.GUID);
                    result.Success = false;
                    return result;
                }
            }
            else
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms re-using bucket " + bucket.GUID + " node " + result.Bucket.GUID);
            }

            #endregion

            #region Bucket-to-Storage-Pool

            List<GraphEdge> edgesBucketPool = await _GraphDriver.EdgesBetweenNodes(
                result.Graph.GUID,
                result.Bucket.GUID,
                result.StoragePool.GUID,
                token).ConfigureAwait(false);

            if (edgesBucketPool == null || edgesBucketPool.Count < 1)
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms creating edge from bucket to storage pool");
                GraphEdge edgeBucketPool = await _GraphDriver.CreateEdge(
                    new GraphEdge
                    {
                        GraphGUID = result.Graph.GUID,
                        Name = "Bucket to storage pool",
                        From = result.Bucket.GUID,
                        To = result.StoragePool.GUID
                    }, token).ConfigureAwait(false);

                if (edgeBucketPool == null)
                {
                    result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms failed to create edge from bucket to storage pool");
                    result.Success = false;
                    return result;
                }

                result.Edges.Add(edgeBucketPool);
            }
            else
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms re-using edge between bucket and storage pool");
                result.Edges.AddRange(edgesBucketPool);
            }

            #endregion

            #region Insert-Object

            result.Object = await ReadObjectMetadata(obj, token).ConfigureAwait(false);
            if (result.Object == null)
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms creating node for object " + obj.GUID);
                result.Object = await CreateObjectMetadata(obj, token).ConfigureAwait(false);
                if (result.Object == null)
                {
                    result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms failed to create node for object " + obj.GUID);
                    result.Success = false;
                    return result;
                }
            }
            else
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms re-using object " + obj.GUID + " node " + result.Object.GUID);
            }

            #endregion

            #region Object-to-Bucket

            List<GraphEdge> edgesObjectBucket = await _GraphDriver.EdgesBetweenNodes(
                result.Graph.GUID,
                result.Object.GUID,
                result.Bucket.GUID,
                token).ConfigureAwait(false);

            if (edgesObjectBucket == null || edgesObjectBucket.Count < 1)
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms creating edge from object to bucket");
                GraphEdge edgeObjectBucket = await _GraphDriver.CreateEdge(
                    new GraphEdge
                    {
                        GraphGUID = result.Graph.GUID,
                        Name = "Object to bucket",
                        From = result.Object.GUID,
                        To = result.Bucket.GUID
                    }, token).ConfigureAwait(false);

                if (edgeObjectBucket == null)
                {
                    result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms failed to create edge from object to bucket");
                    result.Success = false;
                    return result;
                }

                result.Edges.Add(edgeObjectBucket);
            }
            else
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms re-using edge between object and bucket");
                result.Edges.AddRange(edgesObjectBucket);
            }

            #endregion

            result.Success = true;
            result.Timestamp.End = DateTime.UtcNow;
            result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms finished processing after " + result.Timestamp.TotalMs + "ms");
            return result;
        }

        /// <summary>
        /// Insert object metadata and its associated objects from crawler.
        /// </summary>
        /// <param name="tenant">Tenant.</param>
        /// <param name="repo">Data repository.</param>
        /// <param name="obj">Object metadata.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph result.</returns>
        public async Task<GraphResult> InsertObjectMetadata(
            TenantMetadata tenant,
            DataRepository repo,
            ObjectMetadata obj,
            CancellationToken token = default)
        {
            if (tenant == null) throw new ArgumentNullException(nameof(tenant));
            if (repo == null) throw new ArgumentNullException(nameof(repo));
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            GraphResult result = new GraphResult();

            #region Create-Graph

            result.Graph = await ReadGraph(Guid.Parse(_GraphRepository.GraphIdentifier)).ConfigureAwait(false);
            if (result.Graph == null)
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms creating graph " + _GraphRepository.GraphIdentifier);
                result.Graph = await CreateGraph(
                    Guid.Parse(_GraphRepository.GraphIdentifier),
                    _GraphRepository.Name, token).ConfigureAwait(false);

                if (result.Graph == null)
                {
                    result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms failed to create graph " + _GraphRepository.GraphIdentifier);
                    result.Success = false;
                    return result;
                }
            }
            else
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms attached to graph " + result.Graph.GUID);
            }

            #endregion

            #region Insert-Tenant

            result.Tenant = await ReadTenant(tenant, token).ConfigureAwait(false);
            if (result.Tenant == null)
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms creating node for tenant " + tenant.GUID);
                result.Tenant = await CreateTenant(tenant, token).ConfigureAwait(false);
                if (result.Tenant == null)
                {
                    result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms failed to create node for tenant " + tenant.GUID);
                    result.Success = false;
                    return result;
                }
            }
            else
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms re-using tenant " + tenant.GUID + " node " + result.Tenant.GUID);
            }

            #endregion

            #region Insert-Data-Repository

            result.DataRepository = await ReadDataRepository(repo, token).ConfigureAwait(false);
            if (result.DataRepository == null)
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms creating node for data repository " + repo.GUID);
                result.DataRepository = await CreateDataRepository(repo, token).ConfigureAwait(false);
                if (result.DataRepository == null)
                {
                    result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms failed to create node for data repository " + repo.GUID);
                    result.Success = false;
                    return result;
                }
            }
            else
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms re-using data repository " + repo.GUID + " node " + result.DataRepository.GUID);
            }

            #endregion

            #region Data-Repository-to-Tenant

            List<GraphEdge> edgesRepoTenant = await _GraphDriver.EdgesBetweenNodes(
                result.Graph.GUID,
                result.DataRepository.GUID,
                result.Tenant.GUID,
                token).ConfigureAwait(false);

            if (edgesRepoTenant == null || edgesRepoTenant.Count < 1)
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms creating edge from data repository to tenant");
                GraphEdge edgeRepoTenant = await _GraphDriver.CreateEdge(
                    new GraphEdge
                    {
                        GraphGUID = result.Graph.GUID,
                        Name = "Data repository to tenant",
                        From = result.DataRepository.GUID,
                        To = result.Tenant.GUID
                    }, token).ConfigureAwait(false);

                if (edgeRepoTenant == null)
                {
                    result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms failed to create edge from data repository to tenant");
                    result.Success = false;
                    return result;
                }

                result.Edges.Add(edgeRepoTenant);
            }
            else
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms re-using edge(s) between data repository and tenant");
                result.Edges.AddRange(edgesRepoTenant);
            }

            #endregion

            #region Insert-Object

            result.Object = await ReadObjectMetadata(obj, token).ConfigureAwait(false);
            if (result.Object == null)
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms creating node for object " + obj.GUID);
                result.Object = await CreateObjectMetadata(obj, token).ConfigureAwait(false);
                if (result.Object == null)
                {
                    result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms failed to create node for object " + obj.GUID);
                    result.Success = false;
                    return result;
                }
            }
            else
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms re-using object " + obj.GUID + " node " + result.Object.GUID);
            }

            #endregion

            #region Object-to-Data-Repository

            List<GraphEdge> edgesObjectRepository = await _GraphDriver.EdgesBetweenNodes(
                result.Graph.GUID,
                result.Object.GUID,
                result.DataRepository.GUID,
                token).ConfigureAwait(false);

            if (edgesObjectRepository == null || edgesObjectRepository.Count < 1)
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms creating edge from object to data repository");
                GraphEdge edgeObjectRepository = await _GraphDriver.CreateEdge(
                    new GraphEdge
                    {
                        GraphGUID = result.Graph.GUID,
                        Name = "Object to data repository",
                        From = result.Object.GUID,
                        To = result.DataRepository.GUID
                    }, token).ConfigureAwait(false);

                if (edgeObjectRepository == null)
                {
                    result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms failed to create edge from object to data repository");
                    result.Success = false;
                    return result;
                }

                result.Edges.Add(edgeObjectRepository);
            }
            else
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms re-using edge between object and data repository");
                result.Edges.AddRange(edgesObjectRepository);
            }

            #endregion

            result.Success = true;
            result.Timestamp.End = DateTime.UtcNow;
            result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms finished processing after " + result.Timestamp.TotalMs + "ms");
            return result;
        }

        /// <summary>
        /// Insert source document and its associated objects.
        /// </summary>
        /// <param name="tenant">Tenant.</param>
        /// <param name="collection">Collection.</param>
        /// <param name="sourceDoc">Source document.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph result.</returns>
        public async Task<GraphResult> InsertSourceDocument(
            TenantMetadata tenant,
            Collection collection,
            SourceDocument sourceDoc,
            CancellationToken token = default)
        {
            if (tenant == null) throw new ArgumentNullException(nameof(tenant));
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (sourceDoc == null) throw new ArgumentNullException(nameof(sourceDoc));

            GraphResult result = new GraphResult();

            #region Create-Graph

            result.Graph = await ReadGraph(Guid.Parse(_GraphRepository.GraphIdentifier)).ConfigureAwait(false);
            if (result.Graph == null)
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms creating graph " + _GraphRepository.GraphIdentifier);
                result.Graph = await CreateGraph(
                    Guid.Parse(_GraphRepository.GraphIdentifier),
                    _GraphRepository.Name, token).ConfigureAwait(false);

                if (result.Graph == null)
                {
                    result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms failed to create graph " + _GraphRepository.GraphIdentifier);
                    result.Success = false;
                    return result;
                }
            }
            else
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms attached to graph " + result.Graph.GUID);
            }

            #endregion

            #region Insert-Tenant

            result.Tenant = await ReadTenant(tenant, token).ConfigureAwait(false);
            if (result.Tenant == null)
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms creating node for tenant " + tenant.GUID);
                result.Tenant = await CreateTenant(tenant, token).ConfigureAwait(false);
                if (result.Tenant == null)
                {
                    result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms failed to create node for tenant " + tenant.GUID);
                    result.Success = false;
                    return result;
                }
            }
            else
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms re-using tenant " + tenant.GUID + " node " + result.Tenant.GUID);
            }

            #endregion

            #region Insert-Collection

            result.Collection = await ReadCollection(collection, token).ConfigureAwait(false);
            if (result.Collection == null)
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms creating node for collection " + collection.GUID);
                result.Collection = await CreateCollection(collection, token).ConfigureAwait(false);
                if (result.Collection == null)
                {
                    result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms failed to create node for collection " + collection.GUID);
                    result.Success = false;
                    return result;
                }
            }
            else
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms re-using collection " + collection.GUID + " node " + result.Collection.GUID);
            }

            #endregion

            #region Collection-to-Tenant

            List<GraphEdge> edgesCollTenant = await _GraphDriver.EdgesBetweenNodes(
                result.Graph.GUID,
                result.Collection.GUID,
                result.Tenant.GUID,
                token).ConfigureAwait(false);

            if (edgesCollTenant == null || edgesCollTenant.Count < 1)
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms creating edge from collection to tenant");
                GraphEdge edgeCollTenant = await _GraphDriver.CreateEdge(
                    new GraphEdge
                    {
                        GraphGUID = result.Graph.GUID,
                        Name = "Collection to tenant",
                        From = result.Collection.GUID,
                        To = result.Tenant.GUID
                    }, token).ConfigureAwait(false);

                if (edgeCollTenant == null)
                {
                    result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms failed to create edge from collection to tenant");
                    result.Success = false;
                    return result;
                }

                result.Edges.Add(edgeCollTenant);
            }
            else
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms re-using edge between collection and tenant");
                result.Edges.AddRange(edgesCollTenant);
            }

            #endregion

            #region Insert-Source-Document

            result.SourceDocument = await ReadSourceDocument(sourceDoc, token).ConfigureAwait(false);
            if (result.SourceDocument == null)
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms creating node for source document " + sourceDoc.GUID);
                result.SourceDocument = await CreateSourceDocument(sourceDoc, token).ConfigureAwait(false);
                if (result.SourceDocument == null)
                {
                    result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms failed to create node for source document " + sourceDoc.GUID);
                    result.Success = false;
                    return result;
                }
            }
            else
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms re-using source document " + sourceDoc.GUID + " node " + result.SourceDocument.GUID);
            }

            #endregion

            #region Source-Document-to-Collection

            List<GraphEdge> edgesSourceDocColl = await _GraphDriver.EdgesBetweenNodes(
                result.Graph.GUID,
                result.SourceDocument.GUID,
                result.Collection.GUID,
                token).ConfigureAwait(false);

            if (edgesSourceDocColl == null || edgesSourceDocColl.Count < 1)
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms creating edge from source document to collection");
                GraphEdge edgeSourceDocColl = await _GraphDriver.CreateEdge(
                    new GraphEdge
                    {
                        GraphGUID = result.Graph.GUID,
                        Name = "Source document to collection",
                        From = result.SourceDocument.GUID,
                        To = result.Collection.GUID
                    }, token).ConfigureAwait(false);

                if (edgeSourceDocColl == null)
                {
                    result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms failed to create edge from source document to collection");
                    result.Success = false;
                    return result;
                }

                result.Edges.Add(edgeSourceDocColl);
            }
            else
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms re-using edge between source document and collection");
                result.Edges.AddRange(edgesSourceDocColl);
            }

            #endregion

            #region Source-Document-to-Object

            if (!String.IsNullOrEmpty(sourceDoc.ObjectGUID))
            {
                GraphNode obj = await ReadObjectMetadata(sourceDoc.ObjectGUID).ConfigureAwait(false);
                if (obj == null)
                {
                    result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms unable to find referenced object " + sourceDoc.ObjectGUID);
                }
                else
                {
                    result.Object = obj;

                    List<GraphEdge> edgesSourceDocObj = await _GraphDriver.EdgesBetweenNodes(
                        result.Graph.GUID,
                        result.SourceDocument.GUID,
                        result.Object.GUID,
                        token).ConfigureAwait(false);

                    if (edgesSourceDocObj == null || edgesSourceDocObj.Count < 1)
                    {
                        result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms creating edge from source document to object");
                        GraphEdge edgeSourceDocObj = await _GraphDriver.CreateEdge(
                            new GraphEdge
                            {
                                GraphGUID = result.Graph.GUID,
                                Name = "Source document to object",
                                From = result.SourceDocument.GUID,
                                To = result.Object.GUID
                            }, token).ConfigureAwait(false);

                        if (edgeSourceDocObj == null)
                        {
                            result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms failed to create edge from source document to object");
                            result.Success = false;
                            return result;
                        }

                        result.Edges.Add(edgeSourceDocObj);
                    }
                    else
                    {
                        result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms re-using edge between source document and object");
                        result.Edges.AddRange(edgesSourceDocObj);
                    }

                }
            }
            else
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms no object GUID specified in source document");
            }

            #endregion

            #region Insert-Semantic-Cells

            if (sourceDoc.UdrDocument != null)
            {
                if (sourceDoc.UdrDocument.SemanticCells != null
                    && sourceDoc.UdrDocument.SemanticCells.Count > 0)
                {
                    GraphResult semCellResult = await ProcessSemanticCellsInternal(
                        result.Graph,
                        result.SourceDocument,
                        null,
                        sourceDoc.UdrDocument.SemanticCells,
                        token).ConfigureAwait(false);

                    if (!semCellResult.Success)
                    {
                        result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms failure reported during semantic cell processing");
                        result.Success = false;
                        return result;
                    }
                    else
                    {
                        foreach (KeyValuePair<DateTime, string> msg in result.Timestamp.Messages)
                            result.Timestamp.Messages.TryAdd(msg.Key, msg.Value);

                        result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms successfully attached semantic cells to source document");
                    }
                }
                else
                {
                    result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms no semantic cells found in supplied UDR document");
                }
            }
            else
            {
                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms no UDR document supplied in source document");
            }

            #endregion

            result.Success = true;
            result.Timestamp.End = DateTime.UtcNow;
            result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms finished processing after " + result.Timestamp.TotalMs + "ms");
            return result;
        }

        #endregion

        #region Exists-by-Object

        /// <summary>
        /// Check if a tenant exists.
        /// </summary>
        /// <param name="tenant">Tenant.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> TenantExists(TenantMetadata tenant, CancellationToken token = default)
        {
            if (tenant == null) throw new ArgumentNullException(nameof(tenant));
            return await ExistsInternal(TENANT_TYPE, TENANT_GUID, tenant.GUID, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if a storage pool exists.
        /// </summary>
        /// <param name="pool">Storage pool.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> StoragePoolExists(StoragePool pool, CancellationToken token = default)
        {
            if (pool == null) throw new ArgumentNullException(nameof(pool));
            return await ExistsInternal(POOL_TYPE, POOL_GUID, pool.GUID, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if a bucket exists.
        /// </summary>
        /// <param name="bucket">Bucket.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> BucketExists(BucketMetadata bucket, CancellationToken token = default)
        {
            if (bucket == null) throw new ArgumentNullException(nameof(bucket));
            return await ExistsInternal(BUCKET_TYPE, BUCKET_GUID, bucket.GUID, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if an object exists.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> ObjectMetadataExists(ObjectMetadata obj, CancellationToken token = default)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            return await ExistsInternal(OBJECT_TYPE, OBJECT_GUID, obj.GUID, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if a collection exists.
        /// </summary>
        /// <param name="coll">Collection.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> CollectionExists(Collection coll, CancellationToken token = default)
        {
            if (coll == null) throw new ArgumentNullException(nameof(coll));
            return await ExistsInternal(COLLECTION_TYPE, COLLECTION_GUID, coll.GUID, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if a source document exists.
        /// </summary>
        /// <param name="doc">Source document.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> SourceDocumentExists(SourceDocument doc, CancellationToken token = default)
        {
            if (doc == null) throw new ArgumentNullException(nameof(doc));
            return await ExistsInternal(SOURCEDOC_TYPE, SOURCEDOC_GUID, doc.GUID, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if a semantic cell exists.
        /// </summary>
        /// <param name="cell">Semantic cell.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> SemanticCellExists(SemanticCell cell, CancellationToken token = default)
        {
            if (cell == null) throw new ArgumentNullException(nameof(cell));
            return await ExistsInternal(SEMCELL_TYPE, SEMCELL_GUID, cell.GUID, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if a semantic chunk exists.
        /// </summary>
        /// <param name="chunk">Semantic chunk.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> SemanticChunkExists(SemanticChunk chunk, CancellationToken token = default)
        {
            if (chunk == null) throw new ArgumentNullException(nameof(chunk));
            return await ExistsInternal(SEMCHUNK_TYPE, SEMCHUNK_GUID, chunk.GUID, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if a data repository exists.
        /// </summary>
        /// <param name="repo">Data repository.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> DataRepositoryExists(DataRepository repo, CancellationToken token = default)
        {
            if (repo == null) throw new ArgumentNullException(nameof(repo));
            return await ExistsInternal(DATAREPOSITORY_TYPE, DATAREPOSITORY_GUID, repo.GUID, token).ConfigureAwait(false);
        }

        #endregion

        #region Exists-by-GUID

        /// <summary>
        /// Check if a tenant exists by GUID.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> TenantExists(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) return false;
            return await ExistsInternal(TENANT_TYPE, TENANT_GUID, guid, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if a storage pool exists by GUID.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> StoragePoolExists(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) return false;
            return await ExistsInternal(POOL_TYPE, POOL_GUID, guid, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if a bucket exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> BucketExists(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) return false;
            return await ExistsInternal(BUCKET_TYPE, BUCKET_GUID, guid, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if an object exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> ObjectMetadataExists(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) return false;
            return await ExistsInternal(OBJECT_TYPE, OBJECT_GUID, guid, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if a collection exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> CollectionExists(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) return false;
            return await ExistsInternal(COLLECTION_TYPE, COLLECTION_GUID, guid, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if a source document exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> SourceDocumentExists(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) return false;
            return await ExistsInternal(SOURCEDOC_TYPE, SOURCEDOC_GUID, guid, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if a semantic cell exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> SemanticCellExists(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) return false;
            return await ExistsInternal(SEMCELL_TYPE, SEMCELL_GUID, guid, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if a semantic chunk exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> SemanticChunkExists(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) return false;
            return await ExistsInternal(SEMCHUNK_TYPE, SEMCHUNK_GUID, guid, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Check if a data repository exists.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>True if exists.</returns>
        public async Task<bool> DataRepositoryExists(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) return false;
            return await ExistsInternal(DATAREPOSITORY_TYPE, DATAREPOSITORY_GUID, guid, token).ConfigureAwait(false);
        }

        #endregion

        #region Create

        /// <summary>
        /// Create a graph.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="name">Name.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph.</returns>
        public async Task<Graph> CreateGraph(Guid guid, string name, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(name)) name = "Graph created at " + DateTime.UtcNow.ToString(_TimestampFormat);
            return await _GraphDriver.CreateGraph(guid, name, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a tenant.
        /// </summary>
        /// <param name="tenant">Tenant.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> CreateTenant(TenantMetadata tenant, CancellationToken token = default)
        {
            if (tenant == null) throw new ArgumentNullException(nameof(tenant));
            return await CreateNodeInternal(GraphNodeTypeEnum.Tenant, "Tenant " + tenant.Name, tenant, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a storage pool.
        /// </summary>
        /// <param name="pool">Storage pool.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> CreateStoragePool(StoragePool pool, CancellationToken token = default)
        {
            if (pool == null) throw new ArgumentNullException(nameof(pool));
            return await CreateNodeInternal(GraphNodeTypeEnum.StoragePool, "Storage pool " + pool.Name, pool, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a bucket.
        /// </summary>
        /// <param name="bucket">Bucket.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> CreateBucket(BucketMetadata bucket, CancellationToken token = default)
        {
            if (bucket == null) throw new ArgumentNullException(nameof(bucket));
            return await CreateNodeInternal(GraphNodeTypeEnum.Bucket, "Bucket " + bucket.Name, bucket, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Create an object.
        /// </summary>
        /// <param name="obj">Object metadata.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> CreateObjectMetadata(ObjectMetadata obj, CancellationToken token = default)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            return await CreateNodeInternal(GraphNodeTypeEnum.Object, "Object " + obj.Key + ":" + obj.Version, obj, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a collection.
        /// </summary>
        /// <param name="coll">Collection.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> CreateCollection(Collection coll, CancellationToken token = default)
        {
            if (coll == null) throw new ArgumentNullException(nameof(coll));
            return await CreateNodeInternal(GraphNodeTypeEnum.Collection, "Collection " + coll.Name, coll, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a source document.
        /// </summary>
        /// <param name="doc">Source document.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> CreateSourceDocument(SourceDocument doc, CancellationToken token = default)
        {
            if (doc == null) throw new ArgumentNullException(nameof(doc));
            return await CreateNodeInternal(GraphNodeTypeEnum.SourceDocument, "Source document " + doc.GUID, doc, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a semantic cell.
        /// </summary>
        /// <param name="cell">Semantic cell.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> CreateSemanticCell(SemanticCell cell, CancellationToken token = default)
        {
            if (cell == null) throw new ArgumentNullException(nameof(cell));
            return await CreateNodeInternal(GraphNodeTypeEnum.SemanticCell, "Semantic cell " + cell.GUID, cell, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a semantic chunk.
        /// </summary>
        /// <param name="chunk">Semantic chunk.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> CreateSemanticChunk(SemanticChunk chunk, CancellationToken token = default)
        {
            if (chunk == null) throw new ArgumentNullException(nameof(chunk));
            return await CreateNodeInternal(GraphNodeTypeEnum.SemanticChunk, "Chunk " + chunk.GUID, chunk, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a data repository.
        /// </summary>
        /// <param name="repo">Data repository.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> CreateDataRepository(DataRepository repo, CancellationToken token = default)
        {
            if (repo == null) throw new ArgumentNullException(nameof(repo));
            return await CreateNodeInternal(GraphNodeTypeEnum.DataRepository, "Data repository " + repo.Name, repo, token).ConfigureAwait(false);
        }

        #endregion

        #region Read-by-Object

        /// <summary>
        /// Read graph metadata.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph metadata.</returns>
        public async Task<Graph> ReadGraph(Guid guid, CancellationToken token = default)
        {
            return await _GraphDriver.ReadGraph(guid, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a tenant graph node.
        /// </summary>
        /// <param name="tenant">Tenant.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> ReadTenant(TenantMetadata tenant, CancellationToken token = default)
        {
            if (tenant == null) throw new ArgumentNullException(nameof(tenant));
            return await ReadInternal(TENANT_TYPE, TENANT_GUID, tenant.GUID, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a storage pool graph node.
        /// </summary>
        /// <param name="pool">Storage pool.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> ReadStoragePool(StoragePool pool, CancellationToken token = default)
        {
            if (pool == null) throw new ArgumentNullException(nameof(pool));
            return await ReadInternal(POOL_TYPE, POOL_GUID, pool.GUID, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a bucket graph node.
        /// </summary>
        /// <param name="bucket">Bucket.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> ReadBucket(BucketMetadata bucket, CancellationToken token = default)
        {
            if (bucket == null) throw new ArgumentNullException(nameof(bucket));
            return await ReadInternal(BUCKET_TYPE, BUCKET_GUID, bucket.GUID, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read an object node.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> ReadObjectMetadata(ObjectMetadata obj, CancellationToken token = default)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            return await ReadInternal(OBJECT_TYPE, OBJECT_GUID, obj.GUID, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a collection node.
        /// </summary>
        /// <param name="coll">Collection.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> ReadCollection(Collection coll, CancellationToken token = default)
        {
            if (coll == null) throw new ArgumentNullException(nameof(coll));
            return await ReadInternal(COLLECTION_TYPE, COLLECTION_GUID, coll.GUID, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a source document node.
        /// </summary>
        /// <param name="doc">Source document.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> ReadSourceDocument(SourceDocument doc, CancellationToken token = default)
        {
            if (doc == null) throw new ArgumentNullException(nameof(doc));
            return await ReadInternal(SOURCEDOC_TYPE, SOURCEDOC_GUID, doc.GUID, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a semantic cell node.
        /// </summary>
        /// <param name="cell">Semantic cell.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> ReadSemanticCell(SemanticCell cell, CancellationToken token = default)
        {
            if (cell == null) throw new ArgumentNullException(nameof(cell));
            return await ReadInternal(SEMCELL_TYPE, SEMCELL_GUID, cell.GUID, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a semantic chunk node.
        /// </summary>
        /// <param name="chunk">Semantic chunk.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> ReadSemanticChunk(SemanticChunk chunk, CancellationToken token = default)
        {
            if (chunk == null) throw new ArgumentNullException(nameof(chunk));
            return await ReadInternal(SEMCHUNK_TYPE, SEMCHUNK_GUID, chunk.GUID, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a data repository node.
        /// </summary>
        /// <param name="repo">Data repository.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> ReadDataRepository(DataRepository repo, CancellationToken token = default)
        {
            if (repo == null) throw new ArgumentNullException(nameof(repo));
            return await ReadInternal(DATAREPOSITORY_TYPE, DATAREPOSITORY_GUID, repo.GUID, token).ConfigureAwait(false);
        }

        #endregion

        #region Read-by-GUID

        /// <summary>
        /// Read a tenant graph node.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> ReadTenant(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) return null;
            return await ReadInternal(TENANT_TYPE, TENANT_GUID, guid, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a storage pool graph node.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> ReadStoragePool(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) return null;
            return await ReadInternal(POOL_TYPE, POOL_GUID, guid, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a bucket graph node.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> ReadBucket(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) return null;
            return await ReadInternal(BUCKET_TYPE, BUCKET_GUID, guid, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read an object node.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> ReadObjectMetadata(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) return null;
            return await ReadInternal(OBJECT_TYPE, OBJECT_GUID, guid, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a collection node.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> ReadCollection(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) return null;
            return await ReadInternal(COLLECTION_TYPE, COLLECTION_GUID, guid, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a source document node.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> ReadSourceDocument(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) return null;
            return await ReadInternal(SOURCEDOC_TYPE, SOURCEDOC_GUID, guid, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a semantic cell node.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> ReadSemanticCell(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) return null;
            return await ReadInternal(SEMCELL_TYPE, SEMCELL_GUID, guid, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a semantic chunk node.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> ReadSemanticChunk(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) return null;
            return await ReadInternal(SEMCHUNK_TYPE, SEMCHUNK_GUID, guid, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read a data repository node.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Graph node.</returns>
        public async Task<GraphNode> ReadDataRepository(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) return null;
            return await ReadInternal(DATAREPOSITORY_TYPE, DATAREPOSITORY_GUID, guid, token).ConfigureAwait(false);
        }

        #endregion

        #region Read-Many

        /// <summary>
        /// Read tenants.
        /// </summary>
        /// <param name="filter">Filter.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of graph node.</returns>
        public async Task<List<GraphNode>> ReadTenants(Dictionary<string, object> filter = null, CancellationToken token = default)
        {
            return await SearchInternal(TENANT_TYPE, filter, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read storage pools.
        /// </summary>
        /// <param name="filter">Filter.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of graph node.</returns>
        public async Task<List<GraphNode>> ReadStoragePools(Dictionary<string, object> filter = null, CancellationToken token = default)
        {
            return await SearchInternal(POOL_TYPE, filter, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read buckets.
        /// </summary>
        /// <param name="filter">Filter.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of graph node.</returns>
        public async Task<List<GraphNode>> ReadBuckets(Dictionary<string, object> filter = null, CancellationToken token = default)
        {
            return await SearchInternal(BUCKET_TYPE, filter, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read objects.
        /// </summary>
        /// <param name="filter">Filter.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of graph node.</returns>
        public async Task<List<GraphNode>> ReadObjectMetadata(Dictionary<string, object> filter = null, CancellationToken token = default)
        { 
            return await SearchInternal(OBJECT_TYPE, filter, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read collections.
        /// </summary>
        /// <param name="filter">Filter.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of graph node.</returns>
        public async Task<List<GraphNode>> ReadCollections(Dictionary<string, object> filter = null, CancellationToken token = default)
        {
            return await SearchInternal(COLLECTION_TYPE, filter, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read source documents.
        /// </summary>
        /// <param name="filter">Filter.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of graph node.</returns>
        public async Task<List<GraphNode>> ReadSourceDocuments(Dictionary<string, object> filter = null, CancellationToken token = default)
        {
            return await SearchInternal(SOURCEDOC_TYPE, filter, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read semantic cells.
        /// </summary>
        /// <param name="filter">Filter.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of graph node.</returns>
        public async Task<List<GraphNode>> ReadSemanticCells(Dictionary<string, object> filter = null, CancellationToken token = default)
        {
            return await SearchInternal(SEMCELL_TYPE, filter, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read semantic chunks.
        /// </summary>
        /// <param name="filter">Filter.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of graph node.</returns>
        public async Task<List<GraphNode>> ReadSemanticChunks(Dictionary<string, object> filter = null, CancellationToken token = default)
        {
            return await SearchInternal(SEMCHUNK_TYPE, filter, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Read data repositories.
        /// </summary>
        /// <param name="filter">Filter.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of graph node.</returns>
        public async Task<List<GraphNode>> ReadDataRepositories(Dictionary<string, object> filter = null, CancellationToken token = default)
        {
            return await SearchInternal(DATAREPOSITORY_TYPE, filter, token).ConfigureAwait(false);
        }

        #endregion
         
        #region Update

        // TBD

        #endregion

        #region Delete-by-Object

        /// <summary>
        /// Delete a tenant.
        /// </summary>
        /// <param name="tenant">Tenant.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteTenant(TenantMetadata tenant, CancellationToken token = default)
        {
            if (tenant == null) throw new ArgumentNullException(nameof(tenant));
            await DeleteInternal(tenant, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a storage pool.
        /// </summary>
        /// <param name="pool">Storage pool.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteStoragePool(StoragePool pool, CancellationToken token = default)
        {
            if (pool == null) throw new ArgumentNullException(nameof(pool));
            await DeleteInternal(pool, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a bucket.
        /// </summary>
        /// <param name="bucket">Bucket.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteBucket(BucketMetadata bucket, CancellationToken token = default)
        {
            if (bucket == null) throw new ArgumentNullException(nameof(bucket));
            await DeleteInternal(bucket, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete an object.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteObjectMetadata(ObjectMetadata obj, CancellationToken token = default)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            await DeleteInternal(obj, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a collection.
        /// </summary>
        /// <param name="coll">Collection.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteCollection(Collection coll, CancellationToken token = default)
        {
            if (coll == null) throw new ArgumentNullException(nameof(coll));
            await DeleteInternal(coll, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a source document.
        /// </summary>
        /// <param name="doc">Source document.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteSourceDocument(SourceDocument doc, CancellationToken token = default)
        {
            if (doc == null) throw new ArgumentNullException(nameof(doc));
            await DeleteInternal(doc, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a semantic cell.
        /// </summary>
        /// <param name="cell">Semantic cell.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteSemanticCell(SemanticCell cell, CancellationToken token = default)
        {
            if (cell == null) throw new ArgumentNullException(nameof(cell));
            await DeleteInternal(cell, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a semantic chunk.
        /// </summary>
        /// <param name="chunk">Semantic chunk.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteSemanticChunk(SemanticChunk chunk, CancellationToken token = default)
        {
            if (chunk == null) throw new ArgumentNullException(nameof(chunk));
            await DeleteInternal(chunk, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a data repository.
        /// </summary>
        /// <param name="repo">Data repository.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteDataRepository(DataRepository repo, CancellationToken token = default)
        {
            if (repo == null) throw new ArgumentNullException(nameof(repo));
            await DeleteInternal(repo, token).ConfigureAwait(false);
        }

        #endregion

        #region Delete-by-GUID

        /// <summary>
        /// Delete a tenant.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteTenant(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) return;
            await DeleteInternal(TENANT_TYPE, TENANT_GUID, guid, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a storage pool.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteStoragePool(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) return;
            await DeleteInternal(POOL_TYPE, POOL_GUID, guid, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a bucket.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteBucket(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) return;
            await DeleteInternal(BUCKET_TYPE, BUCKET_GUID, guid, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete an object.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteObjectMetadata(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) return;
            await DeleteInternal(OBJECT_TYPE, OBJECT_GUID, guid, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a collection.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteCollection(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) return;
            await DeleteInternal(COLLECTION_TYPE, COLLECTION_GUID, guid, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a source document.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteSourceDocument(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) return;
            await DeleteInternal(SOURCEDOC_TYPE, SOURCEDOC_GUID, guid, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a semantic cell.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteSemanticCell(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) return;
            await DeleteInternal(SEMCELL_TYPE, SEMCELL_GUID, guid, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a semantic chunk.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteSemanticChunk(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) return;
            await DeleteInternal(SEMCHUNK_TYPE, SEMCHUNK_GUID, guid, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a data repository.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Task.</returns>
        public async Task DeleteDataRepository(string guid, CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) return;
            await DeleteInternal(DATAREPOSITORY_TYPE, DATAREPOSITORY_GUID, guid, token).ConfigureAwait(false);
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

        private async Task<GraphNode> CreateNodeInternal(
            GraphNodeTypeEnum nodeType, 
            string name, 
            object data, 
            CancellationToken token = default)
        {
            GraphNode node = new GraphNode
            {
                GraphGUID = Guid.Parse(GraphRepository.GraphIdentifier),
                Name = name,
                Data = new GraphData
                {
                    Type = nodeType
                }
            };

            if (data != null)
            {
                if (nodeType == GraphNodeTypeEnum.Tenant) node.Data.Tenant = (TenantMetadata)data;
                if (nodeType == GraphNodeTypeEnum.StoragePool) node.Data.StoragePool = (StoragePool)data;
                if (nodeType == GraphNodeTypeEnum.Bucket) node.Data.Bucket = (BucketMetadata)data;
                if (nodeType == GraphNodeTypeEnum.Object) node.Data.Object = (ObjectMetadata)data;
                if (nodeType == GraphNodeTypeEnum.Collection) node.Data.Collection = (Collection)data;
                if (nodeType == GraphNodeTypeEnum.SourceDocument) node.Data.SourceDocument = (SourceDocument)data;
                if (nodeType == GraphNodeTypeEnum.VectorRepository) node.Data.VectorRepository = (VectorRepository)data;
                if (nodeType == GraphNodeTypeEnum.SemanticCell) node.Data.SemanticCell = (SemanticCell)data;
                if (nodeType == GraphNodeTypeEnum.SemanticChunk) node.Data.SemanticChunk = (SemanticChunk)data;
                if (nodeType == GraphNodeTypeEnum.DataRepository) node.Data.DataRepository = (DataRepository)data;
            }

            return await _GraphDriver.CreateNode(node, token).ConfigureAwait(false);
        }

        private async Task<bool> ExistsInternal(
            string typeVal, 
            string dataKey, 
            string dataVal, 
            CancellationToken token = default)
        {
            List<GraphNode> nodes = await SearchInternal(typeVal, dataKey, dataVal, token).ConfigureAwait(false);
            if (nodes != null && nodes.Count > 0) return true;
            return false;
        }

        private async Task<bool> ExistsInternal(
            string typeVal, 
            Dictionary<string, object> dict, 
            CancellationToken token = default)
        {
            List<GraphNode> nodes = await SearchInternal(typeVal, dict, token).ConfigureAwait(false);
            if (nodes != null && nodes.Count > 0) return true;
            return false;
        }

        private async Task<List<GraphNode>> SearchInternal(
            string typeVal, 
            string dataKey, 
            object dataVal, 
            CancellationToken token = default)
        {
            Expr e = new Expr("Type", OperatorEnum.Equals, typeVal);
            e = e.PrependAnd(dataKey, OperatorEnum.Equals, dataVal);
            List<GraphNode> nodes = await _GraphDriver.SearchNodes(
                Guid.Parse(GraphRepository.GraphIdentifier),
                e,
                token).ConfigureAwait(false);
            if (nodes == null) nodes = new List<GraphNode>();
            return nodes;
        }

        private async Task<List<GraphNode>> SearchInternal(
            string typeVal, 
            Dictionary<string, object> dict, 
            CancellationToken token = default)
        {
            Expr e = new Expr("Type", OperatorEnum.Equals, typeVal);

            if (dict != null && dict.Count > 0)
            {
                foreach (KeyValuePair<string, object> kvp in dict)
                {
                    e = e.PrependAnd(kvp.Key, OperatorEnum.Equals, kvp.Value);
                }
            }

            List<GraphNode> nodes = await _GraphDriver.SearchNodes(
                Guid.Parse(GraphRepository.GraphIdentifier),
                e,
                token).ConfigureAwait(false);
            if (nodes == null) nodes = new List<GraphNode>();
            return nodes;
        }

        private async Task DeleteInternal(
            object obj, 
            CancellationToken token = default)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (obj.GetType() == typeof(TenantMetadata)) await DeleteInternal(TENANT_TYPE, TENANT_GUID, ((TenantMetadata)obj).GUID, token).ConfigureAwait(false);
            if (obj.GetType() == typeof(StoragePool)) await DeleteInternal(POOL_TYPE, POOL_GUID, ((StoragePool)obj).GUID, token).ConfigureAwait(false);
            if (obj.GetType() == typeof(BucketMetadata)) await DeleteInternal(BUCKET_TYPE, BUCKET_GUID, ((BucketMetadata)obj).GUID, token).ConfigureAwait(false);
            if (obj.GetType() == typeof(ObjectMetadata)) await DeleteInternal(OBJECT_TYPE, OBJECT_GUID, ((ObjectMetadata)obj).GUID, token).ConfigureAwait(false);
            if (obj.GetType() == typeof(Collection)) await DeleteInternal(COLLECTION_TYPE, COLLECTION_GUID, ((Collection)obj).GUID, token).ConfigureAwait(false);
            if (obj.GetType() == typeof(SourceDocument)) await DeleteInternal(SOURCEDOC_TYPE, SOURCEDOC_GUID, ((SourceDocument)obj).GUID, token).ConfigureAwait(false);
            if (obj.GetType() == typeof(SemanticCell)) await DeleteInternal(SEMCELL_TYPE, SEMCELL_GUID, ((SemanticCell)obj).GUID, token).ConfigureAwait(false);
            if (obj.GetType() == typeof(SemanticChunk)) await DeleteInternal(SEMCHUNK_TYPE, SEMCHUNK_GUID, ((SemanticChunk)obj).GUID, token).ConfigureAwait(false);
            if (obj.GetType() == typeof(DataRepository)) await DeleteInternal(DATAREPOSITORY_TYPE, DATAREPOSITORY_GUID, ((DataRepository)obj).GUID, token).ConfigureAwait(false);
        }

        private async Task DeleteInternal(
            string objType, 
            string objGuidField, 
            string guid, 
            CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(objType)) throw new ArgumentNullException(nameof(objType));
            if (String.IsNullOrEmpty(objGuidField)) throw new ArgumentNullException(nameof(objGuidField));
            if (String.IsNullOrEmpty(guid)) throw new ArgumentNullException(nameof(guid));

            List<GraphNode> nodes = await SearchInternal(objType, objGuidField, guid, token).ConfigureAwait(false);
            if (nodes != null && nodes.Count > 0)
                foreach (GraphNode node in nodes)
                    await _GraphDriver.DeleteNode(node.GraphGUID, node.GUID, token).ConfigureAwait(false);
        }

        private async Task<GraphNode> ReadInternal(
            object obj, 
            CancellationToken token = default)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (obj.GetType() == typeof(TenantMetadata)) return await ReadInternal(TENANT_TYPE, TENANT_GUID, ((TenantMetadata)obj).GUID, token).ConfigureAwait(false);
            if (obj.GetType() == typeof(StoragePool)) return await ReadInternal(POOL_TYPE, POOL_GUID, ((StoragePool)obj).GUID, token).ConfigureAwait(false);
            if (obj.GetType() == typeof(BucketMetadata)) return await ReadInternal(BUCKET_TYPE, BUCKET_GUID, ((BucketMetadata)obj).GUID, token).ConfigureAwait(false);
            if (obj.GetType() == typeof(ObjectMetadata)) return await ReadInternal(OBJECT_TYPE, OBJECT_GUID, ((ObjectMetadata)obj).GUID, token).ConfigureAwait(false);
            if (obj.GetType() == typeof(Collection)) return await ReadInternal(COLLECTION_TYPE, COLLECTION_GUID, ((Collection)obj).GUID, token).ConfigureAwait(false);
            if (obj.GetType() == typeof(SourceDocument)) return await ReadInternal(SOURCEDOC_TYPE, SOURCEDOC_GUID, ((SourceDocument)obj).GUID, token).ConfigureAwait(false);
            if (obj.GetType() == typeof(SemanticCell)) return await ReadInternal(SEMCELL_TYPE, SEMCELL_GUID, ((SemanticCell)obj).GUID, token).ConfigureAwait(false);
            if (obj.GetType() == typeof(SemanticChunk)) return await ReadInternal(SEMCHUNK_TYPE, SEMCHUNK_GUID, ((SemanticChunk)obj).GUID, token).ConfigureAwait(false);
            if (obj.GetType() == typeof(DataRepository)) return await ReadInternal(DATAREPOSITORY_TYPE, DATAREPOSITORY_GUID, ((DataRepository)obj).GUID, token).ConfigureAwait(false);
            return null;
        }

        private async Task<GraphNode> ReadInternal(
            string objType,
            string objGuidField,
            string guid,
            CancellationToken token = default)
        {
            if (String.IsNullOrEmpty(guid)) return null;
            List<GraphNode> nodes = await SearchInternal(objType, objGuidField, guid, token).ConfigureAwait(false);
            if (nodes != null && nodes.Count == 1) return nodes[0];
            return null;
        }

        private async Task<GraphResult> ProcessSemanticCellsInternal(
            Graph graph,
            GraphNode sourceDocumentNode,
            GraphNode parentCellNode,
            List<SemanticCell> cells,
            CancellationToken token = default)
        {
            if (sourceDocumentNode == null) throw new ArgumentNullException(nameof(sourceDocumentNode));

            GraphResult result = new GraphResult
            {
                Success = true,
                Graph = graph,
                SemanticCells = new List<GraphNode>(),
                SemanticChunks = new List<GraphNode>()
            };

            if (cells == null || cells.Count < 1) return result;

            foreach (SemanticCell cell in cells)
            {
                #region Insert-Cell

                GraphNode cellNode = await ReadSemanticCell(cell, token).ConfigureAwait(false);
                if (cellNode == null)
                {
                    result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms creating node for semantic cell " + cell.GUID);
                    cellNode = await CreateSemanticCell(cell, token).ConfigureAwait(false);
                    if (cellNode == null)
                    {
                        result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms failed to create node for semantic cell " + cell.GUID);
                        result.Success = false;
                        return result;
                    }
                }
                else
                {
                    result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms re-using semantic cell " + cell.GUID + " node " + cellNode.GUID);
                }

                result.SemanticCells.Add(cellNode);

                #endregion

                #region Cell-to-Parent-or-Source-Document

                if (parentCellNode != null)
                {
                    #region Cell-to-Parent

                    List<GraphEdge> edgesCellToParent = await _GraphDriver.EdgesBetweenNodes(
                        graph.GUID,
                        cellNode.GUID,
                        parentCellNode.GUID,
                        token).ConfigureAwait(false);

                    if (edgesCellToParent == null || edgesCellToParent.Count < 1)
                    {
                        result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms creating edge from cell " + cellNode.GUID + " to parent cell " + parentCellNode.GUID);
                        GraphEdge edgeCellParent = await _GraphDriver.CreateEdge(
                            new GraphEdge
                            {
                                GraphGUID = graph.GUID,
                                Name = "Semantic cell to parent",
                                From = cellNode.GUID,
                                To = parentCellNode.GUID
                            }, token).ConfigureAwait(false);

                        if (edgeCellParent == null)
                        {
                            result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms failed to create edge from cell " + cellNode.GUID + " to parent cell " + parentCellNode.GUID);
                            result.Success = false;
                            return result;
                        }

                        result.Edges.Add(edgeCellParent);
                    }
                    else
                    {
                        result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms re-using edge between cell " + cellNode.GUID + " and parent cell " + parentCellNode.GUID);
                        result.Edges.AddRange(edgesCellToParent);
                    }

                    #endregion
                }
                else
                {
                    #region Cell-to-Source-Document

                    List<GraphEdge> edgesCellToDoc = await _GraphDriver.EdgesBetweenNodes(
                        graph.GUID,
                        cellNode.GUID,
                        sourceDocumentNode.GUID,
                        token).ConfigureAwait(false);

                    if (edgesCellToDoc == null || edgesCellToDoc.Count < 1)
                    {
                        result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms creating edge from cell " + cellNode.GUID + " to source document " + sourceDocumentNode.GUID);
                        GraphEdge edgeCellDoc = await _GraphDriver.CreateEdge(
                            new GraphEdge
                            {
                                GraphGUID = graph.GUID,
                                Name = "Semantic cell to source document",
                                From = cellNode.GUID,
                                To = sourceDocumentNode.GUID
                            }, token).ConfigureAwait(false);

                        if (edgeCellDoc == null)
                        {
                            result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms failed to create edge from cell " + cellNode.GUID + " to source document " + sourceDocumentNode.GUID);
                            result.Success = false;
                            return result;
                        }

                        result.Edges.Add(edgeCellDoc);
                    }
                    else
                    {
                        result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms re-using edge between cell " + cellNode.GUID + " and source document " + sourceDocumentNode.GUID);
                        result.Edges.AddRange(edgesCellToDoc);
                    }

                    #endregion
                }

                #endregion

                #region Process-Chunks

                if (cell.Chunks != null && cell.Chunks.Count > 0)
                {
                    foreach (SemanticChunk chunk in cell.Chunks)
                    {
                        #region Insert-Chunk

                        GraphNode chunkNode = await ReadSemanticChunk(chunk, token).ConfigureAwait(false);
                        if (chunkNode == null)
                        {
                            result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms creating node for semantic chunk " + chunk.GUID);
                            chunkNode = await CreateSemanticChunk(chunk, token).ConfigureAwait(false);
                            if (chunkNode == null)
                            {
                                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms failed to create node for semantic chunk " + chunk.GUID);
                                result.Success = false;
                                return result;
                            }
                        }
                        else
                        {
                            result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms re-using semantic chunk " + chunk.GUID + " node " + chunkNode.GUID);
                        }

                        result.SemanticChunks.Add(chunkNode);

                        #endregion

                        #region Chunk-to-Cell

                        List<GraphEdge> edgesChunkToCell = await _GraphDriver.EdgesBetweenNodes(
                            graph.GUID,
                            chunkNode.GUID,
                            cellNode.GUID,
                            token).ConfigureAwait(false);

                        if (edgesChunkToCell == null || edgesChunkToCell.Count < 1)
                        {
                            result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms creating edge from chunk " + chunkNode.GUID + " to cell " + cellNode.GUID);
                            GraphEdge edgeChunkCell = await _GraphDriver.CreateEdge(
                                new GraphEdge
                                {
                                    GraphGUID = graph.GUID,
                                    Name = "Semantic chunk to semantic cell",
                                    From = chunkNode.GUID,
                                    To = cellNode.GUID
                                }, token).ConfigureAwait(false);

                            if (edgeChunkCell == null)
                            {
                                result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms failed to create edge from chunk " + chunkNode.GUID + " to cell " + cellNode.GUID);
                                result.Success = false;
                                return result;
                            }

                            result.Edges.Add(edgeChunkCell);
                        }
                        else
                        {
                            result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms re-using edge between chunk " + chunkNode.GUID + " and cell " + cellNode.GUID);
                            result.Edges.AddRange(edgesChunkToCell);
                        }

                        #endregion
                    }
                }

                #endregion

                #region Recurse-over-Children-and-Merge

                if (cell.Children != null && cell.Children.Count > 0)
                {
                    GraphResult childResult = await ProcessSemanticCellsInternal(
                        graph,
                        sourceDocumentNode,
                        cellNode,
                        cell.Children,
                        token).ConfigureAwait(false);

                    if (!childResult.Success)
                    {
                        result.Timestamp.AddMessage(result.Timestamp.TotalMs + "ms failure reported while processing descendants from cell node " + cellNode.GUID);
                        result.Success = false;
                        return result;
                    }
                    else
                    {
                        foreach (KeyValuePair<DateTime, string> msg in childResult.Timestamp.Messages)
                            result.Timestamp.Messages.TryAdd(msg.Key, msg.Value);

                        if (childResult.SemanticCells != null && childResult.SemanticCells.Count > 0)
                            result.SemanticCells.AddRange(childResult.SemanticCells);

                        if (childResult.SemanticChunks != null && childResult.SemanticChunks.Count > 0)
                            result.SemanticChunks.AddRange(childResult.SemanticChunks);
                    }
                }

                #endregion
            }

            return result;
        }

        #endregion
    }
}
