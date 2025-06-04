// Name: Luc de Marillac St Julien
// Student Number: ST10382638
// Group: Group 1

// References:
// https://www.w3schools.com/cs/index.php
// ChatGPT (https://chat.openai.com/)

//------------------------------------------------------------------------------------------------------------------------//

using System;
using System.IO;
using System.Media;
using System.Threading;
using System.Threading.Tasks;

namespace Cybersecurity_Chatbot
{
    public class StartUp
    {
        // Path to the greeting audio file
        private static string filePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Greeting Message.wav"));

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Asynchronous startup method that prepares the console, plays a greeting sound, and displays ASCII art.
        /// </summary>
        public static async Task StartupAsync()
        {
            //cmdSetup();

            // Start both visual and audio greetings in parallel
            Task asciiTask = AsciiArtAsync();
            Task voiceTask = PlayVoiceGreetingAsync();

            // Wait for both tasks to finish before showing the start message
            await Task.WhenAll(asciiTask, voiceTask);

            StartMessage();
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Configures the console title and color scheme
        /// </summary>
        private static void cmdSetup()
        {
            Console.Title = "Cybersecurity Chatbot";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Asynchronous display of ASCII art with a typewriter effect
        /// </summary>
        private static async Task AsciiArtAsync()
        {
            string[] asciiArt = {
                "      .-\"      \"-.",
                "     /            \\",
                "    |              |",
                "    |,  .-.  .-.  ,|",
                "    | )(_o/  \\o_)( |",
                "    |/     /\\     \\|",
                "    (_     ^^     _)",
                "     \\__|IIIIII|__/",
                "      | \\IIIIII/ |",
                "      \\          /",
                "       `--------`",
                "      GHOST SHELL"
            };

            foreach (string line in asciiArt)
            {
                Console.WriteLine(line);
                await Task.Delay(200); // Delay for dramatic effect
            }

            PrintLine();  // Visual separator after art
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Prints a decorative dashed line with delay
        /// </summary>
        private static void PrintLine()
        {
            string dashedLine = "----------------------------";

            foreach (char c in dashedLine)
            {
                Console.Write(c);
                Thread.Sleep(100);
            }

            Console.WriteLine();
            Console.WriteLine();
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Plays the voice greeting from a .wav file asynchronously
        /// </summary>
        private static async Task PlayVoiceGreetingAsync()
        {
            if (File.Exists(filePath))
            {
                SoundPlayer player = new SoundPlayer(filePath);
                await Task.Run(() => player.PlaySync());
            }
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Displays the chatbot's first prompt after startup
        /// </summary>
        private static void StartMessage()
        {
            Console.WriteLine("ChatBot: Type /Help for help");
            Console.WriteLine();
        }
    }
}

//--------------------------------------------------...ooo000 END OF FILE 000ooo...------------------------------------------------//
