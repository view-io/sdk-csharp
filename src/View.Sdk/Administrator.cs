namespace View.Sdk
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Administrator.
    /// </summary>
    public class Administrator
    {
        #region Public-Members

        /// <summary>
        /// Account GUID.
        /// </summary>
        public Guid AccountGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// GUID.
        /// </summary>
        public Guid GUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// First name.
        /// </summary>
        public string FirstName { get; set; } = null;

        /// <summary>
        /// Last name.
        /// </summary>
        public string LastName { get; set; } = null;

        /// <summary>
        /// Full name.
        /// </summary>
        public string FullName { get; set; } = null;

        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; } = null;

        /// <summary>
        /// Password SHA256.
        /// </summary>
        public string PasswordSha256 { get; set; } = null;

        /// <summary>
        /// Telephone.
        /// </summary>
        public string Telephone { get; set; } = null;

        /// <summary>
        /// Additional data.
        /// </summary>
        public string AdditionalData { get; set; } = null;

        /// <summary>
        /// Active.
        /// </summary>
        public bool Active { get; set; } = true;

        /// <summary>
        /// Boolean indicating if the object is protected from deletion.
        /// </summary>
        public bool IsProtected { get; set; } = false;

        /// <summary>
        /// Creation timestamp.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public Administrator()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
