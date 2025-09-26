namespace View.Sdk.Completions.Providers.Ollama
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using View.Sdk.Completions;

    /// <summary>
    /// Ollama completions request object for interacting with the Ollama API.
    /// </summary>
    public class OllamaCompletionsRequest
    {
        #region Public-Members

        /// <summary>
        /// Model name to use for completion generation. Default is null.
        /// </summary>
        [JsonPropertyName("model")]
        public string Model { get; set; } = null;

        /// <summary>
        /// The prompt to generate a response for (legacy format). Default is null.
        /// Used when not using the chat/messages format.
        /// </summary>
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; } = null;

        /// <summary>
        /// Messages for chat format. Default is empty list.
        /// Cannot be null - will be replaced with empty list if null is provided.
        /// </summary>
        [JsonPropertyName("messages")]
        public List<OllamaMessage> Messages
        {
            get => _Messages;
            set => _Messages = value ?? new List<OllamaMessage>();
        }

        /// <summary>
        /// System prompt to prepend to the conversation. Default is null.
        /// </summary>
        [JsonPropertyName("system")]
        public string System { get; set; } = null;

        /// <summary>
        /// Additional model parameters for fine-tuning generation. Default is null.
        /// </summary>
        [JsonPropertyName("options")]
        public OllamaOptions Options { get; set; } = null;

        /// <summary>
        /// Whether to stream the response token by token. Default is false.
        /// </summary>
        [JsonPropertyName("stream")]
        public bool Stream { get; set; } = false;

        /// <summary>
        /// Duration to keep the model loaded in memory. Default is null.
        /// Format examples: "5m", "1h", "24h".
        /// </summary>
        [JsonPropertyName("keep_alive")]
        public string KeepAlive { get; set; } = null;

        #endregion

        #region Private-Members

        private List<OllamaMessage> _Messages = new List<OllamaMessage>();

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate a new Ollama completions request.
        /// </summary>
        public OllamaCompletionsRequest()
        {

        }

        /// <summary>
        /// Create an Ollama completions request from a generic completion request.
        /// </summary>
        /// <param name="req">Generic completions request.</param>
        /// <returns>Ollama-specific completions request.</returns>
        public static OllamaCompletionsRequest FromCompletionRequest(GenerateCompletionRequest req)
        {
            if (req == null) throw new ArgumentNullException(nameof(req));

            OllamaCompletionsRequest ollamaReq = new OllamaCompletionsRequest
            {
                Model = req.Model,
                Stream = req.Stream,
                System = req.System
            };

            // Build options from various parameters
            ollamaReq.Options = new OllamaOptions();

            if (req.Temperature.HasValue)
                ollamaReq.Options.Temperature = req.Temperature.Value;
            if (req.TopP.HasValue)
                ollamaReq.Options.TopP = req.TopP.Value;
            if (req.TopK.HasValue)
                ollamaReq.Options.TopK = req.TopK.Value;
            if (req.NumPredict.HasValue)
                ollamaReq.Options.NumPredict = req.NumPredict.Value;
            if (req.NumCtx.HasValue)
                ollamaReq.Options.NumCtx = req.NumCtx.Value;
            if (req.NumKeep.HasValue)
                ollamaReq.Options.NumKeep = req.NumKeep.Value;
            if (req.Seed.HasValue)
                ollamaReq.Options.Seed = req.Seed.Value;
            if (req.RepeatPenalty.HasValue)
                ollamaReq.Options.RepeatPenalty = req.RepeatPenalty.Value;
            if (req.PresencePenalty.HasValue)
                ollamaReq.Options.PenalizePresence = req.PresencePenalty.Value;
            if (req.FrequencyPenalty.HasValue)
                ollamaReq.Options.PenalizeFrequency = req.FrequencyPenalty.Value;
            if (req.TfsZ.HasValue)
                ollamaReq.Options.TfsZ = req.TfsZ.Value;
            if (req.TypicalP.HasValue)
                ollamaReq.Options.TypicalP = req.TypicalP.Value;
            if (req.Mirostat.HasValue)
                ollamaReq.Options.Mirostat = req.Mirostat.Value;
            if (req.MirostatTau.HasValue)
                ollamaReq.Options.MirostatTau = req.MirostatTau.Value;
            if (req.MirostatEta.HasValue)
                ollamaReq.Options.MirostatEta = req.MirostatEta.Value;
            if (req.NumThread.HasValue)
                ollamaReq.Options.NumThread = req.NumThread.Value;
            if (req.Stop != null && req.Stop.Count > 0)
                ollamaReq.Options.Stop = req.Stop;

            // Handle messages or prompt
            if (req.Messages != null && req.Messages.Count > 0)
            {
                ollamaReq.Messages = new List<OllamaMessage>();
                foreach (Message msg in req.Messages)
                {
                    ollamaReq.Messages.Add(new OllamaMessage
                    {
                        Role = msg.Role,
                        Content = msg.Content
                    });
                }
            }
            else if (!string.IsNullOrEmpty(req.Prompt))
            {
                ollamaReq.Prompt = req.Prompt;
            }

            return ollamaReq;
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}