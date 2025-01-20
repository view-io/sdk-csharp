namespace View.Sdk
{
    using System;
    using System.Collections.Generic;
    using PrettyId;
    using View.Sdk.Serialization;

    /// <summary>
    /// Credentials.
    /// </summary>
    public class Credential
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
        /// User GUID.
        /// </summary>
        public Guid UserGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Access key.
        /// </summary>
        public string AccessKey { get; set; } = IdGenerator.Generate("access_", 32);

        /// <summary>
        /// Secret key.
        /// </summary>
        public string SecretKey { get; set; } = IdGenerator.Generate("secret_", 32);

        /// <summary>
        /// Active.
        /// </summary>
        public bool Active { get; set; } = true;

        /// <summary>
        /// Boolean indicating if the object is protected from deletion.
        /// </summary>
        public bool IsProtected { get; set; } = false;

        /// <summary>
        /// Creation timestamp, in UTC.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public Credential()
        {

        }

        /// <summary>
        /// Redact.
        /// </summary>
        /// <param name="serializer">Serializer.</param>
        /// <param name="cred">Credential.</param>
        /// <returns>Credential.</returns>
        public static Credential Redact(Serializer serializer, Credential cred)
        {
            if (cred == null) return null;
            Credential redacted = serializer.CopyObject<Credential>(cred);

            if (!String.IsNullOrEmpty(cred.AccessKey))
            {
                int numAsterisks = cred.AccessKey.Length - 4;
                if (numAsterisks < 4) cred.AccessKey = "****";
                else
                {
                    string accessKey = "";
                    for (int i = 0; i < numAsterisks; i++) accessKey += "*";
                    accessKey += cred.AccessKey.Substring((cred.AccessKey.Length - 4), 4);
                    redacted.AccessKey = accessKey;
                }
            }

            if (!String.IsNullOrEmpty(cred.SecretKey))
            {
                int numAsterisks = cred.SecretKey.Length - 4;
                if (numAsterisks < 4) cred.SecretKey = "****";
                else
                {
                    string secretKey = "";
                    for (int i = 0; i < numAsterisks; i++) secretKey += "*";
                    secretKey += cred.SecretKey.Substring((cred.SecretKey.Length - 4), 4);
                    redacted.SecretKey = secretKey;
                }
            }

            return redacted;
        }

        /// <summary>
        /// Redact.
        /// </summary>
        /// <param name="serializer">Serializer.</param>
        /// <param name="creds">Credentials.</param>
        /// <returns>List.</returns>
        public static List<Credential> Redact(Serializer serializer, List<Credential> creds)
        {
            if (creds == null || creds.Count < 1) return creds;

            List<Credential> redacted = new List<Credential>();

            foreach (Credential cred in creds)
                redacted.Add(Credential.Redact(serializer, cred));

            return redacted;
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}