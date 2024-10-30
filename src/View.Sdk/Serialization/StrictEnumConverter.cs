﻿namespace View.Sdk.Serialization
{
    using System;
    using System.Linq;
    using System.Text.Json.Serialization;
    using System.Text.Json;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Strict enum converter.
    /// </summary>
    public class StrictEnumConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct, Enum
    {
        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">Reader.</param>
        /// <param name="typeToConvert">Type to convert.</param>
        /// <param name="options">JSON serializer options.</param>
        /// <returns>DateTime.</returns>
        public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                string stringValue = reader.GetString();
                if (!Enum.TryParse<TEnum>(stringValue, ignoreCase: true, out var enumValue) ||
                    !Enum.IsDefined(typeof(TEnum), enumValue))
                {
                    throw new JsonException($"String value '{stringValue}' is not valid for enum type {typeof(TEnum).Name}");
                }
                return enumValue;
            }

            if (reader.TokenType == JsonTokenType.Number)
            {
                int intValue = reader.GetInt32();
                // Explicitly get the defined values
                var definedValues = (int[])Enum.GetValues(typeof(TEnum));

                if (!Array.Exists(definedValues, x => x == intValue))
                {
                    throw new JsonException($"Integer value {intValue} is not defined in enum {typeof(TEnum).Name}");
                }

                return (TEnum)Enum.ToObject(typeof(TEnum), intValue);
            }

            throw new JsonException($"Cannot convert {reader.TokenType} to enum {typeof(TEnum).Name}");
        }

        /// <summary>
        /// Write.
        /// </summary>
        /// <param name="writer">Writer.</param>
        /// <param name="value">Value.</param>
        /// <param name="options">JSON serializer options.</param>
        public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
