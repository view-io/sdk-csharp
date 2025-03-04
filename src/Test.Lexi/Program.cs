namespace Test.Lexi
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using GetSomeInput;
    using View.Sdk;
    using View.Sdk.Lexi;
    using View.Sdk.Serialization;

    public static class Program
    {
        private static bool _RunForever = true;
        private static Guid _TenantGuid = default(Guid);
        private static string _Endpoint = "http://localhost:8000/";
        private static ViewLexiSdk _Sdk = null;
        private static Serializer _Serializer = new Serializer();
        private static bool _EnableLogging = true;

        public static void Main(string[] args)
        {
            _TenantGuid =   Inputty.GetGuid("Tenant GUID :", _TenantGuid);
            _Endpoint   = Inputty.GetString("Endpoint    :", _Endpoint, false);
            _Sdk = new ViewLexiSdk(_TenantGuid, _Endpoint);
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

                    case "colls":
                        RetrieveAllCollections().Wait();
                        break;
                    case "coll":
                        RetrieveCollection().Wait();
                        break;
                    case "coll stats":
                        RetrieveCollectionStatistics().Wait();
                        break;
                    case "write coll":
                        WriteCollection().Wait();
                        break;
                    case "del coll":
                        DeleteCollection().Wait();
                        break;

                    case "docs":
                        RetrieveAllDocuments().Wait();
                        break;
                    case "doc":
                        RetrieveDocument().Wait();
                        break;
                    case "doc stats":
                        RetrieveDocumentStatistics().Wait();
                        break;
                    case "write doc":
                        WriteDocument().Wait();
                        break;
                    case "del doc":
                        DeleteDocument().Wait();
                        break;

                    case "enumerate":
                        EnumerateCollection().Wait();
                        break;
                    case "search":
                        SearchCollection().Wait();
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
            Console.WriteLine("  colls         List collections");
            Console.WriteLine("  coll          Retrieve collection");
            Console.WriteLine("  coll stats    Retrieve collection statistics");
            Console.WriteLine("  write coll    Create collection");
            Console.WriteLine("  del coll      Delete collection");
            Console.WriteLine("");
            Console.WriteLine("  docs          List documents in collection");
            Console.WriteLine("  doc           Retrieve document from collection");
            Console.WriteLine("  doc stats     Retrieve document statistics");
            Console.WriteLine("  write doc     Write document");
            Console.WriteLine("  del doc       Delete document");
            Console.WriteLine("");
            Console.WriteLine("  enumerate     Enumerate collection");
            Console.WriteLine("  search        Search collection");
            Console.WriteLine("");
        }

        private static void EmitLogMessage(SeverityEnum sev, string msg)
        {
            if (!String.IsNullOrEmpty(msg)) Console.WriteLine(sev.ToString() + " " + msg);
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

        private static Guid GetCollectionGuid()
        {
            return Inputty.GetGuid("Collection GUID :", default(Guid));
        }

        private static Guid GetDocumentGuid()
        {
            return Inputty.GetGuid("Document GUID   :", default(Guid));
        }

        private static Collection BuildCollection()
        {
            string json = Inputty.GetString("Collection JSON :", null, false);
            return _Serializer.DeserializeJson<Collection>(json);
        }

        private static SourceDocument BuildSourceDocument()
        {
            string json = Inputty.GetString("Source document JSON :", null, false);
            return _Serializer.DeserializeJson<SourceDocument>(json);
        }

        private static EnumerationQuery BuildEnumerationQuery()
        {
            string json = Inputty.GetString("Enumeration query JSON :", null, false);
            return _Serializer.DeserializeJson<EnumerationQuery>(json);
        }

        private static CollectionSearchRequest BuildSearchQuery()
        {
            string json = Inputty.GetString("Search query JSON :", null, false);
            return _Serializer.DeserializeJson<CollectionSearchRequest>(json);
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

        private static async Task RetrieveAllCollections() 
        {
            List<Collection> collections = await _Sdk.RetrieveCollections();
            EnumerateResponse(collections);
        }

        private static async Task RetrieveCollection() 
        {
            Collection coll = await _Sdk.RetrieveCollection(GetCollectionGuid());
            EnumerateResponse(coll);
        }

        private static async Task RetrieveCollectionStatistics() 
        {
            CollectionStatistics stats = await _Sdk.RetrieveCollectionStatistics(GetCollectionGuid());
            EnumerateResponse(stats);
        }

        private static async Task WriteCollection() 
        {
            Collection coll = await _Sdk.CreateCollection(BuildCollection());
            EnumerateResponse(coll);
        }

        private static async Task DeleteCollection() 
        {
            await _Sdk.DeleteCollection(GetCollectionGuid());
        }

        private static async Task RetrieveAllDocuments()
        {
            List<SourceDocument> documents = await _Sdk.RetrieveDocuments(GetCollectionGuid());
            EnumerateResponse(documents);
        }

        private static async Task RetrieveDocument()
        {
            SourceDocument doc = await _Sdk.RetrieveDocument(
                GetCollectionGuid(),
                GetDocumentGuid(),
                true);
            EnumerateResponse(doc);
        }

        private static async Task RetrieveDocumentStatistics()
        {
            SourceDocumentStatistics stats = await _Sdk.RetrieveDocumentStatistics(
                GetCollectionGuid(),
                GetDocumentGuid());
            EnumerateResponse(stats);
        }

        private static async Task WriteDocument() 
        {
            SourceDocument document = await _Sdk.UploadDocument(BuildSourceDocument());
            EnumerateResponse(document);
        }

        private static async Task DeleteDocument()
        {
            await _Sdk.DeleteDocument(
                GetCollectionGuid(),
                GetDocumentGuid());
        }

        private static async Task EnumerateCollection() 
        {
            EnumerationResult<SourceDocument> result = await _Sdk.Enumerate(
                GetCollectionGuid(),
                BuildEnumerationQuery());
            EnumerateResponse(result);
        }

        private static async Task SearchCollection()
        {
            SearchResult result = await _Sdk.Search(
                GetCollectionGuid(),
                BuildSearchQuery());
            EnumerateResponse(result);
        } 
    }
}