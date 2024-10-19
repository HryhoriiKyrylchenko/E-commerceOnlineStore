using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_commerceOnlineStore.Controllers.Account
{
    /// <summary>
    /// Controller for handling external authentication operations, such as Google login.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalAuthenticationController : ControllerBase
    {
        /// <summary>
        /// Initiates the Google login process.
        /// </summary>
        /// <returns>An IActionResult that challenges the user to authenticate using Google.</returns>
        /// <response code="302">Redirects to the Google login page.</response>
        [HttpGet("google")]
        public IActionResult GoogleLogin()
        {
            var redirectUrl = Url.Action("GoogleResponse", "ExternalAuthentication");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// Handles the response from Google after authentication.
        /// </summary>
        /// <returns>An IActionResult indicating the result of the authentication process.</returns>
        /// <response code="200">Authentication successful with user email returned.</response>
        /// <response code="400">Bad request if there was an authentication error.</response>
        [HttpGet("google/response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            if (result.Succeeded)
            {
                var userEmail = result.Principal.FindFirst(ClaimTypes.Email)?.Value;

                return Ok(new { Message = "Authentication successful!", Email = userEmail });
            }

            return BadRequest("Google authentication error.");
        }
    }
}
