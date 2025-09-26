using System.Text.Json.Serialization;

namespace View.Sdk.Completions.Providers.OpenAI
{
    /// <summary>
    /// Represents a single completion choice from the OpenAI API.
    /// </summary>
    public class OpenAiChoice
    {
        /// <summary>
        /// Index of this choice in the list of choices. Default is 0.
        /// </summary>
        [JsonPropertyName("index")]
        public int Index { get; set; } = 0;

        /// <summary>
        /// Complete message for this choice (non-streaming responses). Default is null.
        /// </summary>
        [JsonPropertyName("message")]
        public OpenAiMessage Message { get; set; } = null;

        /// <summary>
        /// Partial message delta for this choice (streaming responses). Default is null.
        /// </summary>
        [JsonPropertyName("delta")]
        public OpenAiDelta Delta { get; set; } = null;

        /// <summary>
        /// Reason the model stopped generating tokens. Default is null.
        /// Common values: "stop", "length", "function_call", "content_filter".
        /// </summary>
        [JsonPropertyName("finish_reason")]
        public string FinishReason { get; set; } = null;

        /// <summary>
        /// Log probabilities for the tokens in this choice. Default is null.
        /// Only present when requested in the API call.
        /// </summary>
        [JsonPropertyName("logprobs")]
        public object Logprobs { get; set; } = null;
    }
}