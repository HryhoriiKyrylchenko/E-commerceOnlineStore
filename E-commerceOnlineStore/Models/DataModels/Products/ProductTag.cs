using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_commerceOnlineStore.Models.DataModels.Products
{
    /// <summary>
    /// Represents the many-to-many relationship between products and tags.
    /// </summary>
    [Table("ProductTags")]
    public class ProductTag
    {
        /// <summary>
        /// Gets or sets the product ID.
        /// </summary>
        [Required] 
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product associated with the tag.
        /// </summary>
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; } = null!;

        /// <summary>
        /// Gets or sets the tag ID.
        /// </summary>
        [Required]
        public int TagId { get; set; }

        /// <summary>
        /// Gets or sets the tag associated with the product.
        /// </summary>
        [ForeignKey(nameof(TagId))]
        public virtual Tag Tag { get; set; } = null!;
    }
}
