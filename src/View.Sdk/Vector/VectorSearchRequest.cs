﻿namespace View.Sdk.Vector
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Vector search request.
    /// </summary>
    public class VectorSearchRequest
    {
        #region Public-Members

        /// <summary>
        /// Search type.
        /// </summary>
        public VectorSearchTypeEnum SearchType { get; set; } = VectorSearchTypeEnum.InnerProduct;

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public Guid TenantGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Vector repository GUID.
        /// </summary>
        public Guid VectorRepositoryGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Starting index.
        /// </summary>
        public int StartIndex
        {
            get
            {
                return _StartIndex;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(StartIndex));
                _StartIndex = value;
            }
        }

        /// <summary>
        /// Maximum number of results to retrieve.
        /// </summary>
        public int MaxResults
        {
            get
            {
                return _MaxResults;
            }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(MaxResults));
                _MaxResults = value;
            }
        }

        /// <summary>
        /// Embeddings.
        /// </summary>
        public List<float> Embeddings
        {
            get
            {
                return _Embeddings;
            }
            set
            {
                if (value == null) _Embeddings = new List<float>();
                else _Embeddings = value;
            }
        }

        #endregion

        #region Private-Members

        private int _StartIndex = 0;
        private int _MaxResults = 100;
        private List<float> _Embeddings = new List<float>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public VectorSearchRequest()
        {
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
