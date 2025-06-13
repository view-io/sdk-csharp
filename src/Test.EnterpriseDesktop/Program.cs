namespace Test.EnterpriseDesktop
{
    using GetSomeInput;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using View.Sdk;
    using View.Sdk.EnterpriseDesktop;
    using View.Sdk.Serialization;

    public static class Program
    {
        private static bool _RunForever = true;
        private static Guid _TenantGuid = default(Guid);
        private static string _Endpoint = "http://localhost:8000/";
        private static string _AccessKey = "default";
        private static ViewEnterpriseDesktopSdk _Sdk = null!;
        private static Serializer _Serializer = new Serializer();
        private static bool _EnableLogging = true;

        public static void Main(string[] args)
        {
            _TenantGuid = Inputty.GetGuid("Tenant GUID :", _TenantGuid);
            _Endpoint = Inputty.GetString("Endpoint    :", _Endpoint, false);
            _AccessKey = Inputty.GetString("Access key  :", _AccessKey, false);
            _Sdk = new ViewEnterpriseDesktopSdk(_TenantGuid, _AccessKey, _Endpoint);
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

                    // DesktopUser commands
                    case "reads users":
                        RetrieveAllUsers().Wait();
                        break;
                    case "read user":
                        RetrieveUser().Wait();
                        break;
                    case "write user":
                        WriteUser().Wait();
                        break;
                    case "update user":
                        UpdateUser().Wait();
                        break;
                    case "del user":
                        DeleteUser().Wait();
                        break;
                    case "exists user":
                        UserExists().Wait();
                        break;

                    // DesktopGroup commands
                    case "reads groups":
                        RetrieveAllGroups().Wait();
                        break;
                    case "read group":
                        RetrieveGroup().Wait();
                        break;
                    case "write group":
                        WriteGroup().Wait();
                        break;
                    case "update group":
                        UpdateGroup().Wait();
                        break;
                    case "del group":
                        DeleteGroup().Wait();
                        break;
                    case "exists group":
                        GroupExists().Wait();
                        break;

                    // DesktopPrinter commands
                    case "reads printers":
                        RetrieveAllPrinters().Wait();
                        break;
                    case "read printer":
                        RetrievePrinter().Wait();
                        break;
                    case "write printer":
                        WritePrinter().Wait();
                        break;
                    case "update printer":
                        UpdatePrinter().Wait();
                        break;
                    case "del printer":
                        DeletePrinter().Wait();
                        break;
                    case "exists printer":
                        PrinterExists().Wait();
                        break;
                }
            }
        }

        private static void Menu()
        {
            Console.WriteLine("");
            Console.WriteLine("Available commands:");
            Console.WriteLine("  q                                 Quit this program");
            Console.WriteLine("  ?                                 Help, this menu");
            Console.WriteLine("  cls                               Clear the screen");
            Console.WriteLine("  conn                              Test connectivity");
            Console.WriteLine("");
            Console.WriteLine("  reads users                       List all desktop users");
            Console.WriteLine("  read user                         Retrieve desktop user");
            Console.WriteLine("  write user                        Create desktop user");
            Console.WriteLine("  update user                       Update desktop user");
            Console.WriteLine("  del user                          Delete desktop user");
            Console.WriteLine("  exists user                       Check if desktop user exists");
            Console.WriteLine("");
            Console.WriteLine("  reads groups                      List all desktop groups");
            Console.WriteLine("  read group                        Retrieve desktop group");
            Console.WriteLine("  write group                       Create desktop group");
            Console.WriteLine("  update group                      Update desktop group");
            Console.WriteLine("  del group                         Delete desktop group");
            Console.WriteLine("  exists group                      Check if desktop group exists");
            Console.WriteLine("");
            Console.WriteLine("  reads printers                    List all desktop printers");
            Console.WriteLine("  read printer                      Retrieve desktop printer");
            Console.WriteLine("  write printer                     Create desktop printer");
            Console.WriteLine("  update printer                    Update desktop printer");
            Console.WriteLine("  del printer                       Delete desktop printer");
            Console.WriteLine("  exists printer                    Check if desktop printer exists");
            Console.WriteLine("");
        }

        private static void EmitLogMessage(SeverityEnum severity, string message)
        {
            Console.WriteLine(severity.ToString() + ": " + message);
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

        private static T BuildObject<T>()
        {
            string json = Inputty.GetString("JSON :", null, false);
            return _Serializer.DeserializeJson<T>(json);
        }

        #region DesktopUser-Methods

        private static async Task RetrieveAllUsers()
        {
            EnumerateResponse(await _Sdk.DesktopUser.RetrieveMany());
        }

        private static async Task RetrieveUser()
        {
            Guid guid = Inputty.GetGuid("User GUID:", default(Guid));
            EnumerateResponse(await _Sdk.DesktopUser.Retrieve(guid));
        }

        private static async Task WriteUser()
        {
            DesktopUser user = BuildObject<DesktopUser>();
            EnumerateResponse(await _Sdk.DesktopUser.Create(user));
        }

        private static async Task UpdateUser()
        {
            DesktopUser user = BuildObject<DesktopUser>();
            EnumerateResponse(await _Sdk.DesktopUser.Update(user));
        }

        private static async Task DeleteUser()
        {
            Guid guid = Inputty.GetGuid("User GUID:", default(Guid));
            await _Sdk.DesktopUser.Delete(guid);
        }

        private static async Task UserExists()
        {
            Guid guid = Inputty.GetGuid("User GUID:", default(Guid));
            bool exists = await _Sdk.DesktopUser.Exists(guid);
            Console.WriteLine("User exists: " + exists);
        }

        #endregion

        #region DesktopGroup-Methods

        private static async Task RetrieveAllGroups()
        {
            EnumerateResponse(await _Sdk.DesktopGroup.RetrieveMany());
        }

        private static async Task RetrieveGroup()
        {
            Guid guid = Inputty.GetGuid("Group GUID:", default(Guid));
            EnumerateResponse(await _Sdk.DesktopGroup.Retrieve(guid));
        }

        private static async Task WriteGroup()
        {
            DesktopGroup group = BuildObject<DesktopGroup>();
            EnumerateResponse(await _Sdk.DesktopGroup.Create(group));
        }

        private static async Task UpdateGroup()
        {
            DesktopGroup group = BuildObject<DesktopGroup>();
            EnumerateResponse(await _Sdk.DesktopGroup.Update(group));
        }

        private static async Task DeleteGroup()
        {
            Guid guid = Inputty.GetGuid("Group GUID:", default(Guid));
            await _Sdk.DesktopGroup.Delete(guid);
        }

        private static async Task GroupExists()
        {
            Guid guid = Inputty.GetGuid("Group GUID:", default(Guid));
            bool exists = await _Sdk.DesktopGroup.Exists(guid);
            Console.WriteLine("Group exists: " + exists);
        }

        #endregion

        #region DesktopPrinter-Methods

        private static async Task RetrieveAllPrinters()
        {
            EnumerateResponse(await _Sdk.DesktopPrinter.RetrieveMany());
        }

        private static async Task RetrievePrinter()
        {
            Guid guid = Inputty.GetGuid("Printer GUID:", default(Guid));
            EnumerateResponse(await _Sdk.DesktopPrinter.Retrieve(guid));
        }

        private static async Task WritePrinter()
        {
            DesktopPrinter printer = BuildObject<DesktopPrinter>();
            EnumerateResponse(await _Sdk.DesktopPrinter.Create(printer));
        }

        private static async Task UpdatePrinter()
        {
            DesktopPrinter printer = BuildObject<DesktopPrinter>();
            EnumerateResponse(await _Sdk.DesktopPrinter.Update(printer));
        }

        private static async Task DeletePrinter()
        {
            Guid guid = Inputty.GetGuid("Printer GUID:", default(Guid));
            await _Sdk.DesktopPrinter.Delete(guid);
        }

        private static async Task PrinterExists()
        {
            Guid guid = Inputty.GetGuid("Printer GUID:", default(Guid));
            bool exists = await _Sdk.DesktopPrinter.Exists(guid);
            Console.WriteLine("Printer exists: " + exists);
        }

        #endregion
    }
}