// Name: Luc de Marillac St Julein
// Student Number: ST10382638
// Group: 1

// References: 
//   https://learn.microsoft.com/en-us/dotnet/desktop/wpf/overview/?view=netdesktop-7.0
//   https://learn.microsoft.com/en-us/dotnet/api/system.windows.window?view=windowsdesktop-7.0
//   https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.textblock?view=windowsdesktop-7.0
//   ChatGPT, OpenAI (2025), assisted with async method structure, UI interaction logic, and formatting.

using Cybersecurity_Chatbot_GUI.Logic;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Cybersecurity_Chatbot_GUI.Views
{
    public partial class ChatWindow : Window
    {
        // Creating instance of the chatbot logic
        private readonly WpfChatBot _chatBot;

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Constructor: Initializes the WPF window and chatbot instance
        /// </summary>
        public ChatWindow()
        {
            InitializeComponent();
            _chatBot = new WpfChatBot();
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Runs on window load: plays ASCII animation and enables user input
        /// </summary>
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UserInputTextBox.IsEnabled = false;
            SendButton.IsEnabled = false;

            string art = await _chatBot.PlayStartupAsyncWpf();
            AppendSystemMessage(art);

            await Task.Delay(500);

            AppendMessage("ChatBot", "Type /Help for help");
            AppendMessage("ChatBot", "Hello! What is your name?");

            UserInputTextBox.IsEnabled = true;
            SendButton.IsEnabled = true;
            UserInputTextBox.Focus();
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Handles button click to send user input
        /// </summary>
        private void OnSendClicked(object sender, RoutedEventArgs e)
        {
            HandleUserInput();
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Handles 'Enter' key press inside the input box
        /// </summary>
        private void UserInputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                HandleUserInput();
                e.Handled = true;
            }
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Processes input and response cycle with logic, and handles UI actions
        /// </summary>
        private async void HandleUserInput()
        {
            string input = UserInputTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(input)) return;

            AppendMessage("You", input);

            string botReply = string.IsNullOrWhiteSpace(_chatBot.Username)
                ? _chatBot.SetUsername(input)
                : _chatBot.ProcessInput(input);

            await AppendTypingMessageAsync("ChatBot", botReply);

            if (_chatBot.ShouldLaunchTaskWindow)
            {
                TaskWindow taskWindow = new TaskWindow();
                taskWindow.ShowDialog();
            }

            if (_chatBot.ShouldLaunchLogWindow)
            {
                LogWindow logWindow = new LogWindow();
                logWindow.ShowDialog();
            }

            if (_chatBot.IsExitRequested)
            {
                AppendMessage("ChatBot", "Shutting Down");
                Close();
            }

            UserInputTextBox.Clear();
            UserInputTextBox.Focus();
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Appends a system-style ASCII art or special message to the chat panel
        /// </summary>
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

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Displays a sender-labelled message in the chat window
        /// </summary>
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

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Simulates a typing animation by printing one character at a time
        /// </summary>
        private async Task AppendTypingMessageAsync(string sender, string message, int delay = 20)
        {
            TextBlock thinkingBlock = new TextBlock
            {
                Text = "ChatBot is thinking.",
                FontStyle = FontStyles.Italic,
                FontSize = 13,
                Margin = new Thickness(5),
                Foreground = Brushes.Gray
            };

            ChatStackPanel.Children.Add(thinkingBlock);
            ChatScrollViewer.ScrollToEnd();

            for (int i = 0; i < 3; i++)
            {
                await Task.Delay(350);
                thinkingBlock.Text += ".";
                ChatScrollViewer.ScrollToEnd();
            }

            await Task.Delay(500);
            ChatStackPanel.Children.Remove(thinkingBlock);

            TextBlock typingBlock = new TextBlock
            {
                Text = sender + ": ",
                TextWrapping = TextWrapping.Wrap,
                FontSize = 14,
                Margin = new Thickness(5),
                Foreground = sender == "You" ? Brushes.LightGreen : Brushes.Cyan
            };

            ChatStackPanel.Children.Add(typingBlock);
            ChatScrollViewer.ScrollToEnd();

            foreach (char c in message)
            {
                typingBlock.Text += c;
                await Task.Delay(delay);
                ChatScrollViewer.ScrollToEnd();
            }
        }
    }
}
//------------------------------------------...ooo000 END OF FILE 000ooo...------------------------------------------------------//
