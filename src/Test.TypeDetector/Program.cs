namespace Test.TypeDetector
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
        private static string _Endpoint = "http://localhost:8000/";
        private static ViewTypeDetectorSdk _Sdk = null;
        private static Serializer _Serializer = new Serializer();
        private static bool _EnableLogging = true;

        public static void Main(string[] args)
        {
            _TenantGUID =   Inputty.GetGuid("Tenant GUID :", _TenantGUID);
            _AccessKey  = Inputty.GetString("Access key  :", _AccessKey, false);
            _Endpoint   = Inputty.GetString("Endpoint    :", _Endpoint, false);

            _Sdk = new ViewTypeDetectorSdk(_TenantGUID, _AccessKey, _Endpoint);
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
                        Process().Wait();
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
            Console.WriteLine("  process       Process a type detection request");
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

        private static async Task Process()
        {
            string file = Inputty.GetString("Filename:", null, true);
            
            if (!String.IsNullOrEmpty(file))
            {
                string contentType = Inputty.GetString("Content type:", null, true);

                TypeResult tr = await _Sdk.Process(File.ReadAllBytes(file), contentType);
                EnumerateResponse(tr);
            }
        }

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
}