using E_commerceOnlineStore.Data;
using E_commerceOnlineStore.Enums.UserManagement;
using E_commerceOnlineStore.Models.DataModels.UserManagement;
using E_commerceOnlineStore.Models.RequestModels.Account;
using E_commerceOnlineStore.Services.Business.Account;
using E_commerceOnlineStore.Utilities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_commerceOnlineStore.Services.Data.User
{
    /// <summary>
    /// Service class for managing employee data operations.
    /// Implements the <see cref="IEmployeeDataService"/> interface.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="EmployeeDataService"/> class.
    /// </remarks>
    /// <param name="context">The application database context.</param>
    public class EmployeeDataService(ApplicationDbContext context,
                                    ILogger<RegistrationService> logger,
                                    IRolesService rolesService,
                                    IUserDataService userDataService) : IEmployeeDataService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ILogger<RegistrationService> _logger = logger;
        private readonly IRolesService _roleService = rolesService;
        private readonly IUserDataService _userDataService = userDataService;

        /// <summary>
        /// Adds a new employee asynchronously using the provided registration model.
        /// </summary>
        /// <param name="model">An instance of <see cref="EmployeeRegistrationModel"/> containing the employee's registration information.</param>
        /// <returns>
        /// An <see cref="OperationResult{ApplicationUser}"/> object containing the result of the operation.
        /// If the email or password is missing, the result contains an error message.
        /// If the addition fails, the result contains the errors encountered during the operation.
        /// If successful, the result contains the newly added employee information.
        /// </returns>
        public async Task<OperationResult<ApplicationUser>> AddEmployeeAsync(EmployeeRegistrationModel model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                return OperationResult<ApplicationUser>.FailureResult(["Email and password are required."]);
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var userRegistrationModel = new UserRegistrationModel
                    {
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Password = model.Password,
                        ConfirmPassword = model.ConfirmPassword,
                        DateOfBirth = model.DateOfBirth,
                        Gender = model.Gender,
                        PhoneNumber = model.PhoneNumber,
                        ProfilePictureUrl = model.ProfilePictureUrl,
                        PreferredLanguage = model.PreferredLanguage,
                        TimeZone = model.TimeZone,
                    };

                    var result = await _userDataService.CreateUserAsync(userRegistrationModel);

                    if (!result.Succeeded || result.Data == null)
                    {
                        return result;
                    }

                    List<string> userRoles = model.Roles.Select(role => role.ToString()).ToList();

                    var roleResult = await _roleService.AssignRolesListAsync(result.Data.Id, userRoles);

                    if (!roleResult.Succeeded)
                    {
                        return OperationResult<ApplicationUser>.FailureResult(roleResult.Errors.Select(e => e.Description).ToList());
                    }

                    var employee = new Employee
                    {
                        UserId = result.Data.Id,
                        Position = model.Position,
                        IsActive = true
                    };

                    var existingEmployee = await _context.Employees.FirstOrDefaultAsync(e => e.UserId == employee.UserId);

                    if (existingEmployee != null)
                    {
                        return OperationResult<ApplicationUser>.FailureResult(["Failed create an employee, employee with the same Id already exists."]);
                    }

                    await _context.Employees.AddAsync(employee);
                    await _context.SaveChangesAsync();

                    var employeeResult = await GetEmployeeByIdAsync(employee.UserId);

                    await transaction.CommitAsync();

                    return employeeResult;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Failed to add employee");
                    return OperationResult<ApplicationUser>.FailureResult(["Failed to add employee due to an unexpected error."]);
                }
            }
        }

        /// <summary>
        /// Deletes an employee asynchronously using their unique identifier.
        /// </summary>
        /// <param name="employeeId">The unique identifier of the employee to delete.</param>
        /// <returns>
        /// An <see cref="OperationResult{ApplicationUser}"/> object containing the result of the operation.
        /// If the employee is not found, the result contains an error message.
        /// If the deletion fails, the result contains an error message indicating the failure.
        /// If successful, the result contains the deleted employee's information.
        /// </returns>
        public async Task<OperationResult<ApplicationUser>> DeleteEmployeeAsync(string employeeId)
        {
            var employeeResult = await GetEmployeeByIdAsync(employeeId);

            if (!employeeResult.Succeeded || employeeResult.Data == null)
            {
                return OperationResult<ApplicationUser>.FailureResult(["Employee not found."]);
            }

            var employeeToDelete = await _context.Employees.Where(c => c.UserId == employeeId).FirstOrDefaultAsync();
            if (employeeToDelete == null) return OperationResult<ApplicationUser>.FailureResult(["Failed to delete customer."]);

            _context.Employees.Remove(employeeToDelete);
            _context.ApplicationUsers.Remove(employeeResult.Data);
            var result = await _context.SaveChangesAsync() > 0;

            return result ? OperationResult<ApplicationUser>.SuccessResult(employeeResult.Data) :
                            OperationResult<ApplicationUser>.FailureResult(["Failed to delete customer."]);
        }

        /// <summary>
        /// Retrieves an employee's details asynchronously using their unique identifier.
        /// </summary>
        /// <param name="employeeId">The unique identifier of the employee to retrieve.</param>
        /// <returns>
        /// An <see cref="OperationResult{ApplicationUser}"/> object containing the result of the operation.
        /// If the employee is found, the result contains the employee information.
        /// If not found, the result contains an error message indicating that the employee was not located.
        /// </returns>
        public async Task<OperationResult<ApplicationUser>> GetEmployeeByIdAsync(string employeeId)
        {
            var employee = await _context.ApplicationUsers
                                         .Where(u => u.Id == employeeId)
                                         .Include(u => u.Employee)
                                         .FirstOrDefaultAsync();

            return employee != null
                ? OperationResult<ApplicationUser>.SuccessResult(employee)
                : OperationResult<ApplicationUser>.FailureResult(["Employee not found."]);
        }

        /// <summary>
        /// Retrieves an employee's details asynchronously using their email address.
        /// </summary>
        /// <param name="email">The email address of the employee to retrieve.</param>
        /// <returns>
        /// An <see cref="OperationResult{ApplicationUser}"/> object containing the result of the operation.
        /// If the email is null or empty, the result contains an error indicating the invalid input.
        /// If the employee is not found, the result contains an error message indicating that the employee could not be located.
        /// If successful, the result contains the employee information.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when the provided email is null or empty.</exception>
        public async Task<OperationResult<ApplicationUser>> GetEmployeeByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return OperationResult<ApplicationUser>.FailureResult([new ArgumentNullException(nameof(email)).Message]);
            }

            var existingEmployee = await _context.ApplicationUsers
                                                    .Where(u => u.Email == email)
                                                    .Include(u => u.Employee)
                                                    .FirstOrDefaultAsync();

            return existingEmployee == null ?
                            OperationResult<ApplicationUser>.FailureResult([$"Employee with an email - {email} did not find in the database."]) :
                            OperationResult<ApplicationUser>.SuccessResult(existingEmployee);
        }

        /// <summary>
        /// Asynchronously retrieves a list of all employees.
        /// </summary>
        /// <returns>The result of the get operation, containing a list of all employees.</returns>
        public async Task<OperationResult<List<ApplicationUser>>> GetAllEmployeesAsync()
        {
            var employees = await _context.ApplicationUsers
                                        .Where(u => u.Customer != null)
                                        .Include(u => u.Employee)
                                        .ToListAsync();

            return OperationResult<List<ApplicationUser>>.SuccessResult(employees);
        }

        /// <summary>
        /// Asynchronously checks if a customer with the specified email exists in the database.
        /// </summary>
        /// <param name="email">The email address to check for existence.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains 
        /// <c>true</c> if a customer with the specified email exists; otherwise, <c>false</c>.</returns>
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.ApplicationUsers.AnyAsync(c => c.Email != null && c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Updates the profile information of an employee asynchronously.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose profile is to be updated.</param>
        /// <param name="model">An instance of <see cref="UpdateProfileModel"/> containing the updated profile information.</param>
        /// <returns>
        /// An <see cref="OperationResult{ApplicationUser}"/> object containing the result of the operation.
        /// If the update fails, the result contains the errors encountered during the update.
        /// If successful, the result contains the updated employee information.
        /// </returns>
        public async Task<OperationResult<ApplicationUser>> UpdateProfileAsync(string userId, UpdateProfileModel model)
        {
            var result = await _userDataService.UpdateProfileAsync(userId, model);

            if (!result.Succeeded || result.Data == null)
            {
                return OperationResult<ApplicationUser>.FailureResult(result.Errors);
            }

            return await GetEmployeeByIdAsync(result.Data.Id);
        }

        /// <summary>
        /// Changes the password of an employee asynchronously.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose password is to be changed.</param>
        /// <param name="model">An instance of <see cref="ChangePasswordModel"/> containing the new password details.</param>
        /// <returns>
        /// An <see cref="OperationResult{ApplicationUser}"/> object containing the result of the operation.
        /// If the password change fails, the result contains the errors encountered during the change.
        /// If successful, the result contains the updated employee information.
        /// </returns>
        public async Task<OperationResult<ApplicationUser>> ChangePasswordAsync(string userId, ChangePasswordModel model)
        {
            var result = await _userDataService.ChangePasswordAsync(userId, model);

            if (!result.Succeeded || result.Data == null)
            {
                return OperationResult<ApplicationUser>.FailureResult(result.Errors);
            }

            return await GetEmployeeByIdAsync(result.Data.Id);
        }

        /// <summary>
        /// Changes the phone number of an employee asynchronously.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose phone number is to be changed.</param>
        /// <param name="model">An instance of <see cref="ChangePhoneModel"/> containing the new phone number details.</param>
        /// <returns>
        /// An <see cref="OperationResult{ApplicationUser}"/> object containing the result of the operation.
        /// If the phone number change fails, the result contains the errors encountered during the change.
        /// If successful, the result contains the updated employee information.
        /// </returns>
        public async Task<OperationResult<ApplicationUser>> ChangePhoneAsync(string userId, ChangePhoneModel model)
        {
            var result = await _userDataService.ChangePhoneAsync(userId, model);

            if (!result.Succeeded || result.Data == null)
            {
                return OperationResult<ApplicationUser>.FailureResult(result.Errors);
            }

            return await GetEmployeeByIdAsync(result.Data.Id);
        }

        /// <summary>
        /// Updates the user settings of an employee asynchronously.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose settings are to be updated.</param>
        /// <param name="model">An instance of <see cref="UpdateSettingsModel"/> containing the updated settings information.</param>
        /// <returns>
        /// An <see cref="OperationResult{ApplicationUser}"/> object containing the result of the operation.
        /// If the settings update fails, the result contains the errors encountered during the update.
        /// If successful, the result contains the updated employee information.
        /// </returns>
        public async Task<OperationResult<ApplicationUser>> UpdateUserSettingsAsync(string userId, UpdateSettingsModel model)
        {
            var result = await _userDataService.UpdateUserSettingsAsync(userId, model);

            if (!result.Succeeded || result.Data == null)
            {
                return OperationResult<ApplicationUser>.FailureResult(result.Errors);
            }

            return await GetEmployeeByIdAsync(result.Data.Id);
        }

        /// <summary>
        /// Changes the email address of an employee asynchronously.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose email address is to be changed.</param>
        /// <param name="model">An instance of <see cref="ChangeEmailModel"/> containing the new email details.</param>
        /// <returns>
        /// An <see cref="OperationResult{ApplicationUser}"/> object containing the result of the operation.
        /// If the email change fails, the result contains the errors encountered during the change.
        /// If successful, the result contains the updated employee information.
        /// </returns>
        public async Task<OperationResult<ApplicationUser>> ChangeEmailAsync(string userId, ChangeEmailModel model)
        {
            var result = await _userDataService.ChangeEmailAsync(userId, model);

            if (!result.Succeeded || result.Data == null)
            {
                return OperationResult<ApplicationUser>.FailureResult(result.Errors);
            }

            return await GetEmployeeByIdAsync(result.Data.Id);
        }
    }
}
