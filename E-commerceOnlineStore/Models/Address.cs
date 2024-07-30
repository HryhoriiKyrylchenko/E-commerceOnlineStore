using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerceOnlineStore.Models
{
    /// <summary>
    /// Represents an address entity.
    /// </summary>
    [Table("Addresses")]
    public class Address
    {
        /// <summary>
        /// Gets or sets the address ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the customer ID associated with the address.
        /// </summary>
        [Required]
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the customer associated with the address.
        /// </summary>
        public Customer? Customer { get; set; }

        /// <summary>
        /// Gets or sets the street of the address.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Street { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the city of the address.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the state of the address.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string State { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the postal code of the address.
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string PostalCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the country of the address.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Country { get; set; } = string.Empty;
    }
}
