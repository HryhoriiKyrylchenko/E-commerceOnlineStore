using E_commerceOnlineStore.Models.DataModels.UserManagement;
using E_commerceOnlineStore.Models.RequestModels.Account;
using E_commerceOnlineStore.Services.Business.Account;
using E_commerceOnlineStore.Services.Business.Notifications;
using E_commerceOnlineStore.Services.Business.Security;
using E_commerceOnlineStore.Services.Data.User;
using E_commerceOnlineStore.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceOnlineStore.Controllers.Account
{
    /// <summary>
    /// Handles email confirmation operations, including resending confirmation emails and confirming user emails.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="EmailConfirmationController"/> class.
    /// </remarks>
    /// <param name="userDataService">Service for managing user data.</param>
    /// <param name="tokenService">Service for generating and validating tokens.</param>
    /// <param name="emailConfirmationService">Service for email confirmation operations.</param>
    /// <param name="logger">Logger for recording events and errors.</param>
    [Route("api/[controller]")]
    [ApiController]
    public class EmailConfirmationController(IUserDataService userDataService,
                                        ITokenService tokenService,
                                        IEmailConfirmationService emailConfirmationService,
                                        ILogger<EmailConfirmationController> logger) : ControllerBase
    {
        private readonly IUserDataService _userDataService = userDataService;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IEmailConfirmationService _emailConfirmationService = emailConfirmationService;
        private readonly ILogger<EmailConfirmationController> _logger = logger;

        /// <summary>
        /// Resends the email confirmation link to the user.
        /// </summary>
        /// <param name="model">The model containing the user ID.</param>
        /// <returns>
        /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation.
        /// </returns>
        /// <response code="200">Confirmation email resent successfully.</response>
        /// <response code="400">Bad request if the model is invalid or if the user is not found.</response>
        /// <response code="500">An internal server error occurred while sending the confirmation email.</response>
        [HttpPost("resend-confirmation-email")]
        public async Task<IActionResult> ResendConfirmationEmail([FromBody] ResendConfirmationEmailModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Customer ID cannot be null or empty" });
            }

            var userResult = await _userDataService.GetUserByIdAsync(model.UserId);

            if (!userResult.Succeeded || userResult.Data == null)
            {
                return BadRequest($"Couldn't find customer by id: {model.UserId}");
            }

            var token = await _tokenService.GenerateEmailConfirmationTokenAsync(userResult.Data);

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Failed create email confirmation token");
            }

            var confirmationLinkCreationResult = _emailConfirmationService.GenerateConfirmationLink(userResult.Data.Id, token);

            if (!confirmationLinkCreationResult.Succeeded || confirmationLinkCreationResult.Data == null)
            {
                return BadRequest(confirmationLinkCreationResult.Errors);
            }

            try
            {
                if (userResult.Data.Email == null)
                {
                    throw new ArgumentNullException(nameof(userResult.Data.Email));
                }

                await _emailConfirmationService.SendEmailConfirmationEmailAsync(userResult.Data.Email, 
                                                                    confirmationLinkCreationResult.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send confirmation email to {userResult.Data.Email} " +
                    $"for customer ID {userResult.Data.Id}.");
            }

            return Ok("Confirmation email resent successfully.");
        }

        /// <summary>
        /// Confirms the user's email address using the provided token.
        /// </summary>
        /// <param name="model">The model containing the user ID and token.</param>
        /// <returns>
        /// A <see cref="Task{IActionResult}"/> representing the asynchronous operation.
        /// </returns>
        /// <response code="200">Email confirmed successfully.</response>
        /// <response code="400">Bad request if the model is invalid or if required fields are missing.</response>
        /// <response code="404">Not found if the user does not exist.</response>
        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailModel model)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid client request" });
            }

            if (string.IsNullOrEmpty(model.UserId) || string.IsNullOrEmpty(model.Token))
            {
                return BadRequest("User ID and token are required.");
            }

            var userResult = await _userDataService.GetUserByIdAsync(model.UserId);

            if (!userResult.Succeeded || userResult.Data == null)
            {
                return NotFound("User not found.");
            }

            var decodedToken = TokenEncoder.DecodeToken(model.Token);

            var result = await _userDataService.ConfirmEmailAsync(userResult.Data, decodedToken);

            if (result.Succeeded)
            {
                return Ok("Email confirmed successfully.");
            }

            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            return BadRequest($"Error confirming email: {errors}");
        }
    }
}
