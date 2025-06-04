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
                    if (userChoice < 1 || userChoice > Options.Length)
                    {
                        return false;
                    }
                    return userChoice == CorrectIndex + 1;
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

        public bool IsValidInput(string input)
        {
            if (IsTrueFalse)
            {
                return input.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                       input.Equals("false", StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                if (int.TryParse(input, out int choice))
                {
                    return choice >= 1 && choice <= Options.Length;
                }
                return false;
            }
        }

    }
}
