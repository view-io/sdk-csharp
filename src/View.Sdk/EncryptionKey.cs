namespace View.Sdk
{
    using System;
    using System.Security.Cryptography;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Encryption key.
    /// </summary>
    public class EncryptionKey
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
        /// Owner GUID.
        /// </summary>
        public Guid OwnerGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Key in base64 form.
        /// </summary>
        public string KeyBase64
        {
            get
            {
                if (_Key == null) return null;
                return Convert.ToBase64String(_Key);
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(KeyBase64));
                _Key = Convert.FromBase64String(value);
            }
        }

        /// <summary>
        /// Key in hex form.
        /// </summary>
        public string KeyHex
        {
            get
            {
                if (_Key == null) return null;
                return Convert.ToHexString(_Key);
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(KeyHex));
                _Key = Convert.FromHexString(value);
            }
        }

        /// <summary>
        /// Initialization vector in base64 form.
        /// </summary>
        public string IvBase64
        {
            get
            {
                if (_Iv == null) return null;
                return Convert.ToBase64String(_Iv);
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(IvBase64));
                _Iv = Convert.FromBase64String(value);
            }
        }

        /// <summary>
        /// Initialization vector in hex form.
        /// </summary>
        public string IvHex
        {
            get
            {
                if (_Iv == null) return null;
                return Convert.ToHexString(_Iv);
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(IvHex));
                _Iv = Convert.FromHexString(value);
            }
        }

        /// <summary>
        /// Salt in base64 form.
        /// </summary>
        public string SaltBase64
        {
            get
            {
                if (_Salt == null) return null;
                return Convert.ToBase64String(_Salt);
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(SaltBase64));
                _Salt = Convert.FromBase64String(value);
            }
        }

        /// <summary>
        /// Salt in hex form.
        /// </summary>
        public string SaltHex
        {
            get
            {
                if (_Salt == null) return null;
                return Convert.ToHexString(_Key);
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(SaltHex));
                _Salt = Convert.FromHexString(value);
            }
        }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; } = null;

        /// <summary>
        /// Description.
        /// </summary>
        public string Description { get; set; } = null;

        /// <summary>
        /// Creation timestamp, in UTC time.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Owner.
        /// </summary>
        public UserMaster Owner { get; set; } = null;

        /// <summary>
        /// Key.
        /// </summary>
        [JsonIgnore]
        public byte[] Key
        {
            get
            {
                return _Key;
            }
            set
            {
                if (value.Length != 32) throw new ArgumentException("Encryption keys must be 32 bytes in length.");
                _Key = value;
            }
        }

        /// <summary>
        /// IV.
        /// </summary>
        [JsonIgnore]
        public byte[] Iv
        {
            get
            {
                return _Iv;
            }
            set
            {
                if (value.Length != 16) throw new ArgumentException("Initialization vector must be 16 bytes in length.");
                _Iv = value;
            }
        }

        /// <summary>
        /// Salt.
        /// </summary>
        [JsonIgnore]
        public byte[] Salt
        {
            get
            {
                return _Salt;
            }
            set
            {
                if (value.Length != 16) throw new ArgumentException("Salt must be 16 bytes in length.");
                Salt = value;
            }
        }

        #endregion

        #region Private-Members

        private byte[] _Key = Convert.FromHexString("0000000000000000000000000000000000000000000000000000000000000000");
        private byte[] _Iv = Convert.FromHexString("00000000000000000000000000000000");
        private byte[] _Salt = Convert.FromHexString("00000000000000000000000000000000");

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public EncryptionKey()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
