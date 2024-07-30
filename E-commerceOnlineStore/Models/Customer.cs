using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace E_commerceOnlineStore.Models
{
    /// <summary>
    /// Represents a customer entity.
    /// </summary>
    [Table("Customers")]
    public class Customer
    {
        /// <summary>
        /// Gets or sets the customer ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user ID associated with the customer.
        /// </summary>
        [Required]
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the application user associated with the customer.
        /// </summary>
        public ApplicationUser? User { get; set; }

        /// <summary>
        /// Gets or sets the first name of the customer.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name of the customer.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date of birth of the customer.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the collection of addresses associated with the customer.
        /// </summary>
        public virtual ICollection<Address>? Addresses { get; set; }

        /// <summary>
        /// Gets or sets the collection of orders associated with the customer.
        /// </summary>
        public virtual ICollection<Order>? Orders { get; set; }
    }
}
