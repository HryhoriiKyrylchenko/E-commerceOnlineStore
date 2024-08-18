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
                EnableSsl = true,
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

        /// <summary>
        /// Sends an email confirmation message to the specified address.
        /// </summary>
        /// <param name="email">The recipient's email address.</param>
        /// <param name="confirmationLink">The link that the recipient must click to confirm their email.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the email fails to send.</exception>
        public async Task SendEmailConfirmationEmailAsync(string email, string confirmationLink)
        {
            // Sender and recipient email addresses
            var fromAddress = new MailAddress(_smtpUser, "E-CommerceOnlineStore");
            var toAddress = new MailAddress(email);

            // Email subject and body
            const string subject = "Email Confirmation";
            var body = $"Please confirm your email by clicking the following link: {confirmationLink}";

            // SMTP client configuration
            var smtpClient = new SmtpClient
            {
                Host = _smtpHost,
                Port = _smtpPort,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_smtpUser, _smtpPass)
            };

            // Create mail message
            var mailMessage = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };

            try
            {
                // Send the email asynchronously
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                // For example: Log.Error("Failed to send email.", ex);
                throw new InvalidOperationException("Failed to send email.", ex);
            }
        }
    }
}
