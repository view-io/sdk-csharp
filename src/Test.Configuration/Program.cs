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
        private static Guid _TenantGuid = default(Guid);
        private static string _Endpoint = "http://localhost:8000/";
        private static string _AccessKey = "default";
        private static string _XToken = "";
        private static ViewConfigurationSdk _Sdk = null;
        private static Serializer _Serializer = new Serializer();
        private static bool _EnableLogging = true;

        public static void Main(string[] args)
        {
            _TenantGuid = Inputty.GetGuid("Tenant GUID :", _TenantGuid);
            _Endpoint = Inputty.GetString("Endpoint    :", _Endpoint, false);
            _AccessKey = Inputty.GetString("Access key  :", _AccessKey, false);
            _XToken = Inputty.GetString("X-Token  :", _XToken, true);

            _Sdk = new ViewConfigurationSdk(_TenantGuid, _AccessKey, _Endpoint, _XToken);
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
                else if (userInput.StartsWith("auth "))
                {
                    string[] parts = userInput.Split(" ");
                    if (parts.Length >= 2)
                    {
                        switch (parts[1])
                        {
                            case "tenants":
                                TestRetrieveTenants().Wait();
                                break;
                            case "token":
                                TestGenerateTokenWithPassword().Wait();
                                break;
                            case "tokensha256":
                                TestGenerateTokenWithPasswordSha256().Wait();
                                break;
                            case "admintoken":
                                TestGenerateAdminTokenWithPassword().Wait();
                                break;
                            case "admintokensha256":
                                TestGenerateAdminTokenWithPasswordSha256().Wait();
                                break;
                            case "validate":
                                TestValidateToken().Wait();
                                break;
                            case "details":
                                TestRetrieveTokenDetails().Wait();
                                break;
                            default:
                                Console.WriteLine("Unknown authentication command: " + parts[1]);
                                break;
                        }
                    }
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
                        else if (parts[0].Equals("enumerate"))
                        {
                            Enumerate(parts[1]).Wait();
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
            Console.WriteLine("    node    tenant     user     cred    pool       bucket enckey");
            Console.WriteLine("    mdrule  embedrule  whevent  whrule  whtarget   lock   vector      collection ");
            Console.WriteLine("    datarepository     blob     graph   permission role   rolepermmap  userrole  deployment");
            Console.WriteLine("    modelconfig");
            Console.WriteLine("");
            Console.WriteLine("Authentication commands:");
            Console.WriteLine("  auth tenants      Retrieve tenants");
            Console.WriteLine("  auth token        Generate token with password");
            Console.WriteLine("  auth tokensha256  Generate token with password SHA-256");
            Console.WriteLine("  auth admintoken   Generate admin token with password");
            Console.WriteLine("  auth admintokensha256  Generate admin token with password SHA-256");
            Console.WriteLine("  auth validate     Validate token");
            Console.WriteLine("  auth details      Retrieve token details");
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
                case "tenant":
                    TenantMetadata tenant = BuildObject<TenantMetadata>();
                    EnumerateResponse(await _Sdk.Tenant.Create(tenant));
                    return;
                case "user":
                    UserMaster user = BuildObject<UserMaster>();
                    EnumerateResponse(await _Sdk.User.Create(user));
                    return;
                case "cred":
                    Credential cred = BuildObject<Credential>();
                    EnumerateResponse(await _Sdk.Credential.Create(cred));
                    return;
                case "pool":
                    StoragePool pool = BuildObject<StoragePool>();
                    EnumerateResponse(await _Sdk.Pool.Create(pool));
                    return;
                case "bucket":
                    BucketMetadata bucket = BuildObject<BucketMetadata>();
                    EnumerateResponse(await _Sdk.Bucket.Create(bucket));
                    return;
                case "enckey":
                    EncryptionKey key = BuildObject<EncryptionKey>();
                    EnumerateResponse(await _Sdk.EncryptionKey.Create(key));
                    return;
                case "mdrule":
                    MetadataRule mdRule = BuildObject<MetadataRule>();
                    EnumerateResponse(await _Sdk.MetadataRule.Create(mdRule));
                    return;
                case "embedrule":
                    EmbeddingsRule embedRule = BuildObject<EmbeddingsRule>();
                    EnumerateResponse(await _Sdk.EmbeddingsRule.Create(embedRule));
                    return;
                case "whevent":
                    return;
                case "whrule":
                    WebhookRule whRule = BuildObject<WebhookRule>();
                    EnumerateResponse(await _Sdk.WebhookRule.Create(whRule));
                    break;
                case "whtarget":
                    WebhookTarget whTarget = BuildObject<WebhookTarget>();
                    EnumerateResponse(await _Sdk.WebhookTarget.Create(whTarget));
                    break;
                case "vector":
                    VectorRepository vectorRepo = BuildObject<VectorRepository>();
                    EnumerateResponse(await _Sdk.VectorRepository.Create(vectorRepo));
                    return;
                case "graph":
                    GraphRepository graphRepo = BuildObject<GraphRepository>();
                    EnumerateResponse(await _Sdk.GraphRepository.Create(graphRepo));
                    return;
                case "collection":
                    Collection collection = BuildObject<Collection>();
                    EnumerateResponse(await _Sdk.Collection.Create(collection));
                    return;
                case "blob":
                    Blob blob = BuildObject<Blob>();
                    EnumerateResponse(await _Sdk.Blob.Create(blob));
                    return;
                case "permission":
                    Permission permission = BuildObject<Permission>();
                    EnumerateResponse(await _Sdk.Permission.Create(permission));
                    return;
                case "role":
                    Role role = BuildObject<Role>();
                    EnumerateResponse(await _Sdk.Role.Create(role));
                    return;
                case "rolepermmap":
                    RolePermissionMap rolePermMap = BuildObject<RolePermissionMap>();
                    EnumerateResponse(await _Sdk.RolePermissionMap.Create(rolePermMap));
                    return;
                case "userrole":
                    UserRoleMap userRoleMap = BuildObject<UserRoleMap>();
                    EnumerateResponse(await _Sdk.UserRoleMap.Create(userRoleMap));
                    return;
                case "modelconfig":
                    ModelConfiguration modelConfig = BuildObject<ModelConfiguration>();
                    EnumerateResponse(await _Sdk.ModelConfiguration.Create(modelConfig));
                    return;
                case "lock":
                    break;
                default:
                    return;
            }

            Console.WriteLine("");
        }

        private static async Task Read(string type)
        {
            if (type == "deployment")
            {
                EnumerateResponse(await _Sdk.DeploymentType.Retrieve());
                return;
            }

            Guid guid = Inputty.GetGuid("GUID:", _TenantGuid);
            switch (type)
            {
                case "node":
                    EnumerateResponse(await _Sdk.Node.Retrieve(guid));
                    return;
                case "tenant":
                    EnumerateResponse(await _Sdk.Tenant.Retrieve());
                    return;
                case "user":
                    EnumerateResponse(await _Sdk.User.Retrieve(guid));
                    return;
                case "cred":
                    EnumerateResponse(await _Sdk.Credential.Retrieve(guid));
                    return;
                case "pool":
                    EnumerateResponse(await _Sdk.Pool.Retrieve(guid));
                    return;
                case "bucket":
                    EnumerateResponse(await _Sdk.Bucket.Retrieve(guid));
                    return;
                case "enckey":
                    EnumerateResponse(await _Sdk.EncryptionKey.Retrieve(guid));
                    return;
                case "mdrule":
                    EnumerateResponse(await _Sdk.MetadataRule.Retrieve(guid));
                    return;
                case "embedrule":
                    EnumerateResponse(await _Sdk.EmbeddingsRule.Retrieve(guid));
                    return;
                case "whevent":
                    EnumerateResponse(await _Sdk.WebhookEvent.Retrieve(guid));
                    return;
                case "whrule":
                    EnumerateResponse(await _Sdk.WebhookRule.Retrieve(guid));
                    break;
                case "whtarget":
                    EnumerateResponse(await _Sdk.WebhookTarget.Retrieve(guid));
                    break;
                case "vector":
                    EnumerateResponse(await _Sdk.VectorRepository.Retrieve(guid));
                    return;
                case "graph":
                    EnumerateResponse(await _Sdk.GraphRepository.Retrieve(guid));
                    return;
                case "collection":
                    EnumerateResponse(await _Sdk.Collection.Retrieve(guid.ToString()));
                    return;
                case "lock":
                    EnumerateResponse(await _Sdk.ObjectLock.Retrieve(guid));
                    break;
                case "blob":
                    bool isPublic = Inputty.GetBoolean("Is public blob?", false);
                    if (isPublic)
                    {
                        EnumerateResponse(await _Sdk.Blob.Read(guid.ToString()));
                    }
                    else
                    {
                        bool inclData = Inputty.GetBoolean("Include data?", false);
                        EnumerateResponse(await _Sdk.Blob.Retrieve(guid.ToString(), inclData));
                    }
                    return;
                case "permission":
                    EnumerateResponse(await _Sdk.Permission.Retrieve(guid));
                    return;
                case "role":
                    EnumerateResponse(await _Sdk.Role.Retrieve(guid.ToString()));
                    return;
                case "rolepermmap":
                    EnumerateResponse(await _Sdk.RolePermissionMap.Retrieve(guid));
                    return;
                case "userrole":
                    EnumerateResponse(await _Sdk.UserRoleMap.Retrieve(guid));
                    return;
                case "modelconfig":
                    EnumerateResponse(await _Sdk.ModelConfiguration.Retrieve(guid));
                    return;
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
                    EnumerateResponse(await _Sdk.Node.RetrieveMany());
                    return;
                case "tenant":
                    EnumerateResponse(await _Sdk.Tenant.RetrieveMany());
                    return;
                case "user":
                    EnumerateResponse(await _Sdk.User.RetrieveMany());
                    return;
                case "cred":
                    EnumerateResponse(await _Sdk.Credential.RetrieveMany());
                    return;
                case "pool":
                    EnumerateResponse(await _Sdk.Pool.RetrieveMany());
                    return;
                case "bucket":
                    EnumerateResponse(await _Sdk.Bucket.RetrieveMany());
                    return;
                case "enckey":
                    EnumerateResponse(await _Sdk.EncryptionKey.RetrieveMany());
                    return;
                case "mdrule":
                    EnumerateResponse(await _Sdk.MetadataRule.RetrieveMany());
                    return;
                case "embedrule":
                    EnumerateResponse(await _Sdk.EmbeddingsRule.RetrieveMany());
                    return;
                case "whevent":
                    EnumerateResponse(await _Sdk.WebhookEvent.RetrieveMany());
                    return;
                case "whrule":
                    EnumerateResponse(await _Sdk.WebhookRule.RetrieveMany());
                    break;
                case "whtarget":
                    EnumerateResponse(await _Sdk.WebhookTarget.RetrieveMany());
                    break;
                case "vector":
                    EnumerateResponse(await _Sdk.VectorRepository.RetrieveMany());
                    return;
                case "graph":
                    EnumerateResponse(await _Sdk.GraphRepository.RetrieveMany());
                    return;
                case "collection":
                    EnumerateResponse(await _Sdk.Collection.RetrieveMany());
                    return;
                case "lock":
                    EnumerateResponse(await _Sdk.ObjectLock.RetrieveMany());
                    break;
                case "blob":
                    EnumerateResponse(await _Sdk.Blob.RetrieveMany());
                    return;
                case "permission":
                    EnumerateResponse(await _Sdk.Permission.RetrieveMany());
                    return;
                case "role":
                    EnumerateResponse(await _Sdk.Role.RetrieveMany());
                    return;
                case "rolepermmap":
                    EnumerateResponse(await _Sdk.RolePermissionMap.RetrieveMany());
                    return;
                case "userrole":
                    EnumerateResponse(await _Sdk.UserRoleMap.RetrieveMany());
                    return;
                case "modelconfig":
                    EnumerateResponse(await _Sdk.ModelConfiguration.RetrieveMany());
                    return;
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
                    EnumerateResponse(await _Sdk.Node.Update(node));
                    return;
                case "tenant":
                    TenantMetadata tenant = BuildObject<TenantMetadata>();
                    EnumerateResponse(await _Sdk.Tenant.Update(tenant));
                    return;
                case "user":
                    UserMaster user = BuildObject<UserMaster>();
                    EnumerateResponse(await _Sdk.User.Update(user));
                    return;
                case "cred":
                    Credential cred = BuildObject<Credential>();
                    EnumerateResponse(await _Sdk.Credential.Update(cred));
                    return;
                case "pool":
                    StoragePool pool = BuildObject<StoragePool>();
                    EnumerateResponse(await _Sdk.Pool.Update(pool));
                    return;
                case "bucket":
                    BucketMetadata bucket = BuildObject<BucketMetadata>();
                    EnumerateResponse(await _Sdk.Bucket.Update(bucket));
                    return;
                case "enckey":
                    EncryptionKey key = BuildObject<EncryptionKey>();
                    EnumerateResponse(await _Sdk.EncryptionKey.Update(key));
                    return;
                case "mdrule":
                    MetadataRule mdRule = BuildObject<MetadataRule>();
                    EnumerateResponse(await _Sdk.MetadataRule.Update(mdRule));
                    return;
                case "embedrule":
                    EmbeddingsRule embedRule = BuildObject<EmbeddingsRule>();
                    EnumerateResponse(await _Sdk.EmbeddingsRule.Update(embedRule));
                    return;
                case "whevent":
                    return;
                case "whrule":
                    WebhookRule whRule = BuildObject<WebhookRule>();
                    EnumerateResponse(await _Sdk.WebhookRule.Update(whRule));
                    break;
                case "whtarget":
                    WebhookTarget whTarget = BuildObject<WebhookTarget>();
                    EnumerateResponse(await _Sdk.WebhookTarget.Update(whTarget));
                    break;
                case "vector":
                    VectorRepository vectorRepo = BuildObject<VectorRepository>();
                    EnumerateResponse(await _Sdk.VectorRepository.Update(vectorRepo));
                    return;
                case "graph":
                    GraphRepository graphRepo = BuildObject<GraphRepository>();
                    EnumerateResponse(await _Sdk.GraphRepository.Update(graphRepo));
                    return;
                case "lock":
                    break;
                case "blob":
                    Blob blob = BuildObject<Blob>();
                    EnumerateResponse(await _Sdk.Blob.Update(blob));
                    return;
                case "permission":
                    Permission permission = BuildObject<Permission>();
                    EnumerateResponse(await _Sdk.Permission.Update(permission));
                    return;
                case "role":
                    Role role = BuildObject<Role>();
                    EnumerateResponse(await _Sdk.Role.Update(role));
                    return;
                case "rolepermmap":
                    RolePermissionMap rolePermMap = BuildObject<RolePermissionMap>();
                    EnumerateResponse(await _Sdk.RolePermissionMap.Update(rolePermMap));
                    return;
                case "userrole":
                    UserRoleMap userRoleMap = BuildObject<UserRoleMap>();
                    EnumerateResponse(await _Sdk.UserRoleMap.Update(userRoleMap));
                    return;
                case "modelconfig":
                    ModelConfiguration modelConfig = BuildObject<ModelConfiguration>();
                    EnumerateResponse(await _Sdk.ModelConfiguration.Update(modelConfig));
                    return;
                default:
                    return;
            }

            Console.WriteLine("");
        }

        private static async Task Delete(string type)
        {
            Guid guid = Inputty.GetGuid("GUID:", _TenantGuid);

            switch (type)
            {
                case "node":
                    await _Sdk.Node.Delete(guid);
                    return;
                case "tenant":
                    await _Sdk.Tenant.Delete(guid);
                    return;
                case "user":
                    await _Sdk.User.Delete(guid);
                    return;
                case "cred":
                    await _Sdk.Credential.Delete(guid);
                    return;
                case "pool":
                    await _Sdk.Pool.Delete(guid);
                    return;
                case "bucket":
                    await _Sdk.Bucket.Delete(guid);
                    return;
                case "enckey":
                    await _Sdk.EncryptionKey.Delete(guid);
                    return;
                case "mdrule":
                    await _Sdk.MetadataRule.Delete(guid);
                    return;
                case "embedrule":
                    await _Sdk.EmbeddingsRule.Delete(guid);
                    return;
                case "whevent":
                    return;
                case "whrule":
                    await _Sdk.WebhookRule.Delete(guid);
                    break;
                case "whtarget":
                    await _Sdk.WebhookTarget.Delete(guid);
                    break;
                case "vector":
                    await _Sdk.VectorRepository.Delete(guid);
                    return;
                case "graph":
                    await _Sdk.GraphRepository.Delete(guid);
                    return;
                case "collection":
                    await _Sdk.Collection.Delete(guid.ToString());
                    return;
                case "lock":
                    await _Sdk.ObjectLock.Delete(guid);
                    break;
                case "blob":
                    string blobGuid = guid.ToString();
                    await _Sdk.Blob.Delete(blobGuid);
                    return;
                case "permission":
                    await _Sdk.Permission.Delete(guid);
                    return;
                case "role":
                    await _Sdk.Role.Delete(guid.ToString());
                    return;
                case "rolepermmap":
                    await _Sdk.RolePermissionMap.Delete(guid);
                    return;
                case "userrole":
                    await _Sdk.UserRoleMap.Delete(guid);
                    return;
                case "modelconfig":
                    await _Sdk.ModelConfiguration.Delete(guid);
                    return;
                default:
                    return;
            }

            Console.WriteLine("");
        }

        private static async Task Exists(string type)
        {
            Guid guid = Inputty.GetGuid("GUID:", _TenantGuid);

            bool exists = false;

            switch (type)
            {
                case "node":
                    exists = await _Sdk.Node.Exists(guid);
                    break;
                case "tenant":
                    exists = await _Sdk.Tenant.Exists(guid);
                    break;
                case "user":
                    exists = await _Sdk.User.Exists(guid);
                    break;
                case "cred":
                    exists = await _Sdk.Credential.Exists(guid);
                    break;
                case "pool":
                    exists = await _Sdk.Pool.Exists(guid);
                    break;
                case "bucket":
                    exists = await _Sdk.Bucket.Exists(guid);
                    break;
                case "enckey":
                    exists = await _Sdk.EncryptionKey.Exists(guid);
                    break;
                case "mdrule":
                    exists = await _Sdk.MetadataRule.Exists(guid);
                    break;
                case "embedrule":
                    exists = await _Sdk.EmbeddingsRule.Exists(guid);
                    break;
                case "whevent":
                    exists = await _Sdk.WebhookEvent.Exists(guid);
                    break;
                case "whrule":
                    exists = await _Sdk.WebhookRule.Exists(guid);
                    break;
                case "whtarget":
                    exists = await _Sdk.WebhookTarget.Exists(guid);
                    break;
                case "vector":
                    exists = await _Sdk.VectorRepository.Exists(guid);
                    break;
                case "blob":
                    exists = await _Sdk.Blob.Exists(guid);
                    break;
                case "graph":
                    exists = await _Sdk.GraphRepository.Exists(guid);
                    break;
                case "lock":
                    break;
                case "permission":
                    exists = await _Sdk.Permission.Exists(guid);
                    break;
                case "role":
                    exists = await _Sdk.Role.Exists(guid.ToString());
                    break;
                case "rolepermmap":
                    exists = await _Sdk.RolePermissionMap.Exists(guid);
                    break;
                case "userrole":
                    exists = await _Sdk.UserRoleMap.Exists(guid);
                    break;
                case "modelconfig":
                    exists = await _Sdk.ModelConfiguration.Exists(guid);
                    break;
                default:
                    return;
            }

            Console.WriteLine("Exists: " + exists);
            Console.WriteLine("");
        }

        private static async Task Enumerate(string type)
        {
            switch (type)
            {
                case "node":
                    EnumerateResponse(await _Sdk.Node.Enumerate());
                    break;
                case "tenant":
                    EnumerateResponse(await _Sdk.Tenant.Enumerate());
                    break;
                case "collection":
                    EnumerateResponse(await _Sdk.Collection.Enumerate());
                    break;
                case "user":
                    EnumerateResponse(await _Sdk.User.Enumerate());
                    return;
                case "cred":
                    EnumerateResponse(await _Sdk.Credential.Enumerate());
                    return;
                case "enckey":
                    EnumerateResponse(await _Sdk.EncryptionKey.Enumerate());
                    return;
                case "mdrule":
                    EnumerateResponse(await _Sdk.MetadataRule.Enumerate());
                    return;
                case "embedrule":
                    EnumerateResponse(await _Sdk.EmbeddingsRule.Enumerate());
                    return;
                case "vector":
                    EnumerateResponse(await _Sdk.VectorRepository.Enumerate());
                    return;
                case "graph":
                    EnumerateResponse(await _Sdk.GraphRepository.Enumerate());
                    return;
                case "whevent":
                    EnumerateResponse(await _Sdk.WebhookEvent.Enumerate());
                    return;
                case "whrule":
                    EnumerateResponse(await _Sdk.WebhookRule.Enumerate());
                    break;
                case "whtarget":
                    EnumerateResponse(await _Sdk.WebhookTarget.Enumerate());
                    break;
                case "lock":
                    break;
                case "blob":
                    EnumerateResponse(await _Sdk.Blob.Enumerate());
                    break;
                case "permission":
                    EnumerateResponse(await _Sdk.Permission.Enumerate());
                    break;
                case "role":
                    EnumerateResponse(await _Sdk.Role.Enumerate());
                    break;
                case "rolepermmap":
                    EnumerateResponse(await _Sdk.RolePermissionMap.Enumerate());
                    break;
                case "userrole":
                    EnumerateResponse(await _Sdk.UserRoleMap.Enumerate());
                    break;
                case "modelconfig":
                    EnumerateResponse(await _Sdk.ModelConfiguration.Enumerate());
                    break;
                default:
                    return;
            }
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

        private static async Task TestRetrieveTenants()
        {
            Console.WriteLine("");
            string email = Inputty.GetString("Email    :", "default@user.com", false);
            _Sdk.Email = email; 
            _Sdk.Password = null;
            _Sdk.PasswordSha256 = null;
            _Sdk.TenantGUID = null;
            _Sdk.XToken = null;
            List<TenantMetadata> tenants = await _Sdk.Authentication.RetrieveTenants();
            EnumerateResponse(tenants);
        }

        private static async Task TestGenerateTokenWithPassword()
        {
            Console.WriteLine("");
            string email = Inputty.GetString("Email    :", "default@user.com", false);
            string password = Inputty.GetString("Password :", "password", true);
            Guid tenantGuid = Inputty.GetGuid("Tenant GUID :", _TenantGuid);
            _Sdk.Email = email;
            _Sdk.Password = password;
            _Sdk.PasswordSha256 = null;
            _Sdk.TenantGUID = tenantGuid;
            _Sdk.XToken = null;
            AuthenticationToken token = await _Sdk.Authentication.GenerateTokenWithPassword();
            EnumerateResponse(token);
        }

        private static async Task TestGenerateTokenWithPasswordSha256()
        {
            Console.WriteLine("");
            string email = Inputty.GetString("Email         :", "default@user.com", false);
            string passwordSha256 = Inputty.GetString("Password SHA256:", "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", true);
            Guid tenantGuid = Inputty.GetGuid("Tenant GUID    :", _TenantGuid);
            _Sdk.Email = email;
            _Sdk.Password = null;
            _Sdk.PasswordSha256 = passwordSha256;
            _Sdk.TenantGUID = tenantGuid;
            _Sdk.XToken = null;
            AuthenticationToken token = await _Sdk.Authentication.GenerateTokenWithPasswordSha256();
            EnumerateResponse(token);
        }

        private static async Task TestGenerateAdminTokenWithPassword()
        {
            Console.WriteLine("");
            string email = Inputty.GetString("Admin Email    :", "admin@view.io", false);
            string password = Inputty.GetString("Admin Password :", "viewadmin", true);
            _Sdk.Email = email;
            _Sdk.Password = password;
            _Sdk.PasswordSha256 = null;
            _Sdk.TenantGUID = null;
            _Sdk.XToken = null;
            AuthenticationToken token = await _Sdk.Authentication.GenerateAdminTokenWithPassword();
            EnumerateResponse(token);
        }

        private static async Task TestGenerateAdminTokenWithPasswordSha256()
        {
            Console.WriteLine("");
            string email = Inputty.GetString("Admin Email         :", "admin@view.io", false);
            string passwordSha256 = Inputty.GetString("Admin Password SHA256:", "e75255193871e245472533552fe45dfda25768d26e9eb92507e75263e90c6a48", true);
            _Sdk.Email = email;
            _Sdk.Password = null;
            _Sdk.PasswordSha256 = passwordSha256;
            _Sdk.TenantGUID = null;
            _Sdk.XToken = null;
            AuthenticationToken token = await _Sdk.Authentication.GenerateAdminTokenWithPasswordSha256();
            EnumerateResponse(token);
        }

        private static async Task TestValidateToken()
        {
            Console.WriteLine("");
            string authToken = Inputty.GetString("Auth Token :", _XToken, true);
            _Sdk.Email = null;
            _Sdk.Password = null;
            _Sdk.PasswordSha256 = null;
            _Sdk.TenantGUID = null;
            _Sdk.XToken = authToken;
            AuthenticationToken token = await _Sdk.Authentication.ValidateToken();
            EnumerateResponse(token);
        }

        private static async Task TestRetrieveTokenDetails()
        {
            Console.WriteLine("");
            string authToken = Inputty.GetString("Auth Token :", _XToken, true);
            _Sdk.Email = null;
            _Sdk.Password = null;
            _Sdk.PasswordSha256 = null;
            _Sdk.TenantGUID = null;
            _Sdk.XToken = authToken;
            AuthenticationToken token = await _Sdk.Authentication.RetrieveTokenDetails();
            EnumerateResponse(token);
        }

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
}