using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace E_commerceOnlineStore.Models
{
    /// <summary>
    /// Represents an application user.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the user's first name.
        /// </summary>
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's last name.
        /// </summary>
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's date of birth.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the user's gender.
        /// </summary>
        [MaxLength(10)]
        public string Gender { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the URL of the user's profile picture.
        /// </summary>
        public string ProfilePictureUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets whether the user is active.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Gets or sets user addresses.
        /// </summary>
        public virtual ICollection<Address>? Addresses { get; set; }

        /// <summary>
        /// Gets or sets the collection of roles associated with the user.
        /// </summary>
        public virtual ICollection<IdentityUserRole<string>>? UserRoles { get; set; }
    }
}
