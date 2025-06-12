namespace Test.Assistant
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using GetSomeInput;
    using View.Sdk;
    using View.Sdk.Assistant;
    using View.Sdk.Serialization;

    public static class Program
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

        private static bool _RunForever = true;
        private static Guid _TenantGuid = default(Guid);
        private static string _Endpoint = "http://view.homedns.org:8000/";
        private static string _AccessKey = "default";
        private static ViewAssistantSdk _Sdk = null;
        private static Serializer _Serializer = new Serializer();
        private static bool _EnableLogging = true;

        public static void Main(string[] args)
        {
            _TenantGuid = Inputty.GetGuid("Tenant GUID :", _TenantGuid);
            _Endpoint = Inputty.GetString("Endpoint:", _Endpoint, false);
            _Sdk = new ViewAssistantSdk(_TenantGuid, _AccessKey, _Endpoint);
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

                    // Connectivity
                    case "conn":
                        TestConnectivity().Wait();
                        break;

                    // Config methods
                    case "create config rag":
                        CreateRagConfig().Wait();
                        break;
                    case "create config chat":
                        CreateChatConfig().Wait();
                        break;
                    case "retrieve config":
                        RetrieveConfig().Wait();
                        break;
                    case "retrieve configs":
                        RetrieveConfigs().Wait();
                        break;
                    case "update config":
                        UpdateConfig().Wait();
                        break;
                    case "delete config":
                        DeleteConfig().Wait();
                        break;
                    case "exists config":
                        ConfigExists().Wait();
                        break;

                    // Chat methods
                    case "process config chat":
                        ProcessConfigChat().Wait();
                        break;
                    case "process rag message":
                        ProcessRagMessage().Wait();
                        break;
                    case "process chat only question":
                        ProcessChatOnlyQuestion().Wait();
                        break;
                    case "process chat only message":
                        ProcessChatOnlyMessage().Wait();
                        break;

                    // Thread methods
                    case "create thread":
                        CreateThread().Wait();
                        break;
                    case "retrieve thread":
                        RetrieveThread().Wait();
                        break;
                    case "retrieve threads":
                        RetrieveThreads().Wait();
                        break;
                    case "delete thread":
                        DeleteThread().Wait();
                        break;
                    case "exists thread":
                        ThreadExists().Wait();
                        break;
                    case "append message":
                        AppendMessage().Wait();
                        break;

                    // Model methods
                    case "retrieve models":
                        RetrieveModels().Wait();
                        break;
                    case "retrieve model":
                        RetrieveModel().Wait();
                        break;
                    case "delete model":
                        DeleteModel().Wait();
                        break;
                    case "preload model":
                        PreloadModel().Wait();
                        break;
                    case "unload model":
                        UnloadModel().Wait();
                        break;

                    default:
                        Console.WriteLine("Unknown command");
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
            Console.WriteLine("");

            Console.WriteLine("Connectivity:");
            Console.WriteLine("  conn          Test connectivity");
            Console.WriteLine("");

            Console.WriteLine("Config methods:");
            Console.WriteLine("  create config rag     Create a RAG assistant configuration");
            Console.WriteLine("  create config chat    Create a chat-only assistant configuration");
            Console.WriteLine("  retrieve config       Retrieve an assistant configuration");
            Console.WriteLine("  retrieve configs      Retrieve all assistant configurations");
            Console.WriteLine("  update config         Update an assistant configuration");
            Console.WriteLine("  delete config         Delete an assistant configuration");
            Console.WriteLine("  exists config         Check if an assistant configuration exists");
            Console.WriteLine("");

            Console.WriteLine("Chat methods:");
            Console.WriteLine("  process config chat         Process a chat with specific config");
            Console.WriteLine("  process rag message         Process a RAG message");
            Console.WriteLine("  process chat only question  Process a chat-only question");
            Console.WriteLine("  process chat only message   Process a chat-only message");
            Console.WriteLine("");

            Console.WriteLine("Thread methods:");
            Console.WriteLine("  create thread         Create a chat thread");
            Console.WriteLine("  retrieve thread       Retrieve a chat thread");
            Console.WriteLine("  retrieve threads      Retrieve all chat threads");
            Console.WriteLine("  delete thread         Delete a chat thread");
            Console.WriteLine("  exists thread         Check if a chat thread exists");
            Console.WriteLine("  append message        Append a message to a chat thread");
            Console.WriteLine("");

            Console.WriteLine("Model methods:");
            Console.WriteLine("  retrieve models       Retrieve available models");
            Console.WriteLine("  retrieve model        Retrieve a specific model");
            Console.WriteLine("  delete model          Delete a model");
            Console.WriteLine("  preload model         Preload a model");
            Console.WriteLine("  unload model          Unload a model");
            Console.WriteLine("");
        }

        #region Connectivity

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

        #endregion

        #region Config Methods

        private static async Task CreateRagConfig()
        {
            AssistantConfig assistantConfig = BuildObject<AssistantConfig>();
            EnumerateResponse(await _Sdk.Config.CreateRag(assistantConfig));
        }

        private static async Task CreateChatConfig()
        {

            AssistantConfig assistantConfig = BuildObject<AssistantConfig>();
            EnumerateResponse(await _Sdk.Config.CreateChat(assistantConfig));

        }

        private static async Task RetrieveConfig()
        {
            Guid configGuid = Inputty.GetGuid("Config GUID:", default(Guid));
            EnumerateResponse(await _Sdk.Config.Retrieve(configGuid));
        }

        private static async Task RetrieveConfigs()
        {
            EnumerateResponse(await _Sdk.Config.RetrieveMany());
        }

        private static async Task UpdateConfig()
        {
            AssistantConfig assistantConfig = BuildObject<AssistantConfig>();
            EnumerateResponse(await _Sdk.Config.Update(assistantConfig));
        }

        private static async Task DeleteConfig()
        {
            Guid configGuid = Inputty.GetGuid("Config GUID:", default(Guid));
            await _Sdk.Config.Delete(configGuid);
        }

        private static async Task ConfigExists()
        {
            Guid configGuid = Inputty.GetGuid("Config GUID:", default(Guid));
            bool exists = await _Sdk.Config.Exists(configGuid);
            Console.WriteLine("Config exists: " + exists);
            Console.WriteLine("");
        }

        #endregion

        #region Chat Methods

        private static async Task ProcessConfigChat()
        {
            Console.WriteLine("");
            Guid configGuid = Inputty.GetGuid("Config GUID:", Guid.Empty);
            AssistantRequest request = BuildObject<AssistantRequest>();

            await foreach (string token in _Sdk.Chat.ProcessConfigChat(configGuid, request))
            {
                Console.Write(token);
                Console.Out.Flush();
            }

            Console.WriteLine("");
        }

        private static async Task ProcessRagMessage()
        {
            Console.WriteLine("");
            AssistantRequest request = BuildObject<AssistantRequest>();
            await foreach (string token in _Sdk.Chat.ProcessRagMessage(request))
            {
                Console.Write(token);
                Console.Out.Flush();
            }
            Console.WriteLine("");
        }

        private static async Task ProcessChatOnlyQuestion()
        {
            Console.WriteLine("");
            AssistantRequest request = BuildObject<AssistantRequest>();
            await foreach (string token in _Sdk.Chat.ProcessChatOnlyQuestion(request))
            {
                Console.Write(token);
                Console.Out.Flush();
            }
            Console.WriteLine("");
        }

        private static async Task ProcessChatOnlyMessage()
        {
            Console.WriteLine("");
            AssistantRequest request = BuildObject<AssistantRequest>();
            await foreach (string token in _Sdk.Chat.ProcessChatOnlyMessage(request))
            {
                Console.Write(token);
                Console.Out.Flush();
            }
            Console.WriteLine("");
        }

        #endregion

        #region Thread Methods

        private static async Task CreateThread()
        {
            Console.WriteLine("");
            ChatThread request = BuildObject<ChatThread>();
            EnumerateResponse(await _Sdk.Thread.Create(request));
        }

        private static async Task RetrieveThread()
        {
            Console.WriteLine("");
            Guid threadGuid = Inputty.GetGuid("Thread GUID:", Guid.Empty);
            EnumerateResponse(await _Sdk.Thread.Retrieve(threadGuid));
        }

        private static async Task RetrieveThreads()
        {
            Console.WriteLine("");
            EnumerateResponse(await _Sdk.Thread.RetrieveMany());
        }

        private static async Task DeleteThread()
        {
            Console.WriteLine("");
            Guid threadGuid = Inputty.GetGuid("Thread GUID:", Guid.Empty);
            await _Sdk.Thread.Delete(threadGuid);
        }

        private static async Task ThreadExists()
        {
            Console.WriteLine("");
            Guid threadGuid = Inputty.GetGuid("Thread GUID:", Guid.Empty);
            bool exists = await _Sdk.Thread.Exists(threadGuid);
            Console.WriteLine("Thread exists: " + exists);
            Console.WriteLine("");
        }

        private static async Task AppendMessage()
        {
            Console.WriteLine("");
            Guid threadGuid = Inputty.GetGuid("Thread GUID:", Guid.Empty);
            ChatMessage request = BuildObject<ChatMessage>();
            EnumerateResponse(await _Sdk.Thread.Append(threadGuid, request));
        }

        #endregion

        #region Model Methods

        private static async Task RetrieveModels()
        {
            ModelRequest request = BuildObject<ModelRequest>();
            EnumerateResponse(await _Sdk.Model.RetrieveMany(request));
        }

        private static async Task RetrieveModel()
        {
            ModelRequest request = BuildObject<ModelRequest>();

            await foreach (string progress in _Sdk.Model.Retrieve(request))
            {
                Console.WriteLine(progress);
            }
            Console.WriteLine("");
        }

        private static async Task DeleteModel()
        {
            ModelRequest request = BuildObject<ModelRequest>();
            await _Sdk.Model.Delete(request);
        }

        private static async Task PreloadModel()
        {
            ModelRequest request = BuildObject<ModelRequest>();
            EnumerateResponse(await _Sdk.Model.PreLoad(request));
        }

        private static async Task UnloadModel()
        {
            ModelRequest request = BuildObject<ModelRequest>();
            EnumerateResponse(await _Sdk.Model.Unload(request));
        }

        #endregion

        private static void EmitLogMessage(SeverityEnum sev, string msg)
        {
            if (!String.IsNullOrEmpty(msg)) Console.WriteLine(sev.ToString() + " " + msg);
        }

        private static T BuildObject<T>()
        {
            string json = Inputty.GetString("JSON :", null, false);
            return _Serializer.DeserializeJson<T>(json);
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

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
}