namespace View.Sdk.Completions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Generate completion result.
    /// </summary>
    public class GenerateCompletionResult
    {
        /// <summary>
        /// Completion provider.
        /// </summary>
        public CompletionsProviderEnum Provider { get; set; } = CompletionsProviderEnum.Ollama;

        /// <summary>
        /// Tokens.
        /// </summary>
        public IAsyncEnumerable<CompletionToken> Tokens { get; set; }

        /// <summary>
        /// Model.
        /// </summary>
        public string Model { get; set; } = null;

        /// <summary>
        /// Start UTC.
        /// </summary>
        public DateTime StartUtc { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// UTC time at which first token arrived.
        /// </summary>
        public DateTime? FirstTokenUtc { get; set; } = null;

        /// <summary>
        /// UTC time at which the last token arrived.
        /// </summary>
        public DateTime? LastTokenUtc { get; set; } = null;

        /// <summary>
        /// Time to first token from the start time.
        /// </summary>
        public TimeSpan? FirstToken
        {
            get
            {
                if (FirstTokenUtc != null) return FirstTokenUtc.Value - StartUtc;
                return null;
            }
        }

        /// <summary>
        /// Time to last token from the start time.
        /// </summary>
        public TimeSpan? TotalTime
        {
            get
            {
                if (LastTokenUtc != null) return LastTokenUtc.Value - StartUtc;
                return null;
            }
        }

        /// <summary>
        /// Total streaming time between first token and last token.
        /// </summary>
        public TimeSpan? StreamingTime
        {
            get
            {
                if (LastTokenUtc != null && FirstTokenUtc != null) return LastTokenUtc.Value - FirstTokenUtc.Value;
                return null;
            }
        }

        /// <summary>
        /// Generate completion result.
        /// </summary>
        public GenerateCompletionResult()
        {

        }
    }
}
