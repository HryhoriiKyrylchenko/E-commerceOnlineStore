using E_commerceOnlineStore.Models;
using E_commerceOnlineStore.Models.Account;
using Microsoft.AspNetCore.Identity;

namespace E_commerceOnlineStore.Services
{
    /// <summary>
    /// Defines methods for managing application users, including user creation, retrieval, and role management.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Creates a new user and assigns them a specified role.
        /// </summary>
        /// <param name="userName">The username for the new user.</param>
        /// <param name="email">The email address for the new user.</param>
        /// <param name="password">The password for the new user.</param>
        /// <param name="firstName">The first name of the new user.</param>
        /// <param name="lastName">The last name of the new user.</param>
        /// <param name="dateOfBirth">The optional date of birth of the new user.</param>
        /// <param name="gender">The optional gender of the new user.</param>
        /// <param name="phoneNumber">The optional gender of the new user.</param>
        /// <param name="profilePictureUrl">The optional profile picture URL of the new user.</param>
        /// <param name="roleName">The name of the role to assign to the new user.</param>
        /// <returns>The created user.</returns>
        /// <exception cref="Exception">Thrown when user creation or role assignment fails.</exception>
        Task<ApplicationUser> CreateUserAsync(
            string userName,
            string email,
            string password,
            string firstName,
            string lastName,
            DateTime? dateOfBirth,
            string? gender,
            string? phoneNumber,
            string? profilePictureUrl,
            string roleName);

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to retrieve.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the user with the specified ID, or null if not found.</returns>
        Task<ApplicationUser?> GetUserByIdAsync(string userId);

        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="userName">The username of the user to retrieve.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the user with the specified username, or null if not found.</returns>
        Task<ApplicationUser?> GetUserByNameAsync(string userName);

        /// <summary>
        /// Retrieves a user by their email address.
        /// </summary>
        /// <param name="email">The email address of the user to retrieve.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the user with the specified email, or null if not found.</returns>
        Task<ApplicationUser?> GetUserByEmailAsync(string email);

        /// <summary>
        /// Checks if the specified password matches the user's current password.
        /// </summary>
        /// <param name="user">The user whose password is to be checked.</param>
        /// <param name="password">The password to check against the user's current password.</param>
        /// <returns>A task representing the asynchronous operation. The task result is true if the password is correct, otherwise false.</returns>
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);

        /// <summary>
        /// Retrieves a list of users that belong to the specified role.
        /// </summary>
        /// <param name="roleName">The name of the role to filter users by.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a list of users that have the specified role.</returns>
        Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName);

        /// <summary>
        /// Resets the password for the user using the provided token and new password.
        /// </summary>
        /// <param name="user">The user whose password is to be reset.</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="token">The password reset token.</param>
        /// <returns>A result indicating success or failure.</returns>
        Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string newPassword);

        /// <summary>
        /// Updates the profile information of a user asynchronously based on the provided model.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose profile is to be updated.</param>
        /// <param name="model">An instance of <see cref="UpdateProfileModel"/> containing the updated profile information.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains an <see cref="IdentityResult"/> indicating the outcome of the update operation.</returns>
        /// <exception cref="Exception">Thrown when the user with the specified <paramref name="userId"/> is not found.</exception>
        Task<IdentityResult> UpdateUserProfileAsync(string userId, UpdateProfileModel model);

        /// <summary>
        /// Assigns a role to a user asynchronously. If the role does not exist, it will be created first.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to whom the role is to be assigned.</param>
        /// <param name="roleName">The name of the role to be assigned to the user.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains an <see cref="IdentityResult"/> indicating the outcome of the role assignment operation.</returns>
        /// <exception cref="Exception">Thrown when the user with the specified <paramref name="userId"/> is not found.</exception>
        /// <exception cref="Exception">Thrown when the role with the specified <paramref name="roleName"/> could not be created (in case of any issues with role creation).</exception>
        Task<IdentityResult> AssignRoleAsync(string userId, string roleName);

        /// <summary>
        /// Removes a role from a user asynchronously. Throws an exception if the role does not exist.
        /// </summary>
        /// <param name="userId">The unique identifier of the user from whom the role is to be removed.</param>
        /// <param name="roleName">The name of the role to be removed from the user.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains an <see cref="IdentityResult"/> indicating the outcome of the role removal operation.</returns>
        /// <exception cref="Exception">Thrown when the user with the specified <paramref name="userId"/> is not found.</exception>
        /// <exception cref="Exception">Thrown when the role with the specified <paramref name="roleName"/> is not found.</exception>
        Task<IdentityResult> RemoveRoleAsync(string userId, string roleName);

        /// <summary>
        /// Enables two-factor authentication (2FA) for a specified user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user for whom 2FA is to be enabled.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains an <see cref="IdentityResult"/> indicating the outcome of enabling 2FA.</returns>
        /// <exception cref="Exception">Thrown when the user with the specified <paramref name="userId"/> is not found.</exception>
        Task<IdentityResult> EnableTwoFactorAuthenticationAsync(string userId);

        /// <summary>
        /// Disables two-factor authentication (2FA) for a specified user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user for whom 2FA is to be disabled.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains an <see cref="IdentityResult"/> indicating the outcome of disabling 2FA.</returns>
        /// <exception cref="Exception">Thrown when the user with the specified <paramref name="userId"/> is not found.</exception>
        Task<IdentityResult> DisableTwoFactorAuthenticationAsync(string userId);

        /// <summary>
        /// Confirms a user's email address using a confirmation token asynchronously.
        /// </summary>
        /// <param name="user">The user whose email is being confirmed.</param>
        /// <param name="token">The email confirmation token.</param>
        /// <returns>An IdentityResult indicating the outcome of the email confirmation process.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the user or token is null.</exception>
        Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token);
    }
}
