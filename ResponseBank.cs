// Name & Surname: Luc de Marillac St Julein  
// Student Number: ST10382638  
// Group: Cybersecurity Chatbot Group  

// References:  
// https://www.w3schools.com/cs/index.php
// ChatGPT (https://chat.openai.com/)

using System;
using System.Collections.Generic;

namespace Cybersecurity_Chatbot
{
    internal class ResponseBank
    {
        //------------------------------------------------------------------------------------------------------------------------//
        // Declaring global random number generator
        private static readonly Random rand = new Random();

        private static bool previousQuestion = false;

        // Dictionary to store chatbot responses mapped by keyword
        private static readonly Dictionary<string, List<string>> responses = new Dictionary<string, List<string>>
        {
            // Greeting-related keywords
            { "hello", new List<string> {
                "Hello! How can I assist you today?",
                "Hi there! Ready to talk cybersecurity?",
                "Hey! What would you like to learn today?"
            }},
            { "how are you", new List<string> {
                "I'm functioning perfectly and ready to help you stay safe online!",
                "I'm all systems go! Let's protect your digital world."
            }},
            { "what's your purpose", new List<string> {
                "I'm here to help you understand cybersecurity and stay protected online.",
                "My mission is to teach and guide you through digital safety."
            }},
            { "/help", new List<string> {
                "Try asking me about passwords, phishing, malware, VPNs, or other cybersecurity topics.",
                "You can ask about cyber threats, safe browsing tips, or how to secure your devices."
            }},

            // Cybersecurity topics
            // Cybersecurity topics
            { "password", new List<string> {
                "Strong passwords are essential. Use a mix of uppercase and lowercase letters, numbers, and special symbols — and never reuse the same password across multiple sites.",
                "Consider using a reputable password manager to generate and store complex passwords. It reduces the risk of forgetting them or using insecure ones."
            }},
            { "phishing", new List<string> {
                "Phishing attacks disguise themselves as legitimate emails or messages. Be cautious of urgent requests, unfamiliar links, and email addresses that look slightly off.",
                "Always hover over links before clicking and never enter sensitive information unless you’re sure the site is authentic. Use email filters and report suspicious messages."
            }},
            { "malware", new List<string> {
                "Malware includes viruses, ransomware, and spyware. These can infect your system through downloads, email attachments, or malicious websites — leading to stolen data or system damage.",
                "Install antivirus software, keep your system updated, and avoid downloading cracked or unverified software to reduce your exposure to malware threats."
            }},
            { "antivirus", new List<string> {
                "Antivirus software scans your files and activities for known threats. It's your digital immune system, constantly protecting you in real-time.",
                "Ensure your antivirus is always up to date, and schedule regular scans. Combine it with good habits like safe browsing and cautious downloads for maximum protection."
            }},
            { "firewall", new List<string> {
                "A firewall filters traffic between your computer and the internet. It blocks unauthorized access while allowing safe communications.",
                "Firewalls are essential in both personal and business networks. Use both software firewalls (on your device) and hardware firewalls (like routers) for layered security."
            }},
            { "vpn", new List<string> {
                "A VPN (Virtual Private Network) creates a secure tunnel between your device and the internet, encrypting your data and masking your IP address.",
                "Using a VPN is highly recommended when using public Wi-Fi or when you want to prevent websites and ISPs from tracking your online activity."
            }},
            { "two-factor authentication", new List<string> {
                "Two-factor authentication (2FA) adds an extra layer of security to your accounts by requiring a second code — often from your phone — in addition to your password.",
                "Enable 2FA on your email, banking, and social media accounts to significantly reduce the risk of unauthorized access, even if your password is stolen."
            }},
            { "encryption", new List<string> {
                "Encryption scrambles your data so that only someone with the correct decryption key can read it. It's used to secure everything from files to messages.",
                "Use services that offer end-to-end encryption (like Signal or WhatsApp), and encrypt sensitive files on your device or cloud storage for enhanced protection."
            }},
            { "social engineering", new List<string> {
                "Social engineering involves manipulating people into giving up confidential information, often by pretending to be someone trustworthy.",
                "Always verify phone calls, emails, or in-person requests — especially when they ask for login credentials, banking info, or urgent action."
            }},
            { "identity theft", new List<string> {
                "Identity theft occurs when someone steals your personal details and uses them to impersonate you — often to commit fraud or gain access to services.",
                "Shred sensitive documents, monitor your credit reports, and never share personal details like ID numbers or banking info unless absolutely necessary and verified."
            }},
            { "safe browsing", new List<string> {
                "To browse safely, stick to secure websites (https), avoid clicking unknown links, and be cautious of pop-ups or downloads that seem too good to be true.",
                "Keep your browser and plugins updated, use privacy tools like ad blockers, and consider using secure DNS or browser-based antivirus protection."
            }},


            // Exit phrase
            { "goodbye", new List<string> {
                "Goodbye" // Special handling done in ChatBot.cs
            }}
        };

        //------------------------------------------------------------------------------------------------------------------------//
        // Dictionary for emotional/sentiment-aware responses
        private static readonly Dictionary<string, string> sentimentResponses = new Dictionary<string, string>
        {
            { "worried", "It's okay to feel worried. I'm here to help you stay safe online." },
            { "scared", "Cyber threats can seem scary, but knowledge is your best defense!" },
            { "frustrated", "Cybersecurity can be frustrating, but I'm here to make it easier." },
            { "curious", "That's great! Curiosity is the key to learning how to protect yourself online." },
            { "confused", "No worries — ask me anything and I'll explain it as clearly as I can." },
            { "overwhelmed", "Take a breath. I'm here to guide you step-by-step." }
        };

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Matches user input to known keywords or sentiment and returns a response.
        /// </summary>
        /// <param name="input">User's typed input</param>
        /// <returns>A chatbot response string</returns>
        public static string GetResponse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return "Please enter something so I can help you.";
            }

            string cleanedInput = input.Trim().ToLower();

            // Store user interest
            if (cleanedInput.StartsWith("i'm interested in") || cleanedInput.StartsWith("i am interested in"))
            {
                previousQuestion = false;
                string interest = cleanedInput
                    .Replace("i'm interested in", "")
                    .Replace("i am interested in", "")
                    .Trim();

                if (!string.IsNullOrWhiteSpace(interest))
                {
                    Memory.Remember("interest", interest);
                    return $"Great! I’ll remember that you’re interested in {interest}.";
                }
            }

            // Recall remembered topic
            if (cleanedInput.Contains("what do you remember") || cleanedInput.Contains("what did i say"))
            {
                if (Memory.HasMemory("interest"))
                {
                    previousQuestion = true;
                    return $"You told me you’re interested in {Memory.Recall("interest")}. Would you like to know more?";
                }
                else
                    return "You haven't told me what you're interested in yet!";
            }

            if ((cleanedInput.Contains("yes") || cleanedInput.Contains("yeah") || cleanedInput.Contains("sure")) && previousQuestion)
            {
                string topic = Memory.Recall("interest")?.ToLower().Trim();

                foreach (var keyword in responses.Keys)
                {
                    if (topic.Contains(keyword))
                    {
                        previousQuestion = false;
                        List<string> possibleResponses = responses[keyword];
                        return possibleResponses[rand.Next(possibleResponses.Count)];
                    }
                }

                previousQuestion = false;
                return "I'm sorry, I don't understand. Try asking me about something cybersecurity-related.";
            }

            // Handle negative response if previous question was asked
            if ((cleanedInput.Contains("no") || cleanedInput.Contains("nope") || cleanedInput.Contains("nah")) && previousQuestion)
            {
                previousQuestion = false;
                return "Alright! Let me know if you'd like help with anything else.";
            }

            // Check for emotional sentiment
            foreach (var mood in sentimentResponses.Keys)
            {
                if (cleanedInput.Contains(mood))
                {
                    previousQuestion = false;
                    return sentimentResponses[mood];
                }
            }

            // Match topic keywords
            foreach (var keyword in responses.Keys)
            {
                if (cleanedInput.Contains(keyword))
                {
                    previousQuestion = false;
                    List<string> possibleResponses = responses[keyword];
                    return possibleResponses[rand.Next(possibleResponses.Count)];
                }
            }

            // Fallback response
            return "I'm sorry, I don't understand. Try asking me about something cybersecurity-related.";
        }
    }
}
//------------------------------------------...ooo000 END OF FILE 000ooo...------------------------------------------------------//
