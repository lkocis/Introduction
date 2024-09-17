using Introduction.Service.Common;
using System.Net;
using System.Net.Mail;

namespace Introduction.Service
{
    public class EmailService : IEmailService
    {
        public SmtpClient _smtpClient;
        public string _fromEmail;

        public EmailService(SmtpClient smtpClient, string fromEmail)
        {
            _smtpClient = smtpClient;
            _fromEmail = fromEmail;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using (var mailMessage = new MailMessage(_fromEmail, to, subject, body))
            {
                mailMessage.From = new MailAddress("hookingapp2@gmail.com");
                mailMessage.To.Add(to);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = false; 

                await _smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}
