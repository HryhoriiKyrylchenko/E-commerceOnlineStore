using E_commerceOnlineStore.Models.DataModels.UserManagement;
using E_commerceOnlineStore.Models.RequestModels.Account;
using E_commerceOnlineStore.Utilities;
using Microsoft.AspNetCore.Identity;

namespace E_commerceOnlineStore.Services.Business.Account
{
    /// <summary>
    /// Interface for handling password reset processes for users.
    /// </summary>
    public interface IPasswordResetService
    {
        /// <summary>
        /// Initiates the password reset process by generating a password reset token and sending a reset email.
        /// </summary>
        /// <param name="model">The model containing the user's email for password reset.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IdentityResult"/> indicating the success or failure of the password reset initiation.</returns>
        Task<IdentityResult> ForgotPasswordAsync(ForgotPasswordModel model);

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
        /// Generates a password reset link for the specified user based on the provided token.
        /// </summary>
        /// <param name="userId">The unique identifier of the user for whom the password reset link is generated.</param>
        /// <param name="token">The reset token used to generate the password reset link.</param>
        /// <returns>An <see cref="OperationResult{string}"/> containing the password reset link or failure information.</returns>
        OperationResult<string> GeneratePasswordResetLink(string userId, string token);
    }
}
