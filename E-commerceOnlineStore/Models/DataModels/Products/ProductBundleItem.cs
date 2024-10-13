using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace E_commerceOnlineStore.Models.DataModels.Products
{
    /// <summary>
    /// Represents a specific product variant that is part of a product bundle.
    /// </summary>
    [Table("ProductBundleItems")]
    [Index(nameof(ProductBundleId), nameof(ProductVariantId))]
    public class ProductBundleItem
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product bundle item.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the product bundle this item belongs to.
        /// This is a foreign key linking to the <see cref="ProductBundle"/> entity.
        /// </summary>
        [Required]
        public int ProductBundleId { get; set; }

        /// <summary>
        /// Gets or sets the associated <see cref="ProductBundle"/> that this item is part of.
        /// </summary>
        [ForeignKey(nameof(ProductBundleId))]
        public virtual ProductBundle ProductBundle { get; set; } = null!;

        /// <summary>
        /// Gets or sets the identifier of the product variant that is included in the bundle.
        /// This is a foreign key linking to the <see cref="ProductVariant"/> entity.
        /// </summary>
        [Required]
        public int ProductVariantId { get; set; }

        /// <summary>
        /// Gets or sets the associated <see cref="ProductVariant"/> that is included in the bundle.
        /// </summary>
        [ForeignKey(nameof(ProductVariantId))]
        public virtual ProductVariant ProductVariant { get; set; } = null!;

        /// <summary>
        /// Gets or sets the quantity of the product variant included in the bundle.
        /// </summary>
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative value.")]
        public int Quantity { get; set; }
    }
}
