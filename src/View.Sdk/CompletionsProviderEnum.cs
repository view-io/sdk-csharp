namespace View.Sdk
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Completions provider.
    /// </summary>
    public enum CompletionsProviderEnum
    {
        /// <summary>
        /// OpenAI.
        /// </summary>
        [EnumMember(Value = "OpenAI")]
        OpenAI,
        /// <summary>
        /// Ollama.
        /// </summary>
        [EnumMember(Value = "Ollama")]
        Ollama
    }
}
