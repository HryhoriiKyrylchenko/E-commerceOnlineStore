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
        /// Gets or sets the user ID associated with the address.
        /// </summary>
        [Required]
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user associated with the address.
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        /// <summary>
        /// Gets or sets the house or apartment number of the address.
        /// </summary>
        [MaxLength(20)]
        [RegularExpression(@"^[a-zA-Z0-9\s\-]+$", ErrorMessage = "Invalid house number format.")]
        public string? HouseNumber { get; set; }

        /// <summary>
        /// Gets or sets the apartment number of the address (if applicable).
        /// </summary>
        [MaxLength(20)]
        [RegularExpression(@"^[a-zA-Z0-9\s\-]+$", ErrorMessage = "Invalid apartment number format.")]
        public string? ApartmentNumber { get; set; }

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
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid postal code format.")]
        public string PostalCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the country of the address.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Country { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets whether the address is main.
        /// </summary>
        public bool IsMain { get; set; } = true;

        /// <summary>
        /// Gets or sets whether the address is used for billing.
        /// </summary>
        public bool IsBillingAddress { get; set; } = true;

        /// <summary>
        /// Gets or sets the collection of shipments associated with the address.
        /// </summary>
        public virtual ICollection<Shipment> Shipments { get; set; } = [];
    }
}
