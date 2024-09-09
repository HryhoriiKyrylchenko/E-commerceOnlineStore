using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using E_commerceOnlineStore.Models.DataModels.UserManagement;

namespace E_commerceOnlineStore.Models.DataModels.Finance
{
    /// <summary>
    /// Represents a payment method used by a customer.
    /// </summary>
    [Table("CustomerPaymentMethods")]
    public class CustomerPaymentMethod
    {
        /// <summary>
        /// Gets or sets the customer ID associated with the payment method.
        /// </summary>
        [Required]
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the customer associated with the payment method.
        /// </summary>
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer Customer { get; set; } = null!;

        /// <summary>
        /// Gets or sets the payment method ID associated with the payment method.
        /// </summary>
        [Required]
        public int PaymentMethodId { get; set; }

        /// <summary>
        /// Gets or sets the payment method details.
        /// </summary>
        [ForeignKey(nameof(PaymentMethodId))]
        public virtual PaymentMethod PaymentMethod { get; set; } = null!;

        /// <summary>
        /// Gets or sets the list of dates when this payment method was used.
        /// </summary>
        [Required]
        public ICollection<DateTime> UsedDates { get; set; } = [];

        /// <summary>
        /// Gets or sets a flag indicating if this is the current payment method.
        /// </summary>
        [Required]
        public bool IsCurrent { get; set; }
    }
}
