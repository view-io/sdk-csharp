namespace View.Sdk.Completions.Providers.Ollama
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Represents a single message in an Ollama chat conversation.
    /// </summary>
    public class OllamaMessage
    {
        /// <summary>
        /// Role of the message sender. Valid values are "system", "user", "assistant". Default is null.
        /// </summary>
        [JsonPropertyName("role")]
        public string Role { get; set; } = null;

        /// <summary>
        /// Content of the message. Default is null.
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; } = null;
        
        /// <summary>
        /// Ollama message.
        /// </summary>
        public OllamaMessage()
        {

        }
    }
}