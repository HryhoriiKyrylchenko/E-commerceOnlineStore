using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerceOnlineStore.Models.DataModels.UserManagement
{
    /// <summary>
    /// Represents a refresh token used in the authentication process.
    /// </summary>
    /// <remarks>
    /// The <see cref="RefreshToken"/> class contains details about the token, including its expiration and revocation status.
    /// It is used to manage and validate refresh tokens for user authentication.
    /// </remarks>
    [Table("RefreshTokens")]
    public class RefreshToken
    {
        /// <summary>
        /// Gets or sets the unique identifier for the refresh token.
        /// </summary>
        /// <remarks>
        /// This is the primary key of the <see cref="RefreshToken"/> table.
        /// </remarks>
        [Key]
        [Required]
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ID of the user associated with this refresh token.
        /// </summary>
        /// <remarks>
        /// This field is used to link the refresh token to a specific user.
        /// </remarks>
        [Required]
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user associated with this refresh token.
        /// </summary>
        /// <remarks>
        /// This is a navigation property to the <see cref="ApplicationUser"/> entity, allowing for navigation between
        /// the token and the user.
        /// </remarks>
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        /// <summary>
        /// Gets or sets the date and time when the refresh token expires.
        /// </summary>
        /// <remarks>
        /// The token is considered expired if the current UTC time is greater than or equal to this expiration time.
        /// </remarks>
        [Required]
        public DateTime Expiration { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the token has been revoked.
        /// </summary>
        /// <remarks>
        /// A token is considered revoked if this property is set to <c>true</c>.
        /// </remarks>
        public bool IsRevoked { get; set; } = false;
    }

}
