﻿namespace View.Sdk
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Data flow step result.
    /// </summary>
    public enum StepResultEnum
    {
        /// <summary>
        /// Success.
        /// </summary>
        [EnumMember(Value = "Success")]
        Success,
        /// <summary>
        /// Failure.
        /// </summary>
        [EnumMember(Value = "Failure")]
        Failure,
        /// <summary>
        /// Exception.
        /// </summary>
        [EnumMember(Value = "Exception")]
        Exception
    }
}
