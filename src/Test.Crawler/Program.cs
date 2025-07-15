namespace Test.Crawler
{
    using GetSomeInput;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using View.Sdk;
    using View.Sdk.Crawler;
    using View.Sdk.Serialization;

    public static class Program
    {
        private static bool _RunForever = true;
        private static Guid _TenantGuid = default(Guid);
        private static string _Endpoint = "http://localhost:8000/";
        private static string _AccessKey = "default";
        private static ViewCrawlerSdk _Sdk = null!;
        private static Serializer _Serializer = new Serializer();
        private static bool _EnableLogging = true;

        public static void Main(string[] args)
        {
            _TenantGuid = Inputty.GetGuid("Tenant GUID :", _TenantGuid);
            _Endpoint = Inputty.GetString("Endpoint    :", _Endpoint, false);
            _AccessKey = Inputty.GetString("Access key  :", _AccessKey, false);
            _Sdk = new ViewCrawlerSdk(_TenantGuid, _AccessKey, _Endpoint);
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

                    // DataRepository commands
                    case "reads repositories":
                        RetrieveAllRepositories().Wait();
                        break;
                    case "read repository":
                        RetrieveRepository().Wait();
                        break;
                    case "write repository":
                        WriteRepository().Wait();
                        break;
                    case "update repository":
                        UpdateRepository().Wait();
                        break;
                    case "del repository":
                        DeleteRepository().Wait();
                        break;
                    case "exists repository":
                        RepositoryExists().Wait();
                        break;
                    case "enumerate repository":
                        EnumerateRepository().Wait();
                        break;

                    // CrawlSchedule commands
                    case "reads schedules":
                        RetrieveAllSchedules().Wait();
                        break;
                    case "read schedule":
                        RetrieveSchedule().Wait();
                        break;
                    case "write schedule":
                        WriteSchedule().Wait();
                        break;
                    case "update schedule":
                        UpdateSchedule().Wait();
                        break;
                    case "del schedule":
                        DeleteSchedule().Wait();
                        break;
                    case "exists schedule":
                        ScheduleExists().Wait();
                        break;
                    case "enumerate schedule":
                        EnumerateSchedule().Wait();
                        break;

                    // CrawlFilter commands
                    case "reads filters":
                        RetrieveAllFilters().Wait();
                        break;
                    case "read filter":
                        RetrieveFilter().Wait();
                        break;
                    case "write filter":
                        WriteFilter().Wait();
                        break;
                    case "update filter":
                        UpdateFilter().Wait();
                        break;
                    case "del filter":
                        DeleteFilter().Wait();
                        break;
                    case "exists filter":
                        FilterExists().Wait();
                        break;
                    case "enumerate filter":
                        EnumerateFilter().Wait();
                        break;

                    // CrawlPlan commands
                    case "reads plans":
                        RetrieveAllPlans().Wait();
                        break;
                    case "read plan":
                        RetrievePlan().Wait();
                        break;
                    case "write plan":
                        WritePlan().Wait();
                        break;
                    case "update plan":
                        UpdatePlan().Wait();
                        break;
                    case "del plan":
                        DeletePlan().Wait();
                        break;
                    case "exists plan":
                        PlanExists().Wait();
                        break;
                    case "enumerate plan":
                        EnumeratePlan().Wait();
                        break;

                    // CrawlOperation commands
                    case "reads operations":
                        RetrieveAllOperations().Wait();
                        break;
                    case "read operation":
                        RetrieveOperation().Wait();
                        break;
                    case "start operation":
                        StartOperation().Wait();
                        break;
                    case "stop operation":
                        StopOperation().Wait();
                        break;
                    case "del operation":
                        DeleteOperation().Wait();
                        break;
                    case "exists operation":
                        OperationExists().Wait();
                        break;
                    case "enumerate retrieve-operation":
                        RetrieveOperationEnumeration().Wait();
                        break;
                    case "enumerate operation":
                        EnumerateOperation().Wait();
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
            Console.WriteLine("  reads repositories                List all data repositories");
            Console.WriteLine("  read repository                   Retrieve data repository");
            Console.WriteLine("  write repository                  Create data repository");
            Console.WriteLine("  update repository                 Update data repository");
            Console.WriteLine("  del repository                    Delete data repository");
            Console.WriteLine("  exists repository                 Check if data repository exists");
            Console.WriteLine("  enumerate repository              Enumerate data repository");
            Console.WriteLine("");
            Console.WriteLine("  reads schedules                   List all crawl schedules");
            Console.WriteLine("  read schedule                     Retrieve crawl schedule");
            Console.WriteLine("  write schedule                    Create crawl schedule");
            Console.WriteLine("  update schedule                   Update crawl schedule");
            Console.WriteLine("  del schedule                      Delete crawl schedule");
            Console.WriteLine("  exists schedule                   Check if crawl schedule exists");
            Console.WriteLine("  enumerate schedule                Enumerate crawl schedule");
            Console.WriteLine("");
            Console.WriteLine("  reads filters                     List all crawl filters");
            Console.WriteLine("  read filter                       Retrieve crawl filter");
            Console.WriteLine("  write filter                      Create crawl filter");
            Console.WriteLine("  update filter                     Update crawl filter");
            Console.WriteLine("  del filter                        Delete crawl filter");
            Console.WriteLine("  exists filter                     Check if crawl filter exists");
            Console.WriteLine("  enumerate filter                  Enumerate crawl filter");
            Console.WriteLine("");
            Console.WriteLine("  reads plans                       List all crawl plans");
            Console.WriteLine("  read plan                         Retrieve crawl plan");
            Console.WriteLine("  write plan                        Create crawl plan");
            Console.WriteLine("  update plan                       Update crawl plan");
            Console.WriteLine("  del plan                          Delete crawl plan");
            Console.WriteLine("  exists plan                       Check if crawl plan exists");
            Console.WriteLine("  enumerate plan                    Enumerate crawl plan");
            Console.WriteLine("");
            Console.WriteLine("  reads operations                  List all crawl operations");
            Console.WriteLine("  read operation                    Retrieve crawl operation");
            Console.WriteLine("  start operation                   Start crawl operation");
            Console.WriteLine("  stop operation                    Stop crawl operation");
            Console.WriteLine("  del operation                     Delete crawl operation");
            Console.WriteLine("  exists operation                  Check if crawl operation exists");
            Console.WriteLine("  enumerate retrieve-operation      Retrieve operation enumeration");
            Console.WriteLine("  enumerate operation               Enumerate operation");
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

        #region CrawlSchedule Methods

        private static async Task RetrieveAllSchedules()
        {
            List<CrawlSchedule> schedules = await _Sdk.CrawlSchedule.RetrieveMany();
            EnumerateResponse(schedules);
        }

        private static async Task RetrieveSchedule()
        {
            Guid guid = Inputty.GetGuid("GUID:", _TenantGuid);
            EnumerateResponse(await _Sdk.CrawlSchedule.Retrieve(guid));
        }

        private static async Task WriteSchedule()
        {
            CrawlSchedule schedule = BuildObject<CrawlSchedule>();
            EnumerateResponse(await _Sdk.CrawlSchedule.Create(schedule));
        }

        private static async Task UpdateSchedule()
        {
            CrawlSchedule schedule = BuildObject<CrawlSchedule>();
            EnumerateResponse(await _Sdk.CrawlSchedule.Update(schedule));
        }

        private static async Task DeleteSchedule()
        {
            Guid guid = Inputty.GetGuid("GUID:", _TenantGuid);
            await _Sdk.CrawlSchedule.Delete(guid);
        }

        private static async Task ScheduleExists()
        {
            Guid guid = Inputty.GetGuid("GUID:", _TenantGuid);
            bool exists = await _Sdk.CrawlSchedule.Exists(guid);
            Console.WriteLine("");
            Console.WriteLine($"Schedule exists: {exists}");
        }
        private static async Task EnumerateSchedule()
        {
            EnumerateResponse(await _Sdk.CrawlSchedule.Enumerate());
        }
        #endregion

        #region CrawlFilter Methods

        private static async Task RetrieveAllFilters()
        {
            List<CrawlFilter> filters = await _Sdk.CrawlFilter.RetrieveMany();
            EnumerateResponse(filters);
        }

        private static async Task RetrieveFilter()
        {
            Guid guid = Inputty.GetGuid("GUID:", _TenantGuid);
            EnumerateResponse(await _Sdk.CrawlFilter.Retrieve(guid));
        }

        private static async Task WriteFilter()
        {
            CrawlFilter filter = BuildObject<CrawlFilter>();
            EnumerateResponse(await _Sdk.CrawlFilter.Create(filter));
        }

        private static async Task UpdateFilter()
        {
            CrawlFilter filter = BuildObject<CrawlFilter>();
            EnumerateResponse(await _Sdk.CrawlFilter.Update(filter));
        }

        private static async Task DeleteFilter()
        {
            Guid guid = Inputty.GetGuid("GUID:", _TenantGuid);
            await _Sdk.CrawlFilter.Delete(guid);
        }

        private static async Task FilterExists()
        {
            Guid guid = Inputty.GetGuid("GUID:", _TenantGuid);
            bool exists = await _Sdk.CrawlFilter.Exists(guid);
            Console.WriteLine("");
            Console.WriteLine($"Filter exists: {exists}");
            Console.WriteLine("");
        }

        private static async Task EnumerateFilter()
        {
            EnumerateResponse(await _Sdk.CrawlFilter.Enumerate());
        }

        #endregion

        #region CrawlPlan Methods

        private static async Task RetrieveAllPlans()
        {
            List<CrawlPlan> plans = await _Sdk.CrawlPlan.RetrieveMany();
            EnumerateResponse(plans);
        }

        private static async Task RetrievePlan()
        {
            Guid guid = Inputty.GetGuid("GUID:", _TenantGuid);
            EnumerateResponse(await _Sdk.CrawlPlan.Retrieve(guid));
        }

        private static async Task WritePlan()
        {
            CrawlPlan plan = BuildObject<CrawlPlan>();
            EnumerateResponse(await _Sdk.CrawlPlan.Create(plan));
        }

        private static async Task UpdatePlan()
        {
            CrawlPlan plan = BuildObject<CrawlPlan>();
            EnumerateResponse(await _Sdk.CrawlPlan.Update(plan));
        }

        private static async Task DeletePlan()
        {
            Guid guid = Inputty.GetGuid("GUID:", _TenantGuid);
            await _Sdk.CrawlPlan.Delete(guid);
        }

        private static async Task PlanExists()
        {
            Guid guid = Inputty.GetGuid("GUID:", _TenantGuid);
            bool exists = await _Sdk.CrawlPlan.Exists(guid);
            Console.WriteLine("");
            Console.WriteLine($"Plan exists: {exists}");
            Console.WriteLine("");
        }

        private static async Task EnumeratePlan()
        {
            EnumerateResponse(await _Sdk.CrawlPlan.Enumerate());
        }

        #endregion

        #region CrawlOperation Methods

        private static async Task RetrieveAllOperations()
        {
            List<CrawlOperation> operations = await _Sdk.CrawlOperation.RetrieveAll();
            EnumerateResponse(operations);
        }

        private static async Task RetrieveOperation()
        {
            Guid guid = Inputty.GetGuid("GUID:", _TenantGuid);
            EnumerateResponse(await _Sdk.CrawlOperation.Retrieve(guid));
        }

        private static async Task StartOperation()
        {
            CrawlOperationRequest operation = BuildObject<CrawlOperationRequest>();
            EnumerateResponse(await _Sdk.CrawlOperation.Start(operation));
        }

        private static async Task StopOperation()
        {
            CrawlOperationRequest operation = BuildObject<CrawlOperationRequest>();
            EnumerateResponse(await _Sdk.CrawlOperation.Stop(operation));
        }

        private static async Task RetrieveOperationEnumeration()
        {
            Guid guid = Inputty.GetGuid("GUID:", _TenantGuid);
            EnumerateResponse(await _Sdk.CrawlOperation.RetrieveEnumeration(guid));
        }

        private static async Task EnumerateOperation()
        {
            EnumerateResponse(await _Sdk.CrawlOperation.Enumerate());
        }

        private static async Task DeleteOperation()
        {
            Guid guid = Inputty.GetGuid("GUID:", _TenantGuid);
            await _Sdk.CrawlOperation.Delete(guid);
            Console.WriteLine("");
            Console.WriteLine("Operation deleted successfully.");
            Console.WriteLine("");
        }

        private static async Task OperationExists()
        {
            Guid guid = Inputty.GetGuid("GUID:", _TenantGuid);
            bool exists = await _Sdk.CrawlOperation.Exists(guid);
            Console.WriteLine("");
            Console.WriteLine($"Plan exists: {exists}");
            Console.WriteLine("");
        }

        #endregion

        #region DataRepository Methods

        private static async Task RetrieveAllRepositories()
        {
            EnumerateResponse(await _Sdk.DataRepository.RetrieveMany());
        }

        private static async Task RetrieveRepository()
        {
            Guid guid = Inputty.GetGuid("GUID:", _TenantGuid);
            EnumerateResponse(await _Sdk.DataRepository.Retrieve(guid.ToString()));
        }

        private static async Task WriteRepository()
        {
            DataRepository repository = BuildObject<DataRepository>();
            EnumerateResponse(await _Sdk.DataRepository.Create(repository));
        }

        private static async Task UpdateRepository()
        {
            DataRepository repository = BuildObject<DataRepository>();
            EnumerateResponse(await _Sdk.DataRepository.Update(repository));
        }

        private static async Task DeleteRepository()
        {
            Guid guid = Inputty.GetGuid("GUID:", _TenantGuid);
            await _Sdk.DataRepository.Delete(guid.ToString());
        }

        private static async Task RepositoryExists()
        {
            Guid guid = Inputty.GetGuid("GUID:", _TenantGuid);
            bool exists = await _Sdk.DataRepository.Exists(guid.ToString());
            Console.WriteLine("");
            Console.WriteLine($"Repository exists: {exists}");
            Console.WriteLine("");
        }

        private static async Task EnumerateRepository()
        {
            EnumerateResponse(await _Sdk.DataRepository.Enumerate());
        }
        #endregion
    }
}
