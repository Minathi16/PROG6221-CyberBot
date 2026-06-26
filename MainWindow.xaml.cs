using System;
using System.Collections.Generic;
using System.Windows;

namespace PROG6221
{
    public partial class MainWindow : Window
    {
        private TaskStorageHelper _storage = new TaskStorageHelper();
        public MainWindow()
        {
            InitializeComponent();
            dgTasks.ItemsSource = _storage.GetTasks();
            txtChat.AppendText("System Ready. Ask me about cybersecurity!\n");
        }
        private void btnSEND_Click(object sender, RoutedEventArgs e)
        {
            string input = txtInput.Text.Trim();
            if (input.ToLower().StartsWith("add task"))
            {
                _storage.AddTask(input.Replace("add task", "").Trim());
                dgTasks.ItemsSource = _storage.GetTasks();
                txtChat.AppendText("Bot: Task added.\n");
            }
            txtInput.Clear();
        }
        private void btnQuiz_Click(object sender, RoutedEventArgs e) => txtChat.AppendText("Bot: Quiz feature active.\n");
        private void btnActivityLog_Click(object sender, RoutedEventArgs e) => txtChat.AppendText("Bot: Log displayed.\n");
        private void txtChat_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) { }
        private void txtInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) { }
    }
}