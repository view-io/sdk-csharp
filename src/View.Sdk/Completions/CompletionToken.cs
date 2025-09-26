namespace View.Sdk.Completions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Completion token.
    /// </summary>
    public class CompletionToken
    {
        /// <summary>
        /// The actual token/text content in this chunk
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Index of this token in the stream (0-based)
        /// </summary>
        public int Index
        {
            get => _Index;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(Index));
                _Index = value;
            }
        }

        /// <summary>
        /// Reason for completion ending (only set on final token)
        /// e.g., "stop", "length", "content_filter", etc.
        /// </summary>
        public string FinishReason { get; set; }

        /// <summary>
        /// Indicates if this is the final token in the stream
        /// </summary>
        public bool IsComplete { get; set; } = false;

        /// <summary>
        /// Timestamp when this token was received
        /// </summary>
        public DateTime TimestampUtc { get; set; } = DateTime.UtcNow;

        private int _Index = 0;

        /// <summary>
        /// Completion token.
        /// </summary>
        public CompletionToken()
        {

        }
    }
}
