// Name: Luc de Marillac St Julein
// Student Number: ST10382638
// Group: 1

// References:
//   https://www.w3schools.com/cs/index.php#gsc.tab=0
//   https://stackoverflow.com/questions
//   ChatGPT, OpenAI (2025), assisted with async flow, chatbot logic, and system I/O methods.

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
        // User and system state
        private string _username;
        private bool _exitRequested;
        private bool _awaitingGoodbye;
        private bool _launchTaskWindow;
        private bool _launchLogWindow;
        private int _logOffset = 0;

        // Quiz instance
        private CyberQuiz _quiz = new CyberQuiz();

        // Public properties
        public string Username => _username;
        public bool IsExitRequested => _exitRequested;
        public bool ShouldLaunchTaskWindow => _launchTaskWindow;
        public bool ShouldLaunchLogWindow => _launchLogWindow;

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Constructor: Initializes default bot state
        /// </summary>
        public WpfChatBot()
        {
            _username = "";
            _exitRequested = false;
            _awaitingGoodbye = false;
            _launchTaskWindow = false;
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Loads startup messages asynchronously (optional)
        /// </summary>
        public async Task<string> InitializeAsync()
        {
            await StartUp.StartupAsync();
            return "ChatBot: Hello! What is your name?";
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Saves the user's name and logs the event
        /// </summary>
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

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Main logic dispatcher based on input and intent
        /// </summary>
        public string ProcessInput(string input)
        {
            _launchTaskWindow = false;
            _launchLogWindow = false;

            input = input?.Trim() ?? "";
            if (string.IsNullOrEmpty(input))
                return "Please say something!";

            var intent = IntentRecognizer.Recognize(input);
            ActivityLog.Log($"Recognized intent: {intent}");

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
                    ActivityLog.Log("ShowLog intent");
                    _launchLogWindow = true;
                    return "Opening Activity Log…";

                default:
                    return ResponseBank.GetResponse(input);
            }
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Shows the last 5 entries from the activity log
        /// </summary>
        private string ShowLogEntries()
        {
            var all = ActivityLog.GetEntries();
            var paged = all
                .Skip(Math.Max(0, all.Count - 5 - _logOffset))
                .Take(5)
                .ToList();

            if (!paged.Any())
                return "No more log entries.";

            return "Activity Log:\n" + string.Join("\n", paged);
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Resets the chatbot session and clears memory
        /// </summary>
        public void Reset()
        {
            _username = "";
            _exitRequested = false;
            _awaitingGoodbye = false;
            Memory.Clear();
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Plays ASCII art greeting and sound effect on app start
        /// </summary>
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
//------------------------------------------...ooo000 END OF FILE 000ooo...------------------------------------------------------//
