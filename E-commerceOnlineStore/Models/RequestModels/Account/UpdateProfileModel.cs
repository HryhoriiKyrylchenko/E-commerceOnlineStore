using E_commerceOnlineStore.Enums.UserManagement;

namespace E_commerceOnlineStore.Models.RequestModels.Account
{
    /// <summary>
    /// Represents the data model used for updating a user's profile information.
    /// </summary>
    public class UpdateProfileModel
    {
        /// <summary>
        /// Gets or sets the user's first name.
        /// This property is initialized to an empty string.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's last name.
        /// This property is initialized to an empty string.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's email address.
        /// This property is initialized to an empty string.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's gender. This property is optional and may be null.
        /// </summary>
        public GenderType? Gender { get; set; }

        /// <summary>
        /// Gets or sets the user's phone number. This property is optional and may be null.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the URL of the user's profile picture. This property is optional and may be null.
        /// </summary>
        public string? ProfilePictureUrl { get; set; }
    }

}
