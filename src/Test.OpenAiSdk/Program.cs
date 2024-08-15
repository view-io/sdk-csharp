namespace Test.OpenAiSdk
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
        private static string _Endpoint = "https://api.openai.com/v1/";
        private static string _ApiKey = null;
        private static string _DefaultModel = "text-embedding-ada-002";
        private static ViewOpenAiSdk _Sdk = null;
        private static Serializer _Serializer = new Serializer();

        public static void Main(string[] args)
        {
            _Endpoint = Inputty.GetString("Endpoint :", _Endpoint, false);
            _ApiKey = Inputty.GetString("API key  :", _ApiKey, true);

            _Sdk = new ViewOpenAiSdk(_Endpoint, _ApiKey);

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

        private static async Task GenerateEmbeddings()
        {
            string model = Inputty.GetString("Model :", _DefaultModel, true);
            if (String.IsNullOrEmpty(model)) return;

            string text = Inputty.GetString("Text  :", null, true);
            if (String.IsNullOrEmpty(text)) return;

            EmbeddingsResult result = await _Sdk.Generate(model, text);
            EnumerateResponse(result);
        }

        private static void EmitLogMessage(SeverityEnum sev, string msg)
        {
            if (!String.IsNullOrEmpty(msg)) Console.WriteLine(sev.ToString() + " " + msg);
        }

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
}