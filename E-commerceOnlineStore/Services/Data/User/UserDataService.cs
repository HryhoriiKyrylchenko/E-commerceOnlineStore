using E_commerceOnlineStore.Data;
using E_commerceOnlineStore.Models.DataModels.UserManagement;
using E_commerceOnlineStore.Models.RequestModels.Account;
using E_commerceOnlineStore.Utilities;
using Microsoft.AspNetCore.Identity;
using System.ClientModel.Primitives;

namespace E_commerceOnlineStore.Services.Data.User
{
    /// <summary>
    /// Provides data services related to user management, such as confirming emails, updating profiles,
    /// and managing two-factor authentication.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="UserDataService"/> class.
    /// </remarks>
    /// <param name="context">The database context used for accessing user data.</param>
    /// <param name="userManager">The user manager used for managing user identities.</param>
    public class UserDataService(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : IUserDataService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        /// <summary>
        /// Confirms the user's email using the specified token.
        /// </summary>
        /// <param name="user">The user whose email is to be confirmed.</param>
        /// <param name="token">The token used for email confirmation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the result of the email confirmation.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="user"/> or <paramref name="token"/> is null or empty.</exception>
        public async Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException(nameof(token), "Token cannot be null or empty.");
            }

            return await _userManager.ConfirmEmailAsync(user, token);
        }

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the user or an error message.</returns>
        public async Task<OperationResult<ApplicationUser>> GetUserByIdAsync(string userId)
        {
            var user = await _context.ApplicationUsers.FindAsync(userId);

            return user != null
                ? OperationResult<ApplicationUser>.SuccessResult(user)
                : OperationResult<ApplicationUser>.FailureResult(["User not found."]);
        }

        /// <summary>
        /// Retrieves a user by their email address.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the user or an error message.</returns>
        public async Task<OperationResult<ApplicationUser>> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return user != null
                ? OperationResult<ApplicationUser>.SuccessResult(user)
                : OperationResult<ApplicationUser>.FailureResult(["User not found."]);
        }

        /// <summary>
        /// Enables two-factor authentication for a user identified by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the result of the operation.</returns>
        public async Task<IdentityResult> EnableTwoFactorAuthenticationAsync(string userId)
        {
            var userResult = await GetUserByIdAsync(userId);

            if (!userResult.Succeeded || userResult.Data == null)
            {
                var identityErrors = userResult.Errors.Select(e => new IdentityError { Description = e }).ToArray();
                return IdentityResult.Failed(identityErrors);
            }

            return await _userManager.SetTwoFactorEnabledAsync(userResult.Data, true);
        }

        /// <summary>
        /// Disables two-factor authentication for a user identified by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the result of the operation.</returns>
        public async Task<IdentityResult> DisableTwoFactorAuthenticationAsync(string userId)
        {
            var userResult = await GetUserByIdAsync(userId);

            if (!userResult.Succeeded || userResult.Data == null)
            {
                var identityErrors = userResult.Errors.Select(e => new IdentityError { Description = e }).ToArray();
                return IdentityResult.Failed(identityErrors);
            }

            return await _userManager.SetTwoFactorEnabledAsync(userResult.Data, false);
        }

        /// <summary>
        /// Updates the profile information of a user identified by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="model">The model containing the updated profile information.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the updated user or an error message.</returns>
        public async Task<OperationResult<ApplicationUser>> UpdateProfileAsync(string userId, UpdateProfileModel model)
        {
            var userResult = await GetUserByIdAsync(userId);
            if (!userResult.Succeeded || userResult.Data == null)
            {
                return userResult;
            }

            var user = userResult.Data;

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.DateOfBirth = model.DateOfBirth;
            user.Gender = model.Gender;
            user.ProfilePictureUrl = model.ProfilePictureUrl;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return OperationResult<ApplicationUser>.FailureResult(result.Errors.Select(e => e.Description).ToList());
            }

            return OperationResult<ApplicationUser>.SuccessResult(user);
        }

        /// <summary>
        /// Changes the password of a user identified by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="model">The model containing the current and new password information.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the updated user or an error message.</returns>
        public async Task<OperationResult<ApplicationUser>> ChangePasswordAsync(string userId, ChangePasswordModel model)
        {
            var userResult = await GetUserByIdAsync(userId);
            if (!userResult.Succeeded || userResult.Data == null)
            {
                return userResult;
            }

            var user = userResult.Data;

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                return OperationResult<ApplicationUser>.FailureResult(result.Errors.Select(e => e.Description).ToList());
            }

            return OperationResult<ApplicationUser>.SuccessResult(user);
        }

        /// <summary>
        /// Changes the phone number of a user identified by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="model">The model containing the new phone number information.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the updated user or an error message.</returns>
        public async Task<OperationResult<ApplicationUser>> ChangePhoneAsync(string userId, ChangePhoneModel model)
        {
            var userResult = await GetUserByIdAsync(userId);
            if (!userResult.Succeeded || userResult.Data == null)
            {
                return userResult;
            }

            var user = userResult.Data;

            user.PhoneNumber = model.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return OperationResult<ApplicationUser>.FailureResult(result.Errors.Select(e => e.Description).ToList());
            }

            return OperationResult<ApplicationUser>.SuccessResult(user);
        }

        /// <summary>
        /// Updates user settings such as preferred language and time zone for a user identified by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="model">The model containing the updated settings information.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the updated user or an error message.</returns>
        public async Task<OperationResult<ApplicationUser>> UpdateUserSettingsAsync(string userId, UpdateSettingsModel model)
        {
            var userResult = await GetUserByIdAsync(userId);
            if (!userResult.Succeeded || userResult.Data == null)
            {
                return userResult;
            }

            var user = userResult.Data;

            user.PreferredLanguage = model.PreferredLanguage;
            user.TimeZone = model.TimeZone;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return OperationResult<ApplicationUser>.FailureResult(result.Errors.Select(e => e.Description).ToList());
            }

            return OperationResult<ApplicationUser>.SuccessResult(user);
        }

        /// <summary>
        /// Changes the email address of a user identified by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="model">The model containing the current email, new email, and password for verification.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the updated user or an error message.</returns>
        public async Task<OperationResult<ApplicationUser>> ChangeEmailAsync(string userId, ChangeEmailModel model)
        {
            var userResult = await GetUserByIdAsync(userId);
            if (!userResult.Succeeded || userResult.Data == null)
            {
                return userResult;
            }

            var user = userResult.Data;

            if (user.Email != model.CurrentEmail)
            {
                return OperationResult<ApplicationUser>.FailureResult(["Current email does not match."]);
            }

            if (user.Email == model.NewEmail)
            {
                return OperationResult<ApplicationUser>.FailureResult(["The new email is the same as the current one."]);
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordCheck)
            {
                return OperationResult<ApplicationUser>.FailureResult(["Incorrect password."]);
            }

            var token = await _userManager.GenerateChangeEmailTokenAsync(user, model.NewEmail);
            var result = await _userManager.ChangeEmailAsync(user, model.NewEmail, token);

            if (!result.Succeeded)
            {
                return OperationResult<ApplicationUser>.FailureResult(result.Errors.Select(e => e.Description).ToList());
            }

            if (user.UserName == model.CurrentEmail)
            {
                var setUserNameResult = await _userManager.SetUserNameAsync(user, model.NewEmail);
                if (!setUserNameResult.Succeeded)
                {
                    return OperationResult<ApplicationUser>.FailureResult(setUserNameResult.Errors.Select(e => e.Description).ToList());
                }
            }

            return OperationResult<ApplicationUser>.SuccessResult(user);
        }
    }
}
