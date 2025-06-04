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
    public class Memory
    {
        //------------------------------------------------------------------------------------------------------------------------//
        // Internal storage for chatbot memory
        private static Dictionary<string, string> memoryStore = new Dictionary<string, string>();

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Stores a user detail or interest with a specified label.
        /// If the key exists, it will be overwritten.
        /// </summary>
        /// <param name="key">Label to identify the memory</param>
        /// <param name="value">Content to store</param>
        public static void Remember(string key, string value)
        {
            memoryStore[key] = value;
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Checks whether a memory has been saved for the given key.
        /// </summary>
        /// <param name="key">Label to check</param>
        /// <returns>True if memory exists; otherwise, false</returns>
        public static bool HasMemory(string key)
        {
            return memoryStore.ContainsKey(key);
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Retrieves a stored value based on the provided key.
        /// </summary>
        /// <param name="key">Label used when storing the memory</param>
        /// <returns>Stored string value or null if not found</returns>
        public static string Recall(string key)
        {
            return memoryStore.ContainsKey(key) ? memoryStore[key] : null;
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Clears all stored chatbot memory.
        /// </summary>
        public static void Clear()
        {
            memoryStore.Clear();
        }
    }
}
//------------------------------------------...ooo000 END OF FILE 000ooo...------------------------------------------------------//
