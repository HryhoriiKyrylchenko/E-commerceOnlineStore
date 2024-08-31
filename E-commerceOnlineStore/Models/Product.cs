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
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the stock quantity of the product.
        /// </summary>
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative.")]
        public int Stock { get; set; }

        /// <summary>
        /// Gets or sets the category ID of the product.
        /// </summary>
        [Required]
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category of the product.
        /// </summary>
        public virtual Category Category { get; set; } = null!;

        /// <summary>
        /// Gets or sets the collection of product images associated with the product.
        /// </summary>
        public virtual ICollection<ProductImage> ProductImages { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of order items associated with the product.
        /// </summary>
        public virtual ICollection<OrderItem> OrderItems { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of products tags associated with the product.
        /// </summary>
        public virtual ICollection<ProductTag> ProductsTags { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of products discounts associated with the product.
        /// </summary>
        public virtual ICollection<ProductDiscount> ProductsDiscounts { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of products reviews associated with the product.
        /// </summary>
        public virtual ICollection<ProductReview> ProductReviews { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of return request items associated with the product.
        /// </summary>
        public virtual ICollection<ReturnRequestItem> ReturnRequestItems { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of shopping cart items associated with the product.
        /// </summary>
        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of wishlist items associated with the product.
        /// </summary>
        public virtual ICollection<WishlistItem> WishlistItems { get; set; } = [];
    }
}
