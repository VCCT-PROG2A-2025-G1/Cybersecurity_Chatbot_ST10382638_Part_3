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

        public CyberQuiz()
        {
            _questions = new List<Question>
{
                new Question(
                    "What is phishing?",
                    new[] {
                        "A type of fishing sport",
                        "A technique to trick users into revealing sensitive info",
                        "A virus removal tool",
                        "None of the above"
                    },
                    correctIndex: 1,
                    explanation: "Phishing is when attackers impersonate legitimate services to steal credentials."
                ),

                new Question(
                    "True or False: You should reuse passwords for convenience.",
                    null,
                    correctIndex: 1,
                    explanation: "False—using unique passwords across sites prevents a single breach from compromising multiple accounts.",
                    isTrueFalse: true
                ),

                new Question(
                    "Which one is a strong password?",
                    new[] {
                        "123456",
                        "password",
                        "MyDog2023!",
                        "qwerty"
                    },
                    correctIndex: 2,
                    explanation: "‘MyDog2023!’ is strong because it’s long, includes uppercase/lowercase letters, numbers, and a symbol."
                ),

                new Question(
                    "True or False: Public Wi-Fi is always safe to use for banking.",
                    null,
                    correctIndex: 1,
                    explanation: "False—public Wi-Fi networks can be insecure, making it easy for attackers to intercept your data.",
                    isTrueFalse: true
                ),

                new Question(
                    "What does 2FA stand for?",
                    new[] {
                        "Two-Factor Authentication",
                        "Two-Firewall Access",
                        "Trusted File Algorithm",
                        "None of the above"
                    },
                    correctIndex: 0,
                    explanation: "2FA stands for Two-Factor Authentication—an extra security layer beyond just a password."
                ),

                new Question(
                    "True or False: Antivirus software should be updated regularly.",
                    null,
                    correctIndex: 0,
                    explanation: "True—up-to-date antivirus definitions help the software detect the latest threats.",
                    isTrueFalse: true
                ),

                new Question(
                    "What is a common sign of a phishing email?",
                    new[] {
                        "Proper grammar",
                        "Personal greeting",
                        "Urgent request for login info",
                        "No links at all"
                    },
                    correctIndex: 2,
                    explanation: "Phishing emails often create a false sense of urgency to trick you into revealing login credentials."
                ),

                new Question(
                    "True or False: HTTPS is more secure than HTTP.",
                    null,
                    correctIndex: 0,
                    explanation: "True—HTTPS encrypts your communication with the website, protecting data in transit.",
                    isTrueFalse: true
                ),

                new Question(
                    "Which is a social engineering technique?",
                    new[] {
                        "Brute-force attack",
                        "Shoulder surfing",
                        "SQL injection",
                        "VPN tunneling"
                    },
                    correctIndex: 1,
                    explanation: "Shoulder surfing is when someone looks over your shoulder to obtain sensitive information."
                ),

                new Question(
                    "True or False: It's safe to click pop-ups offering free prizes.",
                    null,
                    correctIndex: 1,
                    explanation: "False—pop-ups offering free prizes are often used to distribute malware or phishing attacks.",
                    isTrueFalse: true
                )
            };

        }

        public string StartQuiz()
        {
            _quizInProgress = true;
            _currentQuestionIndex = 0;
            _score = 0;
            return $"Cybersecurity Quiz started! Answer the following questions:\n\n{GetNextQuestion()}";
        }

        public string SubmitAnswer(string input)
        {
            if (!_quizInProgress)
                return "No quiz in progress.";

            var current = _questions[_currentQuestionIndex];

            // 1) Validate input as before
            if (!current.IsValidInput(input.Trim()))
            {
                string prompt = current.IsTrueFalse
                    ? "Please enter True or False."
                    : $"Please enter a valid number between 1 and {current.Options.Length}.";
                return $"Invalid answer. {prompt}\n\n{current.Formatted}";
            }

            // 2) Check correctness and update score
            bool correct = current.IsCorrect(input.Trim());
            if (correct) _score++;

            // 3) Build immediate feedback + explanation
            var sb = new StringBuilder();
            sb.AppendLine(correct ? "✅ Correct!" : "❌ Incorrect.");
            sb.AppendLine(current.Explanation);
            sb.AppendLine();

            // 4) Advance to next question
            _currentQuestionIndex++;

            // 5) If that was the last question → final summary
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

            // 6) Otherwise, append the next question inline
            sb.Append(_questions[_currentQuestionIndex].Formatted);
            return sb.ToString();
        }




        public string GetNextQuestion()
        {
            if (_currentQuestionIndex >= _questions.Count)
                return "No more questions.";

            return _questions[_currentQuestionIndex].Formatted;
        }
    }
}
