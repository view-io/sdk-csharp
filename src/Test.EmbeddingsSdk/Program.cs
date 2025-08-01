﻿namespace Test.EmbeddingsSdk
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using GetSomeInput;
    using View.Sdk;
    using View.Sdk.Serialization;
    using View.Sdk.Semantic;
    using Timestamps;
    using View.Sdk.Embeddings;

    public static class Program
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

        private static bool _RunForever = true;

        private static EmbeddingsGeneratorEnum _GeneratorType = EmbeddingsGeneratorEnum.LCProxy;

        private static Guid _TenantGUID = default(Guid);
        private static string _BaseUrl = null;
        private static string _BaseUrlLcproxy = "http://localhost:8000/";
        private static string _BaseUrlOpenAi = "https://api.openai.com/";
        private static string _BaseUrlVoyageAi = "https://api.voyageai.com/";
        private static string _BaseUrlOllama = "http://localhost:11434/";

        private static string _ApiKey = null;
        private static string _AccessKey = "default";

        private static string _DefaultModel = null;
        private static string _DefaultLcproxyModel = "all-MiniLM-L6-v2";
        private static string _DefaultOpenAiModel = "text-embedding-ada-002";
        private static string _DefaultVoyageAiModel = "voyage-3-large";
        private static string _DefaultOllamaModel = "all-minilm";

        private static int _BatchSize = 2;
        private static int _MaxParallelTasks = 4;
        private static int _MaxRetries = 3;
        private static int _MaxFailures = 3;
        private static int _TimeoutMs = 300000;

        private static ViewEmbeddingsSdk _Sdk = null;
        private static Serializer _Serializer = new Serializer();

        public static async Task Main(string[] args)
        {
            _TenantGUID = Inputty.GetGuid("Tenant GUID:", _TenantGUID);

            _GeneratorType = (EmbeddingsGeneratorEnum)(Enum.Parse(
                typeof(EmbeddingsGeneratorEnum),
                Inputty.GetString("Generator type [LCProxy/OpenAI/Ollama/VoyageAI]:", "LCProxy", false)));

            if (_GeneratorType == EmbeddingsGeneratorEnum.LCProxy)
            {
                _BaseUrl = Inputty.GetString("Endpoint :", _BaseUrlLcproxy, false);
                _DefaultModel = _DefaultLcproxyModel;
            }
            else if (_GeneratorType == EmbeddingsGeneratorEnum.OpenAI)
            {
                _BaseUrl = Inputty.GetString("Endpoint :", _BaseUrlOpenAi, false);
                _DefaultModel = _DefaultOpenAiModel;
            }
            else if (_GeneratorType == EmbeddingsGeneratorEnum.Ollama)
            {
                _BaseUrl = Inputty.GetString("Endpoint :", _BaseUrlOllama, false);
                _DefaultModel = _DefaultOllamaModel;
            }
            else if (_GeneratorType == EmbeddingsGeneratorEnum.VoyageAI)
            {
                _BaseUrl = Inputty.GetString("Endpoint :", _BaseUrlVoyageAi, false);
                _DefaultModel = _DefaultVoyageAiModel;
            }
            else
            {
                throw new ArgumentException("Unknown embeddings generator '" + _GeneratorType.ToString() + "'.");
            }

            _ApiKey = Inputty.GetString("API key  :", _ApiKey, true);

            _Sdk = new ViewEmbeddingsSdk(
                _TenantGUID,
                _GeneratorType, 
                _BaseUrl, 
                _ApiKey, 
                _BatchSize, 
                _MaxParallelTasks, 
                _MaxRetries, 
                _MaxFailures, 
                _TimeoutMs);

            _Sdk.LogRequests = true;
            _Sdk.LogResponses = true;
            _Sdk.Logger = SdkLogger;

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
                    case "tasks":
                        string tasksStr = Inputty.GetString("Tasks:", null, true);
                        int tasks;
                        if (!String.IsNullOrEmpty(tasksStr))
                            if (Int32.TryParse(tasksStr, out tasks))
                            {
                                _BatchSize = tasks;
                                _Sdk.MaxParallelTasks = tasks;
                            }
                        break;

                    case "cells":
                        await ProcessSemanticCells();
                        break;
                    case "text":
                        await ProcessText();
                        break;
                    case "hash":
                        await FindByHash();
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
            Console.WriteLine("  tasks         Set max parallel tasks (currently " + _Sdk.MaxParallelTasks + ")");
            Console.WriteLine("  cells         Process semantic cells");
            Console.WriteLine("  text          Process from raw text");
            Console.WriteLine("  hash          Find embeddings by SHA256 hash");
            Console.WriteLine("");
        }

        private static void EnumerateResponse(object obj)
        {
            Console.WriteLine("");
            Console.Write("Response:");
            if (obj != null)
                Console.WriteLine(Environment.NewLine + _Serializer.SerializeJson(obj, false));
            else
                Console.WriteLine("(null)");
            Console.WriteLine("");
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

        private static async Task ProcessSemanticCells()
        {
            string model = Inputty.GetString("Model :", _DefaultModel, true);
            if (String.IsNullOrEmpty(model)) return;

            string file = Inputty.GetString ("File  :", "sample.json", true);
            if (String.IsNullOrEmpty(file)) return;
            if (!File.Exists(file)) return;

            List<SemanticCell> cells = _Serializer.DeserializeJson<List<SemanticCell>>(File.ReadAllText(file));
            GenerateEmbeddingsResult result;
            double? totalMs = 0;

            GenerateEmbeddingsRequest req = new GenerateEmbeddingsRequest
            {
                EmbeddingsRule = new EmbeddingsRule
                {
                    EmbeddingsGenerator = _GeneratorType,
                    EmbeddingsGeneratorUrl = _BaseUrl,
                    EmbeddingsGeneratorApiKey = _ApiKey
                },
                SemanticCells = cells,
                Model = model
            };

            using (Timestamp ts = new Timestamp())
            {
                ts.Start = DateTime.UtcNow;

                result = await _Sdk.GenerateEmbeddings(req);

                ts.End = DateTime.UtcNow;
                totalMs = ts.TotalMs;
            }

            EnumerateResponse(result);
            Console.WriteLine("");
            Console.WriteLine("Completed after " + totalMs + "ms");
            Console.WriteLine("");
        }

        private static async Task ProcessText()
        {
            string model = Inputty.GetString("Model :", _DefaultModel, true);
            if (String.IsNullOrEmpty(model)) return;

            List<string> contents = new List<string>();
            Console.WriteLine("");
            Console.WriteLine("Type the text you wish to process.  Press ENTER on an empty line to end.");
            while (true)
            {
                string content = Inputty.GetString("Text:", null, true);
                if (String.IsNullOrEmpty(content)) break;
                contents.Add(content);
            }

            if (contents.Count > 0)
            {
                GenerateEmbeddingsResult result;
                double? totalMs = 0;

                using (Timestamp ts = new Timestamp())
                {
                    ts.Start = DateTime.UtcNow;

                    result = await _Sdk.GenerateEmbeddings(new GenerateEmbeddingsRequest
                    {
                        EmbeddingsRule = new EmbeddingsRule
                        {
                            EmbeddingsGenerator = _GeneratorType,
                            EmbeddingsGeneratorUrl = _BaseUrl,
                            EmbeddingsGeneratorApiKey = _ApiKey
                        },
                        Model = model,
                        Contents = contents
                    });

                    ts.End = DateTime.UtcNow;
                    totalMs = ts.TotalMs;
                }

                EnumerateResponse(result);
                Console.WriteLine("");
                Console.WriteLine("Completed after " + totalMs + "ms");
                Console.WriteLine("");
            }
        }

        private static void SdkLogger(SeverityEnum sev, string msg)
        {
            if (!String.IsNullOrEmpty(msg)) Console.WriteLine(sev.ToString() + " " + msg);
        }

        private static async Task FindByHash()
        {
            // Create a ViewEmbeddingsServerSdk instance
            ViewEmbeddingsServerSdk serverSdk = new ViewEmbeddingsServerSdk(
                _TenantGUID,
                _BaseUrl,
                _AccessKey);

            FindEmbeddingsRequest request = BuildObject<FindEmbeddingsRequest>();
            EnumerateResponse(await serverSdk.FindByHash(request));
        }

        private static T BuildObject<T>()
        {
            string json = Inputty.GetString("JSON :", null, false);
            return _Serializer.DeserializeJson<T>(json);
        }

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
}