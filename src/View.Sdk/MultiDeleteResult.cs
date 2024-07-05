namespace View.Sdk
{
    using System.Collections.Generic;

    /// <summary>
    /// Multi-delete request.
    /// </summary>
    public class MultiDeleteResult
    {
        #region Public-Members

        /// <summary>
        /// Request.
        /// </summary>
        public MultiDeleteRequest Request { get; set; } = new MultiDeleteRequest();

        /// <summary>
        /// Enable quiet mode.
        /// </summary>
        public bool Quiet { get; set; } = true;

        /// <summary>
        /// List of deleted objects.
        /// </summary>
        public List<ObjectMetadata> Deleted
        {
            get
            {
                return _Deleted;
            }
            set
            {
                if (value == null) _Deleted = new List<ObjectMetadata>();
                else _Deleted = value;
            }
        }

        /// <summary>
        /// List of error objects.
        /// </summary>
        public List<ObjectMetadata> Errors
        {
            get
            {
                return _Errors;
            }
            set
            {
                if (value == null) _Errors = new List<ObjectMetadata>();
                else _Errors = value;
            }
        }

        #endregion

        #region Private-Members

        private List<ObjectMetadata> _Deleted = new List<ObjectMetadata>();
        private List<ObjectMetadata> _Errors = new List<ObjectMetadata>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public MultiDeleteResult()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
