using E_commerceOnlineStore.Enums.Inventory;
using E_commerceOnlineStore.Models.DataModels.Products;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_commerceOnlineStore.Models.DataModels.Inventory
{
    /// <summary>
    /// Represents a label associated with a product for identification purposes, such as barcodes or QR codes.
    /// </summary>
    [Table("Labels")]
    public class Label
    {
        /// <summary>
        /// Gets or sets the unique identifier for the label.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the code for the label, such as a barcode or QR code.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the product associated with this label.
        /// </summary>
        [Required]
        public int ProductVariantId { get; set; }

        /// <summary>
        /// Gets or sets the product associated with this label.
        /// </summary>
        [ForeignKey(nameof(ProductVariantId))]
        public virtual ProductVariant ProductVariant { get; set; } = null!;

        /// <summary>
        /// Gets or sets the date when the label was created.
        /// </summary>
        [Required]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the status of the label (e.g., active, inactive).
        /// </summary>
        [Required]
        public LabelStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the description or additional information about the label.
        /// </summary>
        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
