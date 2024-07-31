using E_commerceOnlineStore.Models;
using E_commerceOnlineStore.Models.Account;
using E_commerceOnlineStore.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceOnlineStore.Controllers
{
    /// <summary>
    /// Provides endpoints for authentication-related operations such as user login and token generation.
    /// </summary>
    /// <remarks>
    /// The <see cref="AuthController"/> class handles authentication requests from clients, including login and token management.
    /// It depends on <see cref="IUserService"/> for user management operations and <see cref="ITokenService"/> for JWT token generation.
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IUserService userService, ITokenService tokenService) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly ITokenService _tokenService = tokenService;

        /// <summary>
        /// Registers a new user and assigns a role.
        /// </summary>
        /// <param name="model">The registration model.</param>
        /// <returns>Action result indicating the outcome of the operation.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
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

                return Ok("User registered successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Authenticates a user and returns a JWT token.
        /// </summary>
        /// <param name="model">The login model.</param>
        /// <returns>Action result with JWT token or an error message.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userService.GetUserByNameAsync(model.UserName);

            if (user != null && await _userService.CheckPasswordAsync(user, model.Password))
            {
                var token = await _tokenService.GenerateTokenAsync(user);
                return Ok(new { Token = token });
            }

            return Unauthorized(new { message = "Invalid username or password" });
        }
    }
}
