namespace View.Sdk.Assistant
{
    using System.Collections.Generic;
    
    /// <summary>
    /// Response containing a list of chat threads.
    /// </summary>
    public class ChatThreadsResponse
    {
        #region Public-Members
        
        /// <summary>
        /// List of chat threads.
        /// </summary>
        public List<ChatThread> ChatThreads { get; set; } = new List<ChatThread>();
        
        #endregion
        
        #region Constructors-and-Factories
        
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        public ChatThreadsResponse()
        {
        }
        
        #endregion
    }
}