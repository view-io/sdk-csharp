namespace Test.Assistant
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using GetSomeInput;
    using View.Sdk;
    using View.Sdk.Assistant;
    using View.Sdk.Serialization;

    public static class Program
    {
        private static bool _RunForever = true;
        private static string _Endpoint = "http://viewdemo:8331/";
        private static ViewAssistantSdk _Sdk = null;
        private static Serializer _Serializer = new Serializer();
        private static bool _EnableLogging = true;

        private static AssistantRagRequest _RagRequest = new AssistantRagRequest
        {
            OllamaHostname = "alienwarer10",
            OllamaPort = 11434,
            GenerationModel = "llama3.1:latest",
            VectorDatabaseHostname = "pgvector",
            VectorDatabaseName = "vectordb",
            VectorDatabaseUser = "postgres",
            VectorDatabasePassword = "password"
        };

        private static AssistantChatRequest _ChatRequest = new AssistantChatRequest
        {
            OllamaHostname = "alienwarer10",
            OllamaPort = 11434,
            GenerationModel = "llama3.1:latest"
        };

        public static void Main(string[] args)
        {
            _Endpoint = Inputty.GetString("Endpoint:", _Endpoint, false);
            _Sdk = new ViewAssistantSdk(_Endpoint);
            if (_EnableLogging)
            {
                _Sdk.LogRequests = true;
                _Sdk.LogResponses = true;
                _Sdk.Logger = EmitLogMessage;
            }

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
                    case "chat":
                        ProcessChat().Wait();
                        break;
                    case "rag":
                        ProcessRag().Wait();
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
            Console.WriteLine("  chat          Process a chat request");
            Console.WriteLine("  rag           Process a RAG request");
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

        private static async Task ProcessChat()
        {
            string prompt = Inputty.GetString("Prompt:", "Write a simple haiku.", false);

            _ChatRequest.Question = prompt;

            await foreach (string token in _Sdk.ProcessChat(_ChatRequest))
            {
                Console.Write(token);
                Console.Out.Flush();
            }

            Console.WriteLine("");
        }

        private static async Task ProcessRag()
        {
            string prompt = Inputty.GetString("Prompt:", "What information do you have available?", false);

            _RagRequest.Question = prompt;

            await foreach (string token in _Sdk.ProcessRag(_RagRequest))
            {
                Console.Write(token);
            }

            Console.WriteLine("");
        }

        private static void EmitLogMessage(SeverityEnum sev, string msg)
        {
            if (!String.IsNullOrEmpty(msg)) Console.WriteLine(sev.ToString() + " " + msg);
        }
    }
}