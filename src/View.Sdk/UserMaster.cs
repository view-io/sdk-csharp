namespace View.Sdk
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// User.
    /// </summary>
    public class UserMaster
    {
        #region Public-Members

        /// <summary>
        /// GUID.
        /// </summary>
        public string GUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public string TenantGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// First name.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Last name.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Full name.
        /// </summary>
        public string FullName
        {
            get
            {
                string ret = FirstName;
                if (!string.IsNullOrEmpty(LastName))
                {
                    ret += " " + LastName;
                }
                return ret;
            }
        }

        /// <summary>
        /// Notes.
        /// </summary>
        public string Notes { get; set; } = string.Empty;

        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Password SHA256.
        /// </summary>
        [JsonIgnore]
        public string PasswordSha256 { get; set; } = string.Empty;

        /// <summary>
        /// Active.
        /// </summary>
        public bool Active { get; set; } = true;

        /// <summary>
        /// Creation time, in UTC.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public UserMaster()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}