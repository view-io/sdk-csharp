namespace Test.Orchestrator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using GetSomeInput;
    using View.Sdk;
    using View.Sdk.Orchestrator;
    using View.Serializer;

    public static class Program
    {
        private static bool _RunForever = true;
        private static string _TenantGuid = "default";
        private static string _Endpoint = "http://localhost:8501/";
        private static string _AccessKey = "default";
        private static ViewOrchestratorSdk _Sdk = null;
        private static SerializationHelper _Serializer = new SerializationHelper();
        private static bool _EnableLogging = true;

        public static void Main(string[] args)
        {
            _TenantGuid     = Inputty.GetString("Tenant GUID :", _TenantGuid, false);
            _Endpoint       = Inputty.GetString("Endpoint    :", _Endpoint, false);
            _AccessKey      = Inputty.GetString("Access key  :", _AccessKey, false);

            _Sdk = new ViewOrchestratorSdk(_TenantGuid, _AccessKey, _Endpoint);
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

                    case "write trigger":
                        WriteTrigger().Wait();
                        break;
                    case "update trigger":
                        UpdateTrigger().Wait();
                        break;
                    case "read triggers":
                        ReadTriggers().Wait();
                        break;
                    case "read trigger":
                        ReadTrigger().Wait();
                        break;
                    case "delete trigger":
                        DeleteTrigger().Wait();
                        break;
                    case "exists trigger":
                        ExistsTrigger().Wait();
                        break;

                    case "write step":
                        WriteStep().Wait();
                        break;
                    case "read steps":
                        ReadSteps().Wait();
                        break;
                    case "read step":
                        ReadStep().Wait();
                        break;
                    case "delete step":
                        DeleteStep().Wait();
                        break;
                    case "exists step":
                        ExistsStep().Wait();
                        break;

                    case "write flow":
                        WriteFlow().Wait();
                        break;
                    case "read flows":
                        ReadFlows().Wait();
                        break;
                    case "read flow":
                        ReadFlow().Wait();
                        break;
                    case "read flow logs":
                        ReadFlowLogs().Wait();
                        break;
                    case "delete flow":
                        DeleteFlow().Wait();
                        break;
                    case "exists flow":
                        ExistsFlow().Wait();
                        break;
                }
            }
        }

        private static void Menu()
        {
            Console.WriteLine("");
            Console.WriteLine("Available commands:");
            Console.WriteLine("  q                Quit this program");
            Console.WriteLine("  ?                Help, this menu");
            Console.WriteLine("  cls              Clear the screen");
            Console.WriteLine("  conn             Test connectivity");
            Console.WriteLine("");
            Console.WriteLine("  write trigger     Create a trigger");
            Console.WriteLine("  update trigger    Update a trigger");
            Console.WriteLine("  read triggers     Read all trigger");
            Console.WriteLine("  read trigger      Read a trigger");
            Console.WriteLine("  delete trigger    Delete a trigger");
            Console.WriteLine("  exists trigger    Check if a trigger exists");
            Console.WriteLine("");
            Console.WriteLine("  write step        Create a step");
            Console.WriteLine("  read steps        Read all steps");
            Console.WriteLine("  read step         Read a step");
            Console.WriteLine("  delete step       Delete a step");
            Console.WriteLine("  exists step       Check if a step exists");
            Console.WriteLine("");
            Console.WriteLine("  write flow        Create a flow");
            Console.WriteLine("  read flows        Read all flows");
            Console.WriteLine("  read flow         Read a flow");
            Console.WriteLine("  read flow logs    Read flow logs");
            Console.WriteLine("  delete flow       Delete a flow");
            Console.WriteLine("  exists flow       Check if a flow exists");
            Console.WriteLine("");
        }

        private static void EmitLogMessage(Severity sev, string msg)
        {
            if (!String.IsNullOrEmpty(msg)) Console.WriteLine(sev.ToString() + " " + msg);
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

        private static string GetGuid(string prompt)
        {
            return Inputty.GetString(prompt, null, false);
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

        #region Triggers

        private static async Task WriteTrigger()
        {
            Trigger obj = BuildObject<Trigger>();
            EnumerateResponse(await _Sdk.CreateTrigger(obj));
        }

        private static async Task UpdateTrigger()
        {
            Trigger obj = BuildObject<Trigger>();
            EnumerateResponse(await _Sdk.UpdateTrigger(obj));
        }

        private static async Task ReadTriggers()
        {
            EnumerateResponse(await _Sdk.RetrieveTriggers());
        }

        private static async Task ReadTrigger()
        {
            string guid = GetGuid("GUID:");
            EnumerateResponse(await _Sdk.RetrieveTrigger(guid));
        }

        private static async Task DeleteTrigger()
        {
            string guid = GetGuid("GUID:");
            await _Sdk.DeleteTrigger(guid);
        }

        private static async Task ExistsTrigger()
        {
            string guid = GetGuid("GUID:");
            bool exists = await _Sdk.ExistsTrigger(guid);
            Console.WriteLine("Exists: " + exists);
        }

        #endregion

        #region Steps

        private static async Task WriteStep()
        {
            StepMetadata obj = BuildObject<StepMetadata>();
            EnumerateResponse(await _Sdk.CreateStep(obj));
        }

        private static async Task ReadSteps()
        {
            EnumerateResponse(await _Sdk.RetrieveSteps());
        }

        private static async Task ReadStep()
        {
            string guid = GetGuid("GUID:");
            EnumerateResponse(await _Sdk.RetrieveStep(guid));
        }

        private static async Task DeleteStep()
        {
            string guid = GetGuid("GUID:");
            await _Sdk.DeleteStep(guid);
        }

        private static async Task ExistsStep()
        {
            string guid = GetGuid("GUID:");
            bool exists = await _Sdk.ExistsStep(guid);
            Console.WriteLine("Exists: " + exists);
        }

        #endregion

        #region Flows

        private static async Task WriteFlow()
        {
            DataFlow obj = BuildObject<DataFlow>();
            EnumerateResponse(await _Sdk.CreateDataFlow(obj));
        }

        private static async Task ReadFlows()
        {
            EnumerateResponse(await _Sdk.RetrieveDataFlows());
        }

        private static async Task ReadFlow()
        {
            string guid = GetGuid("GUID:");
            EnumerateResponse(await _Sdk.RetrieveDataFlow(guid));
        }

        private static async Task ReadFlowLogs()
        {
            string flowGuid = GetGuid("Data flow GUID:");
            string reqGuid  = GetGuid("Request GUID  :");
            EnumerateResponse(await _Sdk.RetrieveDataFlowLogs(flowGuid, reqGuid));
        }

        private static async Task DeleteFlow()
        {
            string guid = GetGuid("GUID:");
            await _Sdk.DeleteDataFlow(guid);
        }

        private static async Task ExistsFlow()
        {
            string guid = GetGuid("GUID:");
            bool exists = await _Sdk.ExistsDataFlow(guid);
            Console.WriteLine("Exists: " + exists);
        }

        #endregion
    }
}