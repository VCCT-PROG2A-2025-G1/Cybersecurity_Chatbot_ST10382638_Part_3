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
    /// Interaction logic for LogWindow.xaml
    /// </summary>
    public partial class LogWindow : Window
    {
        private readonly List<string> _allEntries;
        private int _offset;
        private const int PageSize = 5;
        public LogWindow()
        {
            InitializeComponent();

            // grab the entire log once
            _allEntries = ActivityLog.GetEntries();
            _offset = 0;
            UpdateList();
        }

        private void UpdateList()
        {
            // take the last (PageSize + offset) entries, then skip offset, to page backwards
            var paged = _allEntries
                .Skip(Math.Max(0, _allEntries.Count - (PageSize + _offset)))
                .Take(PageSize)
                .ToList();

            LogList.ItemsSource = paged;
        }

        private void ShowMore_Click(object sender, RoutedEventArgs e)
        {
            _offset += PageSize;
            UpdateList();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
