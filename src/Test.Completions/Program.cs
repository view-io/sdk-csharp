namespace Test.Completions
{
    using GetSomeInput;
    using SyslogLogging;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Sdk.Completions;
    using View.Sdk.Completions.Providers.Ollama;
    using View.Sdk.Completions.Providers.OpenAI;

    internal class Program
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

        static LoggingModule _Logging = null;
        static ViewOllamaCompletionsSdk _OllamaCompletions = null;
        static ViewOpenAiCompletionsSdk _OpenAiCompletions = null;
        static bool _RunForever = true;
        static CompletionsProviderEnum _CurrentProvider = CompletionsProviderEnum.Ollama;
        static string _OllamaBaseUrl = "http://localhost:11434/";
        static string _OpenAiBaseUrl = "https://api.openai.com/";
        static string _OpenAiApiKey = null;
        static string _OllamaModel = "llama3";
        static string _OpenAiModel = "gpt-3.5-turbo";
        static List<Message> _ConversationHistory = new List<Message>();

        static async Task Main(string[] args)
        {
            _Logging = new LoggingModule();

            Console.WriteLine("");
            Console.WriteLine("==============================================");
            Console.WriteLine("Completions SDK Test Application");
            Console.WriteLine("==============================================");
            Console.WriteLine("");

            await InitializeSdks();

            while (_RunForever)
            {
                string provider = _CurrentProvider == CompletionsProviderEnum.Ollama ? "Ollama" : "OpenAI";
                string model = _CurrentProvider == CompletionsProviderEnum.Ollama ? _OllamaModel : _OpenAiModel;
                string prompt = $"[{provider}:{model}] Command [? for help]:";

                string userInput = Inputty.GetString(prompt, null, false);

                switch (userInput?.ToLower())
                {
                    case "q":
                    case "quit":
                        _RunForever = false;
                        break;
                    case "c":
                    case "cls":
                        Console.Clear();
                        break;
                    case "?":
                    case "help":
                        Menu();
                        break;

                    case "provider":
                        SwitchProvider();
                        break;
                    case "model":
                        ChangeModel();
                        break;
                    case "config":
                        await ConfigureEndpoints();
                        break;
                    case "test":
                        await TestConnectivity();
                        break;

                    case "simple":
                        await SimpleCompletion();
                        break;
                    case "stream":
                        await StreamingCompletion();
                        break;
                    case "chat":
                        await ChatConversation();
                        break;
                    case "history":
                        ShowHistory();
                        break;
                    case "clear":
                        ClearHistory();
                        break;
                    case "params":
                        await ParameterizedCompletion();
                        break;
                    case "file":
                        await FileCompletion();
                        break;
                    case "multi":
                        await MultiTurnConversation();
                        break;
                    case "compare":
                        await CompareProviders();
                        break;
                    case "benchmark":
                        await BenchmarkStreaming();
                        break;
                    case "stop":
                        await TestStopSequences();
                        break;
                    case "json":
                        await TestJsonMode();
                        break;
                    case "logging":
                        ToggleLogging();
                        break;

                    default:
                        if (!string.IsNullOrEmpty(userInput))
                        {
                            Console.WriteLine($"Unknown command: {userInput}");
                        }
                        break;
                }
            }
        }

        static void Menu()
        {
            Console.WriteLine("");
            Console.WriteLine("Available commands:");
            Console.WriteLine("");
            Console.WriteLine("  Configuration:");
            Console.WriteLine("    provider        Switch between Ollama and OpenAI");
            Console.WriteLine("    model           Change the model for current provider");
            Console.WriteLine("    config          Configure endpoint URLs and API keys");
            Console.WriteLine("    test            Test connectivity to current provider");
            Console.WriteLine("    logging         Toggle request/response logging");
            Console.WriteLine("");
            Console.WriteLine("  Basic Operations:");
            Console.WriteLine("    simple          Send a simple completion request");
            Console.WriteLine("    stream          Send a streaming completion request");
            Console.WriteLine("    chat            Start an interactive chat session");
            Console.WriteLine("    file            Load prompt from file and save response");
            Console.WriteLine("");
            Console.WriteLine("  Advanced Operations:");
            Console.WriteLine("    params          Test with custom parameters (temperature, top_p, etc.)");
            Console.WriteLine("    multi           Multi-turn conversation test");
            Console.WriteLine("    stop            Test stop sequences");
            Console.WriteLine("    json            Test JSON response format (OpenAI only)");
            Console.WriteLine("");
            Console.WriteLine("  Conversation Management:");
            Console.WriteLine("    history         Show conversation history");
            Console.WriteLine("    clear           Clear conversation history");
            Console.WriteLine("");
            Console.WriteLine("  Comparison:");
            Console.WriteLine("    compare         Compare same prompt across providers");
            Console.WriteLine("    benchmark       Benchmark streaming performance");
            Console.WriteLine("");
            Console.WriteLine("  General:");
            Console.WriteLine("    ?/help          Show this menu");
            Console.WriteLine("    cls/c           Clear the screen");
            Console.WriteLine("    q/quit          Quit the application");
            Console.WriteLine("");
        }

        static async Task InitializeSdks()
        {
            Console.WriteLine("Initializing SDKs...");

            // Check for OpenAI API key in environment variable
            _OpenAiApiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

            if (string.IsNullOrEmpty(_OpenAiApiKey))
            {
                Console.WriteLine("No OPENAI_API_KEY environment variable found.");
                _OpenAiApiKey = Inputty.GetString("Enter OpenAI API key (or press Enter to skip):", null, true);
            }

            _OllamaCompletions = new ViewOllamaCompletionsSdk(
                Guid.NewGuid(),
                _OllamaBaseUrl,
                null);

            _OllamaCompletions.Logger = (sev, msg) => Console.WriteLine($"[{sev}] {msg}");

            _OpenAiCompletions = new ViewOpenAiCompletionsSdk(
                Guid.NewGuid(),
                _OpenAiBaseUrl,
                _OpenAiApiKey);

            _OpenAiCompletions.Logger = (sev, msg) => Console.WriteLine($"[{sev}] {msg}");

            Console.WriteLine("SDKs initialized.");
            Console.WriteLine("");
        }

        static void SwitchProvider()
        {
            Console.WriteLine("");
            Console.WriteLine("Current provider: " + _CurrentProvider);
            Console.WriteLine("1. Ollama");
            Console.WriteLine("2. OpenAI");

            string choice = Inputty.GetString("Select provider [1/2]:", "1", false);

            switch (choice)
            {
                case "1":
                    _CurrentProvider = CompletionsProviderEnum.Ollama;
                    Console.WriteLine("Switched to Ollama");
                    break;
                case "2":
                    if (string.IsNullOrEmpty(_OpenAiApiKey))
                    {
                        Console.WriteLine("OpenAI API key not configured. Please configure first.");
                    }
                    else
                    {
                        _CurrentProvider = CompletionsProviderEnum.OpenAI;
                        Console.WriteLine("Switched to OpenAI");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
            Console.WriteLine("");
        }

        static void ChangeModel()
        {
            Console.WriteLine("");
            if (_CurrentProvider == CompletionsProviderEnum.Ollama)
            {
                Console.WriteLine("Current Ollama model: " + _OllamaModel);
                Console.WriteLine("Common models: llama3, llama2, mistral, codellama, phi");
                _OllamaModel = Inputty.GetString("Enter model name:", _OllamaModel, false);
            }
            else
            {
                Console.WriteLine("Current OpenAI model: " + _OpenAiModel);
                Console.WriteLine("Common models: gpt-3.5-turbo, gpt-4, gpt-4-turbo");
                _OpenAiModel = Inputty.GetString("Enter model name:", _OpenAiModel, false);
            }
            Console.WriteLine("");
        }

        static async Task ConfigureEndpoints()
        {
            Console.WriteLine("");
            Console.WriteLine("Current configuration:");
            Console.WriteLine($"  Ollama URL: {_OllamaBaseUrl}");
            Console.WriteLine($"  OpenAI URL: {_OpenAiBaseUrl}");
            Console.WriteLine($"  OpenAI Key: {(string.IsNullOrEmpty(_OpenAiApiKey) ? "Not configured" : "***" + _OpenAiApiKey.Substring(Math.Max(0, _OpenAiApiKey.Length - 4)))}");
            Console.WriteLine("");

            if (Inputty.GetBoolean("Update configuration?", false))
            {
                string ollamaUrl = Inputty.GetString("Ollama URL:", _OllamaBaseUrl, false);
                string openAiUrl = Inputty.GetString("OpenAI URL:", _OpenAiBaseUrl, false);
                string apiKey = Inputty.GetString("OpenAI API Key (press Enter to keep current):", null, true);

                _OllamaBaseUrl = ollamaUrl;
                _OpenAiBaseUrl = openAiUrl;
                if (!string.IsNullOrEmpty(apiKey))
                {
                    _OpenAiApiKey = apiKey;
                }

                // Reinitialize SDKs with new configuration
                await InitializeSdks();
            }
            Console.WriteLine("");
        }

        static async Task TestConnectivity()
        {
            Console.WriteLine("");
            Console.WriteLine($"Testing connectivity to {_CurrentProvider}...");

            try
            {
                bool connected = false;

                if (_CurrentProvider == CompletionsProviderEnum.Ollama)
                {
                    connected = await _OllamaCompletions.ValidateConnectivity();
                }
                else
                {
                    connected = await _OpenAiCompletions.ValidateConnectivity();
                }

                if (connected)
                {
                    Console.WriteLine("✓ Successfully connected!");
                }
                else
                {
                    Console.WriteLine("✗ Connection failed. Please check your configuration.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Connection error: {ex.Message}");
            }
            Console.WriteLine("");
        }

        static async Task SimpleCompletion()
        {
            Console.WriteLine("");
            string prompt = Inputty.GetString("Enter prompt:", "Tell me a joke about programming", false);

            GenerateCompletionRequest request = new GenerateCompletionRequest
            {
                Model = _CurrentProvider == CompletionsProviderEnum.Ollama ? _OllamaModel : _OpenAiModel,
                Prompt = prompt,
                Stream = false,
                MaxTokens = 500
            };

            Console.WriteLine("\nGenerating completion...\n");

            try
            {
                GenerateCompletionResult result = null;

                if (_CurrentProvider == CompletionsProviderEnum.Ollama)
                {
                    result = await _OllamaCompletions.GenerateCompletionAsync(request);
                }
                else
                {
                    result = await _OpenAiCompletions.GenerateCompletionAsync(request);
                }

                Console.WriteLine("Response:");
                Console.WriteLine("---------");

                await foreach (var token in result.Tokens)
                {
                    Console.Write(token.Content);
                }

                Console.WriteLine("\n");

                if (result.FirstTokenUtc.HasValue && result.LastTokenUtc.HasValue)
                {
                    var ttft = result.FirstToken;
                    var totalTime = result.TotalTime;
                    Console.WriteLine($"\nTime to first token: {ttft?.TotalMilliseconds:F0}ms");
                    Console.WriteLine($"Total time: {totalTime?.TotalMilliseconds:F0}ms");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine("");
        }

        static async Task StreamingCompletion()
        {
            Console.WriteLine("");
            string prompt = Inputty.GetString("Enter prompt:", "Write a haiku about coding", false);

            GenerateCompletionRequest request = new GenerateCompletionRequest
            {
                Model = _CurrentProvider == CompletionsProviderEnum.Ollama ? _OllamaModel : _OpenAiModel,
                Prompt = prompt,
                Stream = true,
                MaxTokens = 500
            };

            Console.WriteLine("\nStreaming response:");
            Console.WriteLine("-------------------");

            try
            {
                GenerateCompletionResult result = null;

                if (_CurrentProvider == CompletionsProviderEnum.Ollama)
                {
                    result = await _OllamaCompletions.GenerateCompletionAsync(request);
                }
                else
                {
                    result = await _OpenAiCompletions.GenerateCompletionAsync(request);
                }

                int tokenCount = 0;
                DateTime? firstTokenTime = null;

                await foreach (var token in result.Tokens)
                {
                    if (!firstTokenTime.HasValue)
                    {
                        firstTokenTime = DateTime.UtcNow;
                    }

                    Console.Write(token.Content);
                    tokenCount++;

                    if (token.IsComplete)
                    {
                        Console.WriteLine($"\n\n[Completion finished: {token.FinishReason}]");
                    }
                }

                var totalTime = DateTime.UtcNow - result.StartUtc;
                var ttft = firstTokenTime.HasValue ? firstTokenTime.Value - result.StartUtc : TimeSpan.Zero;

                Console.WriteLine($"\nTokens: {tokenCount}");
                Console.WriteLine($"Time to first token: {ttft.TotalMilliseconds:F0}ms");
                Console.WriteLine($"Total time: {totalTime.TotalMilliseconds:F0}ms");

                if (tokenCount > 0 && totalTime.TotalSeconds > 0)
                {
                    Console.WriteLine($"Tokens/second: {tokenCount / totalTime.TotalSeconds:F1}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
            Console.WriteLine("");
        }

        static async Task ChatConversation()
        {
            Console.WriteLine("");
            Console.WriteLine("Interactive chat mode. Type 'exit' to return to main menu.");
            Console.WriteLine("Type 'clear' to clear conversation history.");
            Console.WriteLine("");

            bool inChat = true;

            while (inChat)
            {
                string userMessage = Inputty.GetString("You:", null, false);

                if (userMessage?.ToLower() == "exit")
                {
                    inChat = false;
                    continue;
                }

                if (userMessage?.ToLower() == "clear")
                {
                    _ConversationHistory.Clear();
                    Console.WriteLine("[Conversation history cleared]");
                    continue;
                }

                if (string.IsNullOrEmpty(userMessage))
                {
                    continue;
                }

                _ConversationHistory.Add(new Message { Role = "user", Content = userMessage });

                GenerateCompletionRequest request = new GenerateCompletionRequest
                {
                    Model = _CurrentProvider == CompletionsProviderEnum.Ollama ? _OllamaModel : _OpenAiModel,
                    Messages = _ConversationHistory,
                    Stream = true,
                    MaxTokens = 1000
                };

                Console.Write("\nAssistant: ");

                try
                {
                    GenerateCompletionResult result = null;

                    if (_CurrentProvider == CompletionsProviderEnum.Ollama)
                    {
                        result = await _OllamaCompletions.GenerateCompletionAsync(request);
                    }
                    else
                    {
                        result = await _OpenAiCompletions.GenerateCompletionAsync(request);
                    }

                    StringBuilder assistantResponse = new StringBuilder();

                    await foreach (var token in result.Tokens)
                    {
                        Console.Write(token.Content);
                        assistantResponse.Append(token.Content);
                    }

                    _ConversationHistory.Add(new Message { Role = "assistant", Content = assistantResponse.ToString() });
                    Console.WriteLine("\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}\n");
                }
            }
            Console.WriteLine("");
        }

        static void ShowHistory()
        {
            Console.WriteLine("");
            if (_ConversationHistory.Count == 0)
            {
                Console.WriteLine("No conversation history.");
            }
            else
            {
                Console.WriteLine("Conversation History:");
                Console.WriteLine("=====================");
                foreach (var message in _ConversationHistory)
                {
                    Console.WriteLine($"\n[{message.Role.ToUpper()}]:");
                    Console.WriteLine(message.Content);
                }
            }
            Console.WriteLine("");
        }

        static void ClearHistory()
        {
            _ConversationHistory.Clear();
            Console.WriteLine("\nConversation history cleared.\n");
        }

        static async Task ParameterizedCompletion()
        {
            Console.WriteLine("");
            string prompt = Inputty.GetString("Enter prompt:", "Explain quantum computing", false);

            Console.WriteLine("\nConfigure parameters (press Enter for defaults):");

            double temperature = Inputty.GetDouble("Temperature (0.0-2.0):", 0.7, true, false);
            double topP = Inputty.GetDouble("Top-P (0.0-1.0):", 0.9, true, false);
            int maxTokens = Inputty.GetInteger("Max tokens:", 500, true, false);

            GenerateCompletionRequest request = new GenerateCompletionRequest
            {
                Model = _CurrentProvider == CompletionsProviderEnum.Ollama ? _OllamaModel : _OpenAiModel,
                Prompt = prompt,
                Stream = true,
                Temperature = temperature,
                TopP = topP,
                MaxTokens = maxTokens
            };

            if (_CurrentProvider == CompletionsProviderEnum.Ollama)
            {
                int? topK = Inputty.GetInteger("Top-K (1-1000, or 0 to skip):", 0, true, false);
                if (topK > 0) request.TopK = topK;

                double? repeatPenalty = Inputty.GetDouble("Repeat penalty (0.0-2.0, or -1 to skip):", -1, true, false);
                if (repeatPenalty >= 0) request.RepeatPenalty = repeatPenalty;
            }
            else
            {
                double? presencePenalty = Inputty.GetDouble("Presence penalty (-2.0-2.0, or -999 to skip):", -999, true, false);
                if (presencePenalty != -999) request.PresencePenalty = presencePenalty;

                double? frequencyPenalty = Inputty.GetDouble("Frequency penalty (-2.0-2.0, or -999 to skip):", -999, true, false);
                if (frequencyPenalty != -999) request.FrequencyPenalty = frequencyPenalty;
            }

            Console.WriteLine("\nGenerating with custom parameters...\n");

            try
            {
                GenerateCompletionResult result = null;

                if (_CurrentProvider == CompletionsProviderEnum.Ollama)
                {
                    result = await _OllamaCompletions.GenerateCompletionAsync(request);
                }
                else
                {
                    result = await _OpenAiCompletions.GenerateCompletionAsync(request);
                }

                await foreach (var token in result.Tokens)
                {
                    Console.Write(token.Content);
                }
                Console.WriteLine("\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine("");
        }

        static async Task FileCompletion()
        {
            Console.WriteLine("");
            string inputFile = Inputty.GetString("Input file path:", "prompt.txt", false);
            string outputFile = Inputty.GetString("Output file path:", "response.txt", false);

            try
            {
                if (!File.Exists(inputFile))
                {
                    Console.WriteLine($"Input file not found: {inputFile}");
                    return;
                }

                string prompt = File.ReadAllText(inputFile);
                Console.WriteLine($"\nLoaded prompt ({prompt.Length} characters)");

                GenerateCompletionRequest request = new GenerateCompletionRequest
                {
                    Model = _CurrentProvider == CompletionsProviderEnum.Ollama ? _OllamaModel : _OpenAiModel,
                    Prompt = prompt,
                    Stream = true,
                    MaxTokens = 2000
                };

                Console.WriteLine("Generating response...\n");

                GenerateCompletionResult result = null;

                if (_CurrentProvider == CompletionsProviderEnum.Ollama)
                {
                    result = await _OllamaCompletions.GenerateCompletionAsync(request);
                }
                else
                {
                    result = await _OpenAiCompletions.GenerateCompletionAsync(request);
                }

                StringBuilder response = new StringBuilder();
                int tokenCount = 0;

                await foreach (var token in result.Tokens)
                {
                    Console.Write(token.Content);
                    response.Append(token.Content);
                    tokenCount++;
                }

                File.WriteAllText(outputFile, response.ToString());
                Console.WriteLine($"\n\nResponse saved to {outputFile} ({tokenCount} tokens, {response.Length} characters)");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine("");
        }

        static async Task MultiTurnConversation()
        {
            Console.WriteLine("");
            Console.WriteLine("Multi-turn conversation test");
            Console.WriteLine("This will simulate a 3-turn conversation.");
            Console.WriteLine("");

            List<Message> conversation = new List<Message>();

            string[] turns = new string[]
            {
                "Hello! What's your name?",
                "Nice to meet you. Can you help me with math?",
                "What's 42 multiplied by 17?"
            };

            try
            {
                foreach (string turn in turns)
                {
                    Console.WriteLine($"User: {turn}");
                    conversation.Add(new Message { Role = "user", Content = turn });

                    GenerateCompletionRequest request = new GenerateCompletionRequest
                    {
                        Model = _CurrentProvider == CompletionsProviderEnum.Ollama ? _OllamaModel : _OpenAiModel,
                        Messages = conversation,
                        Stream = true,
                        MaxTokens = 200
                    };

                    Console.Write("Assistant: ");

                    GenerateCompletionResult result = null;

                    if (_CurrentProvider == CompletionsProviderEnum.Ollama)
                    {
                        result = await _OllamaCompletions.GenerateCompletionAsync(request);
                    }
                    else
                    {
                        result = await _OpenAiCompletions.GenerateCompletionAsync(request);
                    }

                    StringBuilder response = new StringBuilder();

                    await foreach (var token in result.Tokens)
                    {
                        Console.Write(token.Content);
                        response.Append(token.Content);
                    }

                    conversation.Add(new Message { Role = "assistant", Content = response.ToString() });
                    Console.WriteLine("\n");

                    await Task.Delay(500); // Brief pause between turns
                }

                Console.WriteLine("Multi-turn conversation completed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine("");
        }

        static async Task CompareProviders()
        {
            Console.WriteLine("");

            if (string.IsNullOrEmpty(_OpenAiApiKey))
            {
                Console.WriteLine("OpenAI API key not configured. Cannot compare providers.");
                return;
            }

            string prompt = Inputty.GetString("Enter prompt to compare:", "Explain recursion in one sentence", false);

            Console.WriteLine("\nComparing responses from both providers...\n");

            // Test Ollama
            Console.WriteLine($"=== Ollama ({_OllamaModel}) ===");
            try
            {
                GenerateCompletionRequest ollamaRequest = new GenerateCompletionRequest
                {
                    Model = _OllamaModel,
                    Prompt = prompt,
                    Stream = false,
                    MaxTokens = 200
                };

                var ollamaStart = DateTime.UtcNow;
                var ollamaResult = await _OllamaCompletions.GenerateCompletionAsync(ollamaRequest);

                await foreach (var token in ollamaResult.Tokens)
                {
                    Console.Write(token.Content);
                }

                var ollamaTime = DateTime.UtcNow - ollamaStart;
                Console.WriteLine($"\n[Time: {ollamaTime.TotalMilliseconds:F0}ms]\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\n");
            }

            // Test OpenAI
            Console.WriteLine($"=== OpenAI ({_OpenAiModel}) ===");
            try
            {
                GenerateCompletionRequest openAiRequest = new GenerateCompletionRequest
                {
                    Model = _OpenAiModel,
                    Prompt = prompt,
                    Stream = false,
                    MaxTokens = 200
                };

                var openAiStart = DateTime.UtcNow;
                var openAiResult = await _OpenAiCompletions.GenerateCompletionAsync(openAiRequest);

                await foreach (var token in openAiResult.Tokens)
                {
                    Console.Write(token.Content);
                }

                var openAiTime = DateTime.UtcNow - openAiStart;
                Console.WriteLine($"\n[Time: {openAiTime.TotalMilliseconds:F0}ms]\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\n");
            }

            Console.WriteLine("");
        }

        static async Task BenchmarkStreaming()
        {
            Console.WriteLine("");
            int iterations = Inputty.GetInteger("Number of iterations:", 3, true, false);
            string prompt = Inputty.GetString("Enter prompt:", "Write a short story about a robot", false);

            Console.WriteLine($"\nBenchmarking {_CurrentProvider} with {iterations} iterations...\n");

            List<double> ttftTimes = new List<double>();
            List<double> totalTimes = new List<double>();
            List<int> tokenCounts = new List<int>();

            for (int i = 1; i <= iterations; i++)
            {
                Console.WriteLine($"Iteration {i}/{iterations}...");

                try
                {
                    GenerateCompletionRequest request = new GenerateCompletionRequest
                    {
                        Model = _CurrentProvider == CompletionsProviderEnum.Ollama ? _OllamaModel : _OpenAiModel,
                        Prompt = prompt,
                        Stream = true,
                        MaxTokens = 200
                    };

                    GenerateCompletionResult result = null;

                    if (_CurrentProvider == CompletionsProviderEnum.Ollama)
                    {
                        result = await _OllamaCompletions.GenerateCompletionAsync(request);
                    }
                    else
                    {
                        result = await _OpenAiCompletions.GenerateCompletionAsync(request);
                    }

                    int tokenCount = 0;
                    DateTime? firstTokenTime = null;

                    await foreach (var token in result.Tokens)
                    {
                        if (!firstTokenTime.HasValue)
                        {
                            firstTokenTime = DateTime.UtcNow;
                        }
                        tokenCount++;
                    }

                    if (firstTokenTime.HasValue)
                    {
                        var ttft = (firstTokenTime.Value - result.StartUtc).TotalMilliseconds;
                        var total = (DateTime.UtcNow - result.StartUtc).TotalMilliseconds;

                        ttftTimes.Add(ttft);
                        totalTimes.Add(total);
                        tokenCounts.Add(tokenCount);

                        Console.WriteLine($"  TTFT: {ttft:F0}ms, Total: {total:F0}ms, Tokens: {tokenCount}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"  Error: {ex.Message}");
                }

                if (i < iterations)
                {
                    await Task.Delay(1000); // Brief pause between iterations
                }
            }

            if (ttftTimes.Count > 0)
            {
                Console.WriteLine("\nBenchmark Results:");
                Console.WriteLine("==================");
                Console.WriteLine($"Average TTFT: {ttftTimes.Average():F0}ms");
                Console.WriteLine($"Average Total: {totalTimes.Average():F0}ms");
                Console.WriteLine($"Average Tokens: {tokenCounts.Average():F0}");
                Console.WriteLine($"Average Tokens/sec: {(tokenCounts.Average() / (totalTimes.Average() / 1000)):F1}");
            }
            Console.WriteLine("");
        }

        static async Task TestStopSequences()
        {
            Console.WriteLine("");
            string prompt = Inputty.GetString("Enter prompt:", "Count from 1 to 10:", false);
            string stopSeq = Inputty.GetString("Enter stop sequence:", "5", false);

            GenerateCompletionRequest request = new GenerateCompletionRequest
            {
                Model = _CurrentProvider == CompletionsProviderEnum.Ollama ? _OllamaModel : _OpenAiModel,
                Prompt = prompt,
                Stream = true,
                MaxTokens = 200,
                Stop = new List<string> { stopSeq }
            };

            Console.WriteLine($"\nGenerating with stop sequence '{stopSeq}'...\n");

            try
            {
                GenerateCompletionResult result = null;

                if (_CurrentProvider == CompletionsProviderEnum.Ollama)
                {
                    result = await _OllamaCompletions.GenerateCompletionAsync(request);
                }
                else
                {
                    result = await _OpenAiCompletions.GenerateCompletionAsync(request);
                }

                await foreach (var token in result.Tokens)
                {
                    Console.Write(token.Content);

                    if (token.IsComplete)
                    {
                        Console.WriteLine($"\n\n[Stopped: {token.FinishReason}]");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine("");
        }

        static async Task TestJsonMode()
        {
            if (_CurrentProvider != CompletionsProviderEnum.OpenAI)
            {
                Console.WriteLine("\nJSON mode is only available for OpenAI provider.\n");
                return;
            }

            Console.WriteLine("");
            string prompt = Inputty.GetString(
                "Enter prompt (will request JSON):",
                "List 3 programming languages with their year of creation",
                false);

            GenerateCompletionRequest request = new GenerateCompletionRequest
            {
                Model = _OpenAiModel,
                Messages = new List<Message>
                {
                    new Message
                    {
                        Role = "system",
                        Content = "You are a helpful assistant that responds in valid JSON format."
                    },
                    new Message
                    {
                        Role = "user",
                        Content = prompt + " Respond in JSON format."
                    }
                },
                Stream = false,
                MaxTokens = 500,
                ResponseFormat = "json"
            };

            Console.WriteLine("\nGenerating JSON response...\n");

            try
            {
                var result = await _OpenAiCompletions.GenerateCompletionAsync(request);

                await foreach (var token in result.Tokens)
                {
                    Console.Write(token.Content);
                }
                Console.WriteLine("\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine("");
        }

        static void ToggleLogging()
        {
            bool currentState = _CurrentProvider == CompletionsProviderEnum.Ollama
                ? _OllamaCompletions.LogRequests
                : _OpenAiCompletions.LogRequests;

            bool newState = !currentState;

            _OllamaCompletions.LogRequests = newState;
            _OllamaCompletions.LogResponses = newState;
            _OpenAiCompletions.LogRequests = newState;
            _OpenAiCompletions.LogResponses = newState;

            Console.WriteLine($"\nLogging {(newState ? "enabled" : "disabled")} for all providers.\n");
        }

#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
}