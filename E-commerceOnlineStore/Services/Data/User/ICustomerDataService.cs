using E_commerceOnlineStore.Models.DataModels.UserManagement;
using E_commerceOnlineStore.Models.RequestModels.Account;
using E_commerceOnlineStore.Utilities;

namespace E_commerceOnlineStore.Services.Data.User
{
    /// <summary>
    /// Provides an interface for customer data operations, including 
    /// adding, deleting, retrieving, and updating customer information.
    /// </summary>
    public interface ICustomerDataService
    {
        /// <summary>
        /// Adds a new customer asynchronously using the provided registration model.
        /// </summary>
        /// <param name="model">An instance of <see cref="CustomerRegistrationModel"/> containing the customer's registration information.</param>
        /// <returns>An <see cref="OperationResult{ApplicationUser}"/> object containing the result of the operation.</returns>
        Task<OperationResult<ApplicationUser>> AddCustomerAsync(CustomerRegistrationModel model);

        /// <summary>
        /// Deletes a customer asynchronously using their unique identifier.
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer to delete.</param>
        /// <returns>An <see cref="OperationResult{ApplicationUser}"/> object containing the result of the operation.</returns>
        Task<OperationResult<ApplicationUser>> DeleteCustomerAsync(string customerId);

        /// <summary>
        /// Retrieves a customer's details asynchronously using their unique identifier.
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer to retrieve.</param>
        /// <returns>An <see cref="OperationResult{ApplicationUser}"/> object containing the result of the operation.</returns>
        Task<OperationResult<ApplicationUser>> GetCustomerByIdAsync(string customerId);

        /// <summary>
        /// Retrieves a customer's details asynchronously using their email address.
        /// </summary>
        /// <param name="email">The email address of the customer to retrieve.</param>
        /// <returns>An <see cref="OperationResult{ApplicationUser}"/> object containing the result of the operation.</returns>
        Task<OperationResult<ApplicationUser>> GetCustomerByEmailAsync(string email);

        /// <summary>
        /// Retrieves all customers asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation, containing a list of <see cref="ApplicationUser"/> objects.</returns>
        Task<OperationResult<List<ApplicationUser>>> GetAllCustomersAsync();

        /// <summary>
        /// Checks if a customer email address already exists asynchronously.
        /// </summary>
        /// <param name="email">The email address to check for existence.</param>
        /// <returns>A task that represents the asynchronous operation, returning a boolean indicating if the email exists.</returns>
        Task<bool> EmailExistsAsync(string email);

        /// <summary>
        /// Updates a customer's profile asynchronously using the provided model.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose profile is to be updated.</param>
        /// <param name="model">An instance of <see cref="UpdateProfileModel"/> containing the updated profile information.</param>
        /// <returns>An <see cref="OperationResult{ApplicationUser}"/> object containing the result of the operation.</returns>
        Task<OperationResult<ApplicationUser>> UpdateProfileAsync(string userId, UpdateProfileModel model);

        /// <summary>
        /// Changes a customer's password asynchronously using the provided model.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose password is to be changed.</param>
        /// <param name="model">An instance of <see cref="ChangePasswordModel"/> containing the new password information.</param>
        /// <returns>An <see cref="OperationResult{ApplicationUser}"/> object containing the result of the operation.</returns>
        Task<OperationResult<ApplicationUser>> ChangePasswordAsync(string userId, ChangePasswordModel model);

        /// <summary>
        /// Changes a customer's phone number asynchronously using the provided model.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose phone number is to be changed.</param>
        /// <param name="model">An instance of <see cref="ChangePhoneModel"/> containing the new phone number information.</param>
        /// <returns>An <see cref="OperationResult{ApplicationUser}"/> object containing the result of the operation.</returns>
        Task<OperationResult<ApplicationUser>> ChangePhoneAsync(string userId, ChangePhoneModel model);

        /// <summary>
        /// Updates a customer's user settings asynchronously using the provided model.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose settings are to be updated.</param>
        /// <param name="model">An instance of <see cref="UpdateSettingsModel"/> containing the updated settings information.</param>
        /// <returns>An <see cref="OperationResult{ApplicationUser}"/> object containing the result of the operation.</returns>
        Task<OperationResult<ApplicationUser>> UpdateUserSettingsAsync(string userId, UpdateSettingsModel model);

        /// <summary>
        /// Changes a customer's email address asynchronously using the provided model.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose email is to be changed.</param>
        /// <param name="model">An instance of <see cref="ChangeEmailModel"/> containing the new email information.</param>
        /// <returns>An <see cref="OperationResult{ApplicationUser}"/> object containing the result of the operation.</returns>
        Task<OperationResult<ApplicationUser>> ChangeEmailAsync(string userId, ChangeEmailModel model);
    }
}
