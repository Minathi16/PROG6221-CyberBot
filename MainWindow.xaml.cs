using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace PROG6221
{
    public partial class MainWindow : Window
    {
        private TaskStorageHelper _storage = new TaskStorageHelper();
        private bool quizRunning = false;
        private int currentQuestion = 0;
        private int score = 0;

        // Expanded 11-Question Cybersecurity Quiz
        private List<string> quizQuestions = new List<string>
        {
            "1. What is the most secure type of password? \nA) 'password123' \nB) A long passphrase (4+ random words) with mixed characters",
            "2. What is 'Phishing'? \nA) A scam to steal credentials via fake emails or messages \nB) A type of firewall",
            "3. What does MFA stand for? \nA) Multi-Factor Authentication \nB) Multiple File Access",
            "4. Is it safe to do online banking on public Wi-Fi without a VPN? \nA) Yes, it's completely fine \nB) No, your traffic can be easily intercepted",
            "5. What does Ransomware do? \nA) Speeds up your PC's processing power \nB) Encrypts your files and demands payment to unlock them",
            "6. Why is it important to regularly update your software and OS? \nA) To patch known security vulnerabilities \nB) To change the background color",
            "7. You receive an unexpected email with a .exe attachment. What do you do? \nA) Open it immediately to see what it is \nB) Do not open it, delete it, and report it",
            "8. What should you do when stepping away from your work computer? \nA) Lock the screen (e.g., Win + L) \nB) Leave it unlocked so you don't have to type your password later",
            "9. What is the best practice for backing up important data? \nA) Regularly back up data offline or to a secure cloud \nB) Never back it up; modern hard drives don't fail",
            "10. Someone calls claiming to be from IT and asks for your password to 'fix an issue'. Do you give it? \nA) Yes, IT needs my password to help me \nB) No, real IT support will never ask for your password",
            "11. What is the main purpose of a Virtual Private Network (VPN)? \nA) To securely encrypt your internet connection \nB) To increase your internet speed"
        };

        // Corresponding answers (make sure they are lowercase for the check)
        private List<string> quizAnswers = new List<string>
        {
            "b", "a", "a", "b", "b", "a", "b", "a", "a", "b", "a"
        };

        public MainWindow()
        {
            InitializeComponent();
            RefreshDataGrid();
            AppendBotMessage("System Online. Database connected.");
            AppendBotMessage("Type 'add task [task name]' to create a task, or click 'Start Quiz' to test your knowledge.");
        }

        private void RefreshDataGrid()
        {
            try
            {
                dgTasks.ItemsSource = _storage.GetTasks(); // Loads from SQLite
            }
            catch (Exception ex)
            {
                AppendBotMessage("Database Error: " + ex.Message);
            }
        }

        private void btnSEND_Click(object sender, RoutedEventArgs e) => ProcessInput();

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) ProcessInput();
        }

        private void ProcessInput()
        {
            string input = txtInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(input)) return;

            txtChat.AppendText($"\nYou: {input}\n");
            txtInput.Clear();

            // If the quiz is active, route input to the quiz checker
            if (quizRunning)
            {
                CheckQuizAnswer(input.ToLower());
                return;
            }

            string lowerInput = input.ToLower();

            // NLP / Intent Detection Logic
            if (lowerInput.StartsWith("add task "))
            {
                string taskTitle = input.Substring(9).Trim();
                _storage.AddTask(taskTitle);
                RefreshDataGrid();
                AppendBotMessage($"Task '{taskTitle}' added to the secure database.");
            }
            else if (lowerInput.Contains("hello") || lowerInput.Contains("hi"))
            {
                AppendBotMessage("Hello! I am your Cybersecurity Bot. How can I help you today?");
            }
            else
            {
                AppendBotMessage("Command not recognized. Type 'add task [name]' to save a task, or start a quiz.");
            }
        }

        private void btnQuiz_Click(object sender, RoutedEventArgs e)
        {
            if (quizRunning) return; // Prevent restarting if already running

            quizRunning = true;
            currentQuestion = 0;
            score = 0;
            AppendBotMessage("\n=== Starting Cybersecurity Quiz ===");
            AppendBotMessage("Reply with 'A' or 'B'.");
            AskQuestion();
        }

        private void AskQuestion()
        {
            if (currentQuestion < quizQuestions.Count)
            {
                AppendBotMessage("\n" + quizQuestions[currentQuestion]);
            }
            else
            {
                AppendBotMessage($"\nQuiz Complete! Your secure score: {score}/{quizQuestions.Count}");
                if (score >= 9) AppendBotMessage("Excellent work! You are highly secure.");
                else if (score >= 6) AppendBotMessage("Good job, but there is still room to improve your security habits.");
                else AppendBotMessage("You should review basic cybersecurity practices to stay safe online.");

                quizRunning = false;
            }
        }

        private void CheckQuizAnswer(string answer)
        {
            if (answer != "a" && answer != "b")
            {
                AppendBotMessage("Invalid input. Please type 'A' or 'B'.");
                return;
            }

            if (answer == quizAnswers[currentQuestion])
            {
                AppendBotMessage("Correct!");
                score++;
            }
            else
            {
                AppendBotMessage($"Incorrect. The correct answer was {quizAnswers[currentQuestion].ToUpper()}.");
            }

            currentQuestion++;
            AskQuestion();
        }

        private void btnActivityLog_Click(object sender, RoutedEventArgs e)
        {
            AppendBotMessage("\n=== Activity Log ===");
            AppendBotMessage("System booted securely.");
            AppendBotMessage($"Total tasks stored in database: {_storage.GetTasks().Count}");
            AppendBotMessage("====================");
        }

        private void AppendBotMessage(string message)
        {
            txtChat.AppendText($"Bot: {message}\n");
            txtChat.ScrollToEnd(); // Auto-scrolls so the user always sees the newest message
        }
    }
}