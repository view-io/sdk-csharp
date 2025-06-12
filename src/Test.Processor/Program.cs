namespace Test.Processor
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using GetSomeInput;
    using View.Sdk;
    using View.Sdk.Processor;
    using View.Sdk.Serialization;

    public static class Program
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

        private static bool _RunForever = true;
        private static Guid _TenantGUID = default(Guid);
        private static string _AccessKey = "default";
        private static string _Endpoint = "http://view.homedns.org:8000/v1.0/tenants/default/processing";
        private static ViewProcessorSdk _Sdk = null;
        private static Serializer _Serializer = new Serializer();
        private static bool _EnableLogging = true;

        public static void Main(string[] args)
        {
            _TenantGUID =   Inputty.GetGuid("Tenant GUID :", _TenantGUID);
            _AccessKey  = Inputty.GetString("Access key  :", _AccessKey, false);
            _Endpoint   = Inputty.GetString("Endpoint    :", _Endpoint, false);

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

                    case "process":
                        ProcessDocument().Wait();
                        break;
                        
                    case "enumerate":
                        EnumerateProcessorTasks().Wait();
                        break;
                        
                    case "retrieve":
                        RetrieveProcessorTask().Wait();
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
            Console.WriteLine("  process       Process a document request");
            Console.WriteLine("  enumerate     Enumerate processor tasks");
            Console.WriteLine("  retrieve      Retrieve a processor task by GUID");
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

        private static async Task ProcessDocument()
        {
            string file = Inputty.GetString("Filename:", "./SampleRequest.json", false);

            ProcessorRequest req = _Serializer.DeserializeJson<ProcessorRequest>(File.ReadAllText(file));

            ProcessorResult resp = await _Sdk.Process(
                Guid.NewGuid(),
                req.MetadataRuleGUID,
                req.EmbeddingsRuleGUID,
                req.Object);

            EnumerateResponse(resp);
        }

        private static async Task EnumerateProcessorTasks()
        {
            int maxKeys = Inputty.GetInteger("Max keys to return:", 3, true, true);          
            EnumerateResponse(await _Sdk.Enumerate(maxKeys));
        }

        private static async Task RetrieveProcessorTask()
        {
            Guid taskGuid = Inputty.GetGuid("Processor task GUID:", default(Guid));
            EnumerateResponse(await _Sdk.Retrieve(taskGuid));
        }

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
}