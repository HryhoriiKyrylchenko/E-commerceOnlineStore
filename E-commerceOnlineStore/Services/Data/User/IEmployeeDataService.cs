using E_commerceOnlineStore.Models.DataModels.UserManagement;
using E_commerceOnlineStore.Models.RequestModels.Account;
using E_commerceOnlineStore.Utilities;

namespace E_commerceOnlineStore.Services.Data.User
{
    /// <summary>
    /// Provides methods for employee management operations.
    /// </summary>
    public interface IEmployeeDataService
    {
        /// <summary>
        /// Adds a new employee using the specified registration model.
        /// </summary>
        /// <param name="model">The model containing the employee registration information.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the added employee or an error message.</returns>
        Task<OperationResult<ApplicationUser>> AddEmployeeAsync(EmployeeRegistrationModel model);

        /// <summary>
        /// Deletes an employee identified by their unique identifier.
        /// </summary>
        /// <param name="employeeId">The unique identifier of the employee to be deleted.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the deleted employee or an error message.</returns>
        Task<OperationResult<ApplicationUser>> DeleteEmployeeAsync(string employeeId);

        /// <summary>
        /// Retrieves an employee by their unique identifier.
        /// </summary>
        /// <param name="employeeId">The unique identifier of the employee.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the employee or an error message.</returns>
        Task<OperationResult<ApplicationUser>> GetEmployeeByIdAsync(string employeeId);

        /// <summary>
        /// Retrieves an employee by their email address.
        /// </summary>
        /// <param name="email">The email address of the employee.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the employee or an error message.</returns>
        Task<OperationResult<ApplicationUser>> GetEmployeeByEmailAsync(string email);

        /// <summary>
        /// Retrieves a list of all employees.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the list of employees or an error message.</returns>
        Task<OperationResult<List<ApplicationUser>>> GetAllEmployeesAsync();

        /// <summary>
        /// Checks if an email address already exists in the system.
        /// </summary>
        /// <param name="email">The email address to check.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the email exists.</returns>
        Task<bool> EmailExistsAsync(string email);

        /// <summary>
        /// Updates the profile information of an employee identified by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the employee.</param>
        /// <param name="model">The model containing the updated profile information.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the updated employee or an error message.</returns>
        Task<OperationResult<ApplicationUser>> UpdateProfileAsync(string userId, UpdateProfileModel model);

        /// <summary>
        /// Changes the password of an employee identified by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the employee.</param>
        /// <param name="model">The model containing the current and new password information.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the updated employee or an error message.</returns>
        Task<OperationResult<ApplicationUser>> ChangePasswordAsync(string userId, ChangePasswordModel model);

        /// <summary>
        /// Changes the phone number of an employee identified by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the employee.</param>
        /// <param name="model">The model containing the new phone number information.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the updated employee or an error message.</returns>
        Task<OperationResult<ApplicationUser>> ChangePhoneAsync(string userId, ChangePhoneModel model);

        /// <summary>
        /// Updates user settings such as preferred language and time zone for an employee identified by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the employee.</param>
        /// <param name="model">The model containing the updated settings information.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the updated employee or an error message.</returns>
        Task<OperationResult<ApplicationUser>> UpdateUserSettingsAsync(string userId, UpdateSettingsModel model);

        /// <summary>
        /// Changes the email address of an employee identified by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the employee.</param>
        /// <param name="model">The model containing the current email, new email, and password for verification.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the updated employee or an error message.</returns>
        Task<OperationResult<ApplicationUser>> ChangeEmailAsync(string userId, ChangeEmailModel model);
    }
}
