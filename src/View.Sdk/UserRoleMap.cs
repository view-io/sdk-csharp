namespace View.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// User Role Map.
    /// </summary>
    public class UserRoleMap
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
        /// User GUID.
        /// </summary>
        public Guid? UserGUID { get; set; } = null;

        /// <summary>
        /// Role GUID.
        /// </summary>
        public Guid? RoleGUID { get; set; } = null;

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
        public UserRoleMap()
        {

        }

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="userGuid">User GUID.</param>
        /// <param name="roleGuid">Role GUID.</param>
        public UserRoleMap(Guid userGuid, Guid roleGuid)
        {
            UserGUID = userGuid;
            RoleGUID = roleGuid;
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}