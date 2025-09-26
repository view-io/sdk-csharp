namespace View.Sdk.Completions.Providers.Ollama
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Ollama-specific options for fine-tuning completion generation.
    /// </summary>
    public class OllamaOptions
    {
        /// <summary>
        /// Temperature for sampling. Range: 0.0 to 2.0. Default is null.
        /// Higher values produce more random output, lower values are more deterministic.
        /// </summary>
        [JsonPropertyName("temperature")]
        public double? Temperature
        {
            get => _Temperature;
            set
            {
                if (value.HasValue && (value < 0.0 || value > 2.0))
                    throw new ArgumentOutOfRangeException(nameof(Temperature), "Temperature must be between 0.0 and 2.0");
                _Temperature = value;
            }
        }

        /// <summary>
        /// Top-p (nucleus) sampling. Range: 0.0 to 1.0. Default is null.
        /// Consider tokens with cumulative probability of top_p.
        /// </summary>
        [JsonPropertyName("top_p")]
        public double? TopP
        {
            get => _TopP;
            set
            {
                if (value.HasValue && (value < 0.0 || value > 1.0))
                    throw new ArgumentOutOfRangeException(nameof(TopP), "TopP must be between 0.0 and 1.0");
                _TopP = value;
            }
        }

        /// <summary>
        /// Top-k sampling. Range: 1 to 1000. Default is null.
        /// Only sample from the top K tokens.
        /// </summary>
        [JsonPropertyName("top_k")]
        public int? TopK
        {
            get => _TopK;
            set
            {
                if (value.HasValue && (value < 1 || value > 1000))
                    throw new ArgumentOutOfRangeException(nameof(TopK), "TopK must be between 1 and 1000");
                _TopK = value;
            }
        }

        /// <summary>
        /// Maximum number of tokens to predict. Range: 1 to 128000. Default is null.
        /// </summary>
        [JsonPropertyName("num_predict")]
        public int? NumPredict
        {
            get => _NumPredict;
            set
            {
                if (value.HasValue && (value < 1 || value > 128000))
                    throw new ArgumentOutOfRangeException(nameof(NumPredict), "NumPredict must be between 1 and 128000");
                _NumPredict = value;
            }
        }

        /// <summary>
        /// Size of the context window. Range: 128 to 128000. Default is null.
        /// </summary>
        [JsonPropertyName("num_ctx")]
        public int? NumCtx
        {
            get => _NumCtx;
            set
            {
                if (value.HasValue && (value < 128 || value > 128000))
                    throw new ArgumentOutOfRangeException(nameof(NumCtx), "NumCtx must be between 128 and 128000");
                _NumCtx = value;
            }
        }

        /// <summary>
        /// Number of tokens to keep from the initial prompt. Range: 0 to context size. Default is null.
        /// </summary>
        [JsonPropertyName("num_keep")]
        public int? NumKeep
        {
            get => _NumKeep;
            set
            {
                if (value.HasValue && value < 0)
                    throw new ArgumentOutOfRangeException(nameof(NumKeep), "NumKeep must be 0 or greater");
                _NumKeep = value;
            }
        }

        /// <summary>
        /// Random seed for deterministic generation. Any valid int32 value. Default is null.
        /// </summary>
        [JsonPropertyName("seed")]
        public int? Seed { get; set; } = null;

        /// <summary>
        /// Penalty for repeating tokens. Range: 0.0 to 2.0. Default is null.
        /// Higher values discourage repetition.
        /// </summary>
        [JsonPropertyName("repeat_penalty")]
        public double? RepeatPenalty
        {
            get => _RepeatPenalty;
            set
            {
                if (value.HasValue && (value < 0.0 || value > 2.0))
                    throw new ArgumentOutOfRangeException(nameof(RepeatPenalty), "RepeatPenalty must be between 0.0 and 2.0");
                _RepeatPenalty = value;
            }
        }

        /// <summary>
        /// Presence penalty. Range: -2.0 to 2.0. Default is null.
        /// Positive values penalize tokens based on whether they appear in the text so far.
        /// </summary>
        [JsonPropertyName("penalize_presence")]
        public double? PenalizePresence
        {
            get => _PenalizePresence;
            set
            {
                if (value.HasValue && (value < -2.0 || value > 2.0))
                    throw new ArgumentOutOfRangeException(nameof(PenalizePresence), "PenalizePresence must be between -2.0 and 2.0");
                _PenalizePresence = value;
            }
        }

        /// <summary>
        /// Frequency penalty. Range: -2.0 to 2.0. Default is null.
        /// Positive values penalize tokens based on their frequency in the text so far.
        /// </summary>
        [JsonPropertyName("penalize_frequency")]
        public double? PenalizeFrequency
        {
            get => _PenalizeFrequency;
            set
            {
                if (value.HasValue && (value < -2.0 || value > 2.0))
                    throw new ArgumentOutOfRangeException(nameof(PenalizeFrequency), "PenalizeFrequency must be between -2.0 and 2.0");
                _PenalizeFrequency = value;
            }
        }

        /// <summary>
        /// Tail-free sampling. Range: 0.0 to 1.0. Default is null.
        /// </summary>
        [JsonPropertyName("tfs_z")]
        public double? TfsZ
        {
            get => _TfsZ;
            set
            {
                if (value.HasValue && (value < 0.0 || value > 1.0))
                    throw new ArgumentOutOfRangeException(nameof(TfsZ), "TfsZ must be between 0.0 and 1.0");
                _TfsZ = value;
            }
        }

        /// <summary>
        /// Typical-p sampling. Range: 0.0 to 1.0. Default is null.
        /// </summary>
        [JsonPropertyName("typical_p")]
        public double? TypicalP
        {
            get => _TypicalP;
            set
            {
                if (value.HasValue && (value < 0.0 || value > 1.0))
                    throw new ArgumentOutOfRangeException(nameof(TypicalP), "TypicalP must be between 0.0 and 1.0");
                _TypicalP = value;
            }
        }

        /// <summary>
        /// Mirostat sampling mode. Valid values: 0, 1, or 2. Default is null.
        /// 0 = disabled, 1 = Mirostat v1, 2 = Mirostat v2.
        /// </summary>
        [JsonPropertyName("mirostat")]
        public int? Mirostat
        {
            get => _Mirostat;
            set
            {
                if (value.HasValue && (value < 0 || value > 2))
                    throw new ArgumentOutOfRangeException(nameof(Mirostat), "Mirostat must be 0, 1, or 2");
                _Mirostat = value;
            }
        }

        /// <summary>
        /// Mirostat target entropy. Range: 0.0 to 10.0. Default is null.
        /// </summary>
        [JsonPropertyName("mirostat_tau")]
        public double? MirostatTau
        {
            get => _MirostatTau;
            set
            {
                if (value.HasValue && (value < 0.0 || value > 10.0))
                    throw new ArgumentOutOfRangeException(nameof(MirostatTau), "MirostatTau must be between 0.0 and 10.0");
                _MirostatTau = value;
            }
        }

        /// <summary>
        /// Mirostat learning rate. Range: 0.0 to 1.0. Default is null.
        /// </summary>
        [JsonPropertyName("mirostat_eta")]
        public double? MirostatEta
        {
            get => _MirostatEta;
            set
            {
                if (value.HasValue && (value < 0.0 || value > 1.0))
                    throw new ArgumentOutOfRangeException(nameof(MirostatEta), "MirostatEta must be between 0.0 and 1.0");
                _MirostatEta = value;
            }
        }

        /// <summary>
        /// Number of threads to use for generation. Range: 1 to 128. Default is null.
        /// </summary>
        [JsonPropertyName("num_thread")]
        public int? NumThread
        {
            get => _NumThread;
            set
            {
                if (value.HasValue && (value < 1 || value > 128))
                    throw new ArgumentOutOfRangeException(nameof(NumThread), "NumThread must be between 1 and 128");
                _NumThread = value;
            }
        }

        /// <summary>
        /// Sequences where generation should stop. Default is null.
        /// </summary>
        [JsonPropertyName("stop")]
        public List<string> Stop { get; set; } = null;

        private double? _Temperature = null;
        private double? _TopP = null;
        private int? _TopK = null;
        private int? _NumPredict = null;
        private int? _NumCtx = null;
        private int? _NumKeep = null;
        private double? _RepeatPenalty = null;
        private double? _PenalizePresence = null;
        private double? _PenalizeFrequency = null;
        private double? _TfsZ = null;
        private double? _TypicalP = null;
        private int? _Mirostat = null;
        private double? _MirostatTau = null;
        private double? _MirostatEta = null;
        private int? _NumThread = null;

        /// <summary>
        /// Instantiate a new Ollama options object.
        /// </summary>
        public OllamaOptions()
        {

        }
    }
}