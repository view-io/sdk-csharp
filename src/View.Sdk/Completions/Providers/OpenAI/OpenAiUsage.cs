using System.Text.Json.Serialization;

namespace View.Sdk.Completions.Providers.OpenAI
{
    /// <summary>
    /// Token usage statistics for a completion request.
    /// </summary>
    public class OpenAiUsage
    {
        /// <summary>
        /// Number of tokens in the prompt. Default is 0.
        /// </summary>
        [JsonPropertyName("prompt_tokens")]
        public int PromptTokens { get; set; } = 0;

        /// <summary>
        /// Number of tokens in the generated completion. Default is 0.
        /// </summary>
        [JsonPropertyName("completion_tokens")]
        public int CompletionTokens { get; set; } = 0;

        /// <summary>
        /// Total number of tokens used (prompt + completion). Default is 0.
        /// </summary>
        [JsonPropertyName("total_tokens")]
        public int TotalTokens { get; set; } = 0;
    }
}