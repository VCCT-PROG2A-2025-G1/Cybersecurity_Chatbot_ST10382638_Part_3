// Name: Luc de Marillac St Julein
// Student Number: ST10382638
// Group: 1

// References:
//   https://www.w3schools.com/cs/index.php#gsc.tab=0
//   https://stackoverflow.com/questions
//   ChatGPT, OpenAI (2025), assisted with validation logic, string formatting, and class structure.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersecurity_Chatbot_GUI.Logic
{
    internal class Question
    {
        // Question properties
        public string Text { get; }
        public string[] Options { get; }
        public int CorrectIndex { get; }
        public bool IsTrueFalse { get; }
        public string Explanation { get; }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Constructor to initialize question content and configuration.
        /// </summary>
        public Question(string text, string[] options, int correctIndex, string explanation, bool isTrueFalse = false)
        {
            Text = text;
            Options = options;
            CorrectIndex = correctIndex;
            Explanation = explanation;
            IsTrueFalse = isTrueFalse;
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Checks if the user's input is correct for the current question.
        /// </summary>
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

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Returns a formatted string representing the question for display.
        /// </summary>
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

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Validates the format and range of a user’s input.
        /// </summary>
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
//------------------------------------------...ooo000 END OF FILE 000ooo...------------------------------------------------------//
