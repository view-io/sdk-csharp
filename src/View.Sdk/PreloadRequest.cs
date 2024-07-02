namespace View.Sdk
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Preload request.
    /// </summary>
    public class PreloadRequest
    {
        #region Public-Members

        /// <summary>
        /// Model.
        /// </summary>
        public List<string> Models
        {
            get
            {
                return _Models;
            }
            set
            {
                if (value == null) value = new List<string>();
                _Models = value;
            }
        }

        /// <summary>
        /// API key.
        /// </summary>
        public string ApiKey { get; set; } = null;

        #endregion

        #region Private-Members

        private List<string> _Models = new List<string>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public PreloadRequest()
        {
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
