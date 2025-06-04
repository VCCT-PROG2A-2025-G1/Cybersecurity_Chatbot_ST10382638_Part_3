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

        public static void Log(string message)
        {
            _entries.Add($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
        }

        public static List<string> GetEntries()
        {
            return new List<string>(_entries);
        }

        public static string GetFormattedLog()
        {
            return _entries.Count == 0 ? "No activity yet." : string.Join("\n", _entries);
        }

        public static void Clear()
        {
            _entries.Clear();
        }
    }
}
