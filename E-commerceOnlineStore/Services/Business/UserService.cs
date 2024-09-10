using Azure.Core;
using E_commerceOnlineStore.Controllers;
using E_commerceOnlineStore.Enums.UserManagement;
using E_commerceOnlineStore.Models.DataModels.UserManagement;
using E_commerceOnlineStore.Models.RequestModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace E_commerceOnlineStore.Services.Business
{
    /// <summary>
    /// Provides methods to manage application users.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="UserService"/> class.
    /// </remarks>
    public class UserService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ILogger<AuthController> logger
        ) : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        private readonly RoleManager<IdentityRole> _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        private readonly ILogger<AuthController> _logger = logger;

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
        public async Task<ApplicationUser> CreateUserAsync(
            string userName,
            string email,
            string password,
            string firstName,
            string lastName,
            DateTime? dateOfBirth,
            GenderType? gender,
            string? phoneNumber,
            string? profilePictureUrl,
            string roleName)
        {
            // Check if the role exists; if not, create it
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var role = new IdentityRole(roleName);
                var roleResult = await _roleManager.CreateAsync(role);
                if (!roleResult.Succeeded)
                {
                    throw new Exception("Failed to create role: " + string.Join("; ", roleResult.Errors.Select(e => e.Description)));
                }
            }

            // Create a new user
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                Gender = gender,
                PhoneNumber = phoneNumber,
                ProfilePictureUrl = profilePictureUrl,
                IsActive = true
                // Additional properties can be set here
            };

            // Attempt to create the user
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                // Assign the user to the specified role
                var roleResult = await _userManager.AddToRoleAsync(user, roleName);
                if (!roleResult.Succeeded)
                {
                    throw new Exception("Failed to assign role: " + string.Join("; ", roleResult.Errors.Select(e => e.Description)));
                }

                return user;
            }
            else
            {
                throw new Exception("Failed to create user: " + string.Join("; ", result.Errors.Select(e => e.Description)));
            }
        }

        /// <summary>
        /// Gets a user by ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>The user with the specified ID, or null if not found.</returns>
        public async Task<ApplicationUser?> GetUserByIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return await _userManager.FindByIdAsync(userId);
        }

        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="userName">The username of the user to retrieve.</param>
        /// <returns>The user with the specified username, or null if no such user is found.</returns>
        public async Task<ApplicationUser?> GetUserByNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException(nameof(userName));
            }

            return await _userManager.FindByNameAsync(userName);
        }

        /// <summary>
        /// Retrieves a user by their email address.
        /// </summary>
        /// <param name="email">The email address of the user to retrieve.</param>
        /// <returns>The user with the specified email address, or null if no such user is found.</returns>
        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            return await _userManager.FindByEmailAsync(email);
        }

        /// <summary>
        /// Verifies if the provided password is correct for the specified user.
        /// </summary>
        /// <param name="user">The user whose password is to be verified.</param>
        /// <param name="password">The password to verify.</param>
        /// <returns>True if the password is correct; otherwise, false.</returns>
        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }


        /// <summary>
        /// Gets users that belong to a specific role.
        /// </summary>
        /// <param name="roleName">The role name to filter users by.</param>
        /// <returns>A list of users that have the specified role.</returns>
        public async Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName)
        {
            return await _userManager.GetUsersInRoleAsync(roleName);
        }

        /// <summary>
        /// Resets the password for the user using the provided token and new password.
        /// </summary>
        /// <param name="user">The user whose password is to be reset.</param>
        /// <param name="token">The password reset token.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>A result indicating success or failure.</returns>
        public async Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string newPassword)
        {
            ArgumentNullException.ThrowIfNull(user);

            _logger.LogInformation("ResetPasswordAsync called with token: {Token}", token);



            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            _logger.LogInformation("ResetPasswordAsync result: {Succeeded}, Errors: {Errors}", result.Succeeded, string.Join(", ", result.Errors.Select(e => e.Description)));

            return result;
        }

        /// <summary>
        /// Updates the profile information of a user asynchronously based on the provided model.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose profile is to be updated.</param>
        /// <param name="model">An instance of <see cref="UpdateProfileModel"/> containing the updated profile information.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains an <see cref="IdentityResult"/> indicating the outcome of the update operation.</returns>
        /// <exception cref="Exception">Thrown when the user with the specified <paramref name="userId"/> is not found.</exception>
        public async Task<IdentityResult> UpdateUserProfileAsync(string userId, UpdateProfileModel model)
        {
            // Retrieve the user based on the provided user ID
            var user = await GetUserByIdAsync(userId) ?? throw new Exception("User not found");

            // Update user properties with values from the model
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Gender = model.Gender;
            user.PhoneNumber = model.PhoneNumber;
            user.ProfilePictureUrl = model.ProfilePictureUrl;

            // Save the updated user profile and return the result of the update operation
            return await _userManager.UpdateAsync(user);
        }

        /// <summary>
        /// Assigns a role to a user asynchronously. If the role does not exist, it will be created first.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to whom the role is to be assigned.</param>
        /// <param name="roleName">The name of the role to be assigned to the user.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains an <see cref="IdentityResult"/> indicating the outcome of the role assignment operation.</returns>
        /// <exception cref="Exception">Thrown when the user with the specified <paramref name="userId"/> is not found.</exception>
        /// <exception cref="Exception">Thrown when the role with the specified <paramref name="roleName"/> could not be created (in case of any issues with role creation).</exception>
        public async Task<IdentityResult> AssignRoleAsync(string userId, string roleName)
        {
            var user = await GetUserByIdAsync(userId) ?? throw new Exception("User not found");

            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            return await _userManager.AddToRoleAsync(user, roleName);
        }

        /// <summary>
        /// Removes a role from a user asynchronously. Throws an exception if the role does not exist.
        /// </summary>
        /// <param name="userId">The unique identifier of the user from whom the role is to be removed.</param>
        /// <param name="roleName">The name of the role to be removed from the user.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains an <see cref="IdentityResult"/> indicating the outcome of the role removal operation.</returns>
        /// <exception cref="Exception">Thrown when the user with the specified <paramref name="userId"/> is not found.</exception>
        /// <exception cref="Exception">Thrown when the role with the specified <paramref name="roleName"/> is not found.</exception>
        public async Task<IdentityResult> RemoveRoleAsync(string userId, string roleName)
        {
            var user = await GetUserByIdAsync(userId) ?? throw new Exception("User not found");
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                throw new Exception("Role not found");
            }

            return await _userManager.RemoveFromRoleAsync(user, roleName);
        }

        /// <summary>
        /// Enables two-factor authentication (2FA) for a specified user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user for whom 2FA is to be enabled.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains an <see cref="IdentityResult"/> indicating the outcome of enabling 2FA.</returns>
        /// <exception cref="Exception">Thrown when the user with the specified <paramref name="userId"/> is not found.</exception>
        public async Task<IdentityResult> EnableTwoFactorAuthenticationAsync(string userId)
        {
            // Retrieve the user based on the provided user ID
            var user = await GetUserByIdAsync(userId) ?? throw new Exception("User not found");

            // Enable two-factor authentication for the user
            return await _userManager.SetTwoFactorEnabledAsync(user, true);
        }

        /// <summary>
        /// Disables two-factor authentication (2FA) for a specified user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user for whom 2FA is to be disabled.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains an <see cref="IdentityResult"/> indicating the outcome of disabling 2FA.</returns>
        /// <exception cref="Exception">Thrown when the user with the specified <paramref name="userId"/> is not found.</exception>
        public async Task<IdentityResult> DisableTwoFactorAuthenticationAsync(string userId)
        {
            // Retrieve the user based on the provided user ID
            var user = await GetUserByIdAsync(userId) ?? throw new Exception("User not found");

            // Disable two-factor authentication for the user
            return await _userManager.SetTwoFactorEnabledAsync(user, false);
        }

        /// <summary>
        /// Confirms a user's email address using a confirmation token asynchronously.
        /// </summary>
        /// <param name="user">The user whose email is being confirmed.</param>
        /// <param name="token">The email confirmation token.</param>
        /// <returns>An IdentityResult indicating the outcome of the email confirmation process.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the user or token is null.</exception>
        public async Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token)
        {
            // Validate input parameters
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException(nameof(token), "Token cannot be null or empty.");
            }

            // Confirm the user's email address
            return await _userManager.ConfirmEmailAsync(user, token);
        }
    }
}
