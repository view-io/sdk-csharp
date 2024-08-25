namespace Test.EmbeddingsSdk
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using GetSomeInput;
    using View.Sdk;
    using View.Sdk.Vector;
    using View.Sdk.Serialization;

    public static class Program
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

        private static bool _RunForever = true;

        private static EmbeddingsGeneratorEnum _GeneratorType = EmbeddingsGeneratorEnum.LCProxy;

        private static string _Endpoint = null;
        private static string _EndpointLcproxy = "http://localhost:8301/";
        private static string _EndpointOpenAi = "https://api.openai.com/v1/";
        private static string _EndpointVoyageAi = "https://api.voyageai.com/v1/";
        private static string _EndpointOllama = "http://localhost:7869/";

        private static string _ApiKey = null;

        private static string _DefaultModel = null;
        private static string _DefaultLcproxyModel = "all-MiniLM-L6-v2";
        private static string _DefaultOpenAiModel = "text-embedding-ada-002";
        private static string _DefaultVoyageAiModel = "voyage-large-2-instruct";
        private static string _DefaultOllamaModel = "all-minilm";

        private static int _MaxTasks = 16;
        private static int _TimeoutMs = 300000;

        private static ViewEmbeddingsSdk _Sdk = null;
        private static Serializer _Serializer = new Serializer();

        public static void Main(string[] args)
        {
            _GeneratorType = (EmbeddingsGeneratorEnum)(Enum.Parse(
                typeof(EmbeddingsGeneratorEnum),
                Inputty.GetString("Generator type [LCProxy/OpenAI/Ollama/VoyageAI]:", "LCProxy", false)));

            if (_GeneratorType == EmbeddingsGeneratorEnum.LCProxy)
            {
                _Endpoint = Inputty.GetString("Endpoint :", _EndpointLcproxy, false);
                _DefaultModel = _DefaultLcproxyModel;
            }
            else if (_GeneratorType == EmbeddingsGeneratorEnum.OpenAI)
            {
                _Endpoint = Inputty.GetString("Endpoint :", _EndpointOpenAi, false);
                _DefaultModel = _DefaultOpenAiModel;
            }
            else if (_GeneratorType == EmbeddingsGeneratorEnum.Ollama)
            {
                _Endpoint = Inputty.GetString("Endpoint :", _EndpointOllama, false);
                _DefaultModel = _DefaultOllamaModel;
            }
            else if (_GeneratorType == EmbeddingsGeneratorEnum.VoyageAI)
            {
                _Endpoint = Inputty.GetString("Endpoint :", _EndpointVoyageAi, false);
                _DefaultModel = _DefaultVoyageAiModel;
            }
            else
            {
                throw new ArgumentException("Unknown embeddings generator '" + _GeneratorType.ToString() + "'.");
            }

            _ApiKey = Inputty.GetString("API key  :", _ApiKey, true);

            _Sdk = new ViewEmbeddingsSdk(_GeneratorType, _Endpoint, _ApiKey, _MaxTasks);

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
                                _MaxTasks = tasks;
                                _Sdk.MaxParallelTasks = tasks;
                            }
                        break;

                    case "embeddings":
                        GenerateEmbeddings().Wait();
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
            Console.WriteLine("  embeddings    Generate embeddings");
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

        private static async Task GenerateEmbeddings()
        {
            string model = Inputty.GetString("Model :", _DefaultModel, true);
            if (String.IsNullOrEmpty(model)) return;

            string file = Inputty.GetString ("File  :", "sample.json", true);
            if (String.IsNullOrEmpty(file)) return;
            if (!File.Exists(file)) return;

            List<SemanticCell> cells = _Serializer.DeserializeJson<List<SemanticCell>>(File.ReadAllText(file));

            List<SemanticCell> result = await _Sdk.ProcessSemanticCells(cells, model, _TimeoutMs);
            EnumerateResponse(result);
        }

        private static void EmitLogMessage(SeverityEnum sev, string msg)
        {
            if (!String.IsNullOrEmpty(msg)) Console.WriteLine(sev.ToString() + " " + msg);
        }

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
}