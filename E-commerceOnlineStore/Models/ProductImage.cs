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
        public Product? Product { get; set; }

        /// <summary>
        /// Gets or sets the URL of the image.
        /// </summary>
        [Required]
        public string ImageUrl { get; set; } = string.Empty;
    }
}
