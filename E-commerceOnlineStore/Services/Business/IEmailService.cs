using Microsoft.AspNetCore.Mvc;

namespace E_commerceOnlineStore.Services.Business
{
    /// <summary>
    /// Defines methods for sending emails related to user authentication and other services.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends a password reset email to the specified email address with a reset link.
        /// </summary>
        /// <param name="email">The email address to which the password reset email will be sent.</param>
        /// <param name="resetLink">The URL link for resetting the password.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation. The result indicates the outcome of the email sending process.</returns>
        Task SendPasswordResetEmailAsync(string email, string resetLink);

        /// <summary>
        /// Sends an email confirmation message to the specified address.
        /// </summary>
        /// <param name="email">The recipient's email address.</param>
        /// <param name="confirmationLink">The link that the recipient must click to confirm their email.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the email fails to send.</exception>
        Task SendEmailConfirmationEmailAsync(string email, string confirmationLink);
    }
}
