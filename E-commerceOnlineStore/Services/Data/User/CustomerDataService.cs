using E_commerceOnlineStore.Controllers;
using E_commerceOnlineStore.Controllers.Account;
using E_commerceOnlineStore.Data;
using E_commerceOnlineStore.Enums.UserManagement;
using E_commerceOnlineStore.Models.DataModels.UserManagement;
using E_commerceOnlineStore.Models.RequestModels.Account;
using E_commerceOnlineStore.Services.Business.Account;
using E_commerceOnlineStore.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace E_commerceOnlineStore.Services.Data.User
{
    /// <summary>
    /// Service class for managing customer-related operations.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CustomerDataService"/> class.
    /// </remarks>
    /// <param name="context">The application database context.</param>
    /// <param name="userManager">The user manager for handling user operations.</param>
    /// <param name="logger">The logger for logging operations.</param>
    /// <param name="rolesService">The service for managing user roles.</param>
    /// <param name="userDataService">The service for handling user data operations.</param>
    public class CustomerDataService(ApplicationDbContext context,
                               UserManager<Customer> userManager,
                               ILogger<RegistrationService> logger,
                               IRolesService rolesService,
                               IUserDataService userDataService) : ICustomerDataService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly UserManager<Customer> _userManager = userManager;
        private readonly ILogger<RegistrationService> _logger = logger;
        private readonly IRolesService _roleService = rolesService;
        private readonly IUserDataService _userDataService = userDataService;

        /// <summary>
        /// Adds a new customer asynchronously using the provided registration model.
        /// </summary>
        /// <param name="model">An instance of <see cref="CustomerRegistrationModel"/> containing the customer's registration information.</param>
        /// <returns>An <see cref="OperationResult{Customer}"/> object containing the result of the operation.</returns>
        public async Task<OperationResult<Customer>> AddCustomerAsync(CustomerRegistrationModel model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                return OperationResult<Customer>.FailureResult(["Email and password are required."]);
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var customer = new Customer
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        DateOfBirth = model.DateOfBirth,
                        Gender = model.Gender,
                        PhoneNumber = model.PhoneNumber,
                        ProfilePictureUrl = model.ProfilePictureUrl,
                        IsBlocked = false,
                        CreatedAt = DateTime.UtcNow,
                        PreferredLanguage = model.PreferredLanguage,
                        TimeZone = model.TimeZone,
                    };

                    var result = await _userManager.CreateAsync(customer, model.Password);

                    if (!result.Succeeded)
                    {
                        return OperationResult<Customer>.FailureResult(result.Errors.Select(e => e.Description).ToList());
                    }

                    string userRole = UserRole.Customer.ToString();

                    var roleResult = await _roleService.AssignRoleAsync(customer.Id, userRole);

                    if (!roleResult.Succeeded)
                    {
                        return OperationResult<Customer>.FailureResult(roleResult.Errors.Select(e => e.Description).ToList());
                    }

                    await transaction.CommitAsync();

                    return OperationResult<Customer>.SuccessResult(customer);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Failed to add customer");
                    return OperationResult<Customer>.FailureResult(["Failed to add customer due to an unexpected error."]);
                }
            }
        }

        /// <summary>
        /// Deletes a customer asynchronously using their unique identifier.
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer to delete.</param>
        /// <returns>An <see cref="OperationResult{Customer}"/> object containing the result of the operation.</returns>
        public async Task<OperationResult<Customer>> DeleteCustomerAsync(string customerId)
        {
            var customerResult = await GetCustomerByIdAsync(customerId);

            if (!customerResult.Succeeded || customerResult.Data == null)
            {
                return OperationResult<Customer>.FailureResult(["Customer not found."]);
            }

            _context.Customers.Remove(customerResult.Data);
            var result = await _context.SaveChangesAsync() > 0;

            return result ? OperationResult<Customer>.SuccessResult(customerResult.Data) :
                            OperationResult<Customer>.FailureResult(["Failed to delete customer."]);
        }

        /// <summary>
        /// Retrieves a customer's details asynchronously using their unique identifier.
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer to retrieve.</param>
        /// <returns>An <see cref="OperationResult{Customer}"/> object containing the result of the operation.</returns>
        public async Task<OperationResult<Customer>> GetCustomerByIdAsync(string customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);

            return customer != null
                ? OperationResult<Customer>.SuccessResult(customer)
                : OperationResult<Customer>.FailureResult(["Customer not found."]);
        }

        /// <summary>
        /// Retrieves a customer's details asynchronously using their email address.
        /// </summary>
        /// <param name="email">The email address of the customer to retrieve.</param>
        /// <returns>An <see cref="OperationResult{Customer}"/> object containing the result of the operation.</returns>
        public async Task<OperationResult<Customer>> GetCustomerByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return OperationResult<Customer>.FailureResult([new ArgumentNullException(nameof(email)).Message]);
            }

            var existingCustomer = await _userManager.FindByEmailAsync(email);

            return existingCustomer == null ?
                            OperationResult<Customer>.FailureResult([$"Customer with an email - {email} did not find in the database."]) :
                            OperationResult<Customer>.SuccessResult(existingCustomer);
        }

        /// <summary>
        /// Retrieves all customers asynchronously.
        /// </summary>
        /// <returns>An <see cref="OperationResult{List{Customer}}"/> object containing the list of all customers.</returns>
        public async Task<OperationResult<List<Customer>>> GetAllCustomersAsync()
        {
            var customers = await _context.Customers.ToListAsync();
            return OperationResult<List<Customer>>.SuccessResult(customers);
        }

        /// <summary>
        /// Checks if a customer email address already exists asynchronously.
        /// </summary>
        /// <param name="email">The email address to check for existence.</param>
        /// <returns>A task that represents the asynchronous operation, returning a boolean indicating if the email exists.</returns>
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Customers.AnyAsync(c => c.Email != null && c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Updates a customer's profile asynchronously using the provided model.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose profile is to be updated.</param>
        /// <param name="model">An instance of <see cref="UpdateProfileModel"/> containing the updated profile information.</param>
        /// <returns>An <see cref="OperationResult{Customer}"/> object containing the result of the operation.</returns>
        public async Task<OperationResult<Customer>> UpdateProfileAsync(string userId, UpdateProfileModel model)
        {
            var result = await _userDataService.UpdateProfileAsync(userId, model);

            if (!result.Succeeded || result.Data == null)
            {
                return OperationResult<Customer>.FailureResult(result.Errors);
            }
            
            return await GetCustomerByIdAsync(result.Data.Id);
        }

        /// <summary>
        /// Changes a customer's password asynchronously using the provided model.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose password is to be changed.</param>
        /// <param name="model">An instance of <see cref="ChangePasswordModel"/> containing the new password information.</param>
        /// <returns>An <see cref="OperationResult{Customer}"/> object containing the result of the operation.</returns>
        public async Task<OperationResult<Customer>> ChangePasswordAsync(string userId, ChangePasswordModel model)
        {
            var result = await _userDataService.ChangePasswordAsync(userId, model);

            if (!result.Succeeded || result.Data == null)
            {
                return OperationResult<Customer>.FailureResult(result.Errors);
            }

            return await GetCustomerByIdAsync(result.Data.Id);
        }

        /// <summary>
        /// Changes a customer's phone number asynchronously using the provided model.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose phone number is to be changed.</param>
        /// <param name="model">An instance of <see cref="ChangePhoneModel"/> containing the new phone number information.</param>
        /// <returns>An <see cref="OperationResult{Customer}"/> object containing the result of the operation.</returns>
        public async Task<OperationResult<Customer>> ChangePhoneAsync(string userId, ChangePhoneModel model)
        {
            var result = await _userDataService.ChangePhoneAsync(userId, model);

            if (!result.Succeeded || result.Data == null)
            {
                return OperationResult<Customer>.FailureResult(result.Errors);
            }

            return await GetCustomerByIdAsync(result.Data.Id);
        }

        /// <summary>
        /// Updates a customer's user settings asynchronously using the provided model.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose settings are to be updated.</param>
        /// <param name="model">An instance of <see cref="UpdateSettingsModel"/> containing the updated settings information.</param>
        /// <returns>An <see cref="OperationResult{Customer}"/> object containing the result of the operation.</returns>
        public async Task<OperationResult<Customer>> UpdateUserSettingsAsync(string userId, UpdateSettingsModel model)
        {
            var result = await _userDataService.UpdateUserSettingsAsync(userId, model);

            if (!result.Succeeded || result.Data == null)
            {
                return OperationResult<Customer>.FailureResult(result.Errors);
            }

            return await GetCustomerByIdAsync(result.Data.Id);
        }

        /// <summary>
        /// Changes a customer's email address asynchronously using the provided model.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose email is to be changed.</param>
        /// <param name="model">An instance of <see cref="ChangeEmailModel"/> containing the new email information.</param>
        /// <returns>An <see cref="OperationResult{Customer}"/> object containing the result of the operation.</returns>
        public async Task<OperationResult<Customer>> ChangeEmailAsync(string userId, ChangeEmailModel model)
        {
            var result = await _userDataService.ChangeEmailAsync(userId, model);

            if (!result.Succeeded || result.Data == null)
            {
                return OperationResult<Customer>.FailureResult(result.Errors);
            }

            return await GetCustomerByIdAsync(result.Data.Id);
        }
    }
}
