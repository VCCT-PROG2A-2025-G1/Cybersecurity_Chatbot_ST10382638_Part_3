// Name: Luc de Marillac St Julien
// Student Number: ST10382638
// Group: Group 1

// References:
// https://www.w3schools.com/cs/index.php
// ChatGPT (https://chat.openai.com/)

//------------------------------------------------------------------------------------------------------------------------//

using System;
using System.Collections.Generic;

namespace Cybersecurity_Chatbot
{
    public class ChatBot
    {
        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Main chatbot loop
        /// </summary>
        public static void run()
        {
            string response = "";
            string username;
            string confirm;
            bool exit = false;

            StartUp.StartupAsync().Wait(); // Wait for the startup tasks to complete
            username = GetUsername();

            while (!exit)
            {
                Console.Write($"{username}: ");
                string input = Console.ReadLine();
                Console.WriteLine();

                if(string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("ChatBot: Please enter something so I can help you.\n");
                    continue;
                }

                response = ResponseBank.GetResponse(input);
                
                if(response == "Goodbye")
                {
                    Console.WriteLine("ChatBot: Before you go, would you like to ask anything else? (yes/no)\n");
                    Console.Write(username + ": ");
                    confirm = Console.ReadLine()?.Trim().ToLower();
                    Console.WriteLine();

                    if(confirm == "no" || confirm == "n")
                    {
                        response = $"Goodbye {username}, stay cyber safe!";
                        exit = true;
                        continue;
                    }
                    else
                    {
                        response = "Great! What else would you like to know?";
                    }
                }

                Console.WriteLine($"ChatBot: {response}\n");
            }
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Ask the user for their name
        /// </summary>
        private static string GetUsername()
        {
            string username;
            
            Console.WriteLine("ChatBot: Hello! What is your name?\n");

            do
            {
                Console.Write("You: ");
                username = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(username))
                {
                    Console.WriteLine("\nChatBot: Please enter a valid name.\n");
                }
            } while (string.IsNullOrWhiteSpace(username));

            Console.WriteLine($"\nChatBot: Hello {username}, nice to meet you!\n");
            return username;
        }
    }
}

//--------------------------------------------------...ooo000 END OF FILE 000ooo...------------------------------------------------//
