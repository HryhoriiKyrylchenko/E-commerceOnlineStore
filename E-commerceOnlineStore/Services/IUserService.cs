using E_commerceOnlineStore.Models;

namespace E_commerceOnlineStore.Services
{
    /// <summary>
    /// Defines methods for managing application users, including user creation, retrieval, and role management.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Creates a new user with the specified details and assigns a role.
        /// </summary>
        /// <param name="userName">The username of the new user.</param>
        /// <param name="email">The email address of the new user.</param>
        /// <param name="password">The password for the new user.</param>
        /// <param name="firstName">The first name of the new user.</param>
        /// <param name="lastName">The last name of the new user.</param>
        /// <param name="dateOfBirth">The date of birth of the new user.</param>
        /// <param name="gender">The gender of the new user.</param>
        /// <param name="profilePictureUrl">The URL of the user's profile picture.</param>
        /// <param name="roleName">The role to assign to the new user.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task CreateUserAsync(
            string userName,
            string email,
            string password,
            string firstName,
            string lastName,
            DateTime? dateOfBirth,
            string? gender,
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
    }
}
