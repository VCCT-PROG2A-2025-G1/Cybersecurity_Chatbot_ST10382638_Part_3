// Name: Luc de Marillac St Julein
// Student Number: ST10382638
// Group: 1

// References:
//   https://www.w3schools.com/cs/index.php#gsc.tab=0
//   https://stackoverflow.com/questions
//   ChatGPT, OpenAI (2025), assisted with JSON serialization, WPF event handling, and task reminder logic.

using Cybersecurity_Chatbot_GUI.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Newtonsoft.Json;

namespace Cybersecurity_Chatbot_GUI.Views
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        private List<(string Title, string Description, DateTime Due)> tasks = new List<(string, string, DateTime)>();

        private readonly string dataDir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "CybersecurityChatbot");

        private readonly string dataPath;

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Constructor: Initializes the window and loads tasks from disk
        /// </summary>
        public TaskWindow()
        {
            InitializeComponent();
            dataPath = Path.Combine(dataDir, "tasks.json");
            LoadTasks();
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Pre-fills date and time inputs on window load
        /// </summary>
        private void TaskWindow_Loaded(object sender, RoutedEventArgs e)
        {
            TaskDueDatePicker.SelectedDate = DateTime.Now.Date;
            TaskDueTimeBox.Text = DateTime.Now.ToString("HH:mm");
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Adds a new task to the list and schedules a reminder
        /// </summary>
        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            var title = TaskTitleBox.Text?.Trim();
            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Please enter a task title.");
                return;
            }

            var desc = TaskDescBox.Text?.Trim() ?? "";

            if (TaskDueDatePicker.SelectedDate == null ||
                !TimeSpan.TryParse(TaskDueTimeBox.Text, out var time))
            {
                MessageBox.Show("Please select a valid due date and time.");
                return;
            }

            var due = TaskDueDatePicker.SelectedDate.Value.Date + time;
            tasks.Add((title, desc, due));
            TaskList.Items.Add($"{title}  (Due: {due:g})");

            ScheduleReminder((title, desc, due));

            ActivityLog.Log($"Task added: \"{title}\" due {due:g}");
            SaveTasks();

            TaskTitleBox.Clear();
            TaskDescBox.Clear();
            TaskDueDatePicker.SelectedDate = null;
            TaskDueTimeBox.Clear();
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Marks a selected task as completed and removes it
        /// </summary>
        private void CompleteTask_Click(object sender, RoutedEventArgs e)
        {
            var idx = TaskList.SelectedIndex;
            if (idx < 0) return;

            var title = tasks[idx].Title;
            tasks.RemoveAt(idx);
            TaskList.Items.RemoveAt(idx);

            ActivityLog.Log($"Task completed: \"{title}\"");
            SaveTasks();
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Deletes a selected task from the list
        /// </summary>
        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            var idx = TaskList.SelectedIndex;
            if (idx < 0) return;

            var title = tasks[idx].Title;
            tasks.RemoveAt(idx);
            TaskList.Items.RemoveAt(idx);

            ActivityLog.Log($"Task deleted: \"{title}\"");
            SaveTasks();
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Restricts ReminderDaysBox to accept numeric input only
        /// </summary>
        private void ReminderDaysBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Closes the Task window
        /// </summary>
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Loads tasks from JSON file and restores reminders
        /// </summary>
        private void LoadTasks()
        {
            if (!Directory.Exists(dataDir))
                Directory.CreateDirectory(dataDir);

            if (!File.Exists(dataPath))
                return;

            try
            {
                var json = File.ReadAllText(dataPath);
                tasks = JsonConvert
                    .DeserializeObject<List<(string, string, DateTime)>>(json)
                    ?? new List<(string, string, DateTime)>();

                foreach (var t in tasks)
                {
                    TaskList.Items.Add($"{t.Title}  (Due: {t.Due:g})");
                    ScheduleReminder(t);
                }
            }
            catch
            {
                tasks = new List<(string, string, DateTime)>();
            }
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Saves the current task list to disk as JSON
        /// </summary>
        private void SaveTasks()
        {
            var json = JsonConvert.SerializeObject(tasks);
            File.WriteAllText(dataPath, json);
        }

        //------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Sets up a one-time WPF timer reminder for a task
        /// </summary>
        private void ScheduleReminder((string Title, string Description, DateTime Due) t)
        {
            var interval = t.Due - DateTime.Now;
            if (interval <= TimeSpan.Zero)
                return;

            var timer = new DispatcherTimer { Interval = interval };
            timer.Tick += (s, args) =>
            {
                MessageBox.Show(
                    $"Reminder: {t.Title}\n\n{t.Description}",
                    "Task Reminder",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                ActivityLog.Log($"Reminder fired for \"{t.Title}\"");
                timer.Stop();
            };
            timer.Start();
        }
    }
}
//------------------------------------------...ooo000 END OF FILE 000ooo...------------------------------------------------------//