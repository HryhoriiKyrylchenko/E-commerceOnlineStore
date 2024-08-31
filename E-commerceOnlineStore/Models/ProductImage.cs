using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerceOnlineStore.Models
{
    /// <summary>
    /// Represents a product image entity.
    /// </summary>
    [Table("ProductImages")]
    public class ProductImage
    {
        /// <summary>
        /// Gets or sets the product image ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the product ID associated with the image.
        /// </summary>
        [Required]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product associated with the image.
        /// </summary>
        /// [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; } = null!;

        /// <summary>
        /// Gets or sets the URL of the image.
        /// </summary>
        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether this item is marked as the main item.
        /// This property is initialized to <c>false</c> by default.
        /// </summary>
        public bool IsMain { get; set; } = false;

    }
}
