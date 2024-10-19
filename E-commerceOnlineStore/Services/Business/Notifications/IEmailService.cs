using Microsoft.AspNetCore.Mvc;

namespace E_commerceOnlineStore.Services.Business.Notifications
{
    /// <summary>
    /// Defines methods for sending emails related to user authentication and other services.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email asynchronously.
        /// </summary>
        /// <param name="email">The recipient's email address.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="message">The body of the email.</param>
        /// <returns>A task that represents the asynchronous send operation.</returns>
        Task SendEmailAsync(string email, string subject, string message);
    }
}
