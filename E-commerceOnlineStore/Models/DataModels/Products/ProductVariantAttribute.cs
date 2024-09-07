using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_commerceOnlineStore.Models.DataModels.Products
{
    /// <summary>
    /// Represents an attribute of a product variant, such as size, color, or other characteristics.
    /// </summary>
    [Table("ProductVariantAttributes")]
    public class ProductVariantAttribute
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product variant attribute.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the associated product variant.
        /// This is a foreign key linking to the <see cref="ProductVariant"/> entity.
        /// </summary>
        [Required]
        public int ProductVariantId { get; set; }

        /// <summary>
        /// Gets or sets the associated product variant.
        /// This navigation property provides access to the parent <see cref="ProductVariant"/>.
        /// </summary>
        [ForeignKey(nameof(ProductVariantId))]
        public virtual ProductVariant ProductVariant { get; set; } = null!;

        /// <summary>
        /// Gets or sets the key or name of the attribute.
        /// This could be a property such as "Color" or "Size".
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the value of the attribute.
        /// This could be the specific value for the attribute, such as "Black" or "Large".
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Value { get; set; } = string.Empty;
    }
}
