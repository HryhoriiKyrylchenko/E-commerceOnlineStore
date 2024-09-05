using E_commerceOnlineStore.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_commerceOnlineStore.Models.DataModels.Purchase;

namespace E_commerceOnlineStore.Models.DataModels.Finance
{
    /// <summary>
    /// Represents a payment entity.
    /// </summary>
    [Table("Payments")]
    public class Payment
    {
        /// <summary>
        /// Gets or sets the payment ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the order ID associated with the payment.
        /// </summary>
        [Required]
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the order associated with the payment.
        /// </summary>
        public Order Order { get; set; } = null!;

        /// <summary>
        /// Gets or sets the payment date.
        /// </summary>
        [Required]
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// Gets or sets the amount of the payment.
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the ID of the payment method.
        /// </summary>
        [Required]
        public int PaymentMethodId { get; set; }

        /// <summary>
        /// Gets or sets the payment method.
        /// </summary>
        public virtual PaymentMethod PaymentMethod { get; set; } = null!;

        /// <summary>
        /// Gets or sets the status of the payment.
        /// </summary>
        [Required]
        public PaymentStatus Status { get; set; }
    }
}
