using Azure.Core;
using E_commerceOnlineStore.Models.DataModels.UserManagement;
using E_commerceOnlineStore.Models.RequestModels.Account;
using E_commerceOnlineStore.Services.Business.Notifications;
using E_commerceOnlineStore.Services.Business.Security;
using E_commerceOnlineStore.Services.Data.User;
using E_commerceOnlineStore.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;

namespace E_commerceOnlineStore.Services.Business.Account
{
    /// <summary>
    /// Service for handling password reset functionality for users.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="PasswordResetService"/> class.
    /// </remarks>
    /// <param name="userDataService">Service for managing user data.</param>
    /// <param name="emailService">Service for sending emails.</param>
    /// <param name="userManager">User manager for managing user-related operations.</param>
    /// <param name="logger">Logger for logging events and errors.</param>
    /// <param name="urlHelperFactory">Factory for creating URL helpers.</param>
    /// <param name="actionContext">Context for the current action.</param>
    /// <param name="tokenService">Service for generating and validating tokens.</param>
    public class PasswordResetService(IUserDataService userDataService,
                                 IEmailService emailService,
                                 UserManager<ApplicationUser> userManager,
                                 ILogger<PasswordResetService> logger,
                                 IUrlHelperFactory urlHelperFactory,
                                 ActionContext actionContext,
                                 ITokenService tokenService) : IPasswordResetService
    {
        private readonly IUserDataService _userDataService = userDataService;
        private readonly IEmailService _emailService = emailService;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly ILogger<PasswordResetService> _logger = logger;
        private readonly IUrlHelperFactory _urlHelperFactory = urlHelperFactory;
        private readonly ActionContext _actionContext = actionContext;
        private readonly ITokenService _tokenService = tokenService;

        /// <summary>
        /// Initiates the password reset process by sending a password reset email to the user.
        /// </summary>
        /// <param name="model">The model containing the user's email address.</param>
        /// <returns>An <see cref="IdentityResult"/> indicating the success or failure of the operation.</returns>
        public async Task<IdentityResult> ForgotPasswordAsync(ForgotPasswordModel model)
        {
            var userResult = await _userDataService.GetUserByEmailAsync(model.Email);
            if (!userResult.Succeeded || userResult.Data == null)
            {
                return IdentityResult.Success;
            }

            var token = await _tokenService.GeneratePasswordResetTokenAsync(userResult.Data);

            var resetLinkResult = GeneratePasswordResetLink(userResult.Data.Id, token);

            if (!resetLinkResult.Succeeded || resetLinkResult.Data == null)
            {
                var identityErrors = userResult.Errors.Select(e => new IdentityError { Description = e }).ToArray();
                return IdentityResult.Failed(identityErrors);
            }

            await SendPasswordResetEmailAsync(model.Email, resetLinkResult.Data);

            return IdentityResult.Success;
        }

        /// <summary>
        /// Sends the password reset email containing the reset link.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <param name="resetLink">The password reset link to be sent.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SendPasswordResetEmailAsync(string email, string resetLink)
        {
            const string subject = "Password Reset Request";
            var message = $"To reset your password, click the following link: {resetLink}";

            await _emailService.SendEmailAsync(email, subject, message);
        }

        /// <summary>
        /// Checks if the provided password matches the user's current password.
        /// </summary>
        /// <param name="user">The user whose password is being checked.</param>
        /// <param name="password">The password to check.</param>
        /// <returns>A task that returns true if the password matches; otherwise, false.</returns>
        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        /// <summary>
        /// Resets the user's password to a new password using a token for verification.
        /// </summary>
        /// <param name="model">The model containing email, new password, and token.</param>
        /// <returns>An <see cref="IdentityResult"/> indicating the success or failure of the password reset.</returns>
        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model)
        {
            var userResult = await _userDataService.GetUserByEmailAsync(model.Email);
            if (!userResult.Succeeded || userResult.Data == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Invalid email address" });
            }

            var result = await _userManager.ResetPasswordAsync(userResult.Data, model.Token, model.NewPassword);

            return result;
        }

        /// <summary>
        /// Generates a password reset link using the user's ID and a token.
        /// </summary>
        /// <param name="userId">The ID of the user for whom the reset link is generated.</param>
        /// <param name="token">The token used for password reset verification.</param>
        /// <returns>
        /// An <see cref="OperationResult{string}"/> containing the generated reset link or an error message.
        /// </returns>
        public OperationResult<string> GeneratePasswordResetLink(string userId, string token)
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContext);

            var encodedToken = TokenEncoder.EncodeToken(token);

            var passwordResetLink = urlHelper.Action(
                "ResetPassword",
                "PasswordReset",
                new { userId, token = encodedToken },
                _actionContext.HttpContext.Request.Scheme);

            if (string.IsNullOrEmpty(passwordResetLink))
            {
                return OperationResult<string>.FailureResult(["Failed create password reset link"]);
            }

            return OperationResult<string>.SuccessResult(passwordResetLink);
        }
    }
}
