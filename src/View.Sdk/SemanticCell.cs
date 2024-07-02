namespace View.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Semantic cell.
    /// </summary>
    public class SemanticCell
    {
        #region Public-Members

        /// <summary>
        /// Boolean indicating success.
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// Exception.
        /// </summary>
        public Exception Exception { get; set; } = null;

        /// <summary>
        /// Text.
        /// </summary>
        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value));
                _Text = value.Trim();
            }
        }

        /// <summary>
        /// Tokens.
        /// </summary>
        public List<string> Tokens
        {
            get
            {
                if (!string.IsNullOrEmpty(Text))
                    return Text.Split(' ').ToList();

                return new List<string>();
            }
        }

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
                if (value == null) _Embeddings = new List<float>();
                else _Embeddings = value;
            }
        }

        #endregion

        #region Private-Members

        private string _Text = null;
        private List<float> _Embeddings = new List<float>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public SemanticCell()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
