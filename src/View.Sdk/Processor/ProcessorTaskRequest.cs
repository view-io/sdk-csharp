namespace View.Sdk.Processor
{
    using System.Xml.Linq;
    using System;

    /// <summary>
    /// Processor task request.
    /// </summary>
    public class ProcessorTaskRequest
    {
        #region Public-Members

        /// <summary>
        /// Processor request GUID.
        /// </summary>
        public Guid GUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Processor task type.
        /// </summary>
        public ProcessorTaskTypeEnum Type { get; set; } = ProcessorTaskTypeEnum.TypeDetection;

        /// <summary>
        /// Input file type.
        /// </summary>
        public Type InputFileType { get; set; } = typeof(object);

        /// <summary>
        /// Input file handle.
        /// </summary>
        public string InputFile { get; set; } = null;

        /// <summary>
        /// Output file type.
        /// </summary>
        public Type OutputFileType { get; set; } = typeof(object);

        /// <summary>
        /// Output file handle.
        /// </summary>
        public string OutputFile { get; set; } = null;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public ProcessorTaskRequest()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
