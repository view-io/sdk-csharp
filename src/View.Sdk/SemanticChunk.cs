namespace View.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Semantic chunk.
    /// </summary>
    public class SemanticChunk
    {
        #region Public-Members

        /// <summary>
        /// GUID.
        /// </summary>
        public string GUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// MD5.
        /// </summary>
        public string MD5Hash { get; set; } = null;

        /// <summary>
        /// SHA1.
        /// </summary>
        public string SHA1Hash { get; set; } = null;

        /// <summary>
        /// SHA256.
        /// </summary>
        public string SHA256Hash { get; set; } = null;

        /// <summary>
        /// Position.
        /// </summary>
        public int Position
        {
            get
            {
                return _Position;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(Position));
                _Position = value;
            }
        }

        /// <summary>
        /// Start position.
        /// </summary>
        public int Start
        {
            get
            {
                return _Start;
            }
            set
            {
                if (value < 0) throw new ArgumentException("Start must be zero or greater.");
                _Start = value;
            }
        }

        /// <summary>
        /// End position.
        /// </summary>
        public int End
        {
            get
            {
                return _End;
            }
            set
            {
                if (value < 0) throw new ArgumentException("End must be zero or greater.");
                _End = value;
            }
        }

        /// <summary>
        /// Length.
        /// </summary>
        public int Length
        {
            get
            {
                return _Length;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(Length));
                _Length = value;
            }
        }

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
                else _Embeddings = value;
            }
        }

        #endregion

        #region Private-Members

        private int _Position = 0;
        private int _Start = 0;
        private int _End = 0;
        private int _Length = 0;
        private List<float> _Embeddings = new List<float>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public SemanticChunk()
        {

        }

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="position">Position.</param>
        /// <param name="start">Start.</param>
        /// <param name="end">End.</param>
        /// <param name="content">Content.</param>
        /// <param name="embeddings">Embeddings.</param>
        public SemanticChunk(
            int position,
            int start,
            int end,
            string content,
            List<float> embeddings = null)
        {
            Position = position;
            Start = start;
            End = end;
            Content = content;
            Embeddings = embeddings;

            if (!String.IsNullOrEmpty(content))
            {
                Length = content.Length;
                MD5Hash = Convert.ToHexString(Helpers.HashHelper.MD5Hash(Encoding.UTF8.GetBytes(content)));
                SHA1Hash = Convert.ToHexString(Helpers.HashHelper.SHA1Hash(Encoding.UTF8.GetBytes(content)));
                SHA256Hash = Convert.ToHexString(Helpers.HashHelper.SHA256Hash(Encoding.UTF8.GetBytes(content)));
            }
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
