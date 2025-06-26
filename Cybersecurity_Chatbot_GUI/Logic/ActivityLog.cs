// Name: Luc de Marillac St Julein  
// Student Number: ST10382638  
// Group: 1

// References:  
//   https://www.w3schools.com/cs/index.php#gsc.tab=0  
//   https://stackoverflow.com/questions  
//   ChatGPT, OpenAI (2025), assisted with formatting, list handling, and date-time stamps.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersecurity_Chatbot_GUI.Logic
{
    internal class ActivityLog
    {
        private static List<string> _entries = new List<string>();

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Adds a timestamped message to the activity log.
        /// </summary>
        public static void Log(string message)
        {
            _entries.Add($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Returns a copy of the activity log entries as a list.
        /// </summary>
        public static List<string> GetEntries()
        {
            return new List<string>(_entries);
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Returns all log entries as a single formatted string.
        /// </summary>
        public static string GetFormattedLog()
        {
            return _entries.Count == 0 ? "No activity yet." : string.Join("\n", _entries);
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Clears all entries from the activity log.
        /// </summary>
        public static void Clear()
        {
            _entries.Clear();
        }
    }
}
//------------------------------------------...ooo000 END OF FILE 000ooo...------------------------------------------------------//