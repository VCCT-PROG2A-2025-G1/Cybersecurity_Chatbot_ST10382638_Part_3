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
            return $"ChatBot: Hello {_username}, nice to meet you!";
        }

        public string ProcessInput(string input)
        {
            _launchTaskWindow = false; // ✅ Always reset this at start
            if (string.IsNullOrWhiteSpace(input))
            {
                return "ChatBot: Please enter something so I can help you.";
            }

            input = input.Trim();

            // Handle exit confirmation
            if (_awaitingGoodbye)
            {
                string confirm = input.ToLower();
                if (confirm == "no" || confirm == "n")
                {
                    _exitRequested = true;
                    return $"ChatBot: Goodbye {_username}, stay cyber safe!";
                }
                else
                {
                    _awaitingGoodbye = false;
                    return "ChatBot: Great! What else would you like to know?";
                }
            }

            // ✅ NLP Trigger: Task Assistant
            if (input.Contains("remind me") || input.Contains("add task") || input.Contains("set reminder"))
            {
                _launchTaskWindow = true;
                return "ChatBot: Opening the Task Assistant window for you...";
            }

            // Standard chatbot response
            string response = ResponseBank.GetResponse(input);

            // Check for exit keyword
            if (response == "Goodbye")
            {
                _awaitingGoodbye = true;
                return "ChatBot: Before you go, would you like to ask anything else? (yes/no)";
            }

            _launchTaskWindow = false; // Reset task trigger after handling
            return $"ChatBot: {response}";
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
