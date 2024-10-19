using E_commerceOnlineStore.Models.DataModels.UserManagement;
using E_commerceOnlineStore.Models.RequestModels.Account;
using E_commerceOnlineStore.Utilities;
using Microsoft.AspNetCore.Identity;

namespace E_commerceOnlineStore.Services.Data.User
{
    /// <summary>
    /// Provides methods for user management operations.
    /// </summary>
    public interface IUserDataService
    {
        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the user or an error message.</returns>
        Task<OperationResult<ApplicationUser>> GetUserByIdAsync(string userId);

        /// <summary>
        /// Retrieves a user by their email address.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the user or an error message.</returns>
        Task<OperationResult<ApplicationUser>> GetUserByEmailAsync(string email);

        /// <summary>
        /// Confirms the user's email using the specified token.
        /// </summary>
        /// <param name="user">The user whose email is to be confirmed.</param>
        /// <param name="token">The token used for email confirmation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the result of the email confirmation.</returns>
        Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token);

        /// <summary>
        /// Enables two-factor authentication for a user identified by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the result of the operation.</returns>
        Task<IdentityResult> EnableTwoFactorAuthenticationAsync(string userId);

        /// <summary>
        /// Disables two-factor authentication for a user identified by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the result of the operation.</returns>
        Task<IdentityResult> DisableTwoFactorAuthenticationAsync(string userId);

        /// <summary>
        /// Updates the profile information of a user identified by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="model">The model containing the updated profile information.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the updated user or an error message.</returns>
        Task<OperationResult<ApplicationUser>> UpdateProfileAsync(string userId, UpdateProfileModel model);

        /// <summary>
        /// Changes the password of a user identified by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="model">The model containing the current and new password information.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the updated user or an error message.</returns>
        Task<OperationResult<ApplicationUser>> ChangePasswordAsync(string userId, ChangePasswordModel model);

        /// <summary>
        /// Changes the phone number of a user identified by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="model">The model containing the new phone number information.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the updated user or an error message.</returns>
        Task<OperationResult<ApplicationUser>> ChangePhoneAsync(string userId, ChangePhoneModel model);

        /// <summary>
        /// Updates user settings such as preferred language and time zone for a user identified by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="model">The model containing the updated settings information.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the updated user or an error message.</returns>
        Task<OperationResult<ApplicationUser>> UpdateUserSettingsAsync(string userId, UpdateSettingsModel model);

        /// <summary>
        /// Changes the email address of a user identified by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="model">The model containing the current email, new email, and password for verification.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the updated user or an error message.</returns>
        Task<OperationResult<ApplicationUser>> ChangeEmailAsync(string userId, ChangeEmailModel model);
    }
}
