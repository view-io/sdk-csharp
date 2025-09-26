using System.Text.Json.Serialization;

namespace View.Sdk.Completions.Providers.OpenAI
{
    /// <summary>
    /// Represents a single message in an OpenAI chat conversation.
    /// </summary>
    public class OpenAiMessage
    {
        /// <summary>
        /// Role of the message author. Valid values: "system", "user", "assistant", "function". Default is null.
        /// </summary>
        [JsonPropertyName("role")]
        public string Role { get; set; } = null;

        /// <summary>
        /// Content of the message. Default is null.
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; } = null;

        /// <summary>
        /// Optional name of the message author. Default is null.
        /// Used to differentiate between multiple users or functions.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = null;
    }

    /// <summary>
    /// Specifies the format that the model must output.
    /// </summary>
    public class OpenAiResponseFormat
    {
        /// <summary>
        /// Type of response format. Valid values: "text", "json", "json_object". Default is null.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = null;
    }
}