namespace View.Sdk.Shared.Orchestrator
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Data flow step runtime.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StepRuntimeEnum
    {
        /// <summary>
        /// Dotnet8.
        /// </summary>
        [EnumMember(Value = "Dotnet8")]
        Dotnet8,
    }
}
