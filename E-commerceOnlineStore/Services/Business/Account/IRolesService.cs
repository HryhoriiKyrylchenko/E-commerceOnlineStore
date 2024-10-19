using E_commerceOnlineStore.Models.DataModels.UserManagement;
using Microsoft.AspNetCore.Identity;

namespace E_commerceOnlineStore.Services.Business.Account
{
    /// <summary>
    /// Interface for managing user roles within the application.
    /// </summary>
    public interface IRolesService
    {
        /// <summary>
        /// Retrieves a list of users who are assigned to the specified role.
        /// </summary>
        /// <param name="roleName">The name of the role to retrieve users from.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="ApplicationUser"/> instances in the specified role.</returns>
        Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName);

        /// <summary>
        /// Assigns a specified role to a user.
        /// </summary>
        /// <param name="userId">The ID of the user to whom the role will be assigned.</param>
        /// <param name="roleName">The name of the role to assign to the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IdentityResult"/> indicating the success or failure of the role assignment.</returns>
        Task<IdentityResult> AssignRoleAsync(string userId, string roleName);

        /// <summary>
        /// Assigns a list of roles to a user.
        /// </summary>
        /// <param name="userId">The ID of the user to whom the roles will be assigned.</param>
        /// <param name="userRoles">A list of role names to assign to the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IdentityResult"/> indicating the success or failure of the role assignments.</returns>
        Task<IdentityResult> AssignRolesListAsync(string userId, List<string> userRoles);

        /// <summary>
        /// Removes a specified role from a user.
        /// </summary>
        /// <param name="userId">The ID of the user from whom the role will be removed.</param>
        /// <param name="roleName">The name of the role to remove from the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IdentityResult"/> indicating the success or failure of the role removal.</returns>
        Task<IdentityResult> RemoveRoleAsync(string userId, string roleName);
    }
}
