namespace View.Sdk
{
    using System;

    /// <summary>
    /// Expiration.
    /// </summary>
    public class Expiration
    {
        #region Public-Members

        /// <summary>
        /// Expiration timestamp, in UTC time.
        /// </summary>
        public DateTime? ExpirationUtc
        {
            get
            {
                return _ExpirationUtc;
            }
            set
            {
                _ExpirationUtc = value;
            }
        }

        /// <summary>
        /// Retention minutes.
        /// </summary>
        public int? RetentionMinutes
        {
            get
            {
                return _RetentionMinutes;
            }
            set
            {
                if (value != null && value.Value < 1) throw new ArgumentOutOfRangeException(nameof(RetentionMinutes));

                _RetentionMinutes = value;
            }
        }

        #endregion

        #region Private-Members

        private DateTime? _ExpirationUtc = null;
        private int? _RetentionMinutes = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public Expiration()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
