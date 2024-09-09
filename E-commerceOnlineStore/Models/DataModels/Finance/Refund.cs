using E_commerceOnlineStore.Enums.Finance;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerceOnlineStore.Models.DataModels.Finance
{
    /// <summary>
    /// Represents a refund associated with a transaction.
    /// </summary>
    [Table("Refunds")]
    public class Refund
    {
        /// <summary>
        /// Gets or sets the unique identifier for the refund.
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
        /// Gets or sets the associated <see cref="Transaction"/> for this refund.
        /// </summary>
        [ForeignKey(nameof(TransactionId))]
        public virtual Transaction Transaction { get; set; } = null!;

        /// <summary>
        /// Gets or sets the date when the refund was processed.
        /// </summary>
        public DateTime? RefundDate { get; set; }

        /// <summary>
        /// Gets or sets the amount of money refunded.
        /// Stored as a decimal with 18 digits of precision and 2 decimal places.
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the reason for the refund.
        /// This is an optional string field that provides an explanation for why the refund was issued.
        /// </summary>
        public string? Reason { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the status of the refund, indicating whether it is pending, processed, or rejected.
        /// </summary>
        [Required]
        public RefundStatus Status { get; set; }
    }
}
