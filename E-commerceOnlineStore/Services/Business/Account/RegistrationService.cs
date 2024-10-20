using E_commerceOnlineStore.Models.DataModels.UserManagement;
using E_commerceOnlineStore.Models.RequestModels.Account;
using E_commerceOnlineStore.Services.Business.Notifications;
using E_commerceOnlineStore.Services.Business.Security;
using E_commerceOnlineStore.Services.Data.User;
using E_commerceOnlineStore.Utilities;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace E_commerceOnlineStore.Services.Business.Account
{
    /// <summary>
    /// Service for handling user registration processes, including customers and employees.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="RegistrationService"/> class.
    /// </remarks>
    /// <param name="customerDataService">Service for managing customer data.</param>
    /// <param name="employeeDataService">Service for managing employee data.</param>
    /// <param name="emailConfirmationService">Service for handling email confirmations.</param>
    /// <param name="tokenService">Service for generating tokens.</param>
    /// <param name="logger">Logger for logging events and errors.</param>
    public class RegistrationService(ICustomerDataService customerDataService,
                                    IEmployeeDataService employeeDataService,
                                    IEmailConfirmationService emailConfirmationService,
                                    ITokenService tokenService,
                                    ILogger<RegistrationService> logger) : IRegistrationService
    {
        private readonly ICustomerDataService _customerDataService = customerDataService;
        private readonly IEmployeeDataService _employeeDataService = employeeDataService;
        private readonly IEmailConfirmationService _emailConfirmationService = emailConfirmationService;
        private readonly ITokenService _tokenService = tokenService;
        private readonly ILogger<RegistrationService> _logger = logger;

        /// <summary>
        /// Registers a new customer in the system.
        /// </summary>
        /// <param name="model">The customer registration model containing the user's information.</param>
        /// <param name="modelState">The model state used for validating the input data.</param>
        /// <param name="baseUrl">The base URL for creating callback links (e.g., for email confirmation).</param>
        /// <param name="scheme">The URL scheme (e.g., HTTP or HTTPS) used in creating links.</param>
        /// <returns>An OperationResult containing either the newly registered customer or validation errors.</returns>
        public async Task<OperationResult<ApplicationUser>> RegisterCustomerAsync(CustomerRegistrationModel model, 
                                                                            ModelStateDictionary modelState, 
                                                                            string baseUrl, 
                                                                            string scheme)
        {
            if (!modelState.IsValid)
            {
                var errors = new List<string>();
                foreach (var state in modelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                return OperationResult<ApplicationUser>.FailureResult(errors);
            }

            if (await _customerDataService.EmailExistsAsync(model.Email))
            {
                return OperationResult<ApplicationUser>.FailureResult(["Email already exists."]);
            }

            var result = await _customerDataService.AddCustomerAsync(model);

            if(!result.Succeeded)
            {
                return result;
            }

            if(result.Data == null || string.IsNullOrEmpty(result.Data.Email))
            {
                return OperationResult<ApplicationUser>.FailureResult(["Failed customer creation and data transfer"]);
            }

            var token = await _tokenService.GenerateEmailConfirmationTokenAsync(result.Data);

            if (string.IsNullOrEmpty(token))
            {
                return OperationResult<ApplicationUser>.FailureResult(["Failed create email confirmation token"]);
            }

            var confirmationLinkCreationResult = _emailConfirmationService.GenerateConfirmationLink(result.Data.Id, token, baseUrl, scheme);

            if (!confirmationLinkCreationResult.Succeeded || confirmationLinkCreationResult.Data == null)
            {
                return OperationResult<ApplicationUser>.FailureResult(confirmationLinkCreationResult.Errors);
            }

            try
            {
                await _emailConfirmationService.SendEmailConfirmationEmailAsync(result.Data.Email, confirmationLinkCreationResult.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send confirmation email to {result.Data.Email} for customer ID {result.Data.Id}.");
            }

            return result;
        }

        /// <summary>
        /// Registers a new employee in the system.
        /// </summary>
        /// <param name="model">The employee registration model containing the employee's information.</param>
        /// <param name="modelState">The model state used for validating the input data.</param>
        /// <param name="baseUrl">The base URL for creating callback links (e.g., for email confirmation).</param>
        /// <param name="scheme">The URL scheme (e.g., HTTP or HTTPS) used in creating links.</param>
        /// <returns>An OperationResult containing either the newly registered employee or validation errors.</returns>
        public async Task<OperationResult<ApplicationUser>> RegisterEmployeeAsync(EmployeeRegistrationModel model, 
                                                                            ModelStateDictionary modelState,
                                                                            string baseUrl,
                                                                            string scheme)
        {
            if (!modelState.IsValid)
            {
                var errors = new List<string>();
                foreach (var state in modelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                return OperationResult<ApplicationUser>.FailureResult(errors);
            }

            if (await _employeeDataService.EmailExistsAsync(model.Email))
            {
                return OperationResult<ApplicationUser>.FailureResult(["Email already exists."]);
            }

            var result = await _employeeDataService.AddEmployeeAsync(model);

            if (!result.Succeeded)
            {
                return result;
            }

            if (result.Data == null || string.IsNullOrEmpty(result.Data.Email))
            {
                return OperationResult<ApplicationUser>.FailureResult(["Failed employee creation and data transfer"]);
            }

            var token = await _tokenService.GenerateEmailConfirmationTokenAsync(result.Data);

            if (string.IsNullOrEmpty(token))
            {
                return OperationResult<ApplicationUser>.FailureResult(["Failed create email confirmation token"]);
            }

            var confirmationLinkCreationResult = _emailConfirmationService.GenerateConfirmationLink(result.Data.Id, token, baseUrl, scheme);

            if (!confirmationLinkCreationResult.Succeeded || confirmationLinkCreationResult.Data == null)
            {
                return OperationResult<ApplicationUser>.FailureResult(confirmationLinkCreationResult.Errors);
            }

            try
            {
                await _emailConfirmationService.SendEmailConfirmationEmailAsync(result.Data.Email, confirmationLinkCreationResult.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send confirmation email to {result.Data.Email} " +
                    $"for employee ID {result.Data.Id}.");
            }

            return result;
        }
    }
}
