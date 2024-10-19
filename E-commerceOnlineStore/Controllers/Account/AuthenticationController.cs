using E_commerceOnlineStore.Models.RequestModels.Account;
using E_commerceOnlineStore.Services.Business.Account;
using E_commerceOnlineStore.Services.Data.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_commerceOnlineStore.Controllers.Account
{
    /// <summary>
    /// Manages user authentication operations, including login, logout, token refresh, 
    /// and enabling/disabling two-factor authentication.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
    /// </remarks>
    /// <param name="authService">Service for handling authentication operations.</param>
    /// <param name="userDataService">Service for managing user data operations.</param>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IAuthService authService, IUserDataService userDataService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly IUserDataService _userDataService = userDataService;

        /// <summary>
        /// Authenticates a user and issues a token upon successful login.
        /// </summary>
        /// <param name="model">The model containing user login credentials.</param>
        /// <returns>
        /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation.
        /// </returns>
        /// <response code="200">Login successful, returns the authentication and refresh tokens.</response>
        /// <response code="400">Bad request if the model is invalid.</response>
        /// <response code="401">Unauthorized if the login credentials are invalid.</response>
        /// <response code="500">Internal server error if an unexpected error occurs.</response>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _authService.LoginAsync(model, ModelState);

                if (result.Succeeded)
                {
                    return Ok(new
                    {
                        result.Token,
                        result.RefreshToken
                    });
                }

                return Unauthorized(new { Message = "Invalid email or password" });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An unexpected error occurred: {ex.Message}" });
            }
        }

        /// <summary>
        /// Logs out the user by deleting the authentication token cookie.
        /// </summary>
        /// <returns>
        /// A <see cref="IActionResult"/> representing the result of the logout operation.
        /// </returns>
        /// <response code="200">Logout successful.</response>
        /// <response code="500">Internal server error if an unexpected error occurs.</response>
        [HttpPost("logout")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Logout()
        {
            try
            {
                Response.Cookies.Delete("authToken");

                return Ok("Logged out successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An unexpected error occurred during logout: {ex.Message}" });
            }
        }

        /// <summary>
        /// Refreshes the authentication token using a valid refresh token.
        /// </summary>
        /// <param name="model">The model containing the refresh token.</param>
        /// <returns>
        /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation.
        /// </returns>
        /// <response code="200">Token refreshed successfully, returns the new token and refresh token.</response>
        /// <response code="400">Bad request if the model is invalid.</response>
        /// <response code="401">Unauthorized if the refresh token is invalid.</response>
        /// <response code="500">Internal server error if an unexpected error occurs.</response>
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _authService.RefreshTokenAsync(model, ModelState);

                if (result.Succeeded)
                {
                    return Ok(new
                    {
                        result.NewToken,
                        result.NewRefreshToken
                    });
                }

                return Unauthorized(new { Message = "Invalid email or password" });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An unexpected error occurred: {ex.Message}" });
            }
        }

        /// <summary>
        /// Revokes the specified refresh token, preventing its further use.
        /// </summary>
        /// <param name="model">The model containing the token to revoke.</param>
        /// <returns>
        /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation.
        /// </returns>
        /// <response code="200">Token revoked successfully.</response>
        /// <response code="400">Bad request if the model is invalid or token is not provided.</response>
        /// <response code="401">Unauthorized if the user is not authenticated.</response>
        /// <response code="500">Internal server error if an unexpected error occurs.</response>
        [HttpPost("revoke-token")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new { message = "Token is required" });
            }

            var result = await _authService.RevokeTokenAsync(token);

            return result.Succeeded 
                         ? Ok(new { message = "Token revoked" }) 
                         : BadRequest(new { message = "Failed to revoke token" });
        }

        /// <summary>
        /// Enables two-factor authentication for the authenticated user.
        /// </summary>
        /// <returns>
        /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation.
        /// </returns>
        /// <response code="200">Two-factor authentication enabled successfully.</response>
        /// <response code="401">Unauthorized if the user is not authenticated.</response>
        /// <response code="400">Bad request if enabling fails, with specific error messages.</response>
        /// <response code="500">Internal server error if an unexpected error occurs.</response>
        [HttpPost("enable-2fa")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> EnableTwoFactorAuthentication()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return Unauthorized(new { message = "User not found" });
                }

                var result = await _userDataService.EnableTwoFactorAuthenticationAsync(userId);
                if (result.Succeeded)
                {
                    return Ok(new { message = "Two-factor authentication enabled successfully" });
                }

                return BadRequest(new { message = "Unable to enable two-factor authentication", errors = result.Errors });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Disables two-factor authentication for the authenticated user.
        /// </summary>
        /// <returns>
        /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation.
        /// </returns>
        /// <response code="200">Two-factor authentication disabled successfully.</response>
        /// <response code="401">Unauthorized if the user is not authenticated.</response>
        /// <response code="400">Bad request if disabling fails, with specific error messages.</response>
        /// <response code="500">Internal server error if an unexpected error occurs.</response>
        [HttpPost("disable-2fa")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DisableTwoFactorAuthentication()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return Unauthorized(new { message = "User not found" });
                }

                var result = await _userDataService.DisableTwoFactorAuthenticationAsync(userId);
                if (result.Succeeded)
                {
                    return Ok(new { message = "Two-factor authentication disabled successfully" });
                }

                return BadRequest(new { message = "Unable to disable two-factor authentication", errors = result.Errors });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
