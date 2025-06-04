using Cybersecurity_Chatbot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Cybersecurity_Chatbot_GUI.Logic
{
    internal class WpfChatBot
    {
        private string _username;
        private bool _exitRequested;
        private bool _awaitingGoodbye;
        private bool _launchTaskWindow;

        public string Username => _username;
        public bool IsExitRequested => _exitRequested;
        public bool ShouldLaunchTaskWindow => _launchTaskWindow;

        private CyberQuiz _quiz = new CyberQuiz();
        public WpfChatBot()
        {
            _username = "";
            _exitRequested = false;
            _awaitingGoodbye = false;
        }

        public async Task<string> InitializeAsync()
        {
            await StartUp.StartupAsync();
            return "ChatBot: Hello! What is your name?";
        }

        public string SetUsername(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return "ChatBot: Please enter a valid name.";
            }

            _username = input.Trim();
            ActivityLog.Log($"User set name to {_username}");
            return $"Hello {_username}, nice to meet you!";
        }

        public string ProcessInput(string input)
        {
            _launchTaskWindow = false;

            if (string.IsNullOrWhiteSpace(input))
                return "Please enter something so I can help you.";

            input = input.Trim();

            if (_awaitingGoodbye)
            {
                string confirm = input.ToLower();
                if (confirm == "no" || confirm == "n")
                {
                    _exitRequested = true;
                    return $"Goodbye {_username}, stay cyber safe!";
                }
                else
                {
                    _awaitingGoodbye = false;
                    return "Great! What else would you like to know?";
                }
            }

            if (input.ToLower() == "view log")
            {
                return $"Activity Log:\n\n{ActivityLog.GetFormattedLog()}";
            }

            if (_quiz.QuizInProgress)
            {
                ActivityLog.Log("Answered a quiz question.");
                return _quiz.SubmitAnswer(input);
            }

            if (input.ToLower() == "start quiz")
            {
                ActivityLog.Log("Started cybersecurity quiz.");
                return _quiz.StartQuiz();
            }

            if (input.Contains("remind me") || input.Contains("add task") || input.Contains("set reminder"))
            {
                _launchTaskWindow = true;
                ActivityLog.Log("Opened Task Assistant window.");
                return "Opening the Task Assistant window for you...";
            }

            string response = ResponseBank.GetResponse(input);

            if (response == "Goodbye")
            {
                _awaitingGoodbye = true;
                return "Before you go, would you like to ask anything else? (yes/no)";
            }

            return response;
        }

        public void Reset()
        {
            _username = "";
            _exitRequested = false;
            _awaitingGoodbye = false;
            Memory.Clear();
        }

        public async Task<string> PlayStartupAsyncWpf()
        {
            string asciiArt = string.Join(Environment.NewLine, new[]
            {
                "      .-\"      \"-.",
                "     /            \\",
                "    |              |",
                "    |,  .-.  .-.  ,|",
                "    | )(_o/  \\o_)( |",
                "    |/     /\\     \\|",
                "    (_     ^^     _)",
                "     \\__|IIIIII|__/",
                "      | \\IIIIII/ |",
                "      \\          /",
                "       `--------`",
                "      GHOST SHELL"
            });

            string filePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources\Greeting Message.wav"));
            if (File.Exists(filePath))
            {
                SoundPlayer player = new SoundPlayer(filePath);
                await Task.Run(() => player.PlaySync());
            }

            return asciiArt;
        }

    }
}