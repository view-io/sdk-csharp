namespace Test.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using GetSomeInput;
    using View.Sdk;
    using View.Sdk.Configuration;
    using View.Serializer;

    public static class Program
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

        private static bool _RunForever = true;
        private static string _TenantGuid = "default";
        private static string _Endpoint = "http://localhost:8601/";
        private static string _AccessKey = "default";
        private static ViewConfigurationSdk _Sdk = null;
        private static SerializationHelper _Serializer = new SerializationHelper();
        private static bool _EnableLogging = true;

        public static void Main(string[] args)
        {
            _TenantGuid = Inputty.GetString("Tenant GUID :", _TenantGuid, false);
            _Endpoint = Inputty.GetString("Endpoint    :", _Endpoint, false);
            _AccessKey = Inputty.GetString("Access key  :", _AccessKey, false);

            _Sdk = new ViewConfigurationSdk(_TenantGuid, _AccessKey, _Endpoint);
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

                    case "read tenant":
                        ReadTenant().Wait();
                        break;
                    case "update tenant":
                        UpdateTenant().Wait();
                        break;

                    case "write user":
                        WriteUser().Wait();
                        break;
                    case "update user":
                        UpdateUser().Wait();
                        break;
                    case "read users":
                        ReadUsers().Wait();
                        break;
                    case "read user":
                        ReadUser().Wait();
                        break;
                    case "delete user":
                        DeleteUser().Wait();
                        break;
                    case "exists user":
                        ExistsUser().Wait();
                        break;

                    case "write cred":
                        WriteCredential().Wait();
                        break;
                    case "update cred":
                        UpdateCredential().Wait();
                        break;
                    case "read creds":
                        ReadCredentials().Wait();
                        break;
                    case "read cred":
                        ReadCredential().Wait();
                        break;
                    case "delete cred":
                        DeleteCredential().Wait();
                        break;
                    case "exists cred":
                        ExistsCredential().Wait();
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
            Console.WriteLine("  read tenant       Read tenant metadata");
            Console.WriteLine("  update tenant     Update a tenant");
            Console.WriteLine("");
            Console.WriteLine("  write user        Create a user");
            Console.WriteLine("  update user       Update a user");
            Console.WriteLine("  read users        Read all users");
            Console.WriteLine("  read user         Read a user");
            Console.WriteLine("  delete user       Delete a user");
            Console.WriteLine("  exists user       Check if a user exists");
            Console.WriteLine("");
            Console.WriteLine("  write cred        Create a credential");
            Console.WriteLine("  update cred       Update a credential");
            Console.WriteLine("  read creds        Read all credentials");
            Console.WriteLine("  read cred         Read a credential");
            Console.WriteLine("  delete cred       Delete a credential");
            Console.WriteLine("  exists cred       Check if a credential exists");
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

        #region Tenants

        private static async Task ReadTenant()
        {
            EnumerateResponse(await _Sdk.RetrieveTenant());
        }

        private static async Task UpdateTenant()
        {
            TenantMetadata obj = BuildObject<TenantMetadata>();
            EnumerateResponse(await _Sdk.UpdateTenant(obj));
        }

        #endregion

        #region Users

        private static async Task WriteUser()
        {
            UserMaster obj = BuildObject<UserMaster>();
            EnumerateResponse(await _Sdk.CreateUser(obj));
        }

        private static async Task UpdateUser()
        {
            UserMaster obj = BuildObject<UserMaster>();
            EnumerateResponse(await _Sdk.UpdateUser(obj));
        }

        private static async Task ReadUsers()
        {
            EnumerateResponse(await _Sdk.RetrieveUsers());
        }

        private static async Task ReadUser()
        {
            string guid = GetGuid("GUID:");
            EnumerateResponse(await _Sdk.RetrieveUser(guid));
        }

        private static async Task DeleteUser()
        {
            string guid = GetGuid("GUID:");
            await _Sdk.DeleteUser(guid);
        }

        private static async Task ExistsUser()
        {
            string guid = GetGuid("GUID:");
            bool exists = await _Sdk.ExistsUser(guid);
            Console.WriteLine("Exists: " + exists);
        }

        #endregion

        #region Credentials

        private static async Task WriteCredential()
        {
            Credential obj = BuildObject<Credential>();
            EnumerateResponse(await _Sdk.CreateCredential(obj));
        }

        private static async Task UpdateCredential()
        {
            Credential obj = BuildObject<Credential>();
            EnumerateResponse(await _Sdk.UpdateCredential(obj));
        }

        private static async Task ReadCredentials()
        {
            EnumerateResponse(await _Sdk.RetrieveCredentials());
        }

        private static async Task ReadCredential()
        {
            string guid = GetGuid("GUID:");
            EnumerateResponse(await _Sdk.RetrieveCredential(guid));
        }

        private static async Task DeleteCredential()
        {
            string guid = GetGuid("GUID:");
            await _Sdk.DeleteCredential(guid);
        }

        private static async Task ExistsCredential()
        {
            string guid = GetGuid("GUID:");
            bool exists = await _Sdk.ExistsCredential(guid);
            Console.WriteLine("Exists: " + exists);
        }

        #endregion

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
}