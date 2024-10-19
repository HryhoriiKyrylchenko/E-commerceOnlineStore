using E_commerceOnlineStore.Enums.UserManagement;
using System.ComponentModel.DataAnnotations;

namespace E_commerceOnlineStore.Models.RequestModels.Account
{
    /// <summary>
    /// Represents the model for employee registration, containing necessary details 
    /// for a new employee to create an account.
    /// </summary>
    public class EmployeeRegistrationModel
    {
        /// <summary>
        /// Gets or sets the email address of the employee. 
        /// This field is required and must be in a valid email format.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password for the employee account. 
        /// This field is required and must be between 6 and 100 characters long.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the confirmation password for the employee account. 
        /// This field must match the Password field and is displayed with a custom name.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the first name of the employee. 
        /// This field is optional.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name of the employee. 
        /// This field is optional.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date of birth of the employee. 
        /// This field is optional.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the gender of the employee. 
        /// This field is optional and uses the GenderType enumeration.
        /// </summary>
        public GenderType? Gender { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the employee. 
        /// This field is optional and must be a valid phone number format.
        /// </summary>
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the URL of the employee's profile picture. 
        /// This field is optional.
        /// </summary>
        public string? ProfilePictureUrl { get; set; }

        /// <summary>
        /// Gets or sets the list of roles assigned to the employee. 
        /// This field is required and cannot be empty.
        /// </summary>
        public List<UserRole> Roles { get; set; } = [];

        /// <summary>
        /// Gets or sets the position of the employee within the organization. 
        /// This field is required and must not be empty.
        /// </summary>
        public string Position { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the employee's preferred language. 
        /// This field is optional.
        /// </summary>
        public string? PreferredLanguage { get; set; }

        /// <summary>
        /// Gets or sets the the employee's time zone. 
        /// This field is optional.
        /// </summary>
        public string? TimeZone { get; set; }
    }
}
