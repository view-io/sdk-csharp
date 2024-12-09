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
        private static string _TenantGUID = "default";
        private static string _AccessKey = "default";
        private static string _Endpoint = "http://localhost:8000/v1.0/tenants/default/processing";
        private static ViewProcessorSdk _Sdk = null;
        private static Serializer _Serializer = new Serializer();
        private static bool _EnableLogging = true;

        public static void Main(string[] args)
        {
            _TenantGUID = Inputty.GetString("Tenant GUID :", _TenantGUID, false);
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
            Console.WriteLine("  process       Process a document request");
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

            ProcessorResponse resp = await _Sdk.Process(
                req.Tenant,
                req.Collection,
                req.Pool,
                req.Bucket,
                req.Object, 
                req.MetadataRule, 
                req.EmbeddingsRule, 
                req.VectorRepository,
                req.GraphRepository);

            EnumerateResponse(resp);
        }

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
}