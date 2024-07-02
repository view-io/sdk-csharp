namespace View.Sdk
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// Data flow step.
    /// </summary>
    public class StepMetadata
    {
        #region Public-Members

        /// <summary>
        /// GUID.
        /// </summary>
        public string GUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public string TenantGUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; } = "My data flow step";

        /// <summary>
        /// GUID of the step to invoke when successful.
        /// </summary>
        public string Success { get; set; } = null;

        /// <summary>
        /// GUID of the step to invoke when failed.
        /// </summary>
        public string Failure { get; set; } = null;

        /// <summary>
        /// GUID of the step to invoke the event of an exception.
        /// </summary>
        public string Exception { get; set; } = null;

        /// <summary>
        /// Runtime for the step.
        /// </summary>
        public StepRuntimeEnum Runtime { get; set; } = StepRuntimeEnum.Dotnet8;

        /// <summary>
        /// Archive filename, in ZIP format.
        /// </summary>
        public string StepArchiveFilename
        {
            get
            {
                return _StepArchiveFilename;
            }
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(StepArchiveFilename));
                _StepArchiveFilename = value;
            }
        }

        /// <summary>
        /// Filename to open to access the entrypoint.
        /// </summary>
        public string StepEntrypointFilename
        {
            get
            {
                return _StepEntrypointFilename;
            }
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(StepEntrypointFilename));
                _StepEntrypointFilename = value;
            }
        }

        /// <summary>
        /// Type (e.g. class) to open to access the entrypoint.
        /// </summary>
        public string StepEntrypointType
        {
            get
            {
                return _StepEntrypointType;
            }
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(StepEntrypointType));
                _StepEntrypointType = value;
            }
        }

        /// <summary>
        /// MD5.
        /// </summary>
        public string MD5Hash { get; set; } = null;

        /// <summary>
        /// SHA1.
        /// </summary>
        public string SHA1Hash { get; set; } = null;

        /// <summary>
        /// SHA256.
        /// </summary>
        public string SHA256Hash { get; set; } = null;

        /// <summary>
        /// Notes.
        /// </summary>
        public string Notes { get; set; } = null;

        /// <summary>
        /// Enable or disable assembly load debugging.
        /// </summary>
        public bool DebugAssemblyLoad { get; set; } = false;

        /// <summary>
        /// Virtual environment name.
        /// Used for Python steps.
        /// </summary>
        public string VirtualEnvironment { get; set; } = null;

        /// <summary>
        /// Dependencies file.
        /// Used for Python steps, generally set to requirements.txt.
        /// </summary>
        public string DependenciesFile { get; set; } = null;

        /// <summary>
        /// Creation timestamp, in UTC time.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Package file (zip) contents.
        /// </summary>
        public byte[] Package { get; set; } = null;

        /// <summary>
        /// Step to execute when successful.
        /// </summary>
        public StepMetadata SuccessStep { get; set; } = null;

        /// <summary>
        /// Step to execute when failed.
        /// </summary>
        public StepMetadata FailureStep { get; set; } = null;

        /// <summary>
        /// Step to execute when an exception is encountered.
        /// </summary>
        public StepMetadata ExceptionStep { get; set; } = null;

        #endregion

        #region Private-Members

        private string _StepArchiveFilename = null;
        private string _StepEntrypointFilename = null;
        private string _StepEntrypointType = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public StepMetadata()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    }
}
