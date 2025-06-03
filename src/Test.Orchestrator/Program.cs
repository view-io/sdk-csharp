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
    using View.Sdk.Serialization;

    public static class Program
    {
        private static bool _RunForever = true;
        private static Guid _TenantGuid = default(Guid);
        private static string _Endpoint = "http://localhost:8000/";
        private static string _AccessKey = "default";
        private static ViewOrchestratorSdk _Sdk = null;
        private static Serializer _Serializer = new Serializer();
        private static bool _EnableLogging = true;

        public static void Main(string[] args)
        {
            _TenantGuid =   Inputty.GetGuid("Tenant GUID :", _TenantGuid);
            _Endpoint   = Inputty.GetString("Endpoint    :", _Endpoint, false);
            _AccessKey  = Inputty.GetString("Access key  :", _AccessKey, false);

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

        private static void EmitLogMessage(SeverityEnum sev, string msg)
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

        private static Guid GetGuid(string prompt)
        {
            return Inputty.GetGuid(prompt, default(Guid));
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
            EnumerateResponse(await _Sdk.Trigger.Create(obj));
        }

        private static async Task UpdateTrigger()
        {
            Trigger obj = BuildObject<Trigger>();
            EnumerateResponse(await _Sdk.Trigger.Update(obj));
        }

        private static async Task ReadTriggers()
        {
            EnumerateResponse(await _Sdk.Trigger.RetrieveMany());
        }

        private static async Task ReadTrigger()
        {
            Guid guid = GetGuid("GUID:");
            EnumerateResponse(await _Sdk.Trigger.Retrieve(guid));
        }

        private static async Task DeleteTrigger()
        {
            Guid guid = GetGuid("GUID:");
            await _Sdk.Trigger.Delete(guid);
        }

        private static async Task ExistsTrigger()
        {
            Guid guid = GetGuid("GUID:");
            bool exists = await _Sdk.Trigger.Exists(guid);
            Console.WriteLine("Exists: " + exists);
        }

        #endregion

        #region Steps

        private static async Task WriteStep()
        {
            StepMetadata obj = BuildObject<StepMetadata>();
            EnumerateResponse(await _Sdk.Step.Create(obj));
        }

        private static async Task ReadSteps()
        {
            EnumerateResponse(await _Sdk.Step.RetrieveMany());
        }

        private static async Task ReadStep()
        {
            Guid guid = GetGuid("GUID:");
            EnumerateResponse(await _Sdk.Step.Retrieve(guid));
        }

        private static async Task DeleteStep()
        {
            Guid guid = GetGuid("GUID:");
            await _Sdk.Step.Delete(guid);
        }

        private static async Task ExistsStep()
        {
            Guid guid = GetGuid("GUID:");
            bool exists = await _Sdk.Step.Exists(guid);
            Console.WriteLine("Exists: " + exists);
        }

        #endregion

        #region Flows

        private static async Task WriteFlow()
        {
            DataFlow obj = BuildObject<DataFlow>();
            EnumerateResponse(await _Sdk.DataFlow.Create(obj));
        }

        private static async Task ReadFlows()
        {
            EnumerateResponse(await _Sdk.DataFlow.RetrieveMany());
        }

        private static async Task ReadFlow()
        {
            Guid guid = GetGuid("GUID:");
            EnumerateResponse(await _Sdk.DataFlow.Retrieve(guid));
        }

        private static async Task ReadFlowLogs()
        {
            Guid flowGuid = GetGuid("Data flow GUID:");
            Guid reqGuid  = GetGuid("Request GUID  :");
            EnumerateResponse(await _Sdk.DataFlowLog.Retrieve(flowGuid, reqGuid));
        }

        private static async Task DeleteFlow()
        {
            Guid guid = GetGuid("GUID:");
            await _Sdk.DataFlow.Delete(guid);
        }

        private static async Task ExistsFlow()
        {
            Guid guid = GetGuid("GUID:");
            bool exists = await _Sdk.DataFlow.Exists(guid);
            Console.WriteLine("Exists: " + exists);
        }

        #endregion
    }
}