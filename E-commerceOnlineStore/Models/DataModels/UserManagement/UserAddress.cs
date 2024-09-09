using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerceOnlineStore.Models.DataModels.UserManagement
{
    /// <summary>
    /// Represents a many-to-many relationship between users and addresses.
    /// This class maps to the "UserAddresses" table in the database.
    /// </summary>
    [Table("UserAddresses")]
    public class UserAddress
    {
        /// <summary>
        /// Gets or sets the user ID associated with the address.
        /// </summary>
        [Required]
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user associated with the address.
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        /// <summary>
        /// Gets or sets the address ID associated with the user.
        /// </summary>
        [Required]
        public int AddressId { get; set; }

        /// <summary>
        /// Gets or sets the address associated with the user.
        /// </summary>
        [ForeignKey(nameof(AddressId))]
        public virtual Address Address { get; set; } = null!;

        /// <summary>
        /// Gets or sets whether the address is main.
        /// </summary>
        public bool IsMain { get; set; } = true;

        /// <summary>
        /// Gets or sets whether the address is used for billing.
        /// </summary>
        public bool IsBillingAddress { get; set; } = true;
    }
}
