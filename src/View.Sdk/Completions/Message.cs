namespace View.Sdk.Completions
{
    using System;

    /// <summary>
    /// Completion message.
    /// </summary>
    public class Message
    {
        private string _Role;
        private string _Content;

        /// <summary>
        /// Role: "system", "user", "assistant"
        /// </summary>
        public string Role
        {
            get => _Role;
            set => _Role = value ?? throw new ArgumentNullException(nameof(Role));
        }

        /// <summary>
        /// The message content
        /// </summary>
        public string Content
        {
            get => _Content;
            set => _Content = value ?? throw new ArgumentNullException(nameof(Content));
        }

        /// <summary>
        /// Optional name identifier for the message sender
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Completion message.
        /// </summary>
        public Message()
        {

        }
    }
}
