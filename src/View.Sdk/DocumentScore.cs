using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Sdk
{
    /// <summary>
    /// Document score.
    /// </summary>
    public class DocumentScore
    {
        #region Public-Members

        /// <summary>
        /// The score of the document, between 0 and 1, over both terms and filters.  Only relevant when optional terms or filters are supplied in the search.
        /// </summary>
        public decimal? Score { get; set; } = null;

        /// <summary>
        /// The terms score of the document, between 0 and 1, when optional terms are supplied.
        /// </summary>
        public decimal? TermsScore { get; set; } = null;

        /// <summary>
        /// The filters score of the document, between 0 and 1, when optional filters are supplied.
        /// </summary>
        public decimal? FiltersScore { get; set; } = null;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public DocumentScore()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
