using E_commerceOnlineStore.Models.RequestModels.Account;
using E_commerceOnlineStore.Services.Data.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceOnlineStore.Controllers.Account
{
    /// <summary>
    /// Handles operations related to customer profiles, including updating profile information, 
    /// changing passwords, phone numbers, email addresses, and user settings.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CustomerProfileController"/> class.
    /// </remarks>
    /// <param name="customerDataService">Service for managing customer data.</param>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerProfileController(ICustomerDataService customerDataService) : ControllerBase
    {
        private readonly ICustomerDataService _customerDataService = customerDataService;

        /// <summary>
        /// Updates the profile information of a customer.
        /// </summary>
        /// <param name="userId">The ID of the customer whose profile is to be updated.</param>
        /// <param name="model">The model containing the updated profile information.</param>
        /// <returns>
        /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation.
        /// </returns>
        /// <response code="200">Profile updated successfully.</response>
        /// <response code="400">Bad request if the update fails or validation errors occur.</response>
        [HttpPut("{userId}/profile")]
        public async Task<IActionResult> UpdateProfile(string userId, [FromBody] UpdateProfileModel model)
        {
            var result = await _customerDataService.UpdateProfileAsync(userId, model);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }

        /// <summary>
        /// Changes the password of a customer.
        /// </summary>
        /// <param name="userId">The ID of the customer whose password is to be changed.</param>
        /// <param name="model">The model containing the current and new password.</param>
        /// <returns>
        /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation.
        /// </returns>
        /// <response code="200">Password changed successfully.</response>
        /// <response code="400">Bad request if the change fails or validation errors occur.</response>
        [HttpPut("{userId}/password")]
        public async Task<IActionResult> ChangePassword(string userId, [FromBody] ChangePasswordModel model)
        {
            var result = await _customerDataService.ChangePasswordAsync(userId, model);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }

        /// <summary>
        /// Changes the phone number of a customer.
        /// </summary>
        /// <param name="userId">The ID of the customer whose phone number is to be changed.</param>
        /// <param name="model">The model containing the new phone number.</param>
        /// <returns>
        /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation.
        /// </returns>
        /// <response code="200">Phone number changed successfully.</response>
        /// <response code="400">Bad request if the change fails or validation errors occur.</response>
        [HttpPut("{userId}/phone")]
        public async Task<IActionResult> ChangePhone(string userId, [FromBody] ChangePhoneModel model)
        {
            var result = await _customerDataService.ChangePhoneAsync(userId, model);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }

        /// <summary>
        /// Updates the user settings for a customer.
        /// </summary>
        /// <param name="userId">The ID of the customer whose settings are to be updated.</param>
        /// <param name="model">The model containing the updated user settings.</param>
        /// <returns>
        /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation.
        /// </returns>
        /// <response code="200">User settings updated successfully.</response>
        /// <response code="400">Bad request if the update fails or validation errors occur.</response>
        [HttpPut("{userId}/settings")]
        public async Task<IActionResult> UpdateUserSettings(string userId, [FromBody] UpdateSettingsModel model)
        {
            var result = await _customerDataService.UpdateUserSettingsAsync(userId, model);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }

        /// <summary>
        /// Changes the email address of a customer.
        /// </summary>
        /// <param name="userId">The ID of the customer whose email is to be changed.</param>
        /// <param name="model">The model containing the new email address.</param>
        /// <returns>
        /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation.
        /// </returns>
        /// <response code="200">Email address changed successfully.</response>
        /// <response code="400">Bad request if the change fails or validation errors occur.</response>
        [HttpPut("{userId}/email")]
        public async Task<IActionResult> ChangeEmail(string userId, [FromBody] ChangeEmailModel model)
        {
            var result = await _customerDataService.ChangeEmailAsync(userId, model);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }
    }
}
