using E_commerceOnlineStore.Models.RequestModels.Account;
using E_commerceOnlineStore.Services.Data.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceOnlineStore.Controllers.Account
{
    /// <summary>
    /// Controller for managing employee profile operations such as updating profile details, changing password, phone number, email, and user settings.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="EmployeeProfileController"/> class.
    /// </remarks>
    /// <param name="employeeDataService">The service used for managing employee data operations.</param>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeProfileController(IEmployeeDataService employeeDataService) : ControllerBase
    {
        private readonly IEmployeeDataService _employeeDataService = employeeDataService;

        /// <summary>
        /// Updates the employee's profile information.
        /// </summary>
        /// <param name="userId">The ID of the user whose profile is to be updated.</param>
        /// <param name="model">The updated profile information.</param>
        /// <returns>An IActionResult indicating the result of the update operation.</returns>
        /// <response code="200">Profile updated successfully with the updated data.</response>
        /// <response code="400">Bad request if the update fails.</response>
        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile(string userId, [FromBody] UpdateProfileModel model)
        {
            var result = await _employeeDataService.UpdateProfileAsync(userId, model);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }

        /// <summary>
        /// Changes the employee's password.
        /// </summary>
        /// <param name="userId">The ID of the user whose password is to be changed.</param>
        /// <param name="model">The new password information.</param>
        /// <returns>An IActionResult indicating the result of the password change operation.</returns>
        /// <response code="200">Password changed successfully.</response>
        /// <response code="400">Bad request if the change fails.</response>
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword(string userId, [FromBody] ChangePasswordModel model)
        {
            var result = await _employeeDataService.ChangePasswordAsync(userId, model);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }

        /// <summary>
        /// Changes the employee's phone number.
        /// </summary>
        /// <param name="userId">The ID of the user whose phone number is to be changed.</param>
        /// <param name="model">The new phone number information.</param>
        /// <returns>An IActionResult indicating the result of the phone number change operation.</returns>
        /// <response code="200">Phone number changed successfully.</response>
        /// <response code="400">Bad request if the change fails.</response>
        [HttpPut("change-phone")]
        public async Task<IActionResult> ChangePhone(string userId, [FromBody] ChangePhoneModel model)
        {
            var result = await _employeeDataService.ChangePhoneAsync(userId, model);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }

        /// <summary>
        /// Updates the user's settings.
        /// </summary>
        /// <param name="userId">The ID of the user whose settings are to be updated.</param>
        /// <param name="model">The new user settings.</param>
        /// <returns>An IActionResult indicating the result of the settings update operation.</returns>
        /// <response code="200">User settings updated successfully.</response>
        /// <response code="400">Bad request if the update fails.</response>
        [HttpPut("update-settings")]
        public async Task<IActionResult> UpdateUserSettings(string userId, [FromBody] UpdateSettingsModel model)
        {
            var result = await _employeeDataService.UpdateUserSettingsAsync(userId, model);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }

        /// <summary>
        /// Changes the employee's email address.
        /// </summary>
        /// <param name="userId">The ID of the user whose email is to be changed.</param>
        /// <param name="model">The new email information.</param>
        /// <returns>An IActionResult indicating the result of the email change operation.</returns>
        /// <response code="200">Email changed successfully.</response>
        /// <response code="400">Bad request if the change fails.</response>
        [HttpPut("change-email")]
        public async Task<IActionResult> ChangeEmail(string userId, [FromBody] ChangeEmailModel model)
        {
            var result = await _employeeDataService.ChangeEmailAsync(userId, model);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }
    }
}
