using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Sdk.Vector
{
    /// <summary>
    /// Embeddings map.
    /// </summary>
    public class EmbeddingsMap
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
        /// Instantiate.
        /// </summary>
        public EmbeddingsMap()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
