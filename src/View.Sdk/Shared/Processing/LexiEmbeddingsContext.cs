namespace View.Sdk.Shared.Processing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Timestamps;
    using View.Sdk.Shared.Udr;

    /// <summary>
    /// Lexi embeddings context.
    /// </summary>
    public class LexiEmbeddingsContext
    {
        #region Public-Members

        /// <summary>
        /// Request.
        /// </summary>
        public LexiEmbeddingsRequest Request
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
        public LexiEmbeddingsResponse Response
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

        private string _RequestGuid = Guid.NewGuid().ToString();
        private LexiEmbeddingsRequest _Request = new LexiEmbeddingsRequest();
        private LexiEmbeddingsResponse _Response = new LexiEmbeddingsResponse();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public LexiEmbeddingsContext()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
