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
        private static string _AccessKey = "default";
        private static ViewLexiSdk _Sdk = null;
        private static Serializer _Serializer = new Serializer();
        private static bool _EnableLogging = true;

        public static void Main(string[] args)
        {
            _TenantGuid =   Inputty.GetGuid("Tenant GUID :", _TenantGuid);
            _Endpoint   = Inputty.GetString("Endpoint    :", _Endpoint, false);
            _AccessKey = Inputty.GetString("Access key  :", _AccessKey, false);
            _Sdk = new ViewLexiSdk(_TenantGuid, _AccessKey, _Endpoint);
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
                    case "enum colls":
                        EnumerateCollections().Wait();
                        break;
                    case "topterms coll":
                        RetrieveCollectionTopTerms().Wait();
                        break;
                    case "exists coll":
                        CollectionExists().Wait();
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
                    case "exists doc":
                        DocumentExists().Wait();
                        break;
                    case "topterms doc":
                        RetrieveDocumentTopTerms().Wait();
                        break;

                    case "enumerate":
                        EnumerateCollection().Wait();
                        break;
                    case "search":                        
                        SearchCollection().Wait();
                        break;
                    case "search incldata":
                        SearchCollectionIncludeData().Wait();
                        break;
                    case "search topterms":
                        SearchCollectionIncludeTopTerms().Wait();
                        break;
                    case "search async":
                        SearchCollectionAsync().Wait();
                        break;
                    case "search enumerate":
                        SearchEnumerate().Wait();
                        break;
                    case "ingest queue":
                        RetrieveAllIngestQueueEntries().Wait();
                        break;
                    case "ingest entry":
                        RetrieveIngestQueueEntry().Wait();
                        break;
                    case "ingest entry stats":
                        RetrieveIngestQueueEntryWithStats().Wait();
                        break;
                    case "del ingest entry":
                        DeleteIngestQueueEntry().Wait();
                        break;
                    case "exists ingest entry":
                        IngestQueueEntryExists().Wait();
                        break;
                    case "ingest queue stats":
                        RetrieveIngestQueueStatistics().Wait();
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
            Console.WriteLine("  enum colls    Enumerate collections");
            Console.WriteLine("  topterms coll     Retrieve collection top terms");
            Console.WriteLine("  exists coll   Check if collection exists");
            Console.WriteLine("");
            Console.WriteLine("  docs          List documents in collection");
            Console.WriteLine("  doc           Retrieve document from collection");
            Console.WriteLine("  doc stats     Retrieve document statistics");
            Console.WriteLine("  write doc     Write document");
            Console.WriteLine("  del doc       Delete document");
            Console.WriteLine("  exists doc    Check if document exists");
            Console.WriteLine("  topterms doc  Retrieve document top terms");
            Console.WriteLine("");
            Console.WriteLine("  enumerate     Enumerate collection documents");
            Console.WriteLine("");
            Console.WriteLine("  search        Search collection");
            Console.WriteLine("  search incldata  Search collection with included data");
            Console.WriteLine("  search topterms  Search collection with top terms");
            Console.WriteLine("  search async     Search collection asynchronously");
            Console.WriteLine("  search enumerate Enumerate documents using search");
            Console.WriteLine("");
            Console.WriteLine("  ingest queue         List all ingest queue entries");
            Console.WriteLine("  ingest entry         Retrieve ingest queue entry");
            Console.WriteLine("  ingest entry stats   Retrieve ingest queue entry with statistics");
            Console.WriteLine("  ingest queue stats   Retrieve ingest queue statistics");
            Console.WriteLine("  del ingest entry     Delete ingest queue entry");
            Console.WriteLine("  exists ingest entry  Check if ingest queue entry exists");
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
            List<Collection> collections = await _Sdk.Collection.RetrieveMany();
            EnumerateResponse(collections);
        }

        private static async Task RetrieveCollection() 
        {
            Collection coll = await _Sdk.Collection.Retrieve(GetCollectionGuid());
            EnumerateResponse(coll);
        }

        private static async Task RetrieveCollectionStatistics() 
        {
            CollectionStatistics stats = await _Sdk.Collection.RetrieveStatistics(GetCollectionGuid());
            EnumerateResponse(stats);
        }

        private static async Task WriteCollection() 
        {
            Collection coll = await _Sdk.Collection.Create(BuildCollection());
            EnumerateResponse(coll);
        }

        private static async Task DeleteCollection() 
        {
            await _Sdk.Collection.Delete(GetCollectionGuid());
        }

        private static async Task RetrieveAllDocuments()
        {
            List<SourceDocument> documents = await _Sdk.SourceDocument.RetrieveMany(GetCollectionGuid());
            EnumerateResponse(documents);
        }

        private static async Task RetrieveCollectionTopTerms()
        {
            Guid collectionGuid = GetCollectionGuid();
            int maxKeys = Inputty.GetInteger("Max top terms :", 10, true, false);

            CollectionTopTerms topTerms = await _Sdk.Collection.RetrieveTopTerms(collectionGuid, maxKeys);
            EnumerateResponse(topTerms);
        }

        private static async Task CollectionExists()
        {
            Guid collectionGuid = GetCollectionGuid();

            bool exists = await _Sdk.Collection.Exists(collectionGuid);
            Console.WriteLine("");
            Console.WriteLine($"Collection exists: {exists}");
            Console.WriteLine("");
        }

        private static async Task RetrieveDocument()
        {
            SourceDocument doc = await _Sdk.SourceDocument.Retrieve(
                GetCollectionGuid(),
                GetDocumentGuid(),
                true);
            EnumerateResponse(doc);
        }

        private static async Task RetrieveDocumentStatistics()
        {
            SourceDocumentStatistics stats = await _Sdk.SourceDocument.RetrieveStatistics(
                GetCollectionGuid(),
                GetDocumentGuid());
            EnumerateResponse(stats);
        }

        private static async Task WriteDocument() 
        {
            SourceDocument document = await _Sdk.SourceDocument.Upload(BuildSourceDocument());
            EnumerateResponse(document);
        }

        private static async Task DeleteDocument()
        {
            await _Sdk.SourceDocument.Delete(
                GetCollectionGuid(),
                GetDocumentGuid());
        }
        private static async Task DocumentExists()
        {
            Guid collectionGuid = GetCollectionGuid();
            Guid documentGuid = GetDocumentGuid();

            bool exists = await _Sdk.SourceDocument.Exists(collectionGuid, documentGuid);
            Console.WriteLine("");
            Console.WriteLine($"Document exists: {exists}");
            Console.WriteLine("");
        }

        private static async Task RetrieveDocumentTopTerms()
        {
            Guid collectionGuid = GetCollectionGuid();
            Guid documentGuid = GetDocumentGuid();
            CollectionTopTerms topTerms = await _Sdk.SourceDocument.RetrieveTopTerms(collectionGuid, documentGuid);
            EnumerateResponse(topTerms);
        }

        private static async Task EnumerateCollection() 
        {
            EnumerationResult<SourceDocument> result = await _Sdk.Enumerate.Enumerate(
                GetCollectionGuid(),
                BuildEnumerationQuery());
            EnumerateResponse(result);
        }

        private static async Task EnumerateCollections()
        {
            int maxKeys = Inputty.GetInteger("Max keys :", 5, true, false);
            EnumerationResult<Collection> result = await _Sdk.Collection.Enumerate(maxKeys);
            EnumerateResponse(result);
        }

        private static async Task SearchCollection()
        {
            SearchResult result = await _Sdk.Search.Search(
                GetCollectionGuid(),
                BuildSearchQuery());
            EnumerateResponse(result);
        }

        private static async Task SearchCollectionIncludeData()
        {
            SearchResult result = await _Sdk.Search.SearchIncludeData(
                GetCollectionGuid(),
                BuildSearchQuery());
            EnumerateResponse(result);
        }

        private static async Task SearchCollectionIncludeTopTerms()
        {
            SearchResult result = await _Sdk.Search.SearchIncludeTopTerms(
                GetCollectionGuid(),
                BuildSearchQuery());
            EnumerateResponse(result);
        }

        private static async Task SearchCollectionAsync()
        {
            SearchResult result = await _Sdk.Search.SearchAsync(
                GetCollectionGuid(),
                BuildSearchQuery());
            EnumerateResponse(result);
        }

        private static async Task SearchEnumerate()
        {
            EnumerationResult<SourceDocument> result = await _Sdk.Search.Enumerate(
                GetCollectionGuid(),
                BuildSearchQuery());
            EnumerateResponse(result);
        }

        private static Guid GetIngestQueueEntryGuid()
        {
            return Inputty.GetGuid("Ingest Queue Entry GUID :", default(Guid));
        }

        private static async Task RetrieveAllIngestQueueEntries()
        {
            List<IngestionQueueEntry> entries = await _Sdk.IngestQueue.RetrieveMany();
            EnumerateResponse(entries);
        }

        private static async Task RetrieveIngestQueueEntry()
        {
            IngestionQueueEntry entry = await _Sdk.IngestQueue.Retrieve(
                GetIngestQueueEntryGuid(),
                false);
            EnumerateResponse(entry);
        }

        private static async Task RetrieveIngestQueueEntryWithStats()
        {
            IngestionQueueEntry entry = await _Sdk.IngestQueue.Retrieve(
                GetIngestQueueEntryGuid(),
                true);
            EnumerateResponse(entry);
        }

        private static async Task DeleteIngestQueueEntry()
        {
            await _Sdk.IngestQueue.Delete(GetIngestQueueEntryGuid());
        }

        private static async Task IngestQueueEntryExists()
        {
            Guid entryGuid = GetIngestQueueEntryGuid();

            bool exists = await _Sdk.IngestQueue.Exists(entryGuid);
            Console.WriteLine("");
            Console.WriteLine($"Ingest queue entry exists: {exists}");
            Console.WriteLine("");
        }

        private static async Task RetrieveIngestQueueStatistics()
        {
            IngestQueueStatistics stats = await _Sdk.IngestQueue.RetrieveStatistics();
            EnumerateResponse(stats);
        }
    }
}