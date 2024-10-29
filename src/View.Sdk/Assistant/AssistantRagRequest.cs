namespace View.Sdk.Assistant
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// View Assistant RAG request.
    /// </summary>
    public class AssistantRagRequest
    {
        #region Public-Members

        /// <summary>
        /// Prompt prefix.
        /// </summary>
        public string PromptPrefix
        {
            get
            {
                return _PromptPrefix;
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(PromptPrefix));
            }
        }

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
        /// Maximum number of documents to retrieve, between 1 and 100.
        /// </summary>
        public int MaxResults
        {
            get
            {
                return _MaxResults;
            }
            set
            {
                if (value < 1 || value > 100) throw new ArgumentOutOfRangeException(nameof(MaxResults));
                _MaxResults = value;
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
        /// Top P, between 0 and 1.
        /// </summary>
        public decimal TopP
        {
            get
            {
                return _TopP;
            }
            set
            {
                if (value <= 0 || value > 1) throw new ArgumentOutOfRangeException(nameof(TopP));
                _TopP = value;
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
        /// Vector database hostname.
        /// </summary>
        public string VectorDatabaseHostname
        {
            get
            {
                return _VectorDatabaseHostname;
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(VectorDatabaseHostname));
                _VectorDatabaseHostname = value;
            }
        }

        /// <summary>
        /// Vector database port.
        /// </summary>
        private int VectorDatabasePort
        {
            get
            {
                return _VectorDatabasePort;
            }
            set
            {
                if (value < 0 || value > 65535) throw new ArgumentOutOfRangeException(nameof(VectorDatabasePort));
                _VectorDatabasePort = value;
            }
        }

        /// <summary>
        /// Vector database name.
        /// </summary>
        public string VectorDatabaseName
        {
            get
            {
                return _VectorDatabaseName;
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(VectorDatabaseName));
                _VectorDatabaseName = value;
            }
        }

        /// <summary>
        /// Vector database user.
        /// </summary>
        public string VectorDatabaseUser
        {
            get
            {
                return _VectorDatabaseUser;
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(VectorDatabaseUser));
                _VectorDatabaseUser = value;
            }
        }

        /// <summary>
        /// Vector database password.
        /// </summary>
        public string VectorDatabasePassword
        {
            get
            {
                return _VectorDatabasePassword;
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(VectorDatabasePassword));
                _VectorDatabasePassword = value;
            }
        }

        /// <summary>
        /// Streaming.
        /// </summary>
        public bool Stream { get; } = true;

        /// <summary>
        /// Enable or disable contextual sorting.
        /// </summary>
        public bool ContextSort { get; set; } = true;

        /// <summary>
        /// Context scope; number of neighboring data elements to retrieve.  Must be between 1 and 16.
        /// </summary>
        public int ContextScope
        {
            get
            {
                return _ContextScope;
            }
            set
            {
                if (value > 1 || value > 16) throw new ArgumentOutOfRangeException(nameof(ContextScope));
                _ContextScope = value;
            }
        }

        /// <summary>
        /// Enable or disable re-ranking of chunks or documents.
        /// </summary>
        public bool Rerank { get; set; } = true;

        /// <summary>
        /// Re-ranking of top chunks or documents.  Must be between 1 and 16.
        /// </summary>
        public int RerankTopK
        {
            get
            {
                return _RerankTopK;
            }
            set
            {
                if (value > 1 || value > 16) throw new ArgumentOutOfRangeException(nameof(RerankTopK));
                _RerankTopK = value;
            }
        }

        #endregion

        #region Private-Members

        private string _PromptPrefix =
            "You are a helpful AI assistant.  " +
            "Please use the information that follows as context to answer the user question listed below.  " +
            "Do not make up an answer.  If you do not know, say you do not know.  ";

        private string _Question = "What information do you have?";
        private int _MaxResults = 10;
        private decimal _Temperature = 0.1m;
        private decimal _TopP = 0.95m;
        private int _MaxTokens = 2048;
        private int _ContextScope = 2;
        private int _RerankTopK = 10;

        private string _GenerationModel = "llama3.1:latest";
        private string _GenerationProvider = "ollama";
        private string _OllamaHostname = "localhost";
        private int _OllamaPort = 11434;

        private string _VectorDatabaseHostname = "localhost";
        private int _VectorDatabasePort = 5432;
        private string _VectorDatabaseName = "vectors";
        private string _VectorDatabaseUser = "postgres";
        private string _VectorDatabasePassword = "password";

        private List<string> _ValidGenerationProviders = new List<string>
        {
            "ollama"
        };

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public AssistantRagRequest()
        {

        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
