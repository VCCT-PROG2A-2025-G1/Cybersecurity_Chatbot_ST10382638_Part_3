using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersecurity_Chatbot_GUI.Logic
{
    internal class Question
    {
        public string Text { get; }
        public string[] Options { get; }
        public int CorrectIndex { get; }
        public bool IsTrueFalse { get; }

        public Question(string text, string[] options, int correctIndex, bool isTrueFalse = false)
        {
            Text = text;
            Options = options;
            CorrectIndex = correctIndex;
            IsTrueFalse = isTrueFalse;
        }

        public bool IsCorrect(string input)
        {
            if (IsTrueFalse)
            {
                return (CorrectIndex == 0 && input.ToLower() == "true") ||
                       (CorrectIndex == 1 && input.ToLower() == "false");
            }
            else
            {
                if (int.TryParse(input, out int userChoice))
                {
                    return userChoice == CorrectIndex + 1; // UI shows 1-based index
                }
            }

            return false;
        }

        public string Formatted
        {
            get
            {
                if (IsTrueFalse)
                    return $"Q: {Text} (True/False)";
                else
                {
                    string formatted = $"Q: {Text}\n";
                    for (int i = 0; i < Options.Length; i++)
                    {
                        formatted += $"{i + 1}. {Options[i]}\n";
                    }
                    return formatted.Trim();
                }
            }
        }
    }
}
