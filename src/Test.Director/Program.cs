namespace Test.Director
{
    using GetSomeInput;
    using System;
    using System.Threading.Tasks;
    using View.Sdk;
    using View.Sdk.Director;
    using View.Sdk.Serialization;

    public static class Program
    {
        private static bool _RunForever = true;
        private static Guid _TenantGUID = default(Guid);
        private static string _AccessKey = "default";
        private static string _Endpoint = "http://localhost:8000";
        private static ViewDirectorSdk _Sdk = null;
        private static Serializer _Serializer = new Serializer();
        private static bool _EnableLogging = true;

        public static void Main(string[] args)
        {
            _TenantGUID = Inputty.GetGuid("Tenant GUID :", _TenantGUID);
            _AccessKey = Inputty.GetString("Access key  :", _AccessKey, false);
            _Endpoint = Inputty.GetString("Endpoint    :", _Endpoint, false);
            _Sdk = new ViewDirectorSdk(_TenantGUID, _AccessKey, _Endpoint);
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
                    case "list":
                        ListConnections().Wait();
                        break;
                    case "embed":
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
            Console.WriteLine("  list          List connections");
            Console.WriteLine("  embed         Generate embeddings");
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

        private static T BuildObject<T>()
        {
            string json = Inputty.GetString("JSON :", null, false);
            return _Serializer.DeserializeJson<T>(json);
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

        private static async Task ListConnections()
        {
            _Sdk.XToken = Inputty.GetString("XToken    :", null, false);
            object result = await _Sdk.ConnectionList();
            EnumerateResponse(result);
        }

        private static async Task GenerateEmbeddings()
        {
            DirectorEmbeddingsRequest request = BuildObject<DirectorEmbeddingsRequest>();
            EnumerateResponse(await _Sdk.GenerateEmbeddings(request));
        }
    }
}