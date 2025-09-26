namespace View.Sdk
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Model API type.
    /// </summary>
    public enum ModelApiTypeEnum
    {
        /// <summary>
        /// Ollama.
        /// </summary>
        [EnumMember(Value = "Ollama")]
        Ollama,
        /// <summary>
        /// OpenAi.
        /// </summary>
        [EnumMember(Value = "OpenAi")]
        OpenAi,
    }
}
