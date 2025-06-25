using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cybersecurity_Chatbot_GUI.Logic
{
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
        public static Intent Recognize(string input)
        {
            var lower = input.ToLowerInvariant().Trim();

            // 1) Pure number → quiz answer
            // answer quiz if it's a number _or_ a True/False response
            if (Regex.IsMatch(lower, @"^\d+$")
             || lower == "true"
             || lower == "false")
            {
                return Intent.SubmitQuizAnswer;
            }

            // 2) Start quiz
            if (Regex.IsMatch(lower, @"\b(start|begin)\s+(quiz|test)\b")
             || Regex.IsMatch(lower, @"\bquiz me\b"))
                return Intent.StartQuiz;

            // 3) Add task
            if (Regex.IsMatch(lower, @"\b(add|create)\s+(a\s)?task\b"))
                return Intent.AddTask;

            // 4) Open/show tasks
            if (Regex.IsMatch(lower, @"\b(show|open|view)\s+(tasks|reminders)\b"))
                return Intent.OpenTasks;

            // 5) Show log
            if (Regex.IsMatch(lower, @"\b(show|view)\s+(log|history)\b"))
                return Intent.ShowLog;

            // 6) Show more
            if (Regex.IsMatch(lower, @"\b(show|view)\s+(more|next)\b"))
                return Intent.ShowMoreLog;

            return Intent.Unknown;
        }

    }
}
