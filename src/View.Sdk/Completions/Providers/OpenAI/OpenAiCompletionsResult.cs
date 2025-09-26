namespace View.Sdk.Completions.Providers.OpenAI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Text.Json.Serialization;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Completions;

    /// <summary>
    /// OpenAI completions result object containing the response from the OpenAI Chat Completions API.
    /// </summary>
    public class OpenAiCompletionsResult
    {
        #region Public-Members

        /// <summary>
        /// Unique identifier for the completion. Default is null.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = null;

        /// <summary>
        /// Object type returned by the API. Default is null.
        /// Typically "chat.completion" or "chat.completion.chunk" for streaming.
        /// </summary>
        [JsonPropertyName("object")]
        public string Object { get; set; } = null;

        /// <summary>
        /// Unix timestamp (seconds since epoch) when the completion was created. Default is 0.
        /// </summary>
        [JsonPropertyName("created")]
        public long Created { get; set; } = 0;

        /// <summary>
        /// Model that was used to generate the completion. Default is null.
        /// </summary>
        [JsonPropertyName("model")]
        public string Model { get; set; } = null;

        /// <summary>
        /// List of completion choices. Default is empty list.
        /// Cannot be null - will be replaced with empty list if null is provided.
        /// </summary>
        [JsonPropertyName("choices")]
        public List<OpenAiChoice> Choices
        {
            get => _Choices;
            set => _Choices = value ?? new List<OpenAiChoice>();
        }

        /// <summary>
        /// Token usage statistics for the completion. Default is null.
        /// Only present in non-streaming responses.
        /// </summary>
        [JsonPropertyName("usage")]
        public OpenAiUsage Usage { get; set; } = null;

        /// <summary>
        /// System fingerprint for the completion. Default is null.
        /// Used for debugging and tracking model versions.
        /// </summary>
        [JsonPropertyName("system_fingerprint")]
        public string SystemFingerprint { get; set; } = null;

        #endregion

        #region Private-Members

        private List<OpenAiChoice> _Choices = new List<OpenAiChoice>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate a new OpenAI completions result.
        /// </summary>
        public OpenAiCompletionsResult()
        {

        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// Convert this OpenAI-specific result to a generic completions result.
        /// </summary>
        /// <param name="req">Original completions request.</param>
        /// <param name="success">Whether the request succeeded. Default is true.</param>
        /// <param name="statusCode">HTTP status code from the response. Default is 200.</param>
        /// <param name="error">Error information if the request failed. Default is null.</param>
        /// <returns>Generic completions result.</returns>
        public GenerateCompletionResult ToCompletionResult(
            GenerateCompletionRequest req,
            bool success = true,
            int statusCode = 200,
            ApiErrorResponse error = null)
        {
            if (req == null) throw new ArgumentNullException(nameof(req));

            GenerateCompletionResult result = new GenerateCompletionResult
            {
                Provider = CompletionsProviderEnum.OpenAI,
                Model = req.Model,
                StartUtc = DateTime.UtcNow
            };

            if (!success || error != null)
            {
                // Return error result with a simple async enumerable that yields nothing
                result.Tokens = GenerateEmptyTokenStream();
            }
            else if (!req.Stream)
            {
                // Non-streaming response - create tokens from choices
                result.Tokens = GenerateTokensFromChoices();
            }
            // For streaming, the tokens will be set by the SDK during the streaming process

            return result;
        }

        /// <summary>
        /// Convert a streaming chunk to a completion token.
        /// </summary>
        /// <param name="index">Zero-based index of this token in the stream.</param>
        /// <returns>Completion token representing this chunk.</returns>
        public CompletionToken ToCompletionToken(int index)
        {
            CompletionToken token = new CompletionToken
            {
                Index = index,
                TimestampUtc = DateTime.UtcNow
            };

            // Get content from the first choice's delta
            if (Choices != null && Choices.Count > 0)
            {
                var choice = Choices[0];

                if (choice.Delta != null && choice.Delta.Content != null)
                {
                    token.Content = choice.Delta.Content;
                }
                else if (choice.Message != null && choice.Message.Content != null)
                {
                    token.Content = choice.Message.Content;
                }

                // Set finish reason if present
                if (!string.IsNullOrEmpty(choice.FinishReason))
                {
                    token.FinishReason = choice.FinishReason;
                    token.IsComplete = true;
                }
            }

            return token;
        }

        #endregion

        #region Private-Methods

        private async IAsyncEnumerable<CompletionToken> GenerateEmptyTokenStream()
        {
            await Task.CompletedTask;
            yield break;
        }

        private async IAsyncEnumerable<CompletionToken> GenerateTokensFromChoices()
        {
            await Task.CompletedTask;

            if (Choices != null && Choices.Count > 0)
            {
                // For simplicity, we'll use the first choice
                var choice = Choices[0];

                if (choice.Message != null && !string.IsNullOrEmpty(choice.Message.Content))
                {
                    yield return new CompletionToken
                    {
                        Index = 0,
                        Content = choice.Message.Content,
                        IsComplete = true,
                        FinishReason = choice.FinishReason ?? "stop",
                        TimestampUtc = DateTime.UtcNow
                    };
                }
            }
        }

        #endregion
    }
}