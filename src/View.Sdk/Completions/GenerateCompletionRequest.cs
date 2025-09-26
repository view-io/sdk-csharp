namespace View.Sdk.Completions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Generate completion request.
    /// </summary>
    public class GenerateCompletionRequest
    {
        /// <summary>
        /// The model to use for completion (required)
        /// e.g., "gpt-4", "llama2", "mistral"
        /// </summary>
        public string Model
        {
            get => _Model;
            set => _Model = value ?? throw new ArgumentNullException(nameof(Model));
        }

        /// <summary>
        /// The messages to generate a completion for
        /// </summary>
        public List<Message> Messages
        {
            get => _Messages;
            set => _Messages = value ?? throw new ArgumentNullException(nameof(Messages));
        }

        /// <summary>
        /// Single prompt string (alternative to Messages for simpler requests)
        /// </summary>
        public string Prompt { get; set; }

        /// <summary>
        /// Temperature for sampling (0.0 to 2.0)
        /// Higher values = more random
        /// </summary>
        public double? Temperature
        {
            get => _Temperature;
            set
            {
                if (value.HasValue && (value < 0.0 || value > 2.0))
                    throw new ArgumentOutOfRangeException(nameof(Temperature));
                _Temperature = value;
            }
        }

        /// <summary>
        /// Nucleus sampling - consider tokens with top_p probability mass (0.0 to 1.0)
        /// </summary>
        public double? TopP
        {
            get => _TopP;
            set
            {
                if (value.HasValue && (value < 0.0 || value > 1.0))
                    throw new ArgumentOutOfRangeException(nameof(TopP));
                _TopP = value;
            }
        }

        /// <summary>
        /// Only sample from top K tokens (1 to 1000)
        /// </summary>
        public int? TopK
        {
            get => _TopK;
            set
            {
                if (value.HasValue && (value < 1 || value > 1000))
                    throw new ArgumentOutOfRangeException(nameof(TopK));
                _TopK = value;
            }
        }

        /// <summary>
        /// Maximum number of tokens to generate (1 to 128000)
        /// </summary>
        public int? MaxTokens
        {
            get => _MaxTokens;
            set
            {
                if (value.HasValue && (value < 1 || value > 128000))
                    throw new ArgumentOutOfRangeException(nameof(MaxTokens));
                _MaxTokens = value;
            }
        }

        /// <summary>
        /// Stop sequences - generation stops if these are encountered
        /// </summary>
        public List<string> Stop
        {
            get => _Stop;
            set => _Stop = value ?? throw new ArgumentNullException(nameof(Stop));
        }

        /// <summary>
        /// Whether to stream the response
        /// </summary>
        public bool Stream { get; set; } = false;

        /// <summary>
        /// Number of completions to generate (1 to 10)
        /// </summary>
        public int? N
        {
            get => _N;
            set
            {
                if (value.HasValue && (value < 1 || value > 10))
                    throw new ArgumentOutOfRangeException(nameof(N));
                _N = value;
            }
        }

        /// <summary>
        /// Presence penalty (-2.0 to 2.0)
        /// Positive values penalize new tokens based on whether they appear in the text so far
        /// </summary>
        public double? PresencePenalty
        {
            get => _PresencePenalty;
            set
            {
                if (value.HasValue && (value < -2.0 || value > 2.0))
                    throw new ArgumentOutOfRangeException(nameof(PresencePenalty));
                _PresencePenalty = value;
            }
        }

        /// <summary>
        /// Frequency penalty (-2.0 to 2.0)
        /// Positive values penalize tokens based on their frequency in the text so far
        /// </summary>
        public double? FrequencyPenalty
        {
            get => _FrequencyPenalty;
            set
            {
                if (value.HasValue && (value < -2.0 || value > 2.0))
                    throw new ArgumentOutOfRangeException(nameof(FrequencyPenalty));
                _FrequencyPenalty = value;
            }
        }

        /// <summary>
        /// Penalize repetition of sequences (0.0 to 2.0)
        /// </summary>
        public double? RepeatPenalty
        {
            get => _RepeatPenalty;
            set
            {
                if (value.HasValue && (value < 0.0 || value > 2.0))
                    throw new ArgumentOutOfRangeException(nameof(RepeatPenalty));
                _RepeatPenalty = value;
            }
        }

        /// <summary>
        /// Random seed for deterministic generation (any valid int32)
        /// </summary>
        public int? Seed
        {
            get => _Seed;
            set => _Seed = value;
        }

        /// <summary>
        /// System message/instruction (can also be included in Messages)
        /// </summary>
        public string System { get; set; }

        /// <summary>
        /// Response format (e.g., "json" for JSON mode)
        /// </summary>
        public string ResponseFormat { get; set; }

        /// <summary>
        /// User identifier for tracking
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Number of tokens to keep from prompt (0 to context size)
        /// </summary>
        public int? NumKeep
        {
            get => _NumKeep;
            set
            {
                if (value.HasValue && value < 0)
                    throw new ArgumentOutOfRangeException(nameof(NumKeep));
                _NumKeep = value;
            }
        }

        /// <summary>
        /// Number of tokens for context window (128 to 128000)
        /// </summary>
        public int? NumCtx
        {
            get => _NumCtx;
            set
            {
                if (value.HasValue && (value < 128 || value > 128000))
                    throw new ArgumentOutOfRangeException(nameof(NumCtx));
                _NumCtx = value;
            }
        }

        /// <summary>
        /// Number of tokens to predict (1 to 128000)
        /// </summary>
        public int? NumPredict
        {
            get => _NumPredict;
            set
            {
                if (value.HasValue && (value < 1 || value > 128000))
                    throw new ArgumentOutOfRangeException(nameof(NumPredict));
                _NumPredict = value;
            }
        }

        /// <summary>
        /// Tail free sampling (0.0 to 1.0)
        /// </summary>
        public double? TfsZ
        {
            get => _TfsZ;
            set
            {
                if (value.HasValue && (value < 0.0 || value > 1.0))
                    throw new ArgumentOutOfRangeException(nameof(TfsZ));
                _TfsZ = value;
            }
        }

        /// <summary>
        /// Typical p sampling (0.0 to 1.0)
        /// </summary>
        public double? TypicalP
        {
            get => _TypicalP;
            set
            {
                if (value.HasValue && (value < 0.0 || value > 1.0))
                    throw new ArgumentOutOfRangeException(nameof(TypicalP));
                _TypicalP = value;
            }
        }

        /// <summary>
        /// Mirostat sampling mode (0, 1, or 2)
        /// </summary>
        public int? Mirostat
        {
            get => _Mirostat;
            set
            {
                if (value.HasValue && (value < 0 || value > 2))
                    throw new ArgumentOutOfRangeException(nameof(Mirostat));
                _Mirostat = value;
            }
        }

        /// <summary>
        /// Mirostat target entropy (0.0 to 10.0)
        /// </summary>
        public double? MirostatTau
        {
            get => _MirostatTau;
            set
            {
                if (value.HasValue && (value < 0.0 || value > 10.0))
                    throw new ArgumentOutOfRangeException(nameof(MirostatTau));
                _MirostatTau = value;
            }
        }

        /// <summary>
        /// Mirostat learning rate (0.0 to 1.0)
        /// </summary>
        public double? MirostatEta
        {
            get => _MirostatEta;
            set
            {
                if (value.HasValue && (value < 0.0 || value > 1.0))
                    throw new ArgumentOutOfRangeException(nameof(MirostatEta));
                _MirostatEta = value;
            }
        }

        /// <summary>
        /// Number of threads to use (1 to 128)
        /// </summary>
        public int? NumThread
        {
            get => _NumThread;
            set
            {
                if (value.HasValue && (value < 1 || value > 128))
                    throw new ArgumentOutOfRangeException(nameof(NumThread));
                _NumThread = value;
            }
        }

        private string _Model = string.Empty;
        private double? _Temperature = null;
        private double? _TopP = null;
        private double? _PresencePenalty = null;
        private double? _FrequencyPenalty = null;
        private double? _RepeatPenalty = null;
        private double? _TfsZ = null;
        private double? _TypicalP = null;
        private double? _MirostatTau = null;
        private double? _MirostatEta = null;
        private int? _Mirostat = null;
        private int? _TopK = null;
        private int? _MaxTokens = null;
        private int? _N = null;
        private int? _Seed = null;
        private int? _NumKeep = null;
        private int? _NumCtx = null;
        private int? _NumPredict = null;
        private int? _NumThread = null;
        private List<Message> _Messages = new List<Message>();
        private List<string> _Stop = new List<string>();

        /// <summary>
        /// Generate completion request.
        /// </summary>
        public GenerateCompletionRequest()
        {

        }
    }
}
