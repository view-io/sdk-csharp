namespace Test.UdrGenerator
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using GetSomeInput;
    using View.Sdk;
    using View.Sdk.Processor;
    using View.Sdk.Serialization;

    public static class Program
    {
        private static bool _RunForever = true;
        private static Guid _TenantGUID = default(Guid);
        private static string _AccessKey = "default";
        private static string _Endpoint = "http://localhost:8000/v1.0/tenants/default/processing/udr";
        private static ViewUdrGeneratorSdk _Sdk = null;
        private static Serializer _Serializer = new Serializer();
        private static bool _EnableLogging = true;

        public static void Main(string[] args)
        {
            _TenantGUID =   Inputty.GetGuid("Tenant GUID :", _TenantGUID);
            _AccessKey  = Inputty.GetString("Access key  :", _AccessKey, false);
            _Endpoint   = Inputty.GetString("Endpoint    :", _Endpoint, false);
            _Sdk = new ViewUdrGeneratorSdk(_TenantGUID, _AccessKey, _Endpoint);
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
                    case "doc":
                        ProcessDocument().Wait();
                        break; 
                    case "db":
                        ProcessDataTable().Wait();
                        break;
                    case "sqlite":
                        ProcessSqlite().Wait();
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
            Console.WriteLine("  doc           Process a document");
            Console.WriteLine("  db            Process a data table");
            Console.WriteLine("  sqlite        Process a Sqlite file");
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

        private static async Task ProcessDocument()
        {
            string filename    = Inputty.GetString("Filename        :", "sample/json/1.json", false);
            Guid guid          =   Inputty.GetGuid("GUID            :", Guid.NewGuid());
            string key         = Inputty.GetString("Key             :", Path.GetFileName(filename), true);
            string contentType = Inputty.GetString("Content type    :", GuessContentType(key), false);
            string addlData    = Inputty.GetString("Additional data :", null, true);

            UdrDocumentRequest req = new UdrDocumentRequest
            {
                GUID            = guid,
                Key             = key,
                ContentType     = contentType,
                AdditionalData  = addlData
            };

            UdrDocument resp = await _Sdk.ProcessDocument(req, filename);
            EnumerateResponse(resp);
        }
         
        private static async Task ProcessDataTable()
        {
            UdrDataTableRequest req = new UdrDataTableRequest
            {
                GUID           =    Inputty.GetGuid("GUID            :", Guid.NewGuid()),
                DatabaseType   =  Inputty.GetString("Database type   :", "Mysql", false),
                Hostname       =  Inputty.GetString("Hostname        :", "localhost", false),
                Port           = Inputty.GetInteger("Port            :", 3306, true, true),
                Username       =  Inputty.GetString("Username        :", "root", false),
                Password       =  Inputty.GetString("Password        :", "password", false),
                DatabaseName   =  Inputty.GetString("Database name   :", "test", false),
                Query          =  Inputty.GetString("Query           :", "SELECT * FROM customers", false),
                AdditionalData =  Inputty.GetString("Additional data :", null, true)
            };

            UdrDocument resp = await _Sdk.ProcessDataTable(req);
            EnumerateResponse(resp);
        }

        private static async Task ProcessSqlite()
        {
            string filename = Inputty.GetString("Filename        :", "sample/sqlite/1.db", false);
                
            UdrDataTableRequest req = new UdrDataTableRequest
            {
                GUID           =   Inputty.GetGuid("GUID            :", Guid.NewGuid()),
                Query          = Inputty.GetString("Query           :", "SELECT * FROM customers", false),
                AdditionalData = Inputty.GetString("Additional data :", null, true)
            };

            UdrDocument resp = await _Sdk.ProcessDataTable(req, filename);
            EnumerateResponse(resp);
        }

        private static void EnumerateResponse(UdrDocument resp)
        {
            Console.WriteLine("");
            if (resp == null)
            {
                Console.WriteLine("Response: (null)");
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("Response:");
                Console.WriteLine("");
                Console.WriteLine(_Serializer.SerializeJson(resp, true));
                Console.WriteLine("");
            }
        }

        private static string GuessContentType(string filename)
        {
            if (String.IsNullOrEmpty(filename)) return null;

            filename = filename.ToLower();

            if (filename.EndsWith(".csv")) return "text/csv";
            else if (filename.EndsWith(".docx")) return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            else if (filename.EndsWith(".html")) return "text/html";
            else if (filename.EndsWith(".json")) return "application/json";
            else if (filename.EndsWith(".parquet")) return "application/vnd.apache.parquet";
            else if (filename.EndsWith(".pdf")) return "application/pdf";
            else if (filename.EndsWith(".parquet")) return "application/vnd.apache.parquet";
            else if (filename.EndsWith(".pptx")) return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
            else if (filename.EndsWith(".txt")) return "text/plain";
            else if (filename.EndsWith(".xlsx")) return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            else if (filename.EndsWith(".xml")) return "application/xml";

            return "application/octet-stream";
        }

        private static void EmitLogMessage(SeverityEnum sev, string msg)
        {
            if (!String.IsNullOrEmpty(msg)) Console.WriteLine(sev.ToString() + " " + msg);
        }
    }
}