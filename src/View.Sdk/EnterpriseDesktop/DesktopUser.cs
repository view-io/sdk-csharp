namespace View.Sdk.EnterpriseDesktop
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Desktop user.
    /// </summary>
    public class DesktopUser
    {
        #region Public-Members

        /// <summary>
        /// GUID.
        /// </summary>
        public Guid? GUID { get; set; } = null;

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; } = null;

        /// <summary>
        /// Email address.
        /// </summary>
        public string Email { get; set; } = null;

        /// <summary>
        /// Description.
        /// </summary>
        public string Description { get; set; } = null;

        /// <summary>
        /// Assistants.
        /// </summary>
        public List<NameGuidPair> Assistants { get; set; } = new List<NameGuidPair>();

        /// <summary>
        /// Buckets.
        /// </summary>
        public List<NameGuidPair> Buckets { get; set; } = new List<NameGuidPair>();

        /// <summary>
        /// Printers.
        /// </summary>
        public List<NameGuidPair> Printers { get; set; } = new List<NameGuidPair>();

        /// <summary>
        /// Groups.
        /// </summary>
        public List<NameGuidPair> Groups { get; set; } = new List<NameGuidPair>();

        /// <summary>
        /// Created UTC timestamp.
        /// </summary>
        public DateTime? CreatedUTC { get; set; } = null;

        /// <summary>
        /// Error detail.
        /// </summary>
        [JsonPropertyName("detail")]
        public object Detail { get; set; } = null;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public DesktopUser()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }

    /// <summary>
    /// Name-GUID pair.
    /// </summary>
    public class NameGuidPair
    {
        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; } = null;

        /// <summary>
        /// GUID.
        /// </summary>
        public Guid? GUID { get; set; } = null;
    }
}