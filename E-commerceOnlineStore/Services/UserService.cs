using E_commerceOnlineStore.Models;
using Microsoft.AspNetCore.Identity;

namespace E_commerceOnlineStore.Services
{
    /// <summary>
    /// Provides methods to manage application users.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="UserService"/> class.
    /// </remarks>
    public class UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        private readonly RoleManager<IdentityRole> _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));

        /// <summary>
        /// Creates a new user with specified details and assigns a role.
        /// </summary>
        /// <param name="userName">The username of the new user.</param>
        /// <param name="email">The email of the new user.</param>
        /// <param name="password">The password for the new user.</param>
        /// <param name="firstName">The first name of the new user.</param>
        /// <param name="lastName">The last name of the new user.</param>
        /// <param name="dateOfBirth">The date of birth of the new user.</param>
        /// <param name="gender">The gender of the new user.</param>
        /// <param name="profilePictureUrl">The profile picture URL of the new user.</param>
        /// <param name="roleName">The role to assign to the new user.</param>
        public async Task CreateUserAsync(
            string userName,
            string email,
            string password,
            string firstName,
            string lastName,
            DateTime? dateOfBirth,
            string? gender,
            string? profilePictureUrl,
            string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var role = new IdentityRole(roleName);
                await _roleManager.CreateAsync(role);
            }

            var user = new ApplicationUser
            {
                UserName = userName,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                Gender = gender,
                ProfilePictureUrl = profilePictureUrl,
                IsActive = true
                // Other properties can be set here
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, roleName);
            }
            else
            {
                throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
            }
        }

        /// <summary>
        /// Gets a user by ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>The user with the specified ID, or null if not found.</returns>
        public async Task<ApplicationUser?> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="userName">The username of the user to retrieve.</param>
        /// <returns>The user with the specified username, or null if no such user is found.</returns>
        public async Task<ApplicationUser?> GetUserByNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        /// <summary>
        /// Retrieves a user by their email address.
        /// </summary>
        /// <param name="email">The email address of the user to retrieve.</param>
        /// <returns>The user with the specified email address, or null if no such user is found.</returns>
        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
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
    }
}
