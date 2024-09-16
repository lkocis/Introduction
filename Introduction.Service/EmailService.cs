using Introduction.Service.Common;
using System.Net.Mail;

namespace Introduction.Service
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        public EmailService(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }
        public async Task<bool> SendEmailAsync(string recipient, string subject, string message)
        {
            var mailMessage = new MailMessage("noreply@example.com", recipient, subject, message);
            try
            {
                await _smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
    }
}
