﻿namespace View.Sdk.Vector
{
    using System;

    /// <summary>
    /// Vector delete request.
    /// </summary>
    public class VectorDeleteRequest
    {
        #region Public-Members

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public Guid TenantGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Collection GUID.
        /// </summary>
        public Guid? CollectionGUID { get; set; } = null;

        /// <summary>
        /// Bucket GUID.
        /// </summary>
        public Guid? BucketGUID { get; set; } = null;

        /// <summary>
        /// Data repository GUID.
        /// </summary>
        public Guid? DataRepositoryGUID { get; set; } = null;

        /// <summary>
        /// Object key.
        /// </summary>
        public string ObjectKey { get; set; } = null;

        /// <summary>
        /// Object version.
        /// </summary>
        public string ObjectVersion { get; set; } = null;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public VectorDeleteRequest()
        {
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
