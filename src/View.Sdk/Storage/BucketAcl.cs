namespace View.Sdk.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Bucket access control list.
    /// </summary>
    public class BucketAcl
    {
        #region Public-Members

        /// <summary>
        /// Owner.
        /// </summary>
        public UserMaster Owner { get; set; } = null;

        /// <summary>
        /// Users.
        /// </summary>
        public List<UserMaster> Users { get; set; } = new List<UserMaster>();

        /// <summary>
        /// Entries.
        /// </summary>
        public List<BucketAclEntry> Entries { get; set; } = new List<BucketAclEntry>();

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public BucketAcl()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}