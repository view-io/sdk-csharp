namespace View.Sdk.Graph
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Graph data.
    /// </summary>
    public class GraphData
    {
        #region Public-Members

        /// <summary>
        /// Node type.
        /// </summary>
        public GraphNodeTypeEnum Type { get; set; } = GraphNodeTypeEnum.Unknown;

        /// <summary>
        /// Tenant metadata.
        /// </summary>
        public TenantMetadata Tenant { get; set; } = null;

        /// <summary>
        /// Storage pool.
        /// </summary>
        public StoragePool StoragePool { get; set; } = null;

        /// <summary>
        /// Bucket metadata.
        /// </summary>
        public BucketMetadata Bucket { get; set; } = null;

        /// <summary>
        /// Object metadata.
        /// </summary>
        public ObjectMetadata Object { get; set; } = null;

        /// <summary>
        /// Collection.
        /// </summary>
        public Collection Collection { get; set; } = null;

        /// <summary>
        /// Source document.
        /// </summary>
        public SourceDocument SourceDocument { get; set; } = null;

        /// <summary>
        /// Vector repository.
        /// </summary>
        public VectorRepository VectorRepository { get; set; } = null;

        /// <summary>
        /// Semantic cell.
        /// </summary>
        public SemanticCell SemanticCell { get; set; } = null;

        /// <summary>
        /// Semantic chunk.
        /// </summary>
        public SemanticChunk SemanticChunk { get; set; } = null;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public GraphData()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
