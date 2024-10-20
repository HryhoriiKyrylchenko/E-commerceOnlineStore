using E_commerceOnlineStore.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.ClientModel.Primitives;

namespace E_commerceOnlineStore.Services.Business.Account
{
    /// <summary>
    /// Interface for handling email confirmation processes for users.
    /// </summary>
    public interface IEmailConfirmationService
    {
        /// <summary>
        /// Generates an email confirmation link for a specified user, including a token for verification.
        /// </summary>
        /// <param name="userId">The unique identifier of the user who needs to confirm their email address.</param>
        /// <param name="token">The email confirmation token generated for the user, ensuring the verification process is secure.</param>
        /// <param name="baseUrl">The base URL of the application, used to form the confirmation link.</param>
        /// <param name="scheme">The URL scheme (e.g., HTTP or HTTPS) for constructing the complete confirmation link.</param>
        /// <returns>An OperationResult containing either the generated confirmation link or details about the failure.</returns>
        OperationResult<string> GenerateConfirmationLink(string userId, string token, string baseUrl, string scheme);

        /// <summary>
        /// Sends an email confirmation message to the specified email address containing the confirmation link.
        /// </summary>
        /// <param name="email">The email address of the user to whom the confirmation email will be sent.</param>
        /// <param name="confirmationLink">The confirmation link to be included in the email.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task SendEmailConfirmationEmailAsync(string email, string confirmationLink);
    }
}
