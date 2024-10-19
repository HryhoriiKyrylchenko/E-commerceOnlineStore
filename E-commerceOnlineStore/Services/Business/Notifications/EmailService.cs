using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using E_commerceOnlineStore.Models.DataModels.Common;

namespace E_commerceOnlineStore.Services.Business.Notifications
{
    /// <summary>
    /// Service class for sending emails via SMTP.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="EmailService"/> class.
    /// </remarks>
    /// <param name="smtpHost">The SMTP host address.</param>
    /// <param name="smtpPort">The SMTP port number.</param>
    /// <param name="smtpUser">The SMTP user for authentication.</param>
    /// <param name="smtpPass">The SMTP password for authentication.</param>
    public class EmailService(string smtpHost, int smtpPort, string smtpUser, string smtpPass) : IEmailService
    {
        private readonly string _smtpHost = smtpHost;
        private readonly int _smtpPort = smtpPort;
        private readonly string _smtpUser = smtpUser;
        private readonly string _smtpPass = smtpPass;

        /// <summary>
        /// Sends an email asynchronously.
        /// </summary>
        /// <param name="email">The recipient's email address.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="message">The body of the email.</param>
        /// <returns>A task that represents the asynchronous send operation.</returns>
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var fromAddress = new MailAddress(_smtpUser, "E-CommerceOnlineStore");
            var toAddress = new MailAddress(email);

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
                Body = message,
                IsBodyHtml = false
            };

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to send email.", ex);
            }
        }
    }
}
