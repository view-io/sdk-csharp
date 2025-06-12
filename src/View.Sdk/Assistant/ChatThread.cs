namespace View.Sdk.Assistant
{
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// Chat thread for assistant interactions.
    /// </summary>
    public class ChatThread
    {
        #region Public-Members
        
        /// <summary>
        /// Unique identifier for the chat thread.
        /// </summary>
        public Guid GUID { get; set; } = Guid.Empty;
        
        /// <summary>
        /// Title of the chat thread.
        /// </summary>
        public string Title { get; set; } = null;
        
        /// <summary>
        /// Description of the chat thread.
        /// </summary>
        public string Description { get; set; } = null;
        
        /// <summary>
        /// Number of messages in the thread.
        /// </summary>
        public int MessageCount { get; set; } = 0;
        
        /// <summary>
        /// Messages in the thread.
        /// </summary>
        public List<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
        
        /// <summary>
        /// Creation timestamp in UTC.
        /// </summary>
        public DateTime CreatedUTC { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// Last modified timestamp in UTC.
        /// </summary>
        public DateTime LastModifiedUTC { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// Assistant configuration GUID.
        /// </summary>
        public Guid AssistantConfigGUID { get; set; } = Guid.Empty;
        
        /// <summary>
        /// Metadata associated with the chat thread.
        /// </summary>
        public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
        
        #endregion
        
        #region Constructors-and-Factories
        
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        public ChatThread()
        {
        }
        
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        /// <param name="title">Title of the chat thread.</param>
        /// <param name="description">Description of the chat thread.</param>
        /// <param name="assistantConfigGuid">Assistant configuration GUID.</param>
        public ChatThread(string title, string description, Guid assistantConfigGuid)
        {
            if (String.IsNullOrEmpty(title)) throw new ArgumentNullException(nameof(title));
            if (assistantConfigGuid == Guid.Empty) throw new ArgumentException("Assistant config GUID cannot be empty", nameof(assistantConfigGuid));
            
            Title = title;
            Description = description;
            AssistantConfigGUID = assistantConfigGuid;
        }
        
        #endregion
    }
}