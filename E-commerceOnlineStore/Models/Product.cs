using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerceOnlineStore.Models
{
    /// <summary>
    /// Represents a product entity.
    /// </summary>
    [Table("Products")]
    public class Product
    {
        /// <summary>
        /// Gets or sets the product ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the stock quantity of the product.
        /// </summary>
        [Required]
        public int Stock { get; set; }

        /// <summary>
        /// Gets or sets the category ID of the product.
        /// </summary>
        [Required]
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category of the product.
        /// </summary>
        public virtual Category? Category { get; set; }

        /// <summary>
        /// Gets or sets the collection of product images associated with the product.
        /// </summary>
        public ICollection<ProductImage>? ProductImages { get; set; }

        /// <summary>
        /// Gets or sets the collection of order items associated with the product.
        /// </summary>
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
