using E_commerceOnlineStore.Models;
using E_commerceOnlineStore.Models.Account;
using E_commerceOnlineStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_commerceOnlineStore.Controllers
{
    /// <summary>
    /// Provides endpoints for authentication-related operations such as user login and token generation.
    /// </summary>
    /// <remarks>
    /// The <see cref="AuthController"/> class handles authentication requests from clients, including login and token management.
    /// It depends on <see cref="IUserService"/> for user management operations and <see cref="ITokenService"/> for JWT token generation and <see cref="IEmailService"/> for email operations.
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IUserService userService, ITokenService tokenService, IEmailService emailService) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IEmailService _emailService = emailService;

        /// <summary>
        /// Registers a new user with the provided registration details.
        /// </summary>
        /// <param name="model">An instance of <see cref="RegisterModel"/> containing the registration details, including username, email, password, and optional profile information.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the outcome of the registration attempt.</returns>
        /// <response code="200">User registered successfully.</response>
        /// <response code="400">Bad request if the registration model is null or contains invalid data.</response>
        /// <response code="422">Unprocessable Entity if user creation fails due to validation errors.</response>
        /// <response code="500">Internal server error if an unexpected error occurs.</response>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            // Check if the registration model is null
            if (model == null)
            {
                return BadRequest(new { message = "Registration model cannot be null" });
            }

            try
            {
                // Attempt to create a new user with the provided registration details
                await _userService.CreateUserAsync(
                    model.UserName,
                    model.Email,
                    model.Password,
                    model.FirstName,
                    model.LastName,
                    model.DateOfBirth,
                    model.Gender,
                    model.ProfilePictureUrl,
                    model.RoleName
                );

                // Return a success response if user creation is successful
                return Ok("User registered successfully");
            }
            catch (ArgumentNullException ex)
            {
                // Log the error (ex.Message) and return a bad request with the error message
                return BadRequest(new { message = ex.Message });
            }
            catch
            {
                // Log the error (ex.Message) and return an internal server error
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }

        /// <summary>
        /// Authenticates a user and generates a JWT token if the credentials are valid.
        /// </summary>
        /// <param name="model">An instance of <see cref="LoginModel"/> containing the user's username and password.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the outcome of the login attempt.</returns>
        /// <response code="200">Login successful. Returns a JWT token.</response>
        /// <response code="400">Bad request if the login model is null or contains invalid data.</response>
        /// <response code="401">Unauthorized if the username or password is invalid.</response>
        /// <response code="500">Internal server error if an unexpected error occurs.</response>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                // Check if the login model is null
                if (model == null)
                {
                    return BadRequest(new { message = "Login model cannot be null" });
                }

                // Retrieve the user based on the provided username
                var user = await _userService.GetUserByNameAsync(model.UserName);
                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid username or password" });
                }

                // Verify the user's password
                if (await _userService.CheckPasswordAsync(user, model.Password))
                {
                    // Generate a JWT token if the password is correct
                    var token = await _tokenService.GenerateTokenAsync(user);
                    return Ok(new { Token = token });
                }
                else
                {
                    // Return unauthorized if the password is incorrect
                    return Unauthorized(new { message = "Invalid username or password" });
                }
            }
            catch (ArgumentNullException ex)
            {
                // Log the error (ex.Message) and return a bad request with the error message
                return BadRequest(new { message = ex.Message });
            }
            catch
            {
                // Log the error (ex.Message) and return an internal server error
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }

        /// <summary>
        /// Logs out the currently authenticated user by deleting the authentication token cookie.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> indicating the outcome of the logout operation.</returns>
        /// <response code="200">Logout successful.</response>
        /// <response code="500">Internal server error if an unexpected error occurs during the logout process.</response>
        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            try
            {
                // Delete the authentication token cookie
                Response.Cookies.Delete("authToken");

                // Send a successful response indicating the user has been logged out
                return Ok("Logged out successfully");
            }
            catch
            {
                // Log the error (ex.Message) and return an internal server error response
                return StatusCode(500, new { message = "An unexpected error occurred during logout" });
            }
        }

        /// <summary>
        /// Initiates the password reset process by sending a password reset link to the user's email.
        /// </summary>
        /// <param name="model">An instance of <see cref="ForgotPasswordModel"/> containing the user's email address.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the outcome of the password reset request.</returns>
        /// <response code="200">Password reset link sent successfully.</response>
        /// <response code="400">Bad request if the forgot password model is null.</response>
        /// <response code="404">Not Found if the email address is not associated with any user.</response>
        /// <response code="500">Internal server error if there are issues generating the token or the reset link.</response>
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
        {
            // Check if the forgot password model is null
            if (model == null)
            {
                return BadRequest(new { message = "Forgot password model cannot be null" });
            }

            try
            {
                // Retrieve the user by email
                var user = await _userService.GetUserByEmailAsync(model.Email);
                if (user == null)
                {
                    // Return a 404 Not Found response if the user with the provided email is not found
                    return NotFound(new { message = "Email not found" });
                }

                // Generate a password reset token
                var token = await _tokenService.GeneratePasswordResetTokenAsync(user);
                if (string.IsNullOrEmpty(token))
                {
                    // Return a 500 Internal Server Error if the token could not be generated
                    return StatusCode(500, new { message = "Failed to generate password reset token" });
                }

                // Generate a password reset link using the token
                var resetLink = Url.Action("ResetPassword", "Auth", new { token, email = model.Email }, Request.Scheme);
                if (string.IsNullOrEmpty(resetLink))
                {
                    // Return a 500 Internal Server Error if the password reset link could not be generated
                    return StatusCode(500, new { message = "Failed to generate password reset link" });
                }

                // Send the password reset email
                await _emailService.SendPasswordResetEmailAsync(model.Email, resetLink);

                // Return a success response indicating that the password reset link has been sent
                return Ok("Password reset link sent");
            }
            catch (ArgumentNullException ex)
            {
                // Return a 400 Bad Request with the detailed message in case of an argument error
                return BadRequest(new { message = ex.Message });
            }
            catch
            {
                // Log the error (ex.Message) and return a 500 Internal Server Error
                // Error logging should be implemented here
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }


        /// <summary>
        /// Resets the user's password using the provided email, reset token, and new password.
        /// </summary>
        /// <param name="model">An instance of <see cref="ResetPasswordModel"/> containing the user's email, password reset token, and new password.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the outcome of the password reset attempt.</returns>
        /// <response code="200">Password reset successful.</response>
        /// <response code="400">Bad request if the reset password model is null, the user is not found, or if there are validation errors during password reset.</response>
        /// <response code="500">Internal server error if an unexpected error occurs during the password reset process.</response>
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            // Check if the reset password model is null
            if (model == null)
            {
                return BadRequest(new { message = "Reset password model cannot be null" });
            }

            try
            {
                // Retrieve the user by email
                var user = await _userService.GetUserByEmailAsync(model.Email);
                if (user == null)
                {
                    // Return a 400 Bad Request if the user is not found
                    return BadRequest(new { message = "User not found" });
                }

                // Attempt to reset the user's password using the provided token and new password
                var result = await _userService.ResetPasswordAsync(user, model.Token, model.NewPassword);
                if (result.Succeeded)
                {
                    // Return a 200 OK response if the password reset is successful
                    return Ok(new { message = "Password reset successful" });
                }

                // Return a 400 Bad Request with the error messages if the password reset fails
                return BadRequest(new { message = string.Join("; ", result.Errors.Select(e => e.Description)) });
            }
            catch (ArgumentNullException ex)
            {
                // Return a 400 Bad Request with a detailed message for ArgumentNullException
                return BadRequest(new { message = ex.Message });
            }
            catch
            {
                // Log the exception (ex.Message) and return a 500 Internal Server Error response
                // Ensure proper logging is done here
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }

        /// <summary>
        /// Updates the profile information of the currently authenticated user.
        /// </summary>
        /// <param name="model">An instance of <see cref="UpdateProfileModel"/> containing the updated profile information.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the outcome of the profile update operation.</returns>
        /// <response code="200">Profile updated successfully.</response>
        /// <response code="400">Bad request if the update profile model is null or if the update fails due to validation errors.</response>
        /// <response code="401">Unauthorized if the user ID cannot be found in the claims.</response>
        /// <response code="500">Internal server error if an unexpected error occurs during the profile update process.</response>
        [HttpPut("update-profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileModel model)
        {
            // Check if the update profile model is null
            if (model == null)
            {
                return BadRequest(new { message = "Profile update model cannot be null" });
            }

            try
            {
                // Retrieve the user ID from the claims
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userId == null)
                {
                    // Return a 401 Unauthorized response if the user ID is not found
                    return Unauthorized(new { message = "User not found" });
                }

                // Update the user's profile with the provided model data
                var result = await _userService.UpdateUserProfileAsync(userId, model);

                if (result.Succeeded)
                {
                    // Return a 200 OK response if the profile update is successful
                    return Ok("Profile updated successfully");
                }

                // Return a 400 Bad Request with detailed error messages if the update fails
                return BadRequest(new { message = string.Join("; ", result.Errors.Select(e => e.Description)) });
            }
            catch (ArgumentNullException ex)
            {
                // Return a 400 Bad Request with a detailed message for ArgumentNullException
                return BadRequest(new { message = ex.Message });
            }
            catch
            {
                // Log the exception (ex.Message) and return a 500 Internal Server Error response
                // Ensure proper logging is done here
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }

        /// <summary>
        /// Assigns a specified role to a user. This operation is restricted to users with the "Admin" role.
        /// </summary>
        /// <param name="model">An instance of <see cref="AssignRoleModel"/> containing the user ID and the role name to be assigned.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the outcome of the role assignment operation.</returns>
        /// <response code="200">Role assigned successfully.</response>
        /// <response code="400">Bad request if the assign role model is null or if required fields (User ID or Role Name) are missing or empty, or if the role assignment fails.</response>
        /// <response code="500">Internal server error if an unexpected error occurs during the role assignment process.</response>
        [HttpPost("assign-role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleModel model)
        {
            // Check if the assign role model is null
            if (model == null)
            {
                return BadRequest(new { message = "Assign role model cannot be null" });
            }

            try
            {
                // Validate the input
                if (string.IsNullOrEmpty(model.UserId))
                {
                    return BadRequest(new { message = "User ID cannot be null or empty" });
                }

                if (string.IsNullOrEmpty(model.RoleName))
                {
                    return BadRequest(new { message = "Role name cannot be null or empty" });
                }

                // Assign the specified role to the user
                var result = await _userService.AssignRoleAsync(model.UserId, model.RoleName);

                if (result.Succeeded)
                {
                    // Return a 200 OK response if the role assignment is successful
                    return Ok("Role assigned successfully");
                }

                // Return a 400 Bad Request with detailed error messages if the role assignment fails
                return BadRequest(new { message = string.Join("; ", result.Errors.Select(e => e.Description)) });
            }
            catch (ArgumentNullException ex)
            {
                // Return a 400 Bad Request with a detailed message for ArgumentNullException
                return BadRequest(new { message = ex.Message });
            }
            catch
            {
                // Log the exception (ex.Message) and return a 500 Internal Server Error response
                // Ensure proper logging is done here
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }

        /// <summary>
        /// Removes a specified role from a user.
        /// </summary>
        /// <param name="model">An instance of <see cref="AssignRoleModel"/> containing the user ID and the role name to be removed.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the outcome of the role removal operation.</returns>
        /// <response code="200">Role successfully removed from the user.</response>
        /// <response code="400">Bad request if the remove role model is null or if required fields (User ID or Role Name) are missing or empty, or if the role removal fails due to validation errors.</response>
        /// <response code="404">Not Found if the user or role cannot be found based on the provided information.</response>
        /// <response code="500">Internal server error if an unexpected error occurs during the role removal process.</response>
        [HttpPost("remove-role")]
        public async Task<IActionResult> RemoveRole([FromBody] AssignRoleModel model)
        {
            // Check if the remove role model is null
            if (model == null)
            {
                return BadRequest(new { message = "Assign role model cannot be null" });
            }

            // Validate the input
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
                // Call the service method to remove the role from the user
                var result = await _userService.RemoveRoleAsync(model.UserId, model.RoleName);

                // Check if the role removal was successful
                if (result.Succeeded)
                {
                    // Return a 200 OK response if the role removal is successful
                    return Ok("Role successfully removed from the user.");
                }

                // Return a 400 Bad Request with detailed error messages if the removal fails
                return BadRequest(new { message = string.Join("; ", result.Errors.Select(e => e.Description)) });
            }
            catch (ArgumentNullException ex)
            {
                // Return a 400 Bad Request with a detailed message for ArgumentNullException
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Return a 404 Not Found if the exception message indicates that the user or role was not found
                if (ex.Message.Contains("User not found") || ex.Message.Contains("Role not found"))
                {
                    return NotFound(new { message = ex.Message });
                }

                // Log the exception (logging should be implemented in real scenarios)
                // Return a 500 Internal Server Error with a generic message
                return StatusCode(500, new { message = $"An error occurred while removing the role: {ex.Message}" });
            }
        }

        /// <summary>
        /// Enables two-factor authentication (2FA) for the currently authenticated user.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> indicating the outcome of the operation.</returns>
        /// <response code="200">Two-factor authentication enabled successfully.</response>
        /// <response code="400">Unable to enable two-factor authentication due to an error.</response>
        /// <response code="401">User not authenticated or user ID not found.</response>
        [HttpPost("enable-2fa")]
        public async Task<IActionResult> EnableTwoFactorAuthentication()
        {
            try
            {
                // Retrieve the user ID from the claims
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    // If the user ID is not found, return an unauthorized response
                    return Unauthorized(new { message = "User not found" });
                }

                // Attempt to enable two-factor authentication for the user
                var result = await _userService.EnableTwoFactorAuthenticationAsync(userId);
                if (result.Succeeded)
                {
                    return Ok(new { message = "Two-factor authentication enabled successfully" });
                }

                // If enabling 2FA fails, return a bad request with the error messages
                return BadRequest(new { message = "Unable to enable two-factor authentication", errors = result.Errors });
            }
            catch (Exception ex)
            {
                // Handle any exceptions by returning a bad request
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Disables two-factor authentication (2FA) for the currently authenticated user.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> indicating the outcome of the operation.</returns>
        /// <response code="200">Two-factor authentication disabled successfully.</response>
        /// <response code="400">Unable to disable two-factor authentication due to an error.</response>
        /// <response code="401">User not authenticated or user ID not found.</response>
        [HttpPost("disable-2fa")]
        public async Task<IActionResult> DisableTwoFactorAuthentication()
        {
            try
            {
                // Retrieve the user ID from the claims
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    // If the user ID is not found, return an unauthorized response
                    return Unauthorized(new { message = "User not found" });
                }

                // Attempt to disable two-factor authentication for the user
                var result = await _userService.DisableTwoFactorAuthenticationAsync(userId);
                if (result.Succeeded)
                {
                    return Ok(new { message = "Two-factor authentication disabled successfully" });
                }

                // If disabling 2FA fails, return a bad request with the error messages
                return BadRequest(new { message = "Unable to disable two-factor authentication", errors = result.Errors });
            }
            catch (Exception ex)
            {
                // Handle any exceptions by returning a bad request
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
