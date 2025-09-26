namespace View.Sdk
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Model profiles.
    /// </summary>
    public class ModelProfile
    {
        #region Public-Members

        /// <summary>
        /// GUID.
        /// </summary>
        public Guid GUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public Guid TenantGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Model configuration GUID.
        /// </summary>
        public Guid ModelConfigurationGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Model endpoint GUID.
        /// </summary>
        public Guid ModelEndpointGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; } = "My model profile";

        /// <summary>
        /// Additional data.
        /// </summary>
        public string AdditionalData { get; set; } = null;

        /// <summary>
        /// Active.
        /// </summary>
        public bool Active { get; set; } = true;

        /// <summary>
        /// Created.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Model profile.
        /// </summary>
        public ModelProfile()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
