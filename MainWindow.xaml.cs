using System.Linq;
using System.Windows;
using System.Collections.Generic;

namespace PROG6221
{
    public partial class MainWindow : Window
    {
        private List<string> activityLog = new List<string>();

        private List<Question> questions = new List<Question>();

        private int currentQuestion = 0;

        private int score = 0;

        private bool quizRunning = false;

        public MainWindow()
        {
            InitializeComponent();

            string welcomeSentence =
                "Welcome to the CyberSecurity Awareness Bot! You can ask about two-factor/MFA/2FA, password strength and password managers, " +
                "passphrases, encryption and secure storage, VPNs and public Wi-Fi safety, backups, reporting phishing and suspicious attachments, " +
                "smishing/vishing/social engineering, SIM swap protection, ransomware response, software updates/patches, least privilege/admin rights, " +
                "router and IoT security, secure file sharing, data breaches/account recovery, privacy settings/cookies, antivirus and malware threats, " +
                "safe browsing, phishing and scam prevention, and general help or greetings.";

            txtChat.AppendText(welcomeSentence + "\n\n");
            txtChat.ScrollToEnd();

            txtInput.Focus();

            LoadQuestions();
        }

        private void LoadQuestions()
        {
            questions.Add(new Question
            {
                QuestionText = "What is phishing?",
                Options = new string[]
                {
                    "A scam email",
                    "Antivirus",
                    "Firewall",
                    "Password"
                },
                CorrectAnswer = 0,
                Explanation = "Phishing is a scam designed to steal information."
            });

            questions.Add(new Question
            {
                QuestionText = "Should you share your password?",
                Options = new string[]
                {
                    "Yes",
                    "No"
                },
                CorrectAnswer = 1,
                Explanation = "Passwords should never be shared."
            });

            questions.Add(new Question
            {
                QuestionText = "What does MFA stand for?",
                Options = new string[]
                {
                    "Multi-Factor Authentication",
                    "My First Account",
                    "Managed File Access",
                    "Multiple File Accounts"
                },
                CorrectAnswer = 0,
                Explanation = "MFA means Multi-Factor Authentication."
            });
        }

        private void btnSEND_Click(object sender, RoutedEventArgs e)
        {
            string rawInput = txtInput.Text;

            if (quizRunning)
            {
                int answer;

                if (int.TryParse(rawInput, out answer))
                {
                    CheckAnswer(answer);

                    txtInput.Clear();

                    return;
                }
            }

            string userInput = rawInput.ToLower().Trim();

            activityLog.Add(
                DateTime.Now.ToString("HH:mm:ss")
                + " - User entered: "
                + rawInput);

            if (string.IsNullOrWhiteSpace(userInput))
            {
                txtInput.Clear();
                txtInput.Focus();
                return;
            }

            txtChat.AppendText("You: " + rawInput + "\n");

            var rules = new List<(string[] keys, string[] replies)>
            {
                (new[] { "tell me more about phishing", "tell me more about phishing please", "i am curious about phishing", "i'm curious about phishing" },
                 new[] {
                     "Bot: Phishing is a social-engineering attack that tries to trick you into sharing credentials or downloading malware.",
                     "Bot: Common signs: unexpected senders, urgent/time-sensitive language, suspicious links, and mismatched URLs. Verify via another channel."
                 }),

                (new[] { "i am worried about phishing", "i'm worried about phishing", "worried about phishing" },
                 new[] {
                     "Bot: If you suspect phishing, do not click links or open attachments.",
                     "Bot: Steps: change affected passwords, enable MFA, report the message to the provider/IT, and run a security scan."
                 }),

                (new[] { "give me another tip", "another tip", "more tips", "give me a tip" },
                 new[] {
                     "Bot: Tip — Use a password manager to generate and store unique passwords.",
                     "Bot: Tip — Enable automatic updates so security patches are applied promptly."
                 }),

                (new[] { "two-factor", "two factor", "2fa", "mfa", "multi-factor" },
                 new[] {
                     "Bot: Use two-factor or multi-factor authentication whenever possible.",
                     "Bot: Authenticator apps or hardware tokens are more secure than SMS 2FA."
                 }),

                (new[] { "password manager", "password managers", "passphrase", "pass phrases", "pass phrase" },
                 new[] {
                     "Bot: Use a reputable password manager to store unique passwords for each account.",
                     "Bot: Long passphrases (4+ random words) are easier to remember and secure."
                 }),

                (new[] { "encryption", "encrypted", "encrypt" },
                 new[] {
                     "Bot: Encrypt sensitive files and use full-disk encryption on laptops and mobile devices."
                 }),

                (new[] { "vpn", "virtual private network", "public wifi", "public wifi safety", "wifi safety", "secure wi-fi" },
                 new[] {
                     "Bot: Use a trusted VPN on untrusted networks and avoid sending sensitive data on public Wi-Fi."
                 }),

                (new[] { "backup", "back up", "backups", "backup files" },
                 new[] {
                     "Bot: Keep regular backups offline or in a secure cloud service and test restores periodically."
                 }),

                (new[] { "report phishing", "report scam", "phishing link", "suspicious attachment", "attachment" },
                 new[] {
                     "Bot: Do not open suspicious attachments; report phishing attempts to your provider or IT team."
                 }),

                (new[] { "smishing", "vishing", "sms phishing", "social engineering", "phone scam" },
                 new[] {
                     "Bot: Attackers use phone/SMS tricks—never give sensitive info over unsolicited calls or texts."
                 }),

                (new[] { "sim swap", "sim swapping" },
                 new[] {
                     "Bot: SIM swap attackers try to take your number—use carrier PINs and avoid sharing personal data."
                 }),

                (new[] { "ransomware", "ransom" },
                 new[] {
                     "Bot: If infected, isolate the machine from the network and contact security professionals.",
                     "Bot: Paying ransom is discouraged—report the incident instead."
                 }),

                (new[] { "update os", "os update", "software update", "patch", "update software" },
                 new[] {
                     "Bot: Install updates and security patches promptly to reduce vulnerabilities."
                 }),

                (new[] { "least privilege", "principle of least privilege", "admin rights" },
                 new[] {
                     "Bot: Use non-admin accounts for daily tasks to limit damage from attacks."
                 }),

                (new[] { "router password", "change router password", "iot", "iot security" },
                 new[] {
                     "Bot: Change default router passwords, keep firmware updated, and segment IoT devices on a separate network."
                 }),

                (new[] { "file sharing", "share files securely", "secure file sharing" },
                 new[] {
                     "Bot: Use encrypted file-sharing services and avoid sending sensitive data by insecure methods."
                 }),

                (new[] { "data breach", "breach", "account hacked" },
                 new[] {
                     "Bot: If breached, change passwords, enable MFA, check account activity, and notify affected parties."
                 }),

                (new[] { "privacy settings", "cookie settings", "cookies" },
                 new[] {
                     "Bot: Review and tighten privacy settings on social platforms and limit cookie tracking where possible."
                 }),

                (new[] { "create a strong password", "strong password", "password safety" },
                 new[] {
                     "Bot: Use uppercase, lowercase, numbers, and symbols.",
                     "Bot: Prefer unique passwords per site; consider a password manager."
                 }),

                (new[] { "phishing", "fake email", "scam email", "phishing scams" },
                 new[] {
                     "Bot: Phishing tricks users into revealing credentials or sensitive data.",
                     "Bot: Check sender addresses and hover links before clicking."
                 }),

                (new[] { "scam", "fraud", "online scam" },
                 new[] {
                     "Bot: Be cautious of requests for money or personal information; verify independently."
                 }),

                (new[] { "hacker", "hackers" },
                 new[] {
                     "Bot: Hackers try to gain unauthorized access; keep credentials and devices secure."
                 }),

                (new[] { "help", "what can i ask" },
                 new[] {
                     "Bot: You can ask about passwords, phishing, backups, VPNs, MFA, and incident response."
                 }),

                (new[] { "thanks", "thank you" },
                 new[] {
                     "Bot: You're welcome!"
                 }),

                (new[] { "bye", "goodbye", "see you" },
                 new[] {
                     "Bot: Goodbye! Stay safe online."
                 }),

                (new[] { "hi", "hello", "hey" },
                 new[] {
                     "Bot: Hello! How can I assist you today?"
                 })
            };

            bool matched = false;

            foreach (var (keys, replies) in rules)
            {
                if (keys.Any(k => userInput.Contains(k)))
                {
                    foreach (var reply in replies)
                    {
                        txtChat.AppendText(reply + "\n");
                    }

                    activityLog.Add(
                        DateTime.Now.ToString("HH:mm:ss")
                        + " - Bot responded");

                    matched = true;
                    break;
                }
            }

            if (!matched)
            {
                txtChat.AppendText("Bot: Sorry, I don't understand.\n");
                txtChat.AppendText("Bot: Please ask something related to general conversation or cybersecurity.\n");
            }

            txtInput.Clear();
            txtChat.ScrollToEnd();
            txtInput.Focus();
        }

        private void btnQuiz_Click(object sender, RoutedEventArgs e)
        {
            quizRunning = true;

            currentQuestion = 0;

            score = 0;

            ShowQuestion();

            activityLog.Add(
                DateTime.Now.ToString("HH:mm:ss")
                + " - Quiz Started");
        }

        private void ShowQuestion()
        {
            Question q = questions[currentQuestion];

            txtChat.AppendText(
                "\nQuestion "
                + (currentQuestion + 1)
                + "\n");

            txtChat.AppendText(
                q.QuestionText + "\n");

            for (int i = 0; i < q.Options.Length; i++)
            {
                txtChat.AppendText(
                    i + ": " +
                    q.Options[i] + "\n");
            }

            txtChat.AppendText(
                "Type the answer number.\n");
        }

        private void CheckAnswer(int answer)
        {
            Question q = questions[currentQuestion];

            if (answer == q.CorrectAnswer)
            {
                score++;

                txtChat.AppendText("Correct!\n");
            }
            else
            {
                txtChat.AppendText("Wrong!\n");
            }

            txtChat.AppendText(
                q.Explanation + "\n");

            currentQuestion++;

            if (currentQuestion >= questions.Count)
            {
                EndQuiz();
            }
            else
            {
                ShowQuestion();
            }
        }

        private void EndQuiz()
        {
            quizRunning = false;

            txtChat.AppendText(
                "\nQuiz Finished!\n");

            txtChat.AppendText(
                "Score: "
                + score
                + "/"
                + questions.Count
                + "\n");

            if (score >= 2)
            {
                txtChat.AppendText(
                    "Great job! You're a cybersecurity pro!\n");
            }
            else
            {
                txtChat.AppendText(
                    "Keep learning to stay safe online!\n");
            }

            activityLog.Add(
                DateTime.Now.ToString("HH:mm:ss")
                + " - Quiz Completed");
        }

        private void btnActivityLog_Click(object sender, RoutedEventArgs e)
        {
            txtChat.AppendText(
                "\n=== Activity Log ===\n");

            foreach (string item in activityLog)
            {
                txtChat.AppendText(item + "\n");
            }

            txtChat.AppendText(
                "===================\n");
        }

        private void txtChat_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
        }

        private void txtInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
        }
    }
}