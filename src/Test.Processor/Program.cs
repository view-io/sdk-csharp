namespace Test.Processor
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using GetSomeInput;
    using View.Sdk;
    using View.Sdk.Processor;
    using View.Sdk.Semantic;
    using View.Sdk.Serialization;

    public static class Program
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

        private static bool _RunForever = true;
        private static Guid _TenantGUID = default(Guid);
        private static string _AccessKey = "default";
        private static string _Endpoint = "http://localhost:8000/";
        private static ViewProcessorSdk _Sdk = null;
        private static Serializer _Serializer = new Serializer();
        private static bool _EnableLogging = true;

        public static void Main(string[] args)
        {
            _TenantGUID = Inputty.GetGuid("Tenant GUID :", _TenantGUID);
            _AccessKey = Inputty.GetString("Access key  :", _AccessKey, false);
            _Endpoint = Inputty.GetString("Endpoint    :", _Endpoint, false);

            _Sdk = new ViewProcessorSdk(_TenantGUID, _AccessKey, _Endpoint);
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

                    // Processor methods
                    case "process":
                        ProcessDocument().Wait();
                        break;
                    case "enumerate":
                        EnumerateProcessorTasks().Wait();
                        break;
                    case "retrieve":
                        RetrieveProcessorTask().Wait();
                        break;

                    // Cleanup methods
                    case "cleanup":
                        CleanupDocument().Wait();
                        break;

                    // Lexi embeddings methods
                    case "lexi":
                        ProcessLexiEmbeddings().Wait();
                        break;

                    // Type detector methods
                    case "type-detect":
                        DetectDocumentType().Wait();
                        break;

                    // UDR generator methods
                    case "udr":
                        GenerateUdr().Wait();
                        break;

                    // Semantic cell extraction
                    case "semanticcell":
                        ExtractSemanticCells().Wait();
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
            Console.WriteLine("Processor Methods:");
            Console.WriteLine("  process       Process a document request");
            Console.WriteLine("  enumerate     Enumerate processor tasks");
            Console.WriteLine("  retrieve      Retrieve a processor task by GUID");
            Console.WriteLine("");
            Console.WriteLine("Cleanup Methods:");
            Console.WriteLine("  cleanup       Cleanup a document");
            Console.WriteLine("  cleanup-enumerate  Enumerate cleanup tasks");
            Console.WriteLine("  cleanup-retrieve   Retrieve a cleanup task by GUID");
            Console.WriteLine("");
            Console.WriteLine("Lexi Embeddings Methods:");
            Console.WriteLine("  lexi          Process Lexi embeddings");
            Console.WriteLine("  lexi-enumerate     Enumerate Lexi embeddings tasks");
            Console.WriteLine("  lexi-retrieve      Retrieve a Lexi embeddings task by GUID");
            Console.WriteLine("");
            Console.WriteLine("Type Detector Methods:");
            Console.WriteLine("  type-detect   Detect document type");
            Console.WriteLine("");
            Console.WriteLine("UDR Generator Methods:");
            Console.WriteLine("  udr           Generate UDR document");
            Console.WriteLine("");
            Console.WriteLine("Semantic Cell Extraction:");
            Console.WriteLine("  semanticcell  Extract semantic cells from a document");
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

        private static T BuildObject<T>()
        {
            string json = Inputty.GetString("JSON :", null, false);
            return _Serializer.DeserializeJson<T>(json);
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

        #region Processor Methods

        private static async Task ProcessDocument()
        {
            string file = Inputty.GetString("Filename:", "./SampleRequest.json", false);

            Guid guid = Inputty.GetGuid("GUID:", default(Guid));
            ProcessorRequest req = _Serializer.DeserializeJson<ProcessorRequest>(File.ReadAllText(file));

            ProcessorResult resp = await _Sdk.Processor.Process(
                req.MetadataRuleGUID,
                req.EmbeddingsRuleGUID,
                req.Object);

            EnumerateResponse(resp);
        }

        private static async Task EnumerateProcessorTasks()
        {
            int maxKeys = Inputty.GetInteger("Max keys to return:", 3, true, true);
            EnumerateResponse(await _Sdk.Processor.Enumerate(maxKeys));
        }

        private static async Task RetrieveProcessorTask()
        {
            Guid taskGuid = Inputty.GetGuid("Processor task GUID:", default(Guid));
            EnumerateResponse(await _Sdk.Processor.Retrieve(taskGuid));
        }

        #endregion

        #region Cleanup Methods

        private static async Task CleanupDocument()
        {
            Console.WriteLine("");
            Console.WriteLine("Cleanup Document Test");
            try
            {
                Guid guid = Inputty.GetGuid("GUID:", default(Guid));
                TenantMetadata tenant = BuildObject<TenantMetadata>();
                Collection collection = BuildObject<Collection>();
                StoragePool pool = BuildObject<StoragePool>();
                BucketMetadata bucket = BuildObject<BucketMetadata>();
                ObjectMetadata obj = BuildObject<ObjectMetadata>();
                MetadataRule mdRule = BuildObject<MetadataRule>();
                EmbeddingsRule embedRule = BuildObject<EmbeddingsRule>();
                VectorRepository vectorRepo = BuildObject<VectorRepository>();
                GraphRepository graphRepo = BuildObject<GraphRepository>();

                CleanupResult resp = await _Sdk.Cleanup.Process(
                    tenant,
                    collection,
                    pool,
                    bucket,
                    obj,
                    mdRule,
                    embedRule,
                    vectorRepo,
                    graphRepo,
                    async: false);

                EnumerateResponse(resp);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        #endregion

        #region Lexi Embeddings Methods

        private static async Task ProcessLexiEmbeddings()
        {
            Console.WriteLine("");
            Console.WriteLine("Lexi Embeddings Test");
            Console.WriteLine("Note: This requires valid metadata objects. Using sample data for demonstration.");

            try
            {
                Guid guid = Inputty.GetGuid("GUID:", default(Guid));
                TenantMetadata tenant = BuildObject<TenantMetadata>();
                Collection collection = BuildObject<Collection>();
                SearchResult search = BuildObject<SearchResult>();
                EmbeddingsRule embedRule = BuildObject<EmbeddingsRule>();
                VectorRepository vectorRepo = BuildObject<VectorRepository>();

                LexiEmbeddingsResult resp = await _Sdk.LexiEmbeddings.Process(
                    search,
                    embedRule,
                    vectorRepo);

                EnumerateResponse(resp);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        #endregion

        #region Type Detector Methods

        private static async Task DetectDocumentType()
        {
            Console.WriteLine("");
            Console.WriteLine("Type Detection Test");
            Console.WriteLine("Enter JSON content to analyze for type detection:");

            string jsonContent = Inputty.GetString("JSON Content (or press Enter for sample):", "", true);

            if (string.IsNullOrEmpty(jsonContent))
            {
                jsonContent = @"{""hello"":""world""}";
                Console.WriteLine($"Using sample JSON content: {jsonContent}");
            }

            try
            {
                TypeResult resp = await _Sdk.TypeDetector.DetectType(jsonContent);
                EnumerateResponse(resp);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        #endregion

        #region UDR Generator Methods

        private static async Task GenerateUdr()
        {
            Console.WriteLine("");
            Console.WriteLine("UDR Generation Test");
            try
            {
                string filename = Inputty.GetString("Filename        :", "sample/json/1.json", false);
                UdrDocumentRequest request = new UdrDocumentRequest
                {
                    GUID = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                    Key = "testfile.text",
                    ContentType = "text/plain",
                    Type = DocumentTypeEnum.Text,
                    IncludeFlattened = true,
                    CaseInsensitive = true,
                    TopTerms = 10,
                    AdditionalData = "The body below is simple sample text, base64 encoded, taken from https://en.wikipedia.org/wiki/Artificial_intelligence.",
                    Metadata = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase)
                    {
                        ["foo"] = "bar"
                    },
                    MetadataRule = new MetadataRule
                    {
                        GUID = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                        TenantGUID = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                        BucketGUID = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                        OwnerGUID = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                        Name = "My metadata rule",
                        ContentType = "text/plain",
                        UdrEndpoint = "http://localhost:8000/",
                        DataCatalogType = DataCatalogTypeEnum.Lexi,
                        DataCatalogEndpoint = "http://localhost:8000/",
                        DataCatalogCollection = "00000000-0000-0000-0000-000000000000",
                        TopTerms = 10,
                        CaseInsensitive = true,
                        IncludeFlattened = true
                    },
                    Data = "QXJ0aWZpY2lhbCBpbnRlbG..."
                };

                UdrDocument resp = await _Sdk.UdrGenerator.GenerateUdr(request, filename);
                EnumerateResponse(resp);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        #endregion

        private static async Task ExtractSemanticCells()
        {
            Console.WriteLine("");
            Console.WriteLine("Semantic Cell Extraction Test");

            try
            {
                SemanticCellExtractionRequest req = BuildObject<SemanticCellExtractionRequest>();
                SemanticCellResult resp = await _Sdk.SemanticCellExtraction.Process(req);
                EnumerateResponse(resp);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
}