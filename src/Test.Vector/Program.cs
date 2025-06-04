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
                    case "exists doc":
                        DocumentExists().Wait();
                        break;
                    case "del filter":
                        DeleteDocumentsByFilter().Wait();
                        break;
                    case "repo stats":
                        GetRepositoryStatistics().Wait();
                        break;
                    case "read cells":
                        ReadSemanticCells().Wait();
                        break;
                    case "read cell":
                        ReadSemanticCell().Wait();
                        break;
                    case "read chunks":
                        ReadSemanticChunks().Wait();
                        break;
                    case "read chunk":
                        ReadSemanticChunk().Wait();
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
            Console.WriteLine("  exists doc    Check if document exists");
            Console.WriteLine("  del filter    Delete documents by filter");
            Console.WriteLine("  truncate      Truncate table");
            Console.WriteLine("");
            Console.WriteLine("  repo stats    Get repository statistics");
            Console.WriteLine("");
            Console.WriteLine("  read cells    Read semantic cells for a document");
            Console.WriteLine("  read cell     Read a specific semantic cell");
            Console.WriteLine("  read chunks   Read semantic chunks for a cell");
            Console.WriteLine("  read chunk    Read a specific semantic chunk");
            Console.WriteLine("");
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

            EmbeddingsDocument doc = await _Sdk.Document.Write(
                _Serializer.DeserializeJson<EmbeddingsDocument>(requestJson)
                );

            EnumerateResponse(doc);
        }

        private static async Task DeleteDocument()
        {
            Guid repoGuid = Inputty.GetGuid("Repository GUID:", default(Guid));
            Guid docGuid = Inputty.GetGuid("Document GUID  :", default(Guid));
            bool success = await _Sdk.Document.Delete(repoGuid, docGuid);
            Console.WriteLine("Success: " + success);
        }

        private static async Task TruncateTable()
        {
            Guid repoGuid = Inputty.GetGuid("Repository GUID:", default(Guid));

            bool success = await _Sdk.Repository.Truncate(repoGuid);
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

            List<VectorChunk> chunks = await _Sdk.Vector.Search(
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

            FindEmbeddingsResult result = await _Sdk.Vector.Find(
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

            string ret = await _Sdk.Vector.Query(
                _Serializer.DeserializeJson<VectorQueryRequest>(requestJson)
                );

            Console.WriteLine("");
            Console.WriteLine(ret);
            Console.WriteLine("");
        }

        private static async Task DocumentExists()
        {
            Guid repoGuid = Inputty.GetGuid("Repository GUID:", default(Guid));
            Guid docGuid = Inputty.GetGuid("Document GUID  :", default(Guid));
            
            bool exists = await _Sdk.Document.Exists(repoGuid, docGuid);
            Console.WriteLine("");
            Console.WriteLine($"Document exists: {exists}");
            Console.WriteLine("");
        }

        private static async Task DeleteDocumentsByFilter()
        {
            Console.WriteLine("");
            Console.WriteLine("Example delete request:");
            Console.WriteLine(_Serializer.SerializeJson(new VectorDeleteRequest
            {
                ObjectKey = "test.json",
                ObjectVersion = "1"
            }, false));
            Console.WriteLine("");

            Guid repoGuid = Inputty.GetGuid("Repository GUID:", default(Guid));
            string requestJson = Inputty.GetString("Delete request JSON :", null, true);
            if (String.IsNullOrEmpty(requestJson)) return;

            bool success = await _Sdk.Document.DeleteByFilter(
                repoGuid,
                _Serializer.DeserializeJson<VectorDeleteRequest>(requestJson)
                );

            Console.WriteLine("");
            Console.WriteLine($"Delete by filter success: {success}");
            Console.WriteLine("");
        }

        private static async Task GetRepositoryStatistics()
        {
            Guid repoGuid = Inputty.GetGuid("Repository GUID:", default(Guid));
            
            VectorRepositoryStatistics stats = await _Sdk.Repository.GetStatistics(repoGuid);
            EnumerateResponse(stats);
        }

        private static async Task ReadSemanticCells()
        {
            Guid repoGuid = Inputty.GetGuid("Repository GUID:", default(Guid));
            Guid docGuid = Inputty.GetGuid("Document GUID  :", default(Guid));
            
            List<SemanticCell> cells = await _Sdk.SemanticCell.ReadMany(repoGuid, docGuid);
            EnumerateResponse(cells);
        }

        private static async Task ReadSemanticCell()
        {
            Guid repoGuid = Inputty.GetGuid("Repository GUID:", default(Guid));
            Guid docGuid = Inputty.GetGuid("Document GUID  :", default(Guid));
            Guid cellGuid = Inputty.GetGuid("Cell GUID     :", default(Guid));
            
            SemanticCell cell = await _Sdk.SemanticCell.Read(repoGuid, docGuid, cellGuid);
            EnumerateResponse(cell);
        }

        private static async Task ReadSemanticChunks()
        {
            Guid repoGuid = Inputty.GetGuid("Repository GUID:", default(Guid));
            Guid docGuid = Inputty.GetGuid("Document GUID  :", default(Guid));
            Guid cellGuid = Inputty.GetGuid("Cell GUID     :", default(Guid));
            
            List<SemanticChunk> chunks = await _Sdk.SemanticChunk.ReadMany(repoGuid, docGuid, cellGuid);
            EnumerateResponse(chunks);
        }

        private static async Task ReadSemanticChunk()
        {
            Guid repoGuid = Inputty.GetGuid("Repository GUID:", default(Guid));
            Guid docGuid = Inputty.GetGuid("Document GUID  :", default(Guid));
            Guid cellGuid = Inputty.GetGuid("Cell GUID     :", default(Guid));
            Guid chunkGuid = Inputty.GetGuid("Chunk GUID    :", default(Guid));
            
            SemanticChunk chunk = await _Sdk.SemanticChunk.Read(repoGuid, docGuid, cellGuid, chunkGuid);
            EnumerateResponse(chunk);
        }

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
}