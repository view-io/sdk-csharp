namespace Test.Storage
{
    using GetSomeInput;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using View.Sdk;
    using View.Sdk.Storage;
    using Serializer = View.Sdk.Serialization.Serializer;
    using SeverityEnum = View.Sdk.SeverityEnum;

    public static class Program
    {
        private static bool _RunForever = true;
        private static Guid _TenantGuid = default(Guid);
        private static string _Endpoint = "http://view.homedns.org:8001/";
        private static string _AccessKey = "default";
        private static ViewStorageSdk _Sdk = null!;
        private static Serializer _Serializer = new Serializer();
        private static bool _EnableLogging = true;

        public static void Main(string[] args)
        {
            _TenantGuid = Inputty.GetGuid("Tenant GUID :", _TenantGuid);
            _Endpoint = Inputty.GetString("Endpoint    :", _Endpoint, false);
            _AccessKey = Inputty.GetString("Access key  :", _AccessKey, false);
            _Sdk = new ViewStorageSdk(_TenantGuid, _AccessKey, _Endpoint);
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

                    // Bucket commands
                    case "create bucket":
                        CreateBucket().Wait();
                        break;
                    case "read bucket":
                        RetrieveBucket().Wait();
                        break;
                    case "retrieve bucket metadata":
                        RetrieveBucket().Wait();
                        break;
                    case "read buckets":
                        RetrieveBuckets().Wait();
                        break;
                    case "update bucket":
                        UpdateBucket().Wait();
                        break;
                    case "delete bucket":
                        DeleteBucket().Wait();
                        break;
                    case "bucket stats":
                        GetBucketStatistics().Wait();
                        break;
                    case "create bucket acl":
                        CreateBucketACL().Wait();
                        break;
                    case "retrieve bucket acl":
                        RetrieveBucketACL().Wait();
                        break;
                    case "delete bucket acl":
                        DeleteBucketACL().Wait();
                        break;
                    case "create bucket tag":
                        CreateBucketTag().Wait();
                        break;
                    case "retrieve bucket tags":
                        RetrieveBucketTags().Wait();
                        break;
                    case "delete bucket tag":
                        DeleteBucketTag().Wait();
                        break;
                    case "enumerate bucket":
                        EnumerateBucket().Wait();
                        break;
                    case "create object expiration":
                        CreateObjectExpiration().Wait();
                        break;

                    // Object commands
                    case "create object":
                        CreateNonChunkedObject().Wait();
                        break;
                    case "read object":
                        RetrieveObject().Wait();
                        break;
                    case "read object metadata":
                        RetrieveObjectMetadata().Wait();
                        break;
                    case "delete object":
                        DeleteObject().Wait();
                        break;
                    case "exists object":
                        ObjectExists().Wait();
                        break;
                    case "create chunked object":
                        CreateChunkedObject().Wait();
                        break;
                    case "retrieve range":
                        RetrieveObjectRange().Wait();
                        break;
                    case "create object acl":
                        CreateObjectACL().Wait();
                        break;
                    case "retrieve object acl":
                        RetrieveObjectACL().Wait();
                        break;
                    case "delete object acl":
                        DeleteObjectACL().Wait();
                        break;
                    case "create object tags":
                        CreateObjectTags().Wait();
                        break;
                    case "retrieve object tags":
                        RetrieveObjectTags().Wait();
                        break;
                    case "delete object tags":
                        DeleteObjectTags().Wait();
                        break;
                    case "reprocess object":
                        ReprocessObject().Wait();
                        break;

                    // Multipart upload commands
                    case "create upload":
                        CreateMultipartUpload().Wait();
                        break;
                    case "upload part":
                        UploadPart().Wait();
                        break;
                    case "complete upload":
                        CompleteUpload().Wait();
                        break;
                    case "retrieve upload":
                        RetrieveUpload().Wait();
                        break;
                    case "retrieve uploads":
                        RetrieveUploads().Wait();
                        break;
                    case "delete upload":
                        DeleteUpload().Wait();
                        break;
                    case "retrieve part":
                        RetrievePart().Wait();
                        break;
                    case "delete part":
                        DeletePart().Wait();
                        break;
                    
                    default:
                        Console.WriteLine("Unknown command");
                        break;
                }
            }
        }

        private static void Menu()
        {
            Console.WriteLine("Available commands:\n");

            Console.WriteLine("General commands:");
            Console.WriteLine("  {0,-24} {1}", "q", "Quit");
            Console.WriteLine("  {0,-24} {1}", "?", "Help menu");
            Console.WriteLine("  {0,-24} {1}", "cls", "Clear screen");
            Console.WriteLine();

            Console.WriteLine("Bucket commands:");
            Console.WriteLine("  {0,-24} {1}", "create bucket", "Create a new bucket");
            Console.WriteLine("  {0,-24} {1}", "read bucket", "Retrieve a bucket by GUID");
            Console.WriteLine("  {0,-24} {1}", "retrieve bucket metadata", "Retrieve a bucket's metadata");
            Console.WriteLine("  {0,-24} {1}", "read buckets", "Retrieve all buckets");
            Console.WriteLine("  {0,-24} {1}", "update bucket", "Update a bucket");
            Console.WriteLine("  {0,-24} {1}", "delete bucket", "Delete a bucket");
            Console.WriteLine("  {0,-24} {1}", "bucket stats", "Get bucket statistics");
            Console.WriteLine("  {0,-24} {1}", "enumerate bucket", "List bucket contents");
            Console.WriteLine();

            Console.WriteLine("Bucket ACL commands:");
            Console.WriteLine("  {0,-24} {1}", "create bucket acl", "Create bucket ACL");
            Console.WriteLine("  {0,-24} {1}", "retrieve bucket acl", "Retrieve bucket ACL");
            Console.WriteLine("  {0,-24} {1}", "delete bucket acl", "Delete bucket ACL");
            Console.WriteLine();

            Console.WriteLine("Bucket Tag commands:");
            Console.WriteLine("  {0,-24} {1}", "create bucket tag", "Create bucket tag");
            Console.WriteLine("  {0,-24} {1}", "retrieve bucket tags", "Retrieve bucket tags");
            Console.WriteLine("  {0,-24} {1}", "delete bucket tag", "Delete bucket tag");
            Console.WriteLine();

            Console.WriteLine("Object commands:");
            Console.WriteLine("  {0,-24} {1}", "create object", "Create a non chunked object");
            Console.WriteLine("  {0,-24} {1}", "create chunked object", "Create a chunked object");
            Console.WriteLine("  {0,-24} {1}", "create object expiration", "Create an object with expiration");
            Console.WriteLine("  {0,-24} {1}", "read object", "Retrieve an object");
            Console.WriteLine("  {0,-24} {1}", "read object metadata", "Retrieve object metadata");
            Console.WriteLine("  {0,-24} {1}", "retrieve range", "Retrieve a byte range from an object");
            Console.WriteLine("  {0,-24} {1}", "delete object", "Delete an object");
            Console.WriteLine("  {0,-24} {1}", "exists object", "Check if an object exists");
            Console.WriteLine("  {0,-24} {1}", "reprocess object", "Trigger reprocessing of an object");
            Console.WriteLine();

            Console.WriteLine("Object ACL commands:");
            Console.WriteLine("  {0,-24} {1}", "create object acl", "Create object ACL");
            Console.WriteLine("  {0,-24} {1}", "retrieve object acl", "Retrieve object ACL");
            Console.WriteLine("  {0,-24} {1}", "delete object acl", "Delete object ACL");
            Console.WriteLine();

            Console.WriteLine("Object Tag commands:");
            Console.WriteLine("  {0,-24} {1}", "create object tags", "Create object tags");
            Console.WriteLine("  {0,-24} {1}", "retrieve object tags", "Retrieve object tags");
            Console.WriteLine("  {0,-24} {1}", "delete object tags", "Delete object tags");
            Console.WriteLine();

            Console.WriteLine("Multipart upload commands:");
            Console.WriteLine("  {0,-24} {1}", "create upload", "Create a multipart upload");
            Console.WriteLine("  {0,-24} {1}", "upload part", "Upload a part to a multipart upload");
            Console.WriteLine("  {0,-24} {1}", "complete upload", "Complete a multipart upload");
            Console.WriteLine("  {0,-24} {1}", "retrieve upload", "Retrieve a multipart upload");
            Console.WriteLine("  {0,-24} {1}", "retrieve uploads", "Retrieve all multipart uploads for a bucket");
            Console.WriteLine("  {0,-24} {1}", "delete upload", "Delete a multipart upload");
            Console.WriteLine("  {0,-24} {1}", "retrieve part", "Retrieve a part from a multipart upload");
            Console.WriteLine("  {0,-24} {1}", "delete part", "Delete a part from a multipart upload");
            Console.WriteLine();
        }

        private static void EmitLogMessage(SeverityEnum severity, string msg)
        {
            Console.WriteLine(severity.ToString() + ": " + msg);
        }

        #region Bucket-Operations

        private static async Task CreateBucket()
        {
            BucketMetadata bucket = BuildObject<BucketMetadata>();
            EnumerateResponse(await _Sdk.Bucket.Create(bucket));
        }

        private static async Task RetrieveBucket()
        {
            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            EnumerateResponse(await _Sdk.Bucket.RetrieveMetadata(bucketGuid.ToString()));
        }

        private static async Task RetrieveBuckets()
        {
            Console.WriteLine("Retrieving all buckets...");
            EnumerateResponse(await _Sdk.Bucket.RetrieveMany());
        }

        private static async Task UpdateBucket()
        {
            BucketMetadata bucket = BuildObject<BucketMetadata>();
            EnumerateResponse(await _Sdk.Bucket.Update(bucket));
        }

        private static async Task DeleteBucket()
        {
            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            await _Sdk.Bucket.Delete(bucketGuid.ToString());
        }

        private static async Task GetBucketStatistics()
        {
            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            EnumerateResponse(await _Sdk.Bucket.RetrieveStatistics(bucketGuid.ToString()));
        }

        #endregion

        #region Object-Operations

        private static async Task CreateNonChunkedObject()
        {

            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            string objectKey = Inputty.GetString("Object key:", "test-object", false);
            string data = Inputty.GetString("data", "Hello, World!", false);
            EnumerateResponse(await _Sdk.Object.CreateNonChunked(bucketGuid.ToString(), objectKey, data));
        }

        private static async Task RetrieveObject()
        {
            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            string objectKey = Inputty.GetString("Object key:", "test-object", false);
            EnumerateResponse(await _Sdk.Object.Retrieve(bucketGuid.ToString(), objectKey));
        }

        private static async Task RetrieveObjectMetadata()
        {
            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            string objectKey = Inputty.GetString("Object key:", "test-object", false);
            EnumerateResponse(await _Sdk.Object.RetrieveMetadata(bucketGuid.ToString(), objectKey));
        }

        private static async Task DeleteObject()
        {
            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            string objectKey = Inputty.GetString("Object key:", "test-object", false);
            await _Sdk.Object.Delete(bucketGuid.ToString(), objectKey);
        }

        private static async Task ObjectExists()
        {
            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            string objectKey = Inputty.GetString("Object key:", "test-object", false);

            bool exists = await _Sdk.Object.Exists(bucketGuid.ToString(), objectKey);
            Console.WriteLine($"Object exists: {exists}");
        }

        #endregion

        #region Multipart-Upload-Operations

        private static async Task CreateMultipartUpload()
        {
            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            MultipartUploadMetadata metadata = BuildObject<MultipartUploadMetadata>();
            EnumerateResponse(await _Sdk.MultipartUpload.Create(bucketGuid.ToString(), metadata));
        }

        private static async Task UploadPart()
        {
            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            string objectKey = Inputty.GetString("Object key:", "test-object", false);
            int partNumber = Inputty.GetInteger("Part number:", 1, true, false);
            string data = Inputty.GetString("Data:", "Part data content", false);
            EnumerateResponse(await _Sdk.MultipartUpload.UploadPart(bucketGuid.ToString(), objectKey, partNumber, data));
        }

        private static async Task CompleteUpload()
        {
            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            string objectKey = Inputty.GetString("Object key:", "test-object", false);
            string data = Inputty.GetString("Completion data:", null, false);
            EnumerateResponse(await _Sdk.MultipartUpload.CompleteUpload(bucketGuid.ToString(), objectKey, data));
        }

        private static async Task RetrieveUpload()
        {
            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            string objectKey = Inputty.GetString("Object key:", "test-object", false);
            EnumerateResponse(await _Sdk.MultipartUpload.Retrieve(bucketGuid.ToString(), objectKey));
        }

        private static async Task RetrieveUploads()
        {
            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            EnumerateResponse(await _Sdk.MultipartUpload.RetrieveMany(bucketGuid.ToString()));
        }

        private static async Task DeleteUpload()
        {
            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            string objectKey = Inputty.GetString("Object key:", "test-object", false);
            bool result = await _Sdk.MultipartUpload.DeleteUpload(bucketGuid.ToString(), objectKey);
            Console.WriteLine($"Delete upload result: {result}");
        }

        private static async Task RetrievePart()
        {
            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            string objectKey = Inputty.GetString("Object key:", "test-object", false);
            int partNumber = Inputty.GetInteger("Part number:", 1, true, false);
            EnumerateResponse(await _Sdk.MultipartUpload.RetrievePart(bucketGuid.ToString(), objectKey, partNumber));
        }

        private static async Task DeletePart()
        {
            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            string objectKey = Inputty.GetString("Object key:", "test-object", false);
            int partNumber = Inputty.GetInteger("Part number:", 1, true, false);
            bool result = await _Sdk.MultipartUpload.DeletePart(bucketGuid.ToString(), objectKey, partNumber);
            Console.WriteLine($"Delete part result: {result}");
        }

        #region Object-Additional-Operations

        private static async Task CreateChunkedObject()
        {
            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            string objectKey = Inputty.GetString("Object key:", "test-object", false);
            string data = Inputty.GetString("data", "Hello, World!", false);
            EnumerateResponse(await _Sdk.Object.CreateChunked(bucketGuid.ToString(), objectKey, data));
        }

        private static async Task RetrieveObjectRange()
        {
            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            string objectKey = Inputty.GetString("Object key:", "test-object", false);
            int startByte = Inputty.GetInteger("Start byte:", 1,true, true);
            int endByte = Inputty.GetInteger("End byte:", 3, true, true);
            EnumerateResponse(await _Sdk.Object.RetrieveRange(bucketGuid.ToString(), objectKey, startByte, endByte));
        }

        private static async Task CreateObjectACL()
        {
            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            string objectKey = Inputty.GetString("Object key:", "test-object", false);
            BucketAcl acl = BuildObject<BucketAcl>();
            EnumerateResponse(await _Sdk.Object.CreateACL(bucketGuid.ToString(), objectKey, acl));
        }

        private static async Task RetrieveObjectACL()
        {
            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            string objectKey = Inputty.GetString("Object key:", "test-object", false);
            EnumerateResponse(await _Sdk.Object.RetrieveACL(bucketGuid.ToString(), objectKey));
        }

        private static async Task DeleteObjectACL()
        {
            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            string objectKey = Inputty.GetString("Object key:", "test-object", false);
            bool result = await _Sdk.Object.DeleteACL(bucketGuid.ToString(), objectKey);
            Console.WriteLine($"Delete object ACL result: {result}");
        }

        private static async Task CreateObjectTags()
        {
            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            string objectKey = Inputty.GetString("Object key:", "test-object", false);
            List<BucketTag> tags = BuildObject<List<BucketTag>>();
            EnumerateResponse(await _Sdk.Object.CreateTags(bucketGuid.ToString(), objectKey, tags));
        }

        private static async Task RetrieveObjectTags()
        {
            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            string objectKey = Inputty.GetString("Object key:", "test-object", false);
            EnumerateResponse(await _Sdk.Object.RetrieveTags(bucketGuid.ToString(), objectKey));
        }

        private static async Task DeleteObjectTags()
        {
            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            string objectKey = Inputty.GetString("Object key:", "test-object", false);
            bool result = await _Sdk.Object.DeleteTags(bucketGuid.ToString(), objectKey);
            Console.WriteLine($"Delete object tags result: {result}");
        }

        private static async Task ReprocessObject()
        {
            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            string objectKey = Inputty.GetString("Object key:", "test-object", false);
            bool result = await _Sdk.Object.Reprocess(bucketGuid.ToString(), objectKey);
            Console.WriteLine($"Reprocess object result: {result}");
        }

        private static async Task CreateObjectExpiration()
        {
            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            string objectKey = Inputty.GetString("Object key:", "test-object", false);
            Expiration expiration = BuildObject<Expiration>();
            EnumerateResponse(await _Sdk.Object.CreateExpiration(bucketGuid.ToString(), objectKey, expiration));
        }

        #endregion

        #region Bucket-Additional-Operations

        private static async Task CreateBucketACL()
        {
            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            BucketAcl acl = BuildObject<BucketAcl>();
            EnumerateResponse(await _Sdk.Bucket.CreateACL(bucketGuid.ToString(), acl));
        }

        private static async Task RetrieveBucketACL()
        {
            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            EnumerateResponse(await _Sdk.Bucket.RetrieveACL(bucketGuid.ToString()));
        }

        private static async Task DeleteBucketACL()
        {
            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            bool result = await _Sdk.Bucket.DeleteACL(bucketGuid.ToString());
            Console.WriteLine($"Delete bucket ACL result: {result}");
        }

        private static async Task CreateBucketTag()
        {
            List<BucketTag> tags = BuildObject<List<BucketTag>>();
            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            EnumerateResponse(await _Sdk.Bucket.CreateTag(bucketGuid, tags));
        }

        private static async Task RetrieveBucketTags()
        {
            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            EnumerateResponse(await _Sdk.Bucket.RetrieveTags(bucketGuid.ToString()));
        }

        private static async Task DeleteBucketTag()
        {
            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            bool result = await _Sdk.Bucket.DeleteTag(bucketGuid.ToString());
            Console.WriteLine($"Delete bucket tag result: {result}");
        }

        private static async Task EnumerateBucket()
        {
            Guid bucketGuid = Inputty.GetGuid("Bucket GUID: ", default(Guid));
            int maxKeys = Inputty.GetInteger("Max keys:", 5, true, false);
            EnumerateResponse(await _Sdk.Bucket.Enumerate(bucketGuid.ToString(), maxKeys));
        }

        #endregion

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

        #endregion
    }
}