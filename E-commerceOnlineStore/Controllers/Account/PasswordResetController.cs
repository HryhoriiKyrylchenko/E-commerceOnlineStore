using E_commerceOnlineStore.Models.RequestModels.Account;
using E_commerceOnlineStore.Services.Business.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceOnlineStore.Controllers.Account
{
    /// <summary>
    /// Controller for handling password reset operations.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="PasswordResetController"/> class.
    /// </remarks>
    /// <param name="passwordResetService">The password reset service used to handle password reset logic.</param>
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordResetController(IPasswordResetService passwordResetService) : ControllerBase
    {
        private readonly IPasswordResetService _passwordResetService = passwordResetService;
        
        /// <summary>
        /// Initiates the password reset process by sending a reset token to the user's email.
        /// </summary>
        /// <param name="model">The model containing the user's email address.</param>
        /// <returns>An IActionResult indicating the result of the password reset token request.</returns>
        /// <response code="200">Password reset token sent successfully.</response>
        /// <response code="400">Bad request if the model is invalid.</response>
        [HttpPost("forgot")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _passwordResetService.ForgotPasswordAsync(model);
            if (result.Succeeded)
                return Ok(new { Message = "Password reset token sent" });

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Resets the user's password using the provided reset token and new password.
        /// </summary>
        /// <param name="model">The model containing the reset token, new password, and user identifier.</param>
        /// <returns>An IActionResult indicating the result of the password reset operation.</returns>
        /// <response code="200">Password reset successfully.</response>
        /// <response code="400">Bad request if the model is invalid.</response>
        [HttpPost("reset")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _passwordResetService.ResetPasswordAsync(model);
            if (result.Succeeded)
                return Ok(new { Message = "Password reset successfully" });

            return BadRequest(result.Errors);
        }
    }
}
