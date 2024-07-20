namespace Test.Pgvector
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GetSomeInput;
    using View.Sdk.Vector;
    using View.Serializer;
    using View.Sdk;

    public static class Program
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

        private static bool _RunForever = true;
        private static string _TenantGUID = "default";
        private static string _AccessKey = "default";
        private static string _Endpoint = "http://localhost:8311/";
        private static ViewVectorProxySdk _Sdk = null;
        private static SerializationHelper _Serializer = new SerializationHelper();
        private static bool _EnableLogging = true;

        public static void Main(string[] args)
        {
            _TenantGUID = Inputty.GetString("Tenant GUID :", _Endpoint, false);
            _AccessKey =  Inputty.GetString("Access key  :", _Endpoint, false);
            _Endpoint =   Inputty.GetString("Endpoint    :", _Endpoint, false);
            _Sdk = new ViewVectorProxySdk(_TenantGUID, _AccessKey, _Endpoint);
            if (_EnableLogging) _Sdk.Logger = EmitLogMessage;

            while (_RunForever)
            {
                string userInput = Inputty.GetString("Command [?/help]:", null, false);

                switch (userInput)
                {
                    case "q":
                        _RunForever = false;
                        break;
                    case "?":
                        Menu();
                        break;
                    case "cls":
                        Console.Clear();
                        break;

                    case "conn":
                        TestConnectivity().Wait();
                        break;

                    case "write":
                        WriteDocument().Wait();
                        break;
                    case "del":
                        DeleteDocument().Wait();
                        break;
                    case "truncate":
                        TruncateTable().Wait();
                        break;
                    case "search":
                        SimilaritySearch().Wait();
                        break;
                    case "query":
                        RawQuery().Wait();
                        break;
                }
            }
        }

        private static void Menu()
        {
            Console.WriteLine("");
            Console.WriteLine("Available commands:");
            Console.WriteLine("  q             Quit this program");
            Console.WriteLine("  ?             Help, this menu");
            Console.WriteLine("  cls           Clear the screen");
            Console.WriteLine("  conn          Test connectivity");
            Console.WriteLine("");
            Console.WriteLine("  write         Write embeddings document");
            Console.WriteLine("  del           Delete embeddings document");
            Console.WriteLine("  truncate      Truncate table");
            Console.WriteLine("  search        Execute similarity search");
            Console.WriteLine("  query         Execute raw query");
            Console.WriteLine("");
        }

        private static void EnumerateResponse(object obj)
        {
            Console.WriteLine("");
            Console.Write("Response:");
            if (obj != null)
                Console.WriteLine(Environment.NewLine + _Serializer.SerializeJson(obj, true));
            else
                Console.WriteLine("(null)");
            Console.WriteLine("");
        }

        private static void EmitLogMessage(SeverityEnum sev, string msg)
        {
            if (!String.IsNullOrEmpty(msg)) Console.WriteLine(sev.ToString() + " " + msg);
        }

        private static async Task TestConnectivity()
        {
            Console.WriteLine("");
            Console.Write("State: ");

            if (await _Sdk.ValidateConnectivity())
                Console.WriteLine("Connected");
            else
                Console.WriteLine("Not connected");

            Console.WriteLine("");
        }

        private static async Task WriteDocument()
        {
            string dbSettingsJson = Inputty.GetString("Database settings JSON :", null, true);
            if (String.IsNullOrEmpty(dbSettingsJson)) return;

            string requestJson =    Inputty.GetString("Request JSON           :", null, true);
            if (String.IsNullOrEmpty(requestJson)) return;

            List<EmbeddingsDocument> docs = await _Sdk.WriteDocument(
                _Serializer.DeserializeJson<EmbeddingsDocument>(requestJson)
                );

            EnumerateResponse(docs);
        }

        private static async Task DeleteDocument()
        {
            string dbSettingsJson = Inputty.GetString("Database settings JSON :", null, true);
            if (String.IsNullOrEmpty(dbSettingsJson)) return;

            string requestJson = Inputty.GetString("Request JSON           :", null, true);
            if (String.IsNullOrEmpty(requestJson)) return;

            await _Sdk.DeleteDocument(
                _Serializer.DeserializeJson<DeleteRequest>(requestJson)
                );
        }

        private static async Task TruncateTable()
        {
            string dbSettingsJson = Inputty.GetString("Database settings JSON :", null, true);
            if (String.IsNullOrEmpty(dbSettingsJson)) return;

            string requestJson = Inputty.GetString("Request JSON           :", null, true);
            if (String.IsNullOrEmpty(requestJson)) return;

            await _Sdk.TruncateTable(
                _Serializer.DeserializeJson<DeleteRequest>(requestJson)
                );
        }

        private static async Task SimilaritySearch()
        {
            string dbSettingsJson = Inputty.GetString("Database settings JSON :", null, true);
            if (String.IsNullOrEmpty(dbSettingsJson)) return;

            string requestJson = Inputty.GetString("Request JSON           :", null, true);
            if (String.IsNullOrEmpty(requestJson)) return;

            List<EmbeddingsDocument> docs = await _Sdk.SimilaritySearch(
                _Serializer.DeserializeJson<VectorSearchRequest>(requestJson)
                );

            EnumerateResponse(docs);
        }

        private static async Task RawQuery()
        {
            string dbSettingsJson = Inputty.GetString("Database settings JSON :", null, true);
            if (String.IsNullOrEmpty(dbSettingsJson)) return;

            string requestJson = Inputty.GetString("Request JSON           :", null, true);
            if (String.IsNullOrEmpty(requestJson)) return;

            string ret = await _Sdk.RawQuery(
                _Serializer.DeserializeJson<QueryRequest>(requestJson)
                );

            Console.WriteLine("");
            Console.WriteLine(ret);
            Console.WriteLine("");
        }

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
}