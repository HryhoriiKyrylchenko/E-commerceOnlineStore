using E_commerceOnlineStore.Enums.UserManagement;
using E_commerceOnlineStore.Models.DataModels.UserManagement;
using E_commerceOnlineStore.Services.Business.Notifications;
using E_commerceOnlineStore.Services.Data.User;
using E_commerceOnlineStore.Utilities;
using Microsoft.AspNetCore.Identity;

namespace E_commerceOnlineStore.Services.Business.Account
{
    /// <summary>
    /// Service class for managing user roles in the application.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="RolesService"/> class.
    /// </remarks>
    /// <param name="userDataService">The user data service for retrieving user information.</param>
    /// <param name="userManager">The user manager for managing user-related operations.</param>
    /// <param name="roleManager">The role manager for managing role-related operations.</param>
    public class RolesService(UserManager<ApplicationUser> userManager,
                                RoleManager<IdentityRole> roleManager) : IRolesService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;

        /// <summary>
        /// Retrieves a list of users assigned to a specific role.
        /// </summary>
        /// <param name="roleName">The name of the role.</param>
        /// <returns>A task that represents the asynchronous operation, containing the list of users in the role.</returns>
        public async Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName)
        {
            return await _userManager.GetUsersInRoleAsync(roleName);
        }

        public async Task<IdentityResult> AssignRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found" });
            }

            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var role = new IdentityRole(roleName);
                var roleCreationResult = await _roleManager.CreateAsync(role);
                if (!roleCreationResult.Succeeded)
                {
                    return roleCreationResult;
                }
            }

            return await _userManager.AddToRoleAsync(user, roleName);
        }

        /// <summary>
        /// Assigns a list of roles to a user.
        /// </summary>
        /// <param name="userId">The ID of the user to assign roles to.</param>
        /// <param name="userRoles">A list of role names to assign to the user.</param>
        /// <returns>A task that represents the asynchronous operation, containing the result of the role assignments.</returns>
        public async Task<IdentityResult> AssignRolesListAsync(string userId, List<string> userRoles)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found" });
            }

            foreach (var role in userRoles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    var newRole = new IdentityRole(role);
                    var roleCreationResult = await _roleManager.CreateAsync(newRole);
                    if (!roleCreationResult.Succeeded)
                    {
                        return roleCreationResult;
                    }
                }
            }

            return await _userManager.AddToRolesAsync(user, userRoles);
        }

        /// <summary>
        /// Removes a role from a user.
        /// </summary>
        /// <param name="userId">The ID of the user to remove the role from.</param>
        /// <param name="roleName">The name of the role to remove.</param>
        /// <returns>A task that represents the asynchronous operation, containing the result of the role removal.</returns>
        public async Task<IdentityResult> RemoveRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found" });
            }

            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                return IdentityResult.Failed(new IdentityError { Description = "Role not fount" });
            }

            return await _userManager.RemoveFromRoleAsync(user, roleName);
        }
    }
}
