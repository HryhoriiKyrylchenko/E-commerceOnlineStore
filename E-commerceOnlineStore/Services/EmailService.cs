using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace E_commerceOnlineStore.Services
{
    public class EmailService(string smtpHost, int smtpPort, string smtpUser, string smtpPass) : IEmailService
    {
        private readonly string _smtpHost = smtpHost;
        private readonly int _smtpPort = smtpPort;
        private readonly string _smtpUser = smtpUser;
        private readonly string _smtpPass = smtpPass;

        public async Task SendPasswordResetEmailAsync(string email, string resetLink)
        {
            var fromAddress = new MailAddress(_smtpUser, "E-CommerceOnlineStore");
            var toAddress = new MailAddress(email);
            const string subject = "Password Reset Request";
            var body = $"To reset your password, click the following link: {resetLink}";

            var smtpClient = new SmtpClient
            {
                Host = _smtpHost,
                Port = _smtpPort,
                EnableSsl = true, // Set this based on your SMTP provider's requirements
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_smtpUser, _smtpPass)
            };

            var mailMessage = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new InvalidOperationException("Failed to send email.", ex);
            }
        }
    }
}
