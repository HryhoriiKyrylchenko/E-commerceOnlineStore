using E_commerceOnlineStore.Enums.Account;
using System.ComponentModel.DataAnnotations;

namespace E_commerceOnlineStore.Models.RequestModels.Account
{
    /// <summary>
    /// Represents the data required for user registration.
    /// </summary>
    /// <remarks>
    /// The <see cref="RegisterModel"/> class contains properties that are used when registering a new user.
    /// It includes personal information, credentials, and role details needed for the registration process.
    /// </remarks>
    public class RegisterModel
    {
        /// <summary>
        /// Gets or sets the username of the new user.
        /// </summary>
        /// <remarks>
        /// This property specifies the username that the new user will use to log in to the application.
        /// </remarks>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address of the new user.
        /// </summary>
        /// <remarks>
        /// This property specifies the email address associated with the new user's account.
        /// </remarks>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password for the new user.
        /// </summary>
        /// <remarks>
        /// This property specifies the password that the new user will use to authenticate.
        /// </remarks>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password confirmation that the user wants to set.
        /// This property is initialized to an empty string.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the first name of the new user.
        /// </summary>
        /// <remarks>
        /// This property specifies the first name of the new user, used for personalization and identification.
        /// </remarks>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name of the new user.
        /// </summary>
        /// <remarks>
        /// This property specifies the last name of the new user, used for personalization and identification.
        /// </remarks>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date of birth of the new user.
        /// </summary>
        /// <remarks>
        /// This property specifies the date of birth of the new user, which may be used for age verification or personalization.
        /// </remarks>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the gender of the new user.
        /// </summary>
        /// <remarks>
        /// This property specifies the gender of the new user, which may be used for personalization purposes.
        /// </remarks>
        public GenderType? Gender { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the new user.
        /// </summary>
        /// <remarks>
        /// This property specifies the phone number associated with the new user's account.
        /// </remarks>
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the URL of the new user's profile picture.
        /// </summary>
        /// <remarks>
        /// This property specifies the URL where the profile picture of the new user is stored.
        /// </remarks>
        public string? ProfilePictureUrl { get; set; }

        /// <summary>
        /// Gets or sets the role name assigned to the new user.
        /// </summary>
        /// <remarks>
        /// This property specifies the role that will be assigned to the new user upon registration, defining their permissions within the application.
        /// </remarks>
        public string RoleName { get; set; } = string.Empty;
    }
}
