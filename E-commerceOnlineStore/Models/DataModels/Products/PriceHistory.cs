using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace E_commerceOnlineStore.Models.DataModels.Products
{
    /// <summary>
    /// Represents the price history of a product variant.
    /// </summary>
    [Table("PriceHistories")]
    [Index(nameof(ProductVariantId), nameof(ChangeDate))]
    public class PriceHistory
    {
        /// <summary>
        /// Gets or sets the unique identifier for the price history record.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the product variant that this price history record pertains to.
        /// </summary>
        [Required]
        public int ProductVariantId { get; set; }

        /// <summary>
        /// Gets or sets the product variant associated with this price history record.
        /// </summary>
        [ForeignKey(nameof(ProductVariantId))]
        public virtual ProductVariant ProductVariant { get; set; } = null!;

        /// <summary>
        /// Gets or sets the date and time when the price change occurred.
        /// </summary>
        [Required]
        public DateTime ChangeDate { get; set; }

        /// <summary>
        /// Gets or sets the old price before the change.
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal OldPrice { get; set; }

        /// <summary>
        /// Gets or sets the new price after the change.
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal NewPrice { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the person or system that made the change.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string ChangedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the reason for the price change.
        /// </summary>
        [MaxLength(500)]
        public string ChangeReason { get; set; } = string.Empty;
    }
}
