using Cybersecurity_Chatbot_GUI.Bridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Cybersecurity_Chatbot;


namespace Cybersecurity_Chatbot_GUI.Views
{
    /// <summary>
    /// Interaction logic for ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        private readonly WpfConsoleReader _reader;
        private readonly WpfConsoleWriter _writer;

        public ChatWindow()
        {
            InitializeComponent();
            _reader = new WpfConsoleReader();
            _writer = new WpfConsoleWriter(AppendToChat);

            Console.SetOut(_writer);
            Console.SetIn(_reader);

            Loaded += ChatWindow_Loaded;
        }

        private async void ChatWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await StartUp.StartupAsync();   // Plays ASCII and voice
            ChatBot.run();                  // Calls GetUsername(), stores it, runs chat
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            string input = UserInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(input)) return;

            AppendToChat("You: " + input);
            _reader.FeedInput(input); // Pass input to Console.ReadLine()
            UserInput.Clear();
        }

        private void AppendToChat(string message)
        {
            Dispatcher.Invoke(() =>
            {
                ChatStack.Children.Add(new TextBlock
                {
                    Text = message,
                    Foreground = Brushes.LimeGreen,
                    FontFamily = new FontFamily("Consolas"),
                    FontSize = 14,
                    TextWrapping = TextWrapping.Wrap
                });
            });
        }
    }
}
