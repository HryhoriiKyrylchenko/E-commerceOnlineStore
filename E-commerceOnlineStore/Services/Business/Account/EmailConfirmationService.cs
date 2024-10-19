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
                                    ActionContext actionContext,
                                    IEmailService emailService) : IEmailConfirmationService
    {
        private readonly IUrlHelperFactory _urlHelperFactory = urlHelperFactory;
        private readonly ActionContext _actionContext = actionContext;
        private readonly IEmailService _emailService = emailService;

        /// <summary>
        /// Generates a confirmation link for email verification.
        /// </summary>
        /// <param name="userId">The ID of the user for whom the confirmation link is generated.</param>
        /// <param name="token">The email confirmation token.</param>
        /// <returns>An <see cref="OperationResult{string}"/> containing the confirmation link 
        /// or failure information.</returns>
        public OperationResult<string> GenerateConfirmationLink(string userId, string token)
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContext);

            var encodedToken = TokenEncoder.EncodeToken(token);

            var confirmationLink = urlHelper.Action(
                "ConfirmEmail",
                "EmailConfirmation",
                new { userId, token = encodedToken },
                _actionContext.HttpContext.Request.Scheme);

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
