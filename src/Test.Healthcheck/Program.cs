namespace Test.Healthcheck
{
    using System;
    using System.Threading.Tasks;
    using GetSomeInput;
    using View.Sdk;
    using View.Sdk.Healthcheck;
    using View.Sdk.Serialization;

    public static class Program
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        private static bool _RunForever = true;
        private static Guid _TenantGuid = default(Guid);
        private static string _Endpoint = "http://view.homedns.org:8000/";
        private static string _AccessKey = "default";
        private static ViewHealthcheckSdk _Sdk = null;
        private static Serializer _Serializer = new Serializer();
        private static bool _EnableLogging = true;

        public static void Main(string[] args)
        {
            _TenantGuid = Inputty.GetGuid("Tenant GUID :", _TenantGuid);
            _Endpoint = Inputty.GetString("Endpoint    :", _Endpoint, false);
            _AccessKey = Inputty.GetString("Access key  :", _AccessKey, false);
            _Sdk = new ViewHealthcheckSdk(_TenantGuid, _AccessKey, _Endpoint);
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
                    case "orchestrator":
                        TestOrchestratorExists().Wait();
                        break;
                    case "crawler":
                        TestCrawlerExists().Wait();
                        break;
                    case "lexi":
                        TestLexiExists().Wait();
                        break;
                    case "embedding":
                        TestEmbeddingExists().Wait();
                        break;
                    case "director":
                        TestDirectorExists().Wait();
                        break;
                    case "assistant":
                        TestAssistantExists().Wait();
                        break;
                    case "config":
                        TestConfigExists().Wait();
                        break;
                    case "vector":
                        TestVectorExists().Wait();
                        break;
                    case "processor":
                        TestProcessorExists().Wait();
                        break;
                    case "storage":
                        TestStorageExists().Wait();
                        break;
                    case "switchboard":
                        TestSwitchboardExists().Wait();
                        break;
                    case "all":
                        TestAllServices().Wait();
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
            Console.WriteLine("  orchestrator  Test Orchestrator service");
            Console.WriteLine("  crawler       Test Crawler service");
            Console.WriteLine("  lexi          Test Lexi service");
            Console.WriteLine("  embedding     Test Embedding service");
            Console.WriteLine("  director      Test Director service");
            Console.WriteLine("  assistant     Test Assistant service");
            Console.WriteLine("  config        Test Config service");
            Console.WriteLine("  vector        Test Vector service");
            Console.WriteLine("  processor     Test Processor service");
            Console.WriteLine("  storage       Test Storage service");
            Console.WriteLine("  switchboard   Test Switchboard service");
            Console.WriteLine("  all           Test all services");
            Console.WriteLine("");
        }

        private static async Task TestConnectivity()
        {
            Console.WriteLine("");
            Console.Write("Connectivity: ");

            if (await _Sdk.ValidateConnectivity())
                Console.WriteLine("Connected");
            else
                Console.WriteLine("Not connected");

            Console.WriteLine("");
        }

        private static async Task TestOrchestratorExists()
        {
            Console.WriteLine("");
            Console.Write("Orchestrator service: ");

            if (await _Sdk.Orchestrator.Exists())
                Console.WriteLine("Exists and accessible");
            else
                Console.WriteLine("Does not exist or not accessible");

            Console.WriteLine("");
        }

        private static async Task TestCrawlerExists()
        {
            Console.WriteLine("");
            Console.Write("Crawler service: ");

            if (await _Sdk.Crawler.Exists())
                Console.WriteLine("Exists and accessible");
            else
                Console.WriteLine("Does not exist or not accessible");

            Console.WriteLine("");
        }

        private static async Task TestLexiExists()
        {
            Console.WriteLine("");
            Console.Write("Lexi service: ");

            if (await _Sdk.Lexi.Exists())
                Console.WriteLine("Exists and accessible");
            else
                Console.WriteLine("Does not exist or not accessible");

            Console.WriteLine("");
        }

        private static async Task TestEmbeddingExists()
        {
            Console.WriteLine("");
            Console.Write("Embedding service: ");

            if (await _Sdk.Embedding.Exists())
                Console.WriteLine("Exists and accessible");
            else
                Console.WriteLine("Does not exist or not accessible");

            Console.WriteLine("");
        }

        private static async Task TestDirectorExists()
        {
            Console.WriteLine("");
            Console.Write("Director service: ");

            if (await _Sdk.Director.Exists())
                Console.WriteLine("Exists and accessible");
            else
                Console.WriteLine("Does not exist or not accessible");

            Console.WriteLine("");
        }

        private static async Task TestAssistantExists()
        {
            Console.WriteLine("");
            Console.Write("Assistant service: ");

            if (await _Sdk.Assistant.Exists())
                Console.WriteLine("Exists and accessible");
            else
                Console.WriteLine("Does not exist or not accessible");

            Console.WriteLine("");
        }

        private static async Task TestConfigExists()
        {
            Console.WriteLine("");
            Console.Write("Config service: ");

            if (await _Sdk.Config.Exists())
                Console.WriteLine("Exists and accessible");
            else
                Console.WriteLine("Does not exist or not accessible");

            Console.WriteLine("");
        }

        private static async Task TestVectorExists()
        {
            Console.WriteLine("");
            Console.Write("Vector service: ");

            if (await _Sdk.Vector.Exists())
                Console.WriteLine("Exists and accessible");
            else
                Console.WriteLine("Does not exist or not accessible");

            Console.WriteLine("");
        }

        private static async Task TestProcessorExists()
        {
            Console.WriteLine("");
            Console.Write("Processor service: ");

            if (await _Sdk.Processor.Exists())
                Console.WriteLine("Exists and accessible");
            else
                Console.WriteLine("Does not exist or not accessible");

            Console.WriteLine("");
        }

        private static async Task TestStorageExists()
        {
            Console.WriteLine("");
            Console.Write("Storage service: ");

            if (await _Sdk.Storage.Exists())
                Console.WriteLine("Exists and accessible");
            else
                Console.WriteLine("Does not exist or not accessible");

            Console.WriteLine("");
        }

        private static async Task TestSwitchboardExists()
        {
            Console.WriteLine("");
            Console.Write("Switchboard service: ");

            if (await _Sdk.Switchboard.Exists())
                Console.WriteLine("Exists and accessible");
            else
                Console.WriteLine("Does not exist or not accessible");


            Console.WriteLine("");
        }

        private static async Task TestAllServices()
        {
            Console.WriteLine("");
            Console.WriteLine("Testing all services:");
            Console.WriteLine("");


            Console.Write("Orchestrator: ");
            if (await _Sdk.Orchestrator.Exists())
                Console.WriteLine("Exists and accessible");
            else
                Console.WriteLine("Does not exist or not accessible");
            Console.WriteLine("");

            Console.Write("Crawler: ");
            if (await _Sdk.Crawler.Exists())
                Console.WriteLine("Exists and accessible");
            else
                Console.WriteLine("Does not exist or not accessible");
            Console.WriteLine("");

            Console.Write("Lexi: ");
            if (await _Sdk.Lexi.Exists())
                Console.WriteLine("Exists and accessible");
            else
                Console.WriteLine("Does not exist or not accessible");
            Console.WriteLine("");

            Console.Write("Embedding: ");
            if (await _Sdk.Embedding.Exists())
                Console.WriteLine("Exists and accessible");
            else
                Console.WriteLine("Does not exist or not accessible");
            Console.WriteLine("");

            Console.Write("Director: ");
            if (await _Sdk.Director.Exists())
                Console.WriteLine("Exists and accessible");
            else
                Console.WriteLine("Does not exist or not accessible");
            Console.WriteLine("");

            Console.Write("Assistant: ");
            if (await _Sdk.Assistant.Exists())
                Console.WriteLine("Exists and accessible");
            else
                Console.WriteLine("Does not exist or not accessible");
            Console.WriteLine("");

            Console.Write("Config: ");
            if (await _Sdk.Config.Exists())
                Console.WriteLine("Exists and accessible");
            else
                Console.WriteLine("Does not exist or not accessible");
            Console.WriteLine("");

            Console.Write("Vector: ");
            if (await _Sdk.Vector.Exists())
                Console.WriteLine("Exists and accessible");
            else
                Console.WriteLine("Does not exist or not accessible");
            Console.WriteLine("");

            Console.Write("Processor: ");
            if (await _Sdk.Processor.Exists())
                Console.WriteLine("Exists and accessible");
            else
                Console.WriteLine("Does not exist or not accessible");
            Console.WriteLine("");

            Console.Write("Storage: ");
            if (await _Sdk.Storage.Exists())
                Console.WriteLine("Exists and accessible");
            else
                Console.WriteLine("Does not exist or not accessible");
            Console.WriteLine("");

            Console.Write("Switchboard: ");
            if (await _Sdk.Switchboard.Exists())
                Console.WriteLine("Exists and accessible");
            else
                Console.WriteLine("Does not exist or not accessible");
            Console.WriteLine("");
        }

        private static void EmitLogMessage(SeverityEnum severity, string msg)
        {
            Console.WriteLine(severity.ToString() + ": " + msg);
        }
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
}