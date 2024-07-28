namespace View.Sdk
{
    using System;

    /// <summary>
    /// Processor context.
    /// </summary>
    public class ProcessorContext
    {
        #region Public-Members

        /// <summary>
        /// Request.
        /// </summary>
        public ProcessorRequest Request
        {
            get
            {
                return _Request;
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(Request));
                _Request = value;
            }
        }

        /// <summary>
        /// Response.
        /// </summary>
        public ProcessorResponse Response
        {
            get
            {
                return _Response;
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(Response));
                _Response = value;
            }
        }

        #endregion

        #region Private-Members

        private ProcessorRequest _Request = new ProcessorRequest();
        private ProcessorResponse _Response = new ProcessorResponse();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public ProcessorContext()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
