using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_commerceOnlineStore.Models.DataModels.Purchase;
using E_commerceOnlineStore.Enums.Finance;

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
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the associated transaction.
        /// This is a foreign key linking to the <see cref="Transaction"/> entity.
        /// </summary>
        [Required]
        public int TransactionId { get; set; }

        /// <summary>
        /// Gets or sets the associated <see cref="Transaction"/> for this payment.
        /// </summary>
        [ForeignKey(nameof(TransactionId))]
        public virtual Transaction Transaction { get; set; } = null!;

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
        [ForeignKey(nameof(PaymentMethodId))]
        public virtual PaymentMethod PaymentMethod { get; set; } = null!;

        /// <summary>
        /// Gets or sets the status of the payment.
        /// </summary>
        [Required]
        public PaymentStatus Status { get; set; }
    }
}
