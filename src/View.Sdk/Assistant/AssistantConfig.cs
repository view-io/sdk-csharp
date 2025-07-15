namespace View.Sdk.Assistant
{
    using System;

    /// <summary>
    /// Assistant configuration.
    /// </summary>
    public class AssistantConfig
    {
        #region Public-Members

        /// <summary>
        /// GUID.
        /// </summary>
        public Guid GUID { get; set; } = Guid.Empty;

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; } = null;

        /// <summary>
        /// Description.
        /// </summary>
        public string Description { get; set; } = null;

        /// <summary>
        /// System prompt.
        /// </summary>
        public string SystemPrompt { get; set; } = null;

        /// <summary>
        /// Embedding model.
        /// </summary>
        public string EmbeddingModel { get; set; } = null;

        /// <summary>
        /// Maximum results.
        /// </summary>
        public int MaxResults { get; set; } = 20;

        /// <summary>
        /// Vector database name.
        /// </summary>
        public string VectorDatabaseName { get; set; } = null;

        /// <summary>
        /// Vector database table.
        /// </summary>
        public string VectorDatabaseTable { get; set; } = null;

        /// <summary>
        /// Vector database hostname.
        /// </summary>
        public string VectorDatabaseHostname { get; set; } = null;

        /// <summary>
        /// Vector database port.
        /// </summary>
        public int VectorDatabasePort { get; set; } = 5432;

        /// <summary>
        /// Vector database user.
        /// </summary>
        public string VectorDatabaseUser { get; set; } = null;

        /// <summary>
        /// Vector database password.
        /// </summary>
        public string VectorDatabasePassword { get; set; } = null;

        /// <summary>
        /// Generation provider.
        /// </summary>
        public string GenerationProvider { get; set; } = null;

        /// <summary>
        /// Generation API key.
        /// </summary>
        public string GenerationApiKey { get; set; } = null;

        /// <summary>
        /// Generation model.
        /// </summary>
        public string GenerationModel { get; set; } = null;

        /// <summary>
        /// HuggingFace API key.
        /// </summary>
        public string HuggingFaceApiKey { get; set; } = null;

        /// <summary>
        /// Temperature.
        /// </summary>
        public double Temperature { get; set; } = 0.1;

        /// <summary>
        /// Top P.
        /// </summary>
        public double TopP { get; set; } = 0.95;

        /// <summary>
        /// Maximum tokens.
        /// </summary>
        public int MaxTokens { get; set; } = 500;

        /// <summary>
        /// Ollama hostname.
        /// </summary>
        public string OllamaHostname { get; set; } = null;

        /// <summary>
        /// Ollama port.
        /// </summary>
        public int OllamaPort { get; set; } = 11434;

        /// <summary>
        /// Ollama context length.
        /// </summary>
        public int? OllamaContextLength { get; set; } = null;

        /// <summary>
        /// Context sort.
        /// </summary>
        public bool ContextSort { get; set; } = false;

        /// <summary>
        /// Sort by max similarity.
        /// </summary>
        public bool SortByMaxSimilarity { get; set; } = true;

        /// <summary>
        /// Context scope.
        /// </summary>
        public int ContextScope { get; set; } = 0;

        /// <summary>
        /// Rerank.
        /// </summary>
        public bool Rerank { get; set; } = false;

        /// <summary>
        /// Rerank model.
        /// </summary>
        public string RerankModel { get; set; } = null;

        /// <summary>
        /// Rerank top K.
        /// </summary>
        public int RerankTopK { get; set; } = 3;

        /// <summary>
        /// Rerank level.
        /// </summary>
        public string RerankLevel { get; set; } = "Chunk";

        /// <summary>
        /// Timestamp enabled.
        /// </summary>
        public bool TimestampEnabled { get; set; } = true;

        /// <summary>
        /// Timestamp format.
        /// </summary>
        public string TimestampFormat { get; set; } = "Date";

        /// <summary>
        /// Timestamp timezone.
        /// </summary>
        public string TimestampTimezone { get; set; } = "UTC";

        /// <summary>
        /// Use citations.
        /// </summary>
        public bool UseCitations { get; set; } = false;

        /// <summary>
        /// Query classification.
        /// </summary>
        public QueryClassification QueryClassification { get; set; } = new QueryClassification();

        /// <summary>
        /// Query rewriting.
        /// </summary>
        public QueryRewriting QueryRewriting { get; set; } = new QueryRewriting();

        /// <summary>
        /// Created UTC.
        /// </summary>
        public DateTime CreatedUTC { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Last modified UTC.
        /// </summary>
        public DateTime LastModifiedUTC { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Chat only.
        /// </summary>
        public bool ChatOnly { get; set; } = false;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public AssistantConfig()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }

    /// <summary>
    /// Query classification.
    /// </summary>
    public class QueryClassification
    {
        /// <summary>
        /// Enabled.
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Confidence threshold.
        /// </summary>
        public double ConfidenceThreshold { get; set; } = 0.85;

        /// <summary>
        /// Model name.
        /// </summary>
        public string ModelName { get; set; } = null;
    }

    /// <summary>
    /// Query rewriting.
    /// </summary>
    public class QueryRewriting
    {
        /// <summary>
        /// Enabled.
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Context history depth.
        /// </summary>
        public int ContextHistoryDepth { get; set; } = 3;

        /// <summary>
        /// Model name.
        /// </summary>
        public string ModelName { get; set; } = null;
    }
}