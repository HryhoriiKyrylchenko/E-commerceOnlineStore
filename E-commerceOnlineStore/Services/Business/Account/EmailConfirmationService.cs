using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using E_commerceOnlineStore.Utilities;
using E_commerceOnlineStore.Services.Business.Notifications;

namespace E_commerceOnlineStore.Services.Business.Account
{
    /// <summary>
    /// Service for handling email confirmation operations, including generating confirmation links 
    /// and sending email confirmation messages.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="EmailConfirmationService"/> class.
    /// </remarks>
    /// <param name="urlHelperFactory">Factory to create URL helpers for generating links.</param>
    /// <param name="actionContext">The current action context for URL generation.</param>
    /// <param name="emailService">Service for sending emails.</param>
    public class EmailConfirmationService(IUrlHelperFactory urlHelperFactory,
                                    IEmailService emailService) : IEmailConfirmationService
    {
        private readonly IUrlHelperFactory _urlHelperFactory = urlHelperFactory;
        private readonly IEmailService _emailService = emailService;

        /// <summary>
        /// Generates an email confirmation link for a specified user, including a token for verification.
        /// </summary>
        /// <param name="userId">The unique identifier of the user who needs to confirm their email address.</param>
        /// <param name="token">The email confirmation token generated for the user, ensuring the verification process is secure.</param>
        /// <param name="baseUrl">The base URL of the application, used to form the confirmation link.</param>
        /// <param name="scheme">The URL scheme (e.g., HTTP or HTTPS) for constructing the complete confirmation link.</param>
        /// <returns>An OperationResult containing either the generated confirmation link or details about the failure.</returns>

        public OperationResult<string> GenerateConfirmationLink(string userId, string token, string baseUrl, string scheme)
        {
            var encodedToken = TokenEncoder.EncodeToken(token);

            var confirmationLink = $"{scheme}://{baseUrl}/EmailConfirmation/ConfirmEmail?userId={userId}&token={encodedToken}";

            if (string.IsNullOrEmpty(confirmationLink))
            {
                return OperationResult<string>.FailureResult(["Failed create confirmation link"]);
            }

            return OperationResult<string>.SuccessResult(confirmationLink);
        }

        /// <summary>
        /// Sends an email containing the confirmation link to the specified email address.
        /// </summary>
        /// <param name="email">The email address to send the confirmation link to.</param>
        /// <param name="confirmationLink">The confirmation link to include in the email.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SendEmailConfirmationEmailAsync(string email, string confirmationLink)
        {
            const string subject = "Email Confirmation";
            var message = $"Please confirm your email by clicking the following link: {confirmationLink}";

            await _emailService.SendEmailAsync(email, subject, message);
        }
    }
}
