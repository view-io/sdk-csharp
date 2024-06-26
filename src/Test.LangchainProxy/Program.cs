namespace Test.LangchainProxy
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using GetSomeInput;
    using View.Sdk;
    using View.Sdk.Langchain;
    using View.Sdk.Shared.Embeddings;
    using View.Sdk.Shared.Udr;
    using View.Serializer;
    
    public static class Program
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

        private static bool _RunForever = true;
        private static string _Endpoint = "http://localhost:8301/";
        private static string _ApiKey = null;
        private static ViewLangchainProxySdk _Sdk = null;
        private static SerializationHelper _Serializer = new SerializationHelper();
        private static bool _EnableLogging = true;

        public static void Main(string[] args)
        {
            _Endpoint = Inputty.GetString("Endpoint :", _Endpoint, false);
            _ApiKey   = Inputty.GetString("API key  :", _ApiKey, true);

            _Sdk = new ViewLangchainProxySdk(_Endpoint, _ApiKey);
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

                    case "preload":
                        PreloadModels().Wait();
                        break;
                    case "embeddings":
                        GenerateEmbeddings().Wait();
                        break;                }
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
            Console.WriteLine("  preload       Preload models");
            Console.WriteLine("  embeddings    Generate embeddings");
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

        private static async Task PreloadModels()
        {
            List<string> models = Inputty.GetStringList("Model name:", false);
            if (models == null || models.Count < 1) return;

            bool success = await _Sdk.PreloadModels(models);
            Console.WriteLine(success);
            Console.WriteLine("");
        }

        private static async Task GenerateEmbeddings()
        {
            string model = Inputty.GetString("Model :", null, true);
            if (String.IsNullOrEmpty(model)) return;

            string text = Inputty.GetString("Text  :", null, true);
            if (String.IsNullOrEmpty(text)) return;

            EmbeddingsResult result = await _Sdk.GenerateEmbeddings(model, text);
            EnumerateResponse(result);
        }

        private static void EmitLogMessage(Severity sev, string msg)
        {
            if (!String.IsNullOrEmpty(msg)) Console.WriteLine(sev.ToString() + " " + msg);
        }

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
}