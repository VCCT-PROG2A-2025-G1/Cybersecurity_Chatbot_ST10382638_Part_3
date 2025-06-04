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
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        private List<string> taskLog = new List<string>();
        public TaskWindow()
        {
            InitializeComponent();

            DataObject.AddPastingHandler(ReminderDaysBox, OnReminderBoxPaste);
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string title = TaskTitleBox.Text.Trim();
            string reminder = ReminderDaysBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Please enter a task title.");
                return;
            }

            string taskEntry = title;
            if (!string.IsNullOrWhiteSpace(reminder))
            {
                taskEntry += $" (Remind in {reminder} days)";
            }

            TaskList.Items.Add(taskEntry);
            taskLog.Add($"Task Added: {taskEntry}");

            TaskTitleBox.Clear();
            ReminderDaysBox.Clear();
        }

        private void CompleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskList.SelectedItem != null)
            {
                string completed = $"✅ {TaskList.SelectedItem}";
                TaskList.Items[TaskList.SelectedIndex] = completed;
                taskLog.Add($"Task Completed: {completed}");
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskList.SelectedItem != null)
            {
                taskLog.Add($"Task Deleted: {TaskList.SelectedItem}");
                TaskList.Items.Remove(TaskList.SelectedItem);
            }
        }

        private void ReminderDaysBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _); // Only allow digits
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Simply closes the window and returns to chat
        }
        private void OnReminderBoxPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string pastedText = (string)e.DataObject.GetData(typeof(string));
                if (!int.TryParse(pastedText, out _))
                {
                    e.CancelCommand(); // Block paste if it's not a number
                }
            }
            else
            {
                e.CancelCommand(); // Block paste if no text present
            }
        }

        public List<string> GetTaskLog()
        {
            return taskLog;
        }
    }
}
