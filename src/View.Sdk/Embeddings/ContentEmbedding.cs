namespace View.Sdk.Embeddings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Content embedding.
    /// </summary>
    public class ContentEmbedding
    {
        #region Public-Members

        /// <summary>
        /// Content.
        /// </summary>
        public string Content { get; set; } = null;

        /// <summary>
        /// Embeddings.
        /// </summary>
        public List<float> Embeddings
        {
            get
            {
                return _Embeddings;
            }
            set
            {
                if (value == null) value = new List<float>();
                _Embeddings = value;
            }
        }

        #endregion

        #region Private-Members

        private List<float> _Embeddings = new List<float>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Content embedding.
        /// </summary>
        public ContentEmbedding()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
