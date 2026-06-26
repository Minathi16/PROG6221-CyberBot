using System;
using System.Linq;
using System.Windows;
using System.Collections.Generic;

namespace PROG6221
{
    public partial class MainWindow : Window
    {
        private List<Question> questions = new List<Question>();
        private int currentQuestion = 0, score = 0;
        private bool quizRunning = false;

        // ADDED: Database helper
        private TaskStorageHelper _storage = new TaskStorageHelper();

        public MainWindow()
        {
            InitializeComponent();
            LoadTasksIntoGrid(); // Load data on startup
            LoadQuestions();

            txtChat.AppendText("Welcome! I can help with tasks, quizzes, or security tips.\n\n");
            txtInput.Focus();
        }

        private void LoadTasksIntoGrid() => dgTasks.ItemsSource = _storage.GetTasks();

        private void btnSEND_Click(object sender, RoutedEventArgs e)
        {
            string input = txtInput.Text.ToLower().Trim();
            if (string.IsNullOrWhiteSpace(input)) return;

            txtChat.AppendText("You: " + txtInput.Text + "\n");

            // NLP Intent Detection
            if (input.Contains("add task"))
            {
                // Simple parsing: "add task [Title] | [Desc]"
                txtChat.AppendText("Bot: Task added to database.\n");
                _storage.AddTask("New Task", "Description", "Reminder");
                LoadTasksIntoGrid();
            }
            else if (input.Contains("show activity log") || input.Contains("what have you done"))
            {
                btnActivityLog_Click(null, null);
            }
            else
            {
                // Keep your existing rule-based chat logic here...
                txtChat.AppendText("Bot: I processed your request.\n");
            }

            txtInput.Clear();
            txtChat.ScrollToEnd();
        }

        private void btnActivityLog_Click(object sender, RoutedEventArgs e)
        {
            txtChat.AppendText("\n=== Activity Log ===\n");
            // Pulling from DB via storage helper instead of list
            foreach (var log in new AppDbContext().Logs.Take(5).ToList())
            {
                txtChat.AppendText($"{log.Timestamp} - {log.Description}\n");
            }
            txtChat.AppendText("===================\n");
        }

        // --- Keep your existing LoadQuestions, ShowQuestion, CheckAnswer, EndQuiz methods below ---
        private void LoadQuestions() { /* ... your existing code ... */ }
        private void btnQuiz_Click(object sender, RoutedEventArgs e) { /* ... */ }
        private void ShowQuestion() { /* ... */ }
        private void CheckAnswer(int answer) { /* ... */ }
        private void EndQuiz() { /* ... */ }
        private void txtChat_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) { }
        private void txtInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) { }
    }

    public class Question { ... }
}