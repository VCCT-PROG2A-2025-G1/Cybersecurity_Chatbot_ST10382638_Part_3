using Cybersecurity_Chatbot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        private int _logOffset = 0;

        private CyberQuiz _quiz = new CyberQuiz();
        public WpfChatBot()
        {
            _username = "";
            _exitRequested = false;
            _awaitingGoodbye = false;
            _launchTaskWindow = false;
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
            // reset window‐launch flag on every new input
            _launchTaskWindow = false;

            input = input?.Trim() ?? "";
            if (string.IsNullOrEmpty(input))
                return "Please say something!";

            // 1) Recognise intent
            var intent = IntentRecognizer.Recognize(input);
            ActivityLog.Log($"Recognized intent: {intent}");

            // 2) Dispatch
            switch (intent)
            {
                case Intent.SubmitQuizAnswer:
                    return _quiz.SubmitAnswer(input);

                case Intent.StartQuiz:
                    ActivityLog.Log("Quiz started");
                    return _quiz.StartQuiz();

                case Intent.AddTask:
                case Intent.OpenTasks:
                    ActivityLog.Log($"{intent} intent");
                    _launchTaskWindow = true;
                    return intent == Intent.AddTask
                        ? "Adding a new task…"
                        : "Opening Task Assistant…";

                case Intent.ShowLog:
                    _logOffset = 0;
                    return ShowLogEntries();

                case Intent.ShowMoreLog:
                    _logOffset += 5;
                    return ShowLogEntries();

                default:
                    return ResponseBank.GetResponse(input);
            }
        }

        private string ShowLogEntries()
        {
            var all = ActivityLog.GetEntries();           // returns List<string>
            var paged = all
                .Skip(Math.Max(0, all.Count - 5 - _logOffset))
                .Take(5)
                .ToList();

            if (!paged.Any())
                return "No more log entries.";

            return "Activity Log:\n" + string.Join("\n", paged);
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