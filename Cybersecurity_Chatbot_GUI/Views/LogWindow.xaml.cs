// Name: Luc de Marillac St Julein
// Student Number: ST10382638
// Group: 1

// References:
//   https://www.w3schools.com/cs/index.php#gsc.tab=0
//   https://stackoverflow.com/questions
//   ChatGPT, OpenAI (2025), assisted with paging logic, list binding, and UI event handling.

using Cybersecurity_Chatbot_GUI.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Cybersecurity_Chatbot_GUI.Views
{
    /// <summary>
    /// Interaction logic for LogWindow.xaml
    /// </summary>
    public partial class LogWindow : Window
    {
        private readonly List<string> _allEntries;
        private int _offset;
        private const int PageSize = 5;

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Constructor: Initializes the window and loads the log entries
        /// </summary>
        public LogWindow()
        {
            InitializeComponent();

            _allEntries = ActivityLog.GetEntries();
            _offset = 0;
            UpdateList();
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Refreshes the log list with a new page of entries
        /// </summary>
        private void UpdateList()
        {
            var paged = _allEntries
                .Skip(Math.Max(0, _allEntries.Count - (PageSize + _offset)))
                .Take(PageSize)
                .ToList();

            LogList.ItemsSource = paged;
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Increases offset and displays more entries when "Show More" is clicked
        /// </summary>
        private void ShowMore_Click(object sender, RoutedEventArgs e)
        {
            _offset += PageSize;
            UpdateList();
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Closes the log window
        /// </summary>
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
//------------------------------------------...ooo000 END OF FILE 000ooo...------------------------------------------------------//