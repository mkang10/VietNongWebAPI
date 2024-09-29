using System.Net.Mail;
using System.Net;

namespace VietNongWebAPI.Service
{
    public interface IEmailService
    {
        Task<bool> SendPasswordResetEmail(string toEmail, string resetToken);
    }
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer = "smtp.example.com"; // Địa chỉ SMTP của bạn
        private readonly int _smtpPort = 587; // Port cho SMTP
        private readonly string _fromEmail = "your-email@example.com"; // Email gửi đi
        private readonly string _emailPassword = "your-email-password"; // Mật khẩu email

        public async Task<bool> SendPasswordResetEmail(string toEmail, string resetToken)
        {
            try
            {
                var mail = new MailMessage();
                mail.From = new MailAddress(_fromEmail);
                mail.To.Add(toEmail);
                mail.Subject = "Password Reset";
                mail.Body = $"Your password reset token is: {resetToken}";

                using (var smtp = new SmtpClient(_smtpServer, _smtpPort))
                {
                    smtp.Credentials = new NetworkCredential(_fromEmail, _emailPassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
