using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_commerceOnlineStore.Models.DataModels.Discounts;
using E_commerceOnlineStore.Models.DataModels.UserManagement;
using Microsoft.EntityFrameworkCore;

namespace E_commerceOnlineStore.Models.DataModels.Products
{
    /// <summary>
    /// Represents a product entity.
    /// </summary>
    [Table("Products")]
    [Index(nameof(CategoryId))]
    public class Product
    {
        /// <summary>
        /// Gets or sets the product ID.
        /// </summary>
        [Key]
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
        /// Gets or sets the base price of the product.
        /// This property is required and stores the price as a decimal with a precision of 18 and a scale of 2.
        /// </summary>
        /// <remarks>
        /// The price is stored in the database as a decimal(18, 2), meaning it can have up to 18 digits,
        /// with 2 digits reserved for decimal places (cents).
        /// </remarks>
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal? BasePrice { get; set; }

        /// <summary>
        /// Gets or sets the percentage of profit margin applied to the base price of the product.
        /// This value will be used to calculate the final price by adding the margin on top of the base cost.
        /// </summary>
        [Range(0, 100, ErrorMessage = "Profit margin percentage must be between 0 and 100.")]
        [Column(TypeName = "decimal(5,2)")]
        public decimal? ProfitMarginPercentage { get; set; }

        /// <summary>
        /// Gets or sets the percentage of additional costs (e.g., shipping, handling, overhead) applied to the base price of the product.
        /// These costs are factored into the final product price.
        /// </summary>
        [Range(0, 100, ErrorMessage = "Additional costs percentage must be between 0 and 100.")]
        [Column(TypeName = "decimal(5,2)")]
        public decimal? AdditionalCostsPercentage { get; set; }


        /// <summary>
        /// Gets or sets the category ID of the product.
        /// </summary>
        [Required]
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category of the product.
        /// </summary>
        [ForeignKey(nameof(CategoryId))]
        public virtual ProductCategory Category { get; set; } = null!;

        /// <summary>
        /// Gets or sets the user ID of the person who last edited the product.
        /// </summary>
        public string? LastEditedByUserId { get; set; }

        /// <summary>
        /// Gets or sets the user who last edited the product.
        /// </summary>
        [ForeignKey(nameof(LastEditedByUserId))]
        public virtual Employee? LastEditedByUser { get; set; }

        /// <summary>
        /// Gets or sets the date when the product was last edited.
        /// </summary>
        public DateTime? LastEditedDate { get; set; }

        /// <summary>
        /// Version of the entity for optimistic concurrency control.
        /// </summary>
        [Timestamp]
        public byte[] RowVersion { get; set; } = null!;

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
        /// Gets or sets the collection of product variants associated with this product.
        /// Each product can have multiple variants, such as different sizes or colors.
        /// </summary>
        /// <remarks>
        /// This is a virtual navigation property that allows lazy loading of the related <see cref="ProductVariant"/> entities.
        /// </remarks>
        public virtual ICollection<ProductVariant> ProductVariants { get; set; } = [];
    }
}
