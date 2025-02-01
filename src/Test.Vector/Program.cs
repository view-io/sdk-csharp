namespace Test.Pgvector
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GetSomeInput;
    using View.Sdk;
    using View.Sdk.Vector;
    using View.Sdk.Serialization;
    using View.Sdk.Semantic;
    using View.Sdk.Embeddings;

    public static class Program
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

        private static bool _RunForever = true;
        private static Guid _TenantGUID = default(Guid);
        private static string _AccessKey = "default";
        private static string _Endpoint = "http://localhost:8000/";
        private static ViewVectorSdk _Sdk = null;
        private static Serializer _Serializer = new Serializer();
        private static bool _EnableLogging = true;

        public static void Main(string[] args)
        {
            _TenantGUID =   Inputty.GetGuid("Tenant GUID :", _TenantGUID);
            _AccessKey =  Inputty.GetString("Access key  :", _AccessKey, false);
            _Endpoint =   Inputty.GetString("Endpoint    :", _Endpoint, false);
            _Sdk = new ViewVectorSdk(_TenantGUID, _AccessKey, _Endpoint);
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
                    case "find":
                        FindEmbeddings().Wait();
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
            Console.WriteLine("  find          Find embeddings by SHA-256");
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
            Console.WriteLine("");
            Console.WriteLine("Example document:");
            Console.WriteLine(_Serializer.SerializeJson(new EmbeddingsDocument
            {
                TenantGUID = default(Guid),
                VectorRepositoryGUID = default(Guid),
                ObjectKey = "test.json",
                ObjectVersion = "1",
                SemanticCells = new List<SemanticCell>
                {
                    new SemanticCell
                    {
                        CellType = SemanticCellTypeEnum.Text,
                        MD5Hash = "000",
                        SHA1Hash = "000",
                        SHA256Hash = "000",
                        Position = 0,
                        Chunks = new List<SemanticChunk>
                        {
                            new SemanticChunk
                            {
                                MD5Hash = "000",
                                SHA1Hash = "000",
                                SHA256Hash = "000",
                                Position = 0,
                                Content = "This is a sample chunk",
                                Embeddings = new List<float>
                                {
                                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
                                }
                            }
                        }
                    }
                }
            }, false));
            Console.WriteLine("");

            string requestJson =    Inputty.GetString("Request JSON           :", null, true);
            if (String.IsNullOrEmpty(requestJson)) return;

            EmbeddingsDocument doc = await _Sdk.WriteDocument(
                _Serializer.DeserializeJson<EmbeddingsDocument>(requestJson)
                );

            EnumerateResponse(doc);
        }

        private static async Task DeleteDocument()
        {
            Guid repoGuid = Inputty.GetGuid("Repository GUID:", default(Guid));
            Guid docGuid = Inputty.GetGuid("Document GUID  :", default(Guid));
            bool success = await _Sdk.DeleteDocument(repoGuid, docGuid);
            Console.WriteLine("Success: " + success);
        }

        private static async Task TruncateTable()
        {
            Guid repoGuid = Inputty.GetGuid("Repository GUID:", default(Guid));

            bool success = await _Sdk.TruncateRepository(repoGuid);
            Console.WriteLine("Success: " + success);
        }

        private static async Task SimilaritySearch()
        {
            Console.WriteLine("");
            Console.WriteLine("Example search:");
            Console.WriteLine(_Serializer.SerializeJson(new VectorSearchRequest
            {
                SearchType = VectorSearchTypeEnum.L2Distance,
                TenantGUID = default(Guid),
                VectorRepositoryGUID = default(Guid),
                MaxResults = 5,
                Embeddings = new List<float>
                {
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
                }
            }, false));
            Console.WriteLine("");

            string requestJson = Inputty.GetString("Request JSON           :", null, true);
            if (String.IsNullOrEmpty(requestJson)) return;

            List<VectorChunk> chunks = await _Sdk.VectorSearch(
                _Serializer.DeserializeJson<VectorSearchRequest>(requestJson)
                );

            EnumerateResponse(chunks);
        }

        private static async Task FindEmbeddings()
        {
            Console.WriteLine("");
            Console.WriteLine("Example find:");
            Console.WriteLine(_Serializer.SerializeJson(new FindEmbeddingsRequest
            { 
                TenantGUID = default(Guid),
                VectorRepositoryGUID = default(Guid),
                Criteria = new List<FindEmbeddingsObject>
                {
                    new FindEmbeddingsObject
                    {
                        SHA256Hash = "000"
                    },
                    new FindEmbeddingsObject
                    {
                        SHA256Hash = "111"
                    },
                    new FindEmbeddingsObject
                    {
                        SHA256Hash = "222"
                    },
                }
            }, false));
            Console.WriteLine("");

            string requestJson = Inputty.GetString("Request JSON           :", null, true);
            if (String.IsNullOrEmpty(requestJson)) return;

            FindEmbeddingsResult result = await _Sdk.FindEmbeddings(
                _Serializer.DeserializeJson<FindEmbeddingsRequest>(requestJson)
                );

            EnumerateResponse(result);
        }

        private static async Task RawQuery()
        {
            Console.WriteLine("");
            Console.WriteLine("Example request:");
            Console.WriteLine(_Serializer.SerializeJson(new VectorQueryRequest
            {
                Query = "SELECT * FROM public.minilm;",
                VectorRepositoryGUID = default(Guid)
            }, false));
            Console.WriteLine("");

            string requestJson = Inputty.GetString("Request JSON           :", null, true);
            if (String.IsNullOrEmpty(requestJson)) return;

            string ret = await _Sdk.VectorQuery(
                _Serializer.DeserializeJson<VectorQueryRequest>(requestJson)
                );

            Console.WriteLine("");
            Console.WriteLine(ret);
            Console.WriteLine("");
        }

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
}