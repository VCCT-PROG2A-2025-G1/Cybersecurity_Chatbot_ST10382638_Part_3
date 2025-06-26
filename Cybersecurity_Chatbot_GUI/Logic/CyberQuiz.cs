// Name: Luc de Marillac St Julein
// Student Number: ST10382638
// Group: 1

// References:
//   https://www.w3schools.com/cs/index.php#gsc.tab=0
//   https://stackoverflow.com/questions
//   ChatGPT, OpenAI (2025), assisted with quiz scoring logic, string building, and input validation.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cybersecurity_Chatbot_GUI.Logic
{
    internal class CyberQuiz
    {
        private int _currentQuestionIndex = 0;
        private int _score = 0;
        private bool _quizInProgress = false;
        private List<Question> _questions;

        public bool QuizInProgress => _quizInProgress;

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Constructor: Initializes quiz questions and answers.
        /// </summary>
        public CyberQuiz()
        {
            _questions = new List<Question>
            {
                new Question("What is phishing?", new[]
                {
                    "A type of fishing sport",
                    "A technique to trick users into revealing sensitive info",
                    "A virus removal tool",
                    "None of the above"
                }, 1, "Phishing is when attackers impersonate legitimate services to steal credentials."),

                new Question("True or False: You should reuse passwords for convenience.", null, 1,
                    "False—using unique passwords across sites prevents a single breach from compromising multiple accounts.", true),

                new Question("Which one is a strong password?", new[]
                {
                    "123456", "password", "MyDog2023!", "qwerty"
                }, 2, "‘MyDog2023!’ is strong because it’s long, includes uppercase/lowercase letters, numbers, and a symbol."),

                new Question("True or False: Public Wi-Fi is always safe to use for banking.", null, 1,
                    "False—public Wi-Fi networks can be insecure, making it easy for attackers to intercept your data.", true),

                new Question("What does 2FA stand for?", new[]
                {
                    "Two-Factor Authentication",
                    "Two-Firewall Access",
                    "Trusted File Algorithm",
                    "None of the above"
                }, 0, "2FA stands for Two-Factor Authentication—an extra security layer beyond just a password."),

                new Question("True or False: Antivirus software should be updated regularly.", null, 0,
                    "True—up-to-date antivirus definitions help the software detect the latest threats.", true),

                new Question("What is a common sign of a phishing email?", new[]
                {
                    "Proper grammar",
                    "Personal greeting",
                    "Urgent request for login info",
                    "No links at all"
                }, 2, "Phishing emails often create a false sense of urgency to trick you into revealing login credentials."),

                new Question("True or False: HTTPS is more secure than HTTP.", null, 0,
                    "True—HTTPS encrypts your communication with the website, protecting data in transit.", true),

                new Question("Which is a social engineering technique?", new[]
                {
                    "Brute-force attack",
                    "Shoulder surfing",
                    "SQL injection",
                    "VPN tunneling"
                }, 1, "Shoulder surfing is when someone looks over your shoulder to obtain sensitive information."),

                new Question("True or False: It's safe to click pop-ups offering free prizes.", null, 1,
                    "False—pop-ups offering free prizes are often used to distribute malware or phishing attacks.", true)
            };
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Starts the quiz and displays the first question.
        /// </summary>
        public string StartQuiz()
        {
            _quizInProgress = true;
            _currentQuestionIndex = 0;
            _score = 0;
            return $"Cybersecurity Quiz started! Answer the following questions:\n\n{GetNextQuestion()}";
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Submits the user's answer, checks correctness, and returns feedback.
        /// </summary>
        public string SubmitAnswer(string input)
        {
            if (!_quizInProgress)
                return "No quiz in progress.";

            var current = _questions[_currentQuestionIndex];

            if (!current.IsValidInput(input.Trim()))
            {
                string prompt = current.IsTrueFalse
                    ? "Please enter True or False."
                    : $"Please enter a valid number between 1 and {current.Options.Length}.";

                return $"Invalid answer. {prompt}\n\n{current.Formatted}";
            }

            bool correct = current.IsCorrect(input.Trim());
            if (correct) _score++;

            var sb = new StringBuilder();
            sb.AppendLine(correct ? "✅ Correct!" : "❌ Incorrect.");
            sb.AppendLine(current.Explanation);
            sb.AppendLine();

            _currentQuestionIndex++;

            if (_currentQuestionIndex >= _questions.Count)
            {
                _quizInProgress = false;

                int total = _questions.Count;
                double pct = (double)_score / total;
                string msg;

                if (pct >= 0.9)
                    msg = "Excellent work! You’re a cybersecurity pro!";
                else if (pct >= 0.5)
                    msg = "Good job—keep studying to sharpen your skills!";
                else
                    msg = "You scored below 50%. Review the basics and try again!";

                sb.AppendLine($"Quiz complete! You scored {_score}/{total}. {msg}");
                return sb.ToString();
            }

            sb.Append(_questions[_currentQuestionIndex].Formatted);
            return sb.ToString();
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Returns the next question to be answered.
        /// </summary>
        public string GetNextQuestion()
        {
            if (_currentQuestionIndex >= _questions.Count)
                return "No more questions.";

            return _questions[_currentQuestionIndex].Formatted;
        }
    }
}
//------------------------------------------...ooo000 END OF FILE 000ooo...------------------------------------------------------//
