using E_commerceOnlineStore.Utilities;
using System.ClientModel.Primitives;

namespace E_commerceOnlineStore.Services.Business.Account
{
    /// <summary>
    /// Interface for handling email confirmation processes for users.
    /// </summary>
    public interface IEmailConfirmationService
    {
        /// <summary>
        /// Generates an email confirmation link for the specified user based on the provided token.
        /// </summary>
        /// <param name="userId">The unique identifier of the user for whom the confirmation link is generated.</param>
        /// <param name="token">The token used to generate the confirmation link.</param>
        /// <returns>An <see cref="OperationResult{string}"/> containing the confirmation link or failure information.</returns>
        OperationResult<string> GenerateConfirmationLink(string userId, string token);

        /// <summary>
        /// Sends an email confirmation message to the specified email address containing the confirmation link.
        /// </summary>
        /// <param name="email">The email address of the user to whom the confirmation email will be sent.</param>
        /// <param name="confirmationLink">The confirmation link to be included in the email.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task SendEmailConfirmationEmailAsync(string email, string confirmationLink);
    }
}
