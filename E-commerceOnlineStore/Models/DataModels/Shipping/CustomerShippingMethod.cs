using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using E_commerceOnlineStore.Models.DataModels.UserManagement;

namespace E_commerceOnlineStore.Models.DataModels.Shipping
{
    /// <summary>
    /// Represents a shipping method used by a customer.
    /// </summary>
    [Table("CustomerShippingMethods")]
    public class CustomerShippingMethod
    {
        /// <summary>
        /// Gets or sets the customer ID associated with the shipping method.
        /// </summary>
        [Required]
        public string CustomerId { get; set; } = null!;

        /// <summary>
        /// Gets or sets the customer associated with the payment method.
        /// </summary>
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer Customer { get; set; } = null!;

        /// <summary>
        /// Gets or sets the shipping method ID associated with the payment method.
        /// </summary>
        [Required]
        public int ShippingMethodId { get; set; }

        /// <summary>
        /// Gets or sets the shipping method details.
        /// </summary>
        [ForeignKey(nameof(ShippingMethodId))]
        public virtual ShippingMethod ShippingMethod { get; set; } = null!;

        /// <summary>
        /// Gets or sets the list of dates when this shipping method was used.
        /// </summary>
        [Required]
        public ICollection<DateTime> UsedDates { get; set; } = [];

        /// <summary>
        /// Gets or sets a flag indicating if this is the current shipping method.
        /// </summary>
        [Required]
        public bool IsCurrent { get; set; }
    }

}
