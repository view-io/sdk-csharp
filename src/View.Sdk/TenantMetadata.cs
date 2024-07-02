namespace View.Sdk
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Tenant metadata.
    /// </summary>
    public class TenantMetadata
    {
        #region Public-Members

        /// <summary>
        /// GUID.
        /// </summary>
        public string GUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Parent GUID.
        /// </summary>
        public string ParentGUID { get; set; } = null;

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Active.
        /// </summary>
        public bool Active { get; set; } = true;

        /// <summary>
        /// Creation timestamp, in UTC time.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public TenantMetadata()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}