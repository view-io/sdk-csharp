namespace View.Sdk.Completions.Providers.OpenAI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using View.Sdk.Completions;

    /// <summary>
    /// OpenAI completions request object for interacting with the OpenAI Chat Completions API.
    /// </summary>
    public class OpenAiCompletionsRequest
    {
        #region Public-Members

        /// <summary>
        /// Model to use for completion generation. Default is null.
        /// Examples: "gpt-3.5-turbo", "gpt-4", "gpt-4-turbo".
        /// </summary>
        [JsonPropertyName("model")]
        public string Model { get; set; } = null;

        /// <summary>
        /// List of messages comprising the conversation so far. Default is empty list.
        /// Cannot be null - will be replaced with empty list if null is provided.
        /// </summary>
        [JsonPropertyName("messages")]
        public List<OpenAiMessage> Messages
        {
            get => _Messages;
            set => _Messages = value ?? new List<OpenAiMessage>();
        }

        /// <summary>
        /// Temperature for sampling. Range: 0.0 to 2.0. Default is null.
        /// Higher values make output more random, lower values more deterministic.
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
        /// Nucleus sampling parameter. Range: 0.0 to 1.0. Default is null.
        /// Consider tokens with cumulative probability mass of top_p.
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
        /// Number of completions to generate for each prompt. Range: 1 to 10. Default is null.
        /// </summary>
        [JsonPropertyName("n")]
        public int? N
        {
            get => _N;
            set
            {
                if (value.HasValue && (value < 1 || value > 10))
                    throw new ArgumentOutOfRangeException(nameof(N), "N must be between 1 and 10");
                _N = value;
            }
        }

        /// <summary>
        /// Whether to stream partial message deltas. Default is null.
        /// </summary>
        [JsonPropertyName("stream")]
        public bool? Stream { get; set; } = null;

        /// <summary>
        /// Sequences where the API will stop generating further tokens. Default is null.
        /// Can be a string or array of strings.
        /// </summary>
        [JsonPropertyName("stop")]
        public object Stop { get; set; } = null;

        /// <summary>
        /// Maximum number of tokens to generate. Range: 1 to 128000. Default is null.
        /// </summary>
        [JsonPropertyName("max_tokens")]
        public int? MaxTokens
        {
            get => _MaxTokens;
            set
            {
                if (value.HasValue && (value < 1 || value > 128000))
                    throw new ArgumentOutOfRangeException(nameof(MaxTokens), "MaxTokens must be between 1 and 128000");
                _MaxTokens = value;
            }
        }

        /// <summary>
        /// Presence penalty. Range: -2.0 to 2.0. Default is null.
        /// Positive values penalize new tokens based on whether they appear in the text so far.
        /// </summary>
        [JsonPropertyName("presence_penalty")]
        public double? PresencePenalty
        {
            get => _PresencePenalty;
            set
            {
                if (value.HasValue && (value < -2.0 || value > 2.0))
                    throw new ArgumentOutOfRangeException(nameof(PresencePenalty), "PresencePenalty must be between -2.0 and 2.0");
                _PresencePenalty = value;
            }
        }

        /// <summary>
        /// Frequency penalty. Range: -2.0 to 2.0. Default is null.
        /// Positive values penalize tokens based on their frequency in the text so far.
        /// </summary>
        [JsonPropertyName("frequency_penalty")]
        public double? FrequencyPenalty
        {
            get => _FrequencyPenalty;
            set
            {
                if (value.HasValue && (value < -2.0 || value > 2.0))
                    throw new ArgumentOutOfRangeException(nameof(FrequencyPenalty), "FrequencyPenalty must be between -2.0 and 2.0");
                _FrequencyPenalty = value;
            }
        }

        /// <summary>
        /// Modify the likelihood of specified tokens appearing in the completion. Default is null.
        /// Maps token IDs to bias values from -100 to 100.
        /// </summary>
        [JsonPropertyName("logit_bias")]
        public Dictionary<string, double> LogitBias { get; set; } = null;

        /// <summary>
        /// Unique identifier representing the end-user. Default is null.
        /// Helps OpenAI monitor and detect abuse.
        /// </summary>
        [JsonPropertyName("user")]
        public string User { get; set; } = null;

        /// <summary>
        /// Random seed for deterministic generation. Any valid int32 value. Default is null.
        /// </summary>
        [JsonPropertyName("seed")]
        public int? Seed { get; set; } = null;

        /// <summary>
        /// Response format specification. Default is null.
        /// Used to enable JSON mode or other structured outputs.
        /// </summary>
        [JsonPropertyName("response_format")]
        public OpenAiResponseFormat ResponseFormat { get; set; } = null;

        /// <summary>
        /// Top-k sampling parameter. Range: 1 to 1000. Default is null.
        /// Only available for certain models.
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

        #endregion

        #region Private-Members

        private List<OpenAiMessage> _Messages = new List<OpenAiMessage>();
        private double? _Temperature = null;
        private double? _TopP = null;
        private int? _N = null;
        private int? _MaxTokens = null;
        private double? _PresencePenalty = null;
        private double? _FrequencyPenalty = null;
        private int? _TopK = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate a new OpenAI completions request.
        /// </summary>
        public OpenAiCompletionsRequest()
        {

        }

        /// <summary>
        /// Create an OpenAI completions request from a generic completion request.
        /// </summary>
        /// <param name="req">Generic completions request.</param>
        /// <returns>OpenAI-specific completions request.</returns>
        public static OpenAiCompletionsRequest FromCompletionRequest(GenerateCompletionRequest req)
        {
            if (req == null) throw new ArgumentNullException(nameof(req));

            OpenAiCompletionsRequest openAiReq = new OpenAiCompletionsRequest
            {
                Model = req.Model,
                Stream = req.Stream,
                Temperature = req.Temperature,
                TopP = req.TopP,
                TopK = req.TopK,
                N = req.N,
                MaxTokens = req.MaxTokens,
                PresencePenalty = req.PresencePenalty,
                FrequencyPenalty = req.FrequencyPenalty,
                Seed = req.Seed,
                User = req.User
            };

            // Handle stop sequences
            if (req.Stop != null && req.Stop.Count > 0)
            {
                if (req.Stop.Count == 1)
                    openAiReq.Stop = req.Stop[0];
                else
                    openAiReq.Stop = req.Stop;
            }

            // Handle response format
            if (!string.IsNullOrEmpty(req.ResponseFormat))
            {
                openAiReq.ResponseFormat = new OpenAiResponseFormat
                {
                    Type = req.ResponseFormat.ToLower()
                };
            }

            // Handle messages
            if (req.Messages != null && req.Messages.Count > 0)
            {
                openAiReq.Messages = new List<OpenAiMessage>();

                // Add system message if present
                if (!string.IsNullOrEmpty(req.System))
                {
                    openAiReq.Messages.Add(new OpenAiMessage
                    {
                        Role = "system",
                        Content = req.System
                    });
                }

                foreach (Message msg in req.Messages)
                {
                    openAiReq.Messages.Add(new OpenAiMessage
                    {
                        Role = msg.Role,
                        Content = msg.Content,
                        Name = msg.Name
                    });
                }
            }
            else if (!string.IsNullOrEmpty(req.Prompt))
            {
                // Convert prompt to messages format
                openAiReq.Messages = new List<OpenAiMessage>();

                if (!string.IsNullOrEmpty(req.System))
                {
                    openAiReq.Messages.Add(new OpenAiMessage
                    {
                        Role = "system",
                        Content = req.System
                    });
                }

                openAiReq.Messages.Add(new OpenAiMessage
                {
                    Role = "user",
                    Content = req.Prompt
                });
            }

            return openAiReq;
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}