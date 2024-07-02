namespace View.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A posting from a parsed document.
    /// </summary>
    public class Posting
    {
        #region Public-Members

        /// <summary>
        /// The token.
        /// </summary>
        [JsonPropertyOrder(1)]
        public string Term { get; set; }

        /// <summary>
        /// The frequency with which the token occurs.
        /// </summary>
        [JsonPropertyOrder(2)]
        public long Count
        {
            get
            {
                long ret = 0;

                if (AbsolutePositions != null) ret += AbsolutePositions.Count;
                if (RelativePositions != null) ret += RelativePositions.Count;

                return ret;
            }
        }

        /// <summary>
        /// The absolute positions in the token list where the token appears.
        /// </summary>
        [JsonPropertyOrder(3)]
        public List<long> AbsolutePositions { get; set; } = new List<long>();

        /// <summary>
        /// The relative positions in the token list where the token appears.
        /// </summary>
        [JsonPropertyOrder(4)]
        public List<long> RelativePositions { get; set; } = new List<long>();

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiates the Token.
        /// </summary>
        public Posting()
        {

        }

        /// <summary>
        /// Instantiates the Token.
        /// </summary>
        /// <param name="value">The token.</param>
        public Posting(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value));

            Term = value;
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
