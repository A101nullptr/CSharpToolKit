using System;
using System.Net.Mail;
using System.Net;
using CSharpToolKit.LogManager;
using System.Threading.Tasks;
using CSharpToolKit.TimeManager;

namespace CSharpToolKit.NotificationManager
{
    public class Email : IDisposable
    {
        private readonly SmtpClient _smtpClient;
        private Log _log { get; set; }
        private Now _now { get; }

        public Email(string senderEmail, string senderPassword, string logPath)
        {
            _smtpClient = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail, senderPassword)
            };

            _log = new Log(logPath); // Instantiates a logger with specified log path
            _log.CreateLog();
            
            _now = new Now();
        }

        public void SendEmailSync(string recipientEmail, string subject, string body)
        {
            try
            {
                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(((NetworkCredential)_smtpClient.Credentials).UserName);
                    mailMessage.To.Add(recipientEmail);
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;

                    _smtpClient.Send(mailMessage);
                }

                _log.WriteToLog($"NotificationManager::Email sent successfully on {_now.Date(0)} at {_now.Time(0)}");
            }
            catch (Exception ex)
            {
                _log.WriteToLog($"NotificationManager::Failed to send email:{ex.Message} on {_now.Date(0)} at {_now.Time(0)}");
            }
        }

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

                _log.WriteToLog($"NotificationManager::Email sent successfully on {_now.Date(0)} at {_now.Time(0)}");
            }
            catch (Exception ex)
            {
                _log.WriteToLog($"NotificationManager::Failed to send email:{ex.Message} on {_now.Date(0)} at {_now.Time(0)}");
            }
        }

        public void Dispose()
        {
            _smtpClient.Dispose();
        }
    }
}
