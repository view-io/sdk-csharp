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
    using View.Sdk.Serialization;

    public static class Program
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

        private static bool _RunForever = true;
        private static string _TenantGuid = "default";
        private static string _Endpoint = "http://localhost:8601/";
        private static string _AccessKey = "default";
        private static ViewConfigurationSdk _Sdk = null;
        private static Serializer _Serializer = new Serializer();
        private static bool _EnableLogging = true;

        public static void Main(string[] args)
        {
            _TenantGuid = Inputty.GetString("Tenant GUID :", _TenantGuid, false);
            _Endpoint =   Inputty.GetString("Endpoint    :", _Endpoint, false);
            _AccessKey =  Inputty.GetString("Access key  :", _AccessKey, false);

            _Sdk = new ViewConfigurationSdk(_TenantGuid, _AccessKey, _Endpoint);
            if (_EnableLogging) _Sdk.Logger = EmitLogMessage;

            while (_RunForever)
            {
                string userInput = Inputty.GetString("Command [?/help]:", null, false);

                if (userInput.Equals("q"))
                {
                    _RunForever = false;
                    break; 
                }
                else if (userInput.Equals("?"))
                {
                    Menu();
                }
                else if (userInput.Equals("cls"))
                {
                    Console.Clear();
                }
                else if (userInput.Equals("conn"))
                {
                    TestConnectivity().Wait();
                }
                else
                {
                    string[] parts = userInput.Split(" ");
                    if (parts != null && parts.Length == 2)
                    {
                        if (parts[0].Equals("write"))
                        {
                            Write(parts[1]).Wait();
                        }
                        else if (parts[0].Equals("update"))
                        {
                            Update(parts[1]).Wait();
                        }
                        else if (parts[0].Equals("read"))
                        {
                            Read(parts[1]).Wait();
                        }
                        else if (parts[0].Equals("reads"))
                        {
                            ReadMultiple(parts[1]).Wait();
                        }
                        else if (parts[0].Equals("delete"))
                        {
                            Delete(parts[1]).Wait();
                        }
                        else if (parts[0].Equals("exists"))
                        {
                            Exists(parts[1]).Wait();  
                        }
                    }
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
            Console.WriteLine("  [cmd] [type]     Execute an API");
            Console.WriteLine("");
            Console.WriteLine("Where:");
            Console.WriteLine("");
            Console.WriteLine("  [cmd] is one of:");
            Console.WriteLine("    write  update  read  reads  delete  exists");
            Console.WriteLine("");
            Console.WriteLine("  [type] is one of:");
            Console.WriteLine("    node    tenant     user     cred    pool      bucket    enckey");
            Console.WriteLine("    mdrule  embedrule  whevent  whrule  whtarget  lock");
            Console.WriteLine("");
            Console.WriteLine("Not all object types support all commands");
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

        private static async Task Write(string type)
        {
            switch (type)
            {
                case "node":
                    Node node = BuildObject<Node>();
                    EnumerateResponse(await _Sdk.CreateNode(node));
                    return;
                case "tenant":
                    return;
                case "user":
                    UserMaster user = BuildObject<UserMaster>();
                    EnumerateResponse(await _Sdk.CreateUser(user));
                    return;
                case "cred":
                    Credential cred = BuildObject<Credential>();
                    EnumerateResponse(await _Sdk.CreateCredential(cred));
                    return;
                case "pool":
                    StoragePool pool = BuildObject<StoragePool>();
                    EnumerateResponse(await _Sdk.CreatePool(pool));
                    return;
                case "bucket":
                    BucketMetadata bucket = BuildObject<BucketMetadata>();
                    EnumerateResponse(await _Sdk.CreateBucket(bucket));
                    return;
                case "enckey":
                    EncryptionKey key = BuildObject<EncryptionKey>();
                    EnumerateResponse(await _Sdk.CreateEncryptionKey(key));
                    return;
                case "mdrule":
                    MetadataRule mdRule = BuildObject<MetadataRule>();
                    EnumerateResponse(await _Sdk.CreateMetadataRule(mdRule));
                    return;
                case "embedrule":
                    EmbeddingsRule embedRule = BuildObject<EmbeddingsRule>();
                    EnumerateResponse(await _Sdk.CreateEmbeddingsRule(embedRule));
                    return;
                case "whevent":
                    return;
                case "whrule":
                    WebhookRule whRule = BuildObject<WebhookRule>();
                    EnumerateResponse(await _Sdk.CreateWebhookRule(whRule));
                    break;
                case "whtarget":
                    WebhookTarget whTarget = BuildObject<WebhookTarget>();
                    EnumerateResponse(await _Sdk.CreateWebhookTarget(whTarget));
                    break;
                case "lock":
                    break;
                default:
                    return;
            }

            Console.WriteLine("");
        }

        private static async Task Read(string type)
        {
            string guid = Inputty.GetString("GUID:", null, true);
            if (String.IsNullOrEmpty(guid) && !type.Equals("tenant")) return;

            switch (type)
            {
                case "node":
                    EnumerateResponse(await _Sdk.RetrieveNode(guid));
                    return;
                case "tenant":
                    EnumerateResponse(await _Sdk.RetrieveTenant());
                    return;
                case "user":
                    EnumerateResponse(await _Sdk.RetrieveUser(guid));
                    return;
                case "cred":
                    EnumerateResponse(await _Sdk.RetrieveCredential(guid));
                    return;
                case "pool":
                    EnumerateResponse(await _Sdk.RetrievePool(guid));
                    return;
                case "bucket":
                    EnumerateResponse(await _Sdk.RetrieveBucket(guid));
                    return;
                case "enckey":
                    EnumerateResponse(await _Sdk.RetrieveEncryptionKey(guid));
                    return;
                case "mdrule":
                    EnumerateResponse(await _Sdk.RetrieveMetadataRule(guid));
                    return;
                case "embedrule":
                    EnumerateResponse(await _Sdk.RetrieveEmbeddingsRule(guid));
                    return;
                case "whevent":
                    EnumerateResponse(await _Sdk.RetrieveWebhookEvent(guid));
                    return;
                case "whrule":
                    EnumerateResponse(await _Sdk.RetrieveWebhookRule(guid));
                    break;
                case "whtarget":
                    EnumerateResponse(await _Sdk.RetrieveWebhookTarget(guid));
                    break;
                case "lock":
                    EnumerateResponse(await _Sdk.RetrieveObjectLock(guid));
                    break;
                default:
                    return;
            }

            Console.WriteLine("");
        }

        private static async Task ReadMultiple(string type)
        {
            switch (type)
            {
                case "node":
                    EnumerateResponse(await _Sdk.RetrieveNodes());
                    return;
                case "tenant":
                    return;
                case "user":
                    EnumerateResponse(await _Sdk.RetrieveUsers());
                    return;
                case "cred":
                    EnumerateResponse(await _Sdk.RetrieveCredentials());
                    return;
                case "pool":
                    EnumerateResponse(await _Sdk.RetrievePools());
                    return;
                case "bucket":
                    EnumerateResponse(await _Sdk.RetrieveBuckets());
                    return;
                case "enckey":
                    EnumerateResponse(await _Sdk.RetrieveEncryptionKeys());
                    return;
                case "mdrule":
                    EnumerateResponse(await _Sdk.RetrieveMetadataRules());
                    return;
                case "embedrule":
                    EnumerateResponse(await _Sdk.RetrieveEmbeddingsRules());
                    return;
                case "whevent":
                    EnumerateResponse(await _Sdk.RetrieveWebhookEvents());
                    return;
                case "whrule":
                    EnumerateResponse(await _Sdk.RetrieveWebhookRules());
                    break;
                case "whtarget":
                    EnumerateResponse(await _Sdk.RetrieveWebhookTargets());
                    break;
                case "lock":
                    EnumerateResponse(await _Sdk.RetrieveObjectLocks());
                    break;
                default:
                    return;
            }

            Console.WriteLine("");
        }

        private static async Task Update(string type)
        {
            switch (type)
            {
                case "node":
                    Node node = BuildObject<Node>();
                    EnumerateResponse(await _Sdk.UpdateNode(node));
                    return;
                case "tenant":
                    TenantMetadata tenant = BuildObject<TenantMetadata>();
                    EnumerateResponse(await _Sdk.UpdateTenant(tenant));
                    return;
                case "user":
                    UserMaster user = BuildObject<UserMaster>();
                    EnumerateResponse(await _Sdk.UpdateUser(user));
                    return;
                case "cred":
                    Credential cred = BuildObject<Credential>();
                    EnumerateResponse(await _Sdk.UpdateCredential(cred));
                    return;
                case "pool":
                    StoragePool pool = BuildObject<StoragePool>();
                    EnumerateResponse(await _Sdk.UpdatePool(pool));
                    return;
                case "bucket":
                    BucketMetadata bucket = BuildObject<BucketMetadata>();
                    EnumerateResponse(await _Sdk.UpdateBucket(bucket));
                    return;
                case "enckey":
                    EncryptionKey key = BuildObject<EncryptionKey>();
                    EnumerateResponse(await _Sdk.UpdateEncryptionKey(key));
                    return;
                case "mdrule":
                    MetadataRule mdRule = BuildObject<MetadataRule>();
                    EnumerateResponse(await _Sdk.UpdateMetadataRule(mdRule));
                    return;
                case "embedrule":
                    EmbeddingsRule embedRule = BuildObject<EmbeddingsRule>();
                    EnumerateResponse(await _Sdk.UpdateEmbeddingsRule(embedRule));
                    return;
                case "whevent":
                    return;
                case "whrule":
                    WebhookRule whRule = BuildObject<WebhookRule>();
                    EnumerateResponse(await _Sdk.UpdateWebhookRule(whRule));
                    break;
                case "whtarget":
                    WebhookTarget whTarget = BuildObject<WebhookTarget>();
                    EnumerateResponse(await _Sdk.UpdateWebhookTarget(whTarget));
                    break;
                case "lock":
                    break;
                default:
                    return;
            }

            Console.WriteLine("");
        }

        private static async Task Delete(string type)
        {
            string guid = Inputty.GetString("GUID:", null, true);
            if (String.IsNullOrEmpty(guid)) return;

            switch (type)
            {
                case "node":
                    await _Sdk.DeleteNode(guid);
                    return;
                case "tenant":
                    return;
                case "user":
                    await _Sdk.DeleteUser(guid);
                    return;
                case "cred":
                    await _Sdk.DeleteCredential(guid);
                    return;
                case "pool":
                    await _Sdk.DeletePool(guid);
                    return;
                case "bucket":
                    await _Sdk.DeleteBucket(guid);
                    return;
                case "enckey":
                    await _Sdk.DeleteEncryptionKey(guid);
                    return;
                case "mdrule":
                    await _Sdk.DeleteMetadataRule(guid);
                    return;
                case "embedrule":
                    await _Sdk.DeleteEmbeddingsRule(guid);
                    return;
                case "whevent":
                    return;
                case "whrule":
                    await _Sdk.DeleteWebhookRule(guid);
                    break;
                case "whtarget":
                    await _Sdk.DeleteWebhookTarget(guid);
                    break;
                case "lock":
                    await _Sdk.DeleteObjectLock(guid);
                    break;
                default:
                    return;
            }

            Console.WriteLine("");
        }

        private static async Task Exists(string type)
        {
            string guid = Inputty.GetString("GUID:", null, true);
            if (String.IsNullOrEmpty(guid)) return;

            bool exists = false;

            switch (type)
            {
                case "node":
                    exists = await _Sdk.ExistsNode(guid);
                    break;
                case "tenant":
                    return;
                case "user":
                    exists = await _Sdk.ExistsUser(guid);
                    break;
                case "cred":
                    exists = await _Sdk.ExistsCredential(guid);
                    break;
                case "pool":
                    exists = await _Sdk.ExistsPool(guid);
                    break;
                case "bucket":
                    exists = await _Sdk.ExistsBucket(guid);
                    break;
                case "enckey":
                    exists = await _Sdk.ExistsEncryptionKey(guid);
                    break;
                case "mdrule":
                    exists = await _Sdk.ExistsMetadataRule(guid);
                    break;
                case "embedrule":
                    exists = await _Sdk.ExistsEmbeddingsRule(guid);
                    break;
                case "whevent":
                    exists = await _Sdk.ExistsWebhookEvent(guid);
                    break;
                case "whrule":
                    exists = await _Sdk.ExistsWebhookRule(guid);
                    break;
                case "whtarget":
                    exists = await _Sdk.ExistsWebhookTarget(guid);
                    break;
                case "lock":
                    break;
                default:
                    return;
            }

            Console.WriteLine("Exists: " + exists);
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

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
}