using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using E_commerceOnlineStore.Models.DataModels.Account;

namespace E_commerceOnlineStore.Models.DataModels.Shipping
{
    /// <summary>
    /// Represents a delivery method that can be used for shipping orders.
    /// </summary>
    [Table("ShippingMethods")]
    public class ShippingMethod
    {
        /// <summary>
        /// Gets or sets the unique identifier for the delivery method.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the delivery method.
        /// This property is initialized to an empty string to avoid null values.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the delivery method.
        /// This property is nullable to accommodate cases where no description is provided.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the cost of using this shipping method.
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }

        /// <summary>
        /// Gets or sets the estimated delivery time for this shipping method.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string EstimatedDeliveryTime { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the availability status of the shipping method.
        /// </summary>
        [Required]
        public bool IsAvailable { get; set; } = true;

        /// <summary>
        /// Gets or sets the collection of deliveries associated with this shipping method.
        /// </summary>
        public virtual ICollection<Shipment> Shipments { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of customers associated with this shipping method.
        /// </summary>
        public virtual ICollection<Customer> Customers { get; set; } = [];
    }
}
