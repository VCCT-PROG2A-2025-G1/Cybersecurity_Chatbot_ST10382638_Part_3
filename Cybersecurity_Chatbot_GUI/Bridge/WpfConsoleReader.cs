using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersecurity_Chatbot_GUI.Bridge
{
    internal class WpfConsoleReader : System.IO.TextReader
    {
        private TaskCompletionSource<string> _inputSource = new TaskCompletionSource<string>();

        public override string ReadLine()
        {
            return _inputSource.Task.Result;
        }

        public void FeedInput(string input)
        {
            _inputSource.SetResult(input);
            _inputSource = new TaskCompletionSource<string>();
        }
    }
}
