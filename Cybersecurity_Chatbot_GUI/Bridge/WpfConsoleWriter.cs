using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersecurity_Chatbot_GUI.Bridge
{
    internal class WpfConsoleWriter: System.IO.TextWriter
    {
        private readonly Action<string> _writeCallback;

        public WpfConsoleWriter(Action<string> writeCallback)
        {
            _writeCallback = writeCallback;
        }

        public override void WriteLine(string value)
        {
            _writeCallback(value + "\n");
        }

        public override void Write(char value)
        {
            _writeCallback(value.ToString());
        }

        public override Encoding Encoding => Encoding.UTF8;
    }
}
