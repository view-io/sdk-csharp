using System.Text.Json.Serialization;

namespace View.Sdk.Assistant
{
    /// <summary>
    /// Model operation response.
    /// </summary>
    public class ModelResponse
    {
        /// <summary>
        /// Response message.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// Response details.
        /// </summary>
        [JsonPropertyName("details")]
        public ModelResponseDetails Details { get; set; }
    }

    /// <summary>
    /// Model response details.
    /// </summary>
    public class ModelResponseDetails
    {
        /// <summary>
        /// Model name.
        /// </summary>
        [JsonPropertyName("model")]
        public string Model { get; set; }

        /// <summary>
        /// Created at timestamp.
        /// </summary>
        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }

        /// <summary>
        /// Message information.
        /// </summary>
        [JsonPropertyName("message")]
        public ModelResponseMessage Message { get; set; }

        /// <summary>
        /// Done reason.
        /// </summary>
        [JsonPropertyName("done_reason")]
        public string DoneReason { get; set; }

        /// <summary>
        /// Done flag.
        /// </summary>
        [JsonPropertyName("done")]
        public bool Done { get; set; }
    }

    /// <summary>
    /// Model response message.
    /// </summary>
    public class ModelResponseMessage
    {
        /// <summary>
        /// Message role.
        /// </summary>
        [JsonPropertyName("role")]
        public string Role { get; set; }

        /// <summary>
        /// Message content.
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}