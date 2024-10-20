using E_commerceOnlineStore.Models.DataModels.UserManagement;
using E_commerceOnlineStore.Models.RequestModels.Account;
using E_commerceOnlineStore.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace E_commerceOnlineStore.Services.Business.Account
{
    /// <summary>
    /// Interface for handling user registration processes for customers and employees.
    /// </summary>
    public interface IRegistrationService
    {
        /// <summary>
        /// Registers a new customer in the system.
        /// </summary>
        /// <param name="model">The customer registration model containing the user's information.</param>
        /// <param name="modelState">The model state used for validating the input data.</param>
        /// <param name="baseUrl">The base URL for creating callback links (e.g., for email confirmation).</param>
        /// <param name="scheme">The URL scheme (e.g., HTTP or HTTPS) used in creating links.</param>
        /// <returns>An OperationResult containing either the newly registered customer or validation errors.</returns>
        Task<OperationResult<ApplicationUser>> RegisterCustomerAsync(CustomerRegistrationModel model, ModelStateDictionary modelState, string baseUrl, string scheme);

        /// <summary>
        /// Registers a new employee in the system.
        /// </summary>
        /// <param name="model">The employee registration model containing the employee's information.</param>
        /// <param name="modelState">The model state used for validating the input data.</param>
        /// <param name="baseUrl">The base URL for creating callback links (e.g., for email confirmation).</param>
        /// <param name="scheme">The URL scheme (e.g., HTTP or HTTPS) used in creating links.</param>
        /// <returns>An OperationResult containing either the newly registered employee or validation errors.</returns>
        Task<OperationResult<ApplicationUser>> RegisterEmployeeAsync(EmployeeRegistrationModel model, ModelStateDictionary modelState, string baseUrl, string scheme);
    }
}
