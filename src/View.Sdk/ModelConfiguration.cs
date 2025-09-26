namespace View.Sdk
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Model configuration.
    /// </summary>
    public class ModelConfiguration
    {
        #region Public-Members

        /// <summary>
        /// GUID.
        /// </summary>
        public Guid GUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Tenant GUID.
        /// </summary>
        public Guid TenantGUID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Name.
        /// </summary>
        public string ModelName { get; set; } = "owner/modelname";

        /// <summary>
        /// Model is capable of being used for embeddings.
        /// </summary>
        public bool Embeddings { get; set; } = true;

        /// <summary>
        /// Model is capable of being used for embeddings.
        /// </summary>
        public bool Completions { get; set; } = true;

        /// <summary>
        /// Gets or sets the context size (input tokens) for the model.
        /// Default: 4096. Valid range: 1 to Int32.Max.
        /// </summary>
        public int ContextSize
        {
            get => _ContextSize;
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(ContextSize));
                _ContextSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum output tokens the model can generate.
        /// Default: 4096. Valid range: 1 to 2,000,000.
        /// </summary>
        public int MaxOutputTokens
        {
            get => _MaxOutputTokens;
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(MaxOutputTokens));
                _MaxOutputTokens = value;
            }
        }

        /// <summary>
        /// Gets or sets the temperature for output randomness.
        /// Default: 0.2. Valid range: 0.0 to 2.0.
        /// Lower values make output more deterministic, higher values more creative.
        /// </summary>
        public double Temperature
        {
            get => _Temperature;
            set
            {
                if (value < 0.0 || value > 2.0) throw new ArgumentOutOfRangeException(nameof(Temperature));
                _Temperature = value;
            }
        }

        /// <summary>
        /// Gets or sets the nucleus sampling parameter.
        /// Default: 1.0. Valid range: 0.0 to 1.0.
        /// Controls diversity via nucleus sampling: 0.5 means half of all likelihood-weighted options are considered.
        /// </summary>
        public double TopP
        {
            get => _TopP;
            set
            {
                if (value < 0.0 || value > 1.0) throw new ArgumentOutOfRangeException(nameof(TopP));
                _TopP = value;
            }
        }

        /// <summary>
        /// Gets or sets the top-k sampling parameter.
        /// Default: 40. Valid range: 1 to 100.
        /// Limits the number of highest probability vocabulary tokens to keep for top-k filtering.
        /// </summary>
        public int TopK
        {
            get => _TopK;
            set
            {
                if (value < 1 || value > 100) throw new ArgumentOutOfRangeException(nameof(TopK));
                _TopK = value;
            }
        }

        /// <summary>
        /// Gets or sets the frequency penalty to reduce repetition.
        /// Default: 0.0. Valid range: -2.0 to 2.0.
        /// Positive values decrease the likelihood of repeating tokens based on their frequency.
        /// </summary>
        public double FrequencyPenalty
        {
            get => _FrequencyPenalty;
            set
            {
                if (value < -2.0 || value > 2.0) throw new ArgumentOutOfRangeException(nameof(FrequencyPenalty));
                _FrequencyPenalty = value;
            }
        }

        /// <summary>
        /// Gets or sets the presence penalty to encourage topic diversity.
        /// Default: 0.0. Valid range: -2.0 to 2.0.
        /// Positive values decrease the likelihood of repeating any tokens that have appeared.
        /// </summary>
        public double PresencePenalty
        {
            get => _PresencePenalty;
            set
            {
                if (value < -2.0 || value > 2.0) throw new ArgumentOutOfRangeException(nameof(PresencePenalty));
                _PresencePenalty = value;
            }
        }

        /// <summary>
        /// Gets or sets whether streaming mode is enabled for this model.
        /// </summary>
        public bool EnableStreaming { get; set; } = true;

        /// <summary>
        /// Request timeout.  Default is 30000 (30 seconds).
        /// </summary>
        public int TimeoutMs
        {
            get => _TimeoutMs;
            set
            {
                if (value > 1000) _TimeoutMs = value;
                else throw new ArgumentOutOfRangeException(nameof(TimeoutMs));
            }
        }

        /// <summary>
        /// Additional data.
        /// </summary>
        public string AdditionalData { get; set; } = null;

        /// <summary>
        /// Active.
        /// </summary>
        public bool Active { get; set; } = true;

        /// <summary>
        /// Created.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        #endregion

        #region Private-Members

        private int _ContextSize = 4096;
        private int _MaxOutputTokens = 4096;
        private double _Temperature = 0.2;
        private double _TopP = 1.0;
        private int _TopK = 40;
        private double _FrequencyPenalty = 0;
        private double _PresencePenalty = 0;
        private int _TimeoutMs = 30000;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Model configuration.
        /// </summary>
        public ModelConfiguration()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
