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
                new Question("What is phishing?", new[] {
                    "A type of fishing sport",
                    "A technique to trick users into revealing sensitive info",
                    "A virus removal tool",
                    "None of the above"
                }, 1),

                new Question("True or False: You should reuse passwords for convenience.", null, 1, true),

                new Question("Which one is a strong password?", new[] {
                    "123456",
                    "password",
                    "MyDog2023!",
                    "qwerty"
                }, 2),

                new Question("True or False: Public Wi-Fi is always safe to use for banking.", null, 1, true),

                new Question("What does 2FA stand for?", new[] {
                    "Two-Factor Authentication",
                    "Two-Firewall Access",
                    "Trusted File Algorithm",
                    "None of the above"
                }, 0),

                new Question("True or False: Antivirus software should be updated regularly.", null, 0, true),

                new Question("What is a common sign of a phishing email?", new[] {
                    "Proper grammar",
                    "Personal greeting",
                    "Urgent request for login info",
                    "No links at all"
                }, 2),

                new Question("True or False: HTTPS is more secure than HTTP.", null, 0, true),

                new Question("Which is a social engineering technique?", new[] {
                    "Brute-force attack",
                    "Shoulder surfing",
                    "SQL injection",
                    "VPN tunneling"
                }, 1),

                new Question("True or False: It's safe to click pop-ups offering free prizes.", null, 1, true)
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

            bool correct = current.IsCorrect(input.Trim());
            if (correct) _score++;

            _currentQuestionIndex++;

            if (_currentQuestionIndex >= _questions.Count)
            {
                _quizInProgress = false;
                return $"Quiz complete!\nScore: {_score}/{_questions.Count}\nType 'start quiz' to try again.";
            }

            return (correct ? "✅ Correct!\n" : "❌ Incorrect.\n") + GetNextQuestion();
        }

        public string GetNextQuestion()
        {
            if (_currentQuestionIndex >= _questions.Count)
                return "No more questions.";

            return _questions[_currentQuestionIndex].Formatted;
        }
    }
}
