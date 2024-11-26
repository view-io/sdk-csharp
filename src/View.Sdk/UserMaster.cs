namespace View.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using View.Sdk.Serialization;

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
        /// Boolean indicating if the object is protected from deletion.
        /// </summary>
        public bool IsProtected { get; set; } = false;

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

        /// <summary>
        /// Redact.
        /// </summary>
        /// <param name="serializer">Serializer.</param>
        /// <param name="user">User.</param>
        /// <returns>User.</returns>
        public static UserMaster Redact(Serializer serializer, UserMaster user)
        {
            if (user == null) return null;
            UserMaster redacted = serializer.CopyObject<UserMaster>(user);

            if (!String.IsNullOrEmpty(user.PasswordSha256))
            {
                int numAsterisks = user.PasswordSha256.Length - 4;
                if (numAsterisks < 4) user.PasswordSha256 = "****";
                else
                {
                    string password = "";
                    for (int i = 0; i < numAsterisks; i++) password += "*";
                    password += user.PasswordSha256.Substring((user.PasswordSha256.Length - 4), 4);
                    redacted.PasswordSha256 = password;
                }
            }

            return redacted;
        }

        /// <summary>
        /// Redact.
        /// </summary>
        /// <param name="serializer">Serializer.</param>
        /// <param name="users">Users.</param>
        /// <returns>List.</returns>
        public static List<UserMaster> Redact(Serializer serializer, List<UserMaster> users)
        {
            if (users == null || users.Count < 1) return users;

            List<UserMaster> redacted = new List<UserMaster>();

            foreach (UserMaster user in users)
                redacted.Add(UserMaster.Redact(serializer, user));

            return redacted;
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}