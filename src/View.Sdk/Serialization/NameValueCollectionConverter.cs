namespace View.Sdk.Serialization
{
    using System;
    using System.Linq;
    using System.Text.Json.Serialization;
    using System.Text.Json;
    using System.Collections.Specialized;

    /// <summary>
    /// NameValueCollection converter.
    /// </summary>
    public class NameValueCollectionConverter : JsonConverter<NameValueCollection>
    {
        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">Reader.</param>
        /// <param name="typeToConvert">Type to convert.</param>
        /// <param name="options">JSON serializer options.</param>
        /// <returns>NameValueCollection.</returns>
        public override NameValueCollection Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

        /// <summary>
        /// Write.
        /// </summary>
        /// <param name="writer">Writer.</param>
        /// <param name="value">Value.</param>
        /// <param name="options">JSON serializer options.</param>
        public override void Write(Utf8JsonWriter writer, NameValueCollection value, JsonSerializerOptions options)
        {
            var val = value.Keys.Cast<string>()
                .ToDictionary(k => k, k => string.Join(", ", value.GetValues(k)));
            System.Text.Json.JsonSerializer.Serialize(writer, val);
        }
    }
}
