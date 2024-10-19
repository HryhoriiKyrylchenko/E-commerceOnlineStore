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
        /// Registers a new customer using the provided registration model and validation state.
        /// </summary>
        /// <param name="model">The model containing customer registration data.</param>
        /// <param name="modelState">The model state dictionary to validate the model.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="OperationResult{Customer}"/> indicating the success or failure of the registration process.</returns>
        Task<OperationResult<Customer>> RegisterCustomerAsync(CustomerRegistrationModel model, ModelStateDictionary modelState);

        /// <summary>
        /// Registers a new employee using the provided registration model and validation state.
        /// </summary>
        /// <param name="model">The model containing employee registration data.</param>
        /// <param name="modelState">The model state dictionary to validate the model.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="OperationResult{Employee}"/> indicating the success or failure of the registration process.</returns>
        Task<OperationResult<Employee>> RegisterEmployeeAsync(EmployeeRegistrationModel model, ModelStateDictionary modelState);
    }
}
