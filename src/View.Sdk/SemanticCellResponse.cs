namespace View.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Timestamps;

    /// <summary>
    /// Semantic cell response.
    /// </summary>
    public class SemanticCellResponse
    {
        #region Public-Members

        /// <summary>
        /// Data flow request GUID.
        /// </summary>
        public string DataFlowRequestGUID { get; set; } = null;

        /// <summary>
        /// Boolean indicating success.
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// Timestamps.
        /// </summary>
        public Timestamp Timestamp { get; set; } = new Timestamp();

        /// <summary>
        /// Error response, if any.
        /// </summary>
        public ApiErrorResponse Error { get; set; } = null;

        /// <summary>
        /// Semantic cells.
        /// </summary>
        public List<SemanticCell> SemanticCells { get; set; } = null;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public SemanticCellResponse()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
