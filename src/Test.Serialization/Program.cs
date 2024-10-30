namespace Test.Serialization
{
    using System.Text.Json;
    using View.Sdk;
    using View.Sdk.Serialization;
    using View.Models;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new StrictEnumConverterFactory());

            // Test 1: Direct enum deserialization (we know this throws correctly)
            try
            {
                var enumValue = JsonSerializer.Deserialize<DataCatalogTypeEnum>("123", options);
                Console.WriteLine("Test 1 failed - should have thrown");
            }
            catch (JsonException)
            {
                Console.WriteLine("Test 1 passed - correctly threw exception");
            }

            // Test 2: Simple object containing enum
            var json2 = @"{""DataCatalogType"": 123}";
            try
            {
                var obj = JsonSerializer.Deserialize<MetadataRule>(json2, options);
                Console.WriteLine("Test 2 failed - should have thrown");
            }
            catch (JsonException)
            {
                Console.WriteLine("Test 2 passed - correctly threw exception");
            }

            // Test 3: Using the Serializer class
            var serializer = new Serializer();
            try
            {
                var obj = serializer.DeserializeJson<MetadataRule>(json2);
                Console.WriteLine("Test 3 failed - should have thrown");
            }
            catch (JsonException)
            {
                Console.WriteLine("Test 3 passed - correctly threw exception");
            }
        }
    }
}