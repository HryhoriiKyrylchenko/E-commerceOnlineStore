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
        /// Registers a new customer asynchronously.
        /// </summary>
        /// <param name="model">The model containing customer registration data.</param>
        /// <param name="modelState">The state of the model validation.</param>
        /// <returns>
        /// An <see cref="OperationResult{Customer}"/> indicating the success or failure of the registration process.
        /// </returns>
        public async Task<OperationResult<Customer>> RegisterCustomerAsync(CustomerRegistrationModel model, 
                                                                            ModelStateDictionary modelState)
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
                return OperationResult<Customer>.FailureResult(errors);
            }

            if (await _customerDataService.EmailExistsAsync(model.Email))
            {
                return OperationResult<Customer>.FailureResult(["Email already exists."]);
            }

            var result = await _customerDataService.AddCustomerAsync(model);

            if(!result.Succeeded)
            {
                return result;
            }

            if(result.Data == null || string.IsNullOrEmpty(result.Data.Email))
            {
                return OperationResult<Customer>.FailureResult(["Failed customer creation and data transfer"]);
            }

            var token = await _tokenService.GenerateEmailConfirmationTokenAsync(result.Data);

            if (string.IsNullOrEmpty(token))
            {
                return OperationResult<Customer>.FailureResult(["Failed create email confirmation token"]);
            }

            var confirmationLinkCreationResult = _emailConfirmationService.GenerateConfirmationLink(result.Data.Id, token);

            if (!confirmationLinkCreationResult.Succeeded || confirmationLinkCreationResult.Data == null)
            {
                return OperationResult<Customer>.FailureResult(confirmationLinkCreationResult.Errors);
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
        /// Registers a new employee asynchronously.
        /// </summary>
        /// <param name="model">The model containing employee registration data.</param>
        /// <param name="modelState">The state of the model validation.</param>
        /// <returns>
        /// An <see cref="OperationResult{Employee}"/> indicating the success or failure of the registration process.
        /// </returns>
        public async Task<OperationResult<Employee>> RegisterEmployeeAsync(EmployeeRegistrationModel model, 
                                                                            ModelStateDictionary modelState)
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
                return OperationResult<Employee>.FailureResult(errors);
            }

            if (await _employeeDataService.EmailExistsAsync(model.Email))
            {
                return OperationResult<Employee>.FailureResult(["Email already exists."]);
            }

            var result = await _employeeDataService.AddEmployeeAsync(model);

            if (!result.Succeeded)
            {
                return result;
            }

            if (result.Data == null || string.IsNullOrEmpty(result.Data.Email))
            {
                return OperationResult<Employee>.FailureResult(["Failed employee creation and data transfer"]);
            }

            var token = await _tokenService.GenerateEmailConfirmationTokenAsync(result.Data);

            if (string.IsNullOrEmpty(token))
            {
                return OperationResult<Employee>.FailureResult(["Failed create email confirmation token"]);
            }

            var confirmationLinkCreationResult = _emailConfirmationService.GenerateConfirmationLink(result.Data.Id, token);

            if (!confirmationLinkCreationResult.Succeeded || confirmationLinkCreationResult.Data == null)
            {
                return OperationResult<Employee>.FailureResult(confirmationLinkCreationResult.Errors);
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
