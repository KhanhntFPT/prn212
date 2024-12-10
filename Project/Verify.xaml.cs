using Microsoft.Identity.Client.NativeInterop;
using System.Net.Mail;
using System.Windows;

namespace Project
{
    public partial class Verify : Window
    {
        public Boolean issucc { get; set; } = false;
        private String? code;
        private String Email;
        private Timer? timer;
        public Verify(String Email)
        {
            InitializeComponent();
            this.code = GenerateSixDigitCode();
            this.Email = Email;
            CreateTimeoutTestMessage(code, this.Email);
        }
        private void StartExpirationTimer()
        {
            timer?.Dispose();

            timer = new Timer(_ => ClearVerificationCode(), null, 120000, Timeout.Infinite);
        }

        private void ClearVerificationCode()
        {
            code = null;
            timer?.Dispose(); // Clean up the timer
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void resend_Click(object sender, RoutedEventArgs e)
        {
            this.code = GenerateSixDigitCode();
            CreateTimeoutTestMessage(code, this.Email);
        }

        private void Verify_Click(object sender, RoutedEventArgs e)
        {
            String inputCode = codetb.Text;
            if (inputCode.Equals(this.code))
            {
                this.issucc = true;
                this.Close();
            }
            else
            {
                this.issucc = false;
                MessageBox.Show("wrong verify code");
            }
        }

        public static void CreateTimeoutTestMessage(String code, String email)
        {
            try
            {
                string to = email;
                string from = "khanhnthe181027@fpt.edu.vn";
                string subject = "Verify code";

                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(from);
                mail.To.Add(to);
                mail.Subject = subject;
                string body = $@"
    <html>
        <body style='font-family: Arial, sans-serif;'>
            <h2 style='color: #4CAF50;'>Verification Code</h2>
            <p>This is your verification code:</p>
            <p style='font-size: 24px; font-weight: bold; color: #333;'>{code}</p>
 <p style='color: #555;'>The code will exprire after 2 minutes</p>
            <br/>
            <p style='color: #555;'>Please use this code to complete your verification. If you did not request this, please ignore this email.</p>
            <br/>
            <p style='font-size: 12px; color: #888;'>Thank you</p>
        </body>
    </html>";
                mail.Body = body;
                mail.IsBodyHtml = true;

                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential(from, "tucr piux smja jvdt");
                smtp.EnableSsl = true;

                smtp.Send(mail);
            }
            catch (SmtpException smtpEx)
            {
                MessageBox.Show($"SMTP error: {smtpEx.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"General error: {ex.Message}");
            }
        }
        public static string GenerateSixDigitCode()
        {
            Random random = new Random();
            int code = random.Next(100000, 1000000);
            return code.ToString();
        }

    }
}
