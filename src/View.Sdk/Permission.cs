namespace View.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Permission.
    /// </summary>
    public class Permission
    {
        #region Public-Members

        /// <summary>
        /// GUID.
        /// </summary>
        public Guid? GUID { get; set; } = null;

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public Guid? TenantGUID { get; set; } = null;

        /// <summary>
        /// Resource types.
        /// </summary>
        public List<string> ResourceTypes { get; set; } = new List<string>();

        /// <summary>
        /// Operation types.
        /// </summary>
        public List<string> OperationTypes { get; set; } = new List<string>();

        /// <summary>
        /// Permission type.
        /// </summary>
        public string PermissionType { get; set; }

        /// <summary>
        /// Active.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Is protected.
        /// </summary>
        public bool IsProtected { get; set; }

        /// <summary>
        /// Created UTC.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public Permission()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}