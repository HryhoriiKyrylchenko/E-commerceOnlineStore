using E_commerceOnlineStore.Models.DataModels.UserManagement;
using E_commerceOnlineStore.Models.RequestModels.Account;
using E_commerceOnlineStore.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceOnlineStore.Services.Business.Account
{
    /// <summary>
    /// Interface for handling password reset processes for users.
    /// </summary>
    public interface IPasswordResetService
    {
        /// <summary>
        /// Initiates the password reset process for a user who has forgotten their password.
        /// </summary>
        /// <param name="model">The model containing the user's email address for which the password reset is requested.</param>
        /// <param name="baseUrl">The base URL used to generate the password reset link (e.g., for email notification).</param>
        /// <param name="scheme">The URL scheme (e.g., HTTP or HTTPS) used when creating the password reset link.</param>
        /// <returns>An IdentityResult indicating whether the operation succeeded, along with potential errors.</returns>
        Task<IdentityResult> ForgotPasswordAsync(ForgotPasswordModel model, string baseUrl, string scheme);

        /// <summary>
        /// Sends a password reset email to the specified user.
        /// </summary>
        /// <param name="email">The email address of the user who requested the password reset.</param>
        /// <param name="resetLink">The password reset link to be included in the email.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task SendPasswordResetEmailAsync(string email, string resetLink);

        /// <summary>
        /// Checks if the provided password is valid for the specified user.
        /// </summary>
        /// <param name="user">The user whose password is being checked.</param>
        /// <param name="password">The password to check against the user's credentials.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the password is valid.</returns>
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);

        /// <summary>
        /// Resets the user's password using the provided token and new password.
        /// </summary>
        /// <param name="model">The model containing the user's email, reset token, and the new password.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IdentityResult"/> indicating the success or failure of the password reset operation.</returns>
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model);

        /// <summary>
        /// Generates a password reset link for a specified user, including a token for security.
        /// </summary>
        /// <param name="userId">The unique identifier of the user requesting the password reset.</param>
        /// <param name="token">The password reset token generated for the user, ensuring the reset process is secure.</param>
        /// <param name="baseUrl">The base URL of the application, used to form the password reset link.</param>
        /// <param name="scheme">The URL scheme (e.g., HTTP or HTTPS) for constructing the complete reset link.</param>
        /// <returns>An OperationResult containing either the generated password reset link or details about the failure.</returns>
        OperationResult<string> GeneratePasswordResetLink(string userId, string token, string baseUrl, string scheme);
    }
}
