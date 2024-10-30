namespace View.Sdk
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Data flow step runtime.
    /// </summary>
    public enum StepRuntimeEnum
    {
        /// <summary>
        /// net8.0.
        /// </summary>
        [EnumMember(Value = "Dotnet8")]
        Dotnet8,
        /// <summary>
        /// Python 3.12.
        /// </summary>
        [EnumMember(Value = "Python3_12")]
        Python3_12,
    }
}
