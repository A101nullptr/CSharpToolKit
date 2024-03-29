using System;
using System.Net.Mail;
using System.Net;
using CSharpToolKit.LogManager;
using System.Threading.Tasks;
using CSharpToolKit.TimeManager;

namespace CSharpToolKit.NotificationManager
{
    /// <summary>
    /// Provides functionality to send emails using SMTP protocol.
    /// </summary>
    public class Email : IDisposable
    {
        private SmtpClient _smtpClient { get; set; }
        private Log _log { get; set; }
        private Now _now { get; }
        private string _email { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Email"/> class with the specified sender email and password.
        /// </summary>
        /// <param name="senderEmail">The sender's email address.</param>
        /// <param name="senderPassword">The password for the sender's email account.</param>
        /// <param name="logPath">Optional. The path to the log file. If provided, logs will be recorded.</param>
        public Email(string senderEmail, string senderPassword, string logPath = null)
        {
            _email = senderEmail;
            EmailCheck(senderPassword);

            if (logPath != null)
            {
                _log = new Log(logPath);
                _log.CreateLog();
            }

            _now = new Now();
        }

        /// <summary>
        /// Checks the validity of the email address and initializes the SMTP client with the provided credentials.
        /// </summary>
        /// <param name="senderPassword">The password for the sender's email account.</param>
        private void EmailCheck(string senderPassword)
        {
            var host = _email.Split('@')[1];

            _smtpClient = new SmtpClient($"smtp-mail.{host}", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_email, senderPassword)
            };
        }

        /// <summary>
        /// Sends an email asynchronously.
        /// </summary>
        /// <param name="recipientEmail">The recipient's email address.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="body">The body of the email.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SendEmailAsync(string recipientEmail, string subject, string body)
        {
            try
            {
                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(((NetworkCredential)_smtpClient.Credentials).UserName);
                    mailMessage.To.Add(recipientEmail);
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;

                    await _smtpClient.SendMailAsync(mailMessage);
                }

                MsgRecorder(1); 
            }
            catch (Exception ex)
            {
                MsgRecorder(0, ex);
            }
        }

        /// <summary>
        /// Records a message indicating the outcome of the email sending operation.
        /// </summary>
        /// <param name="id">An identifier indicating the outcome. 0 for failure, 1 for success.</param>
        /// <param name="ex">Optional. The exception encountered during the operation.</param>
        private void MsgRecorder(int id, Exception ex = null)
        {
            string message;

            if (id == 0)
            {
                message = $"NotificationManager::Failed to send email:{ex?.Message ?? "Unknown error"}";
            }
            else if (id == 1)
            {
                message = "NotificationManager::Email sent successfully";
            }
            else
            {
                throw new ArgumentException("Invalid id value", nameof(id));
            }

            message += $" on {_now.Date(0)} at {_now.Time(0)}";

            if (_log != null)
            {
                _log.WriteToLog(message);
            }
            else
            {
                Console.WriteLine(message);
            }
        }

        /// <summary>
        /// Releases all resources used by the <see cref="Email"/> object.
        /// </summary>
        public void Dispose()
        {
            _smtpClient.Dispose();
        }
    }
}
