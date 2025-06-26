// Name: Luc de Marillac St Julein  
// Student Number: ST10382638  
// Group: 1  

// References:  
//   https://www.w3schools.com/cs/index.php#gsc.tab=0  
//   https://stackoverflow.com/questions  
//   ChatGPT, OpenAI (2025), assisted with regular expressions and intent parsing logic.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cybersecurity_Chatbot_GUI.Logic
{
    /// <summary>
    /// Enum listing the types of chatbot intents
    /// </summary>
    public enum Intent
    {
        AddTask,
        OpenTasks,
        StartQuiz,
        SubmitQuizAnswer,
        ShowLog,
        ShowMoreLog,
        Unknown
    }

    internal class IntentRecognizer
    {
        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Attempts to classify a user's input into a known chatbot intent using regex.
        /// </summary>
        /// <param name="input">The raw string entered by the user</param>
        /// <returns>The matched Intent enum value</returns>
        public static Intent Recognize(string input)
        {
            var lower = input.ToLowerInvariant().Trim();

            // 1) Pure number or boolean answer → quiz answer
            if (Regex.IsMatch(lower, @"^\d+$")
             || lower == "true"
             || lower == "false")
            {
                return Intent.SubmitQuizAnswer;
            }

            // 2) Start quiz
            if (Regex.IsMatch(lower, @"\b(start|begin)\s+(quiz|test)\b")
             || Regex.IsMatch(lower, @"\bquiz me\b"))
            {
                return Intent.StartQuiz;
            }

            // 3) Add task
            if (Regex.IsMatch(lower, @"\b(add|create)\s+(a\s)?task\b"))
            {
                return Intent.AddTask;
            }

            // 4) Open/show tasks
            if (Regex.IsMatch(lower, @"\b(show|open|view)\s+(tasks|reminders)\b"))
            {
                return Intent.OpenTasks;
            }

            // 5) Show log
            if (Regex.IsMatch(lower, @"\b(show|view)\s+(log|history)\b"))
            {
                return Intent.ShowLog;
            }

            // 6) Show more log entries
            if (Regex.IsMatch(lower, @"\b(show|view)\s+(more|next)\b"))
            {
                return Intent.ShowMoreLog;
            }

            // Default fallback
            return Intent.Unknown;
        }
    }
}
//------------------------------------------...ooo000 END OF FILE 000ooo...------------------------------------------------------//
