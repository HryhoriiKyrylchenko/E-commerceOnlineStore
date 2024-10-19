using E_commerceOnlineStore.Models.RequestModels.Account;
using E_commerceOnlineStore.Services.Business.Account;
using E_commerceOnlineStore.Services.Data.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceOnlineStore.Controllers.Account
{
    /// <summary>
    /// Controller for managing user roles.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="RolesController"/> class.
    /// </remarks>
    /// <param name="rolesService">The service responsible for role management.</param>
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController(IRolesService rolesService) : ControllerBase
    {
        private readonly IRolesService _rolesService = rolesService;

        /// <summary>
        /// Assigns a role to a user.
        /// </summary>
        /// <param name="model">The model containing user ID and role name.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        /// <response code="200">Role assigned successfully.</response>
        /// <response code="400">Bad request if the model is null or if validation fails.</response>
        /// <response code="500">An unexpected error occurred.</response>
        [HttpPost("assign-role")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleModel model)
        {
            if (model == null)
            {
                return BadRequest(new { message = "Assign role model cannot be null" });
            }

            try
            {
                if (string.IsNullOrEmpty(model.UserId))
                {
                    return BadRequest(new { message = "User ID cannot be null or empty" });
                }

                if (string.IsNullOrEmpty(model.RoleName))
                {
                    return BadRequest(new { message = "Role name cannot be null or empty" });
                }

                var result = await _rolesService.AssignRoleAsync(model.UserId, model.RoleName);

                if (result.Succeeded)
                {
                    return Ok("Role assigned successfully");
                }

                return BadRequest(new { message = string.Join("; ", result.Errors.Select(e => e.Description)) });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch
            {
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }

        /// <summary>
        /// Removes a role from a user.
        /// </summary>
        /// <param name="model">The model containing user ID and role name.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        /// <response code="200">Role successfully removed from the user.</response>
        /// <response code="400">Bad request if the model is null or if validation fails.</response>
        /// <response code="404">User or role not found.</response>
        /// <response code="500">An error occurred while removing the role.</response>
        [HttpPost("remove-role")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> RemoveRole([FromBody] AssignRoleModel model)
        {
            if (model == null)
            {
                return BadRequest(new { message = "Assign role model cannot be null" });
            }

            if (string.IsNullOrEmpty(model.UserId))
            {
                return BadRequest(new { message = "User ID cannot be null or empty" });
            }

            if (string.IsNullOrEmpty(model.RoleName))
            {
                return BadRequest(new { message = "Role name cannot be null or empty" });
            }

            try
            {
                var result = await _rolesService.RemoveRoleAsync(model.UserId, model.RoleName);

                if (result.Succeeded)
                {
                    return Ok("Role successfully removed from the user.");
                }

                return BadRequest(new { message = string.Join("; ", result.Errors.Select(e => e.Description)) });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("User not found") || ex.Message.Contains("Role not found"))
                {
                    return NotFound(new { message = ex.Message });
                }

                return StatusCode(500, new { message = $"An error occurred while removing the role: {ex.Message}" });
            }
        }
    }
}
