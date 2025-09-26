using System.Text.Json.Serialization;

namespace View.Sdk.Completions.Providers.OpenAI
{
    /// <summary>
    /// Represents a partial message delta in streaming responses.
    /// </summary>
    public class OpenAiDelta
    {
        /// <summary>
        /// Role of the message author. Default is null.
        /// Only present in the first chunk of a streaming response.
        /// </summary>
        [JsonPropertyName("role")]
        public string Role { get; set; } = null;

        /// <summary>
        /// Partial content being streamed. Default is null.
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; } = null;
    }
}