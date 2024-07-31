using System.ComponentModel.DataAnnotations;

namespace E_commerceOnlineStore.Models.Account
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
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password for the new user.
        /// </summary>
        /// <remarks>
        /// This property specifies the password that the new user will use to authenticate.
        /// </remarks>
        public string Password { get; set; } = string.Empty;

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
        public string? Gender { get; set; }

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
