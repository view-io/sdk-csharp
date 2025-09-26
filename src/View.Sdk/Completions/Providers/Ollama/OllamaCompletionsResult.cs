namespace View.Sdk.Completions.Providers.Ollama
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using View.Sdk.Completions;

    /// <summary>
    /// Ollama completions result object containing the response from the Ollama API.
    /// </summary>
    public class OllamaCompletionsResult
    {
        #region Public-Members

        /// <summary>
        /// Model that generated the response. Default is null.
        /// </summary>
        [JsonPropertyName("model")]
        public string Model { get; set; } = null;

        /// <summary>
        /// Timestamp when the response was created. Default is null.
        /// ISO 8601 format string.
        /// </summary>
        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; } = null;

        /// <summary>
        /// Message response for chat completions. Default is null.
        /// </summary>
        [JsonPropertyName("message")]
        public OllamaMessage Message { get; set; } = null;

        /// <summary>
        /// Response content for non-chat completions. Default is null.
        /// Used when in prompt/response format rather than chat format.
        /// </summary>
        [JsonPropertyName("response")]
        public string Response { get; set; } = null;

        /// <summary>
        /// Whether the response generation is complete. Default is false.
        /// </summary>
        [JsonPropertyName("done")]
        public bool Done { get; set; } = false;

        /// <summary>
        /// Reason why the generation ended. Default is null.
        /// Common values: "stop", "length", "abort".
        /// </summary>
        [JsonPropertyName("done_reason")]
        public string DoneReason { get; set; } = null;

        /// <summary>
        /// Context tokens that can be used to continue the conversation. Default is null.
        /// </summary>
        [JsonPropertyName("context")]
        public List<int> Context { get; set; } = null;

        /// <summary>
        /// Total duration of the request in nanoseconds. Default is null.
        /// </summary>
        [JsonPropertyName("total_duration")]
        public long? TotalDuration { get; set; } = null;

        /// <summary>
        /// Time spent loading the model in nanoseconds. Default is null.
        /// </summary>
        [JsonPropertyName("load_duration")]
        public long? LoadDuration { get; set; } = null;

        /// <summary>
        /// Number of tokens in the prompt that were evaluated. Default is null.
        /// </summary>
        [JsonPropertyName("prompt_eval_count")]
        public int? PromptEvalCount { get; set; } = null;

        /// <summary>
        /// Time spent evaluating the prompt in nanoseconds. Default is null.
        /// </summary>
        [JsonPropertyName("prompt_eval_duration")]
        public long? PromptEvalDuration { get; set; } = null;

        /// <summary>
        /// Number of tokens generated in the response. Default is null.
        /// </summary>
        [JsonPropertyName("eval_count")]
        public int? EvalCount { get; set; } = null;

        /// <summary>
        /// Time spent generating the response in nanoseconds. Default is null.
        /// </summary>
        [JsonPropertyName("eval_duration")]
        public long? EvalDuration { get; set; } = null;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate a new Ollama completions result.
        /// </summary>
        public OllamaCompletionsResult()
        {

        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// Convert this Ollama-specific result to a generic completions result.
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
                Provider = CompletionsProviderEnum.Ollama,
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
                // Non-streaming response - create a single token
                result.Tokens = GenerateSingleTokenStream();
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
                TimestampUtc = DateTime.UtcNow,
                IsComplete = Done
            };

            // Get content from either message or response field
            if (Message != null && !string.IsNullOrEmpty(Message.Content))
            {
                token.Content = Message.Content;
            }
            else if (!string.IsNullOrEmpty(Response))
            {
                token.Content = Response;
            }

            // Set finish reason if done
            if (Done)
            {
                token.FinishReason = DoneReason ?? "stop";
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

        private async IAsyncEnumerable<CompletionToken> GenerateSingleTokenStream()
        {
            await Task.CompletedTask;

            string content = null;
            if (Message != null && !string.IsNullOrEmpty(Message.Content))
            {
                content = Message.Content;
            }
            else if (!string.IsNullOrEmpty(Response))
            {
                content = Response;
            }

            if (!string.IsNullOrEmpty(content))
            {
                yield return new CompletionToken
                {
                    Index = 0,
                    Content = content,
                    IsComplete = true,
                    FinishReason = DoneReason ?? "stop",
                    TimestampUtc = DateTime.UtcNow
                };
            }
        }

        #endregion
    }
}