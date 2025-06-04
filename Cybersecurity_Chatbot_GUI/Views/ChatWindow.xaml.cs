using Cybersecurity_Chatbot_GUI.Logic;
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

namespace Cybersecurity_Chatbot_GUI.Views
{
    /// <summary>
    /// Interaction logic for ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        private readonly WpfChatBot _chatBot;
        public ChatWindow()
        {
            InitializeComponent();
            _chatBot = new WpfChatBot();
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UserInputTextBox.IsEnabled = false;
            SendButton.IsEnabled = false;

            // 2. Play ASCII and voice
            string art = await _chatBot.PlayStartupAsyncWpf();
            AppendSystemMessage(art);

            await Task.Delay(500);

            // 3. Show startup messages
            AppendMessage("ChatBot", "Type /Help for help");
            AppendMessage("ChatBot", "Hello! What is your name?");

            // 4. Re-enable input after greeting
            UserInputTextBox.IsEnabled = true;
            SendButton.IsEnabled = true;

            // 5. Focus the input box
            UserInputTextBox.Focus();
        }


        private void OnSendClicked(object sender, RoutedEventArgs e)
        {
            HandleUserInput();
        }

        private void UserInputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                HandleUserInput();
                e.Handled = true; // Prevent ding sound
            }
        }

        private void HandleUserInput()
        {
            string input = UserInputTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(input))
                return;

            AppendMessage("You", input);

            string botReply;

            if (string.IsNullOrWhiteSpace(_chatBot.Username))
                botReply = _chatBot.SetUsername(input);
            else
                botReply = _chatBot.ProcessInput(input);

            AppendMessage("ChatBot", botReply);

            // ✅ ADD THIS BLOCK TO HANDLE TASK WINDOW
            if (_chatBot.ShouldLaunchTaskWindow)
            {
                TaskWindow taskWindow = new TaskWindow();
                taskWindow.ShowDialog();
            }

            if (_chatBot.IsExitRequested)
            {
                AppendMessage("ChatBot", "Shutting Down");
                Close();
            }

            UserInputTextBox.Clear();
            UserInputTextBox.Focus();
        }

        private void AppendSystemMessage(string message)
        {
            TextBlock systemBlock = new TextBlock
            {
                Text = message,
                FontFamily = new FontFamily("Consolas"),
                TextWrapping = TextWrapping.Wrap,
                FontSize = 14,
                Margin = new Thickness(5),
                Foreground = Brushes.Cyan
            };

            ChatStackPanel.Children.Add(systemBlock);
            ChatScrollViewer.ScrollToEnd();
        }

        private void AppendMessage(string sender, string message)
        {
            TextBlock messageBlock = new TextBlock
            {
                Text = sender + ": " + message,
                TextWrapping = TextWrapping.Wrap,
                FontSize = 14,
                Margin = new Thickness(5),
                Foreground = sender == "You" ? Brushes.LightGreen : Brushes.Cyan
            };

            ChatStackPanel.Children.Add(messageBlock);
            ChatScrollViewer.ScrollToEnd();
        }
    }
}
