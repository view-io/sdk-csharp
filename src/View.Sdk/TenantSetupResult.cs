namespace View.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Tenant setup result.
    /// </summary>
    public class TenantSetupResult
    {
        #region Public-Members

        /// <summary>
        /// Tenant.
        /// </summary>
        public TenantMetadata Tenant { get; set; } = null;

        /// <summary>
        /// User.
        /// </summary>
        public UserMaster User { get; set; } = null;

        /// <summary>
        /// Credential.
        /// </summary>
        public Credential Credential { get; set; } = null;

        /// <summary>
        /// Graph repository.
        /// </summary>
        public GraphRepository GraphRepository { get; set; } = null;

        /// <summary>
        /// Metadata rule.
        /// </summary>
        public MetadataRule MetadataRule { get; set; } = null;

        /// <summary>
        /// Embeddings rule.
        /// </summary>
        public EmbeddingsRule EmbeddingsRule { get; set; } = null;

        /// <summary>
        /// Bucket.
        /// </summary>
        public BucketMetadata Bucket { get; set; } = null;

        /// <summary>
        /// Collection.
        /// </summary>
        public Collection Collection { get; set; } = null;

        /// <summary>
        /// Vector repository.
        /// </summary>
        public VectorRepository VectorRepository { get; set; } = null;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Tenant setup result.
        /// </summary>
        public TenantSetupResult()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
