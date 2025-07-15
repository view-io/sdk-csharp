namespace View.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Role Permission Map.
    /// </summary>
    public class RolePermissionMap
    {
        #region Public-Members

        /// <summary>
        /// GUID.
        /// </summary>
        [JsonPropertyOrder(-1)]
        public Guid? GUID { get; set; } = null;

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public Guid? TenantGUID { get; set; } = null;

        /// <summary>
        /// Role GUID.
        /// </summary>
        public Guid? RoleGUID { get; set; } = null;

        /// <summary>
        /// Permission GUID.
        /// </summary>
        public Guid? PermissionGUID { get; set; } = null;

        /// <summary>
        /// Is active.
        /// </summary>
        public bool Active { get; set; } = true;

        /// <summary>
        /// Is protected.
        /// </summary>
        public bool IsProtected { get; set; } = false;

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
        public RolePermissionMap()
        {

        }

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="roleGuid">Role GUID.</param>
        /// <param name="permissionGuid">Permission GUID.</param>
        public RolePermissionMap(Guid roleGuid, Guid permissionGuid)
        {
            RoleGUID = roleGuid;
            PermissionGUID = permissionGuid;
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}