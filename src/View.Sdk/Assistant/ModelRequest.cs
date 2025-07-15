namespace View.Sdk.Assistant
{
    /// <summary>
    /// Model request for operations like pull, delete, load, and unload.
    /// </summary>
    public class ModelRequest
    {
        /// <summary>
        /// Model name.
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// Ollama hostname.
        /// </summary>
        public string OllamaHostname { get; set; }

        /// <summary>
        /// Ollama port.
        /// </summary>
        public int OllamaPort { get; set; }

        /// <summary>
        /// Unload flag for load/unload operations.
        /// </summary>
        public bool? Unload { get; set; }
    }
}