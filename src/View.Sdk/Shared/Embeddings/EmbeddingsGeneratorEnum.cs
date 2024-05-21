﻿namespace View.Sdk.Shared.Embeddings
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Embeddings provider.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EmbeddingsGeneratorEnum
    {
        /// <summary>
        /// OpenAI.
        /// </summary>
        [EnumMember(Value = "OpenAI")]
        OpenAI,
        /// <summary>
        /// Langchain proxy.
        /// </summary>
        [EnumMember(Value = "LCProxy")]
        LCProxy
    }
}