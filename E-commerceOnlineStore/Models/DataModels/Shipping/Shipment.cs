using E_commerceOnlineStore.Enums;
using E_commerceOnlineStore.Models.DataModels.Account;
using E_commerceOnlineStore.Models.DataModels.Order;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerceOnlineStore.Models.DataModels.Shipping
{
    /// <summary>
    /// Represents a delivery entity.
    /// </summary>
    [Table("Shipments")]
    public class Shipment
    {
        /// <summary>
        /// Gets or sets the delivery ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the order ID associated with the delivery.
        /// </summary>
        [Required]
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the order associated with the delivery.
        /// </summary>
        public Order Order { get; set; } = null!;

        /// <summary>
        /// Gets or sets the delivery method.
        /// </summary>
        [Required]
        public int ShippingMethodId { get; set; }

        /// <summary>
        /// Gets or sets the delivery method associated with the delivery.
        /// </summary>
        public virtual ShippingMethod ShippingMethod { get; set; } = null!;

        /// <summary>
        /// Gets or sets the delivery address.
        /// </summary>
        public int? ShippingAddressId { get; set; }

        /// <summary>
        /// Gets or sets the address associated with the delivery.
        /// </summary>
        public virtual Address? ShippingAddress { get; set; }

        /// <summary>
        /// Gets or sets the location where the order can be picked up by the customer.
        /// This property is nullable to accommodate cases where a pickup point is not specified.
        /// </summary>
        public string? PickupPoint { get; set; }


        /// <summary>
        /// Gets or sets the expected delivery date.
        /// </summary>
        public DateTime? ExpectedShippingDate { get; set; }

        /// <summary>
        /// Gets or sets the status of the delivery.
        /// </summary>
        [MaxLength(50)]
        public ShippingStatus Status { get; set; }
    }
}
