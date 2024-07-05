namespace View.Sdk
{
    using System.Collections.Generic;

    /// <summary>
    /// Multi-delete request.
    /// </summary>
    public class MultiDeleteRequest
    {
        #region Public-Members

        /// <summary>
        /// Enable quiet mode.
        /// </summary>
        public bool Quiet { get; set; } = true;

        /// <summary>
        /// Object metadata.
        /// </summary>
        public List<ObjectMetadata> Objects
        {
            get
            {
                return _Objects;
            }
            set
            {
                if (value == null) _Objects = new List<ObjectMetadata>();
                else _Objects = value;
            }
        }

        #endregion

        #region Private-Members

        private List<ObjectMetadata> _Objects = new List<ObjectMetadata>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public MultiDeleteRequest()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
