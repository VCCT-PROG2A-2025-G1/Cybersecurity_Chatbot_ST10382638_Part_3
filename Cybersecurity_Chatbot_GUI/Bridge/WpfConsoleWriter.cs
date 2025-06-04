using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersecurity_Chatbot_GUI
{
    internal class WpfConsoleWriter : System.IO.TextWriter
    {
        private readonly Action<string> _writeAction;

        public WpfConsoleWriter(Action<string> writeAction)
        {
            _writeAction = writeAction;
        }

        public override void Write(char value)
        {
            _writeAction?.Invoke(value.ToString());
        }

        public override void WriteLine(string value)
        {
            _writeAction?.Invoke(value);
        }

        public override Encoding Encoding => Encoding.UTF8;
    }
}
