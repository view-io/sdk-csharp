namespace View.Sdk.Assistant
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Chat message for assistant interactions.
    /// </summary>
    public class ChatMessage
    {
        #region Public-Members

        /// <summary>
        /// Role of the message sender (e.g., "user", "assistant", "system").
        /// </summary>
        [JsonPropertyName("role")]
        public string Role { get; set; } = null;

        /// <summary>
        /// Content of the message.
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; } = null;

        /// <summary>
        /// Creation timestamp in UTC.
        /// </summary>
        public DateTime CreatedUTC { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Creation timestamp.
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Metadata associated with the chat messages.
        /// </summary>
        [JsonPropertyName("metadata")]
        public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate the object.
        /// </summary>
        public ChatMessage()
        {
        }
        
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        /// <param name="role">Role of the message sender.</param>
        /// <param name="content">Content of the message.</param>
        public ChatMessage(string role, string content)
        {
            if (String.IsNullOrEmpty(role)) throw new ArgumentNullException(nameof(role));
            if (String.IsNullOrEmpty(content)) throw new ArgumentNullException(nameof(content));
            
            Role = role;
            Content = content;
        }
        
        #endregion
    }
}