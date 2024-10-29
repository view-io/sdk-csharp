namespace View.Sdk.Assistant
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// View Assistant chat request.
    /// </summary>
    public class AssistantChatRequest
    {
        #region Public-Members

        /// <summary>
        /// Question.
        /// </summary>
        public string Question
        {
            get
            {
                return _Question;
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(Question));
                _Question = value;
            }
        }

        /// <summary>
        /// Temperature, between 0 and 1.
        /// </summary>
        public decimal Temperature
        {
            get
            {
                return _Temperature;
            }
            set
            {
                if (value <= 0 || value > 1) throw new ArgumentOutOfRangeException(nameof(Temperature));
                _Temperature = value;
            }
        }

        /// <summary>
        /// Maximum number of tokens to generate, between 1 and 16384.
        /// </summary>
        public int MaxTokens
        {
            get
            {
                return _MaxTokens;
            }
            set
            {
                if (value < 1 || value > 16384) throw new ArgumentOutOfRangeException(nameof(MaxTokens));
                _MaxTokens = value;
            }
        }

        /// <summary>
        /// Generation model and tag; default is llama3.1:latest.
        /// </summary>
        public string GenerationModel
        {
            get
            {
                return _GenerationModel;
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(GenerationModel));
                _GenerationModel = value;
            }
        }

        /// <summary>
        /// Generation provider.  Value values are: ollama.
        /// </summary>
        public string GenerationProvider
        {
            get
            {
                return _GenerationProvider;
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(GenerationProvider));
                if (!_ValidGenerationProviders.Contains(value)) throw new ArgumentException("The specified generation provider '" + value + "' is not valid.");
                _GenerationProvider = value;
            }
        }

        /// <summary>
        /// Ollama hostname.
        /// </summary>
        public string OllamaHostname
        {
            get
            {
                return _OllamaHostname;
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(OllamaHostname));
                _OllamaHostname = value;
            }
        }

        /// <summary>
        /// Ollama TCP port.
        /// </summary>
        public int OllamaPort
        {
            get
            {
                return _OllamaPort;
            }
            set
            {
                if (value < 0 || value > 65535) throw new ArgumentOutOfRangeException(nameof(OllamaPort));
                _OllamaPort = value;
            }
        }

        /// <summary>
        /// Streaming.
        /// </summary>
        public bool Stream { get; } = true;

        #endregion

        #region Private-Members

        private string _Question = "What information do you have?";
        private decimal _Temperature = 0.1m;
        private int _MaxTokens = 2048;

        private string _GenerationModel = "llama3.1:latest";
        private string _GenerationProvider = "ollama";
        private string _OllamaHostname = "localhost";
        private int _OllamaPort = 11434;

        private List<string> _ValidGenerationProviders = new List<string>
        {
            "ollama"
        };

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public AssistantChatRequest()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
