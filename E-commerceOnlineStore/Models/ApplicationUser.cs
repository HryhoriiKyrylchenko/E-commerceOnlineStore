using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string? Gender { get; set; }

        /// <summary>
        /// Gets or sets the URL of the user's profile picture.
        /// </summary>
        public string? ProfilePictureUrl { get; set; }

        /// <summary>
        /// Gets or sets whether the user is active.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Gets or sets user addresses.
        /// </summary>
        public virtual ICollection<Address>? Addresses { get; set; }

        /// <summary>
        /// Gets or sets the collection of refresh tokens associated with the user.
        /// </summary>
        /// <remarks>
        /// This collection is typically used to store multiple refresh tokens for the user, 
        /// allowing for the management of issued tokens for security purposes.
        /// </remarks>
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = [];
    }
}
