using Cybersecurity_Chatbot_GUI.Logic;
using System;
using System.Collections.Generic;
using System.IO;
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

        private readonly string dataDir =
            System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                         "CybersecurityChatbot");

        private readonly string dataPath;
        public TaskWindow()
        {
            InitializeComponent();
            dataPath = System.IO.Path.Combine(dataDir, "tasks.json");
            LoadTasks();
        }

        private void TaskWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // pre-fill with today’s date
            TaskDueDatePicker.SelectedDate = DateTime.Now.Date;

            // pre-fill with current time in HH:mm format
            TaskDueTimeBox.Text = DateTime.Now.ToString("HH:mm");
        }

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

            // schedule reminder
            ScheduleReminder((title, desc, due));

            // log and persist
            ActivityLog.Log($"Task added: \"{title}\" due {due:g}");
            SaveTasks();

            // clear inputs
            TaskTitleBox.Clear();
            TaskDescBox.Clear();
            TaskDueDatePicker.SelectedDate = null;
            TaskDueTimeBox.Clear();
        }

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

        private void ReminderDaysBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _); 
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LoadTasks()
        {
            // ensure folder
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

                // repopulate UI & re-schedule reminders
                foreach (var t in tasks)
                {
                    TaskList.Items.Add($"{t.Title}  (Due: {t.Due:g})");
                    ScheduleReminder(t);
                }
            }
            catch
            {
                // if corruption, start fresh
                tasks = new List<(string, string, DateTime)>();
            }
        }

        private void SaveTasks()
        {
            var json = JsonConvert.SerializeObject(tasks);
            File.WriteAllText(dataPath, json);
        }

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
