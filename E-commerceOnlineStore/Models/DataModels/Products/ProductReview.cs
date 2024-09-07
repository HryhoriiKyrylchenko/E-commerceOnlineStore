using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using E_commerceOnlineStore.Models.DataModels.Account;
using E_commerceOnlineStore.Enums.Products;

namespace E_commerceOnlineStore.Models.DataModels.Products
{
    /// <summary>
    /// Represents a product review entity.
    /// </summary>
    [Table("ProductReviews")]
    public class ProductReview
    {
        /// <summary>
        /// Gets or sets the product review ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the product ID associated with the review.
        /// </summary>
        [Required]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product associated with the review.
        /// </summary>
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; } = null!;

        /// <summary>
        /// Gets or sets the customer ID associated with the review.
        /// </summary>
        [Required]
        public string CustomerId { get; set; } = null!;

        /// <summary>
        /// Gets or sets the customer who wrote the review.
        /// </summary>
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer Customer { get; set; } = null!;

        /// <summary>
        /// Gets or sets the rating of the product.
        /// </summary>
        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        /// <summary>
        /// Gets or sets the title of the review.
        /// </summary>
        [MaxLength(100)]
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the review content.
        /// </summary>
        [Required]
        [MaxLength(1000)]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date when the review was written.
        /// </summary>
        [Required]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the status of the review (e.g., approved, pending).
        /// </summary>
        [Required]
        public ReviewStatus Status { get; set; }
    }
}
