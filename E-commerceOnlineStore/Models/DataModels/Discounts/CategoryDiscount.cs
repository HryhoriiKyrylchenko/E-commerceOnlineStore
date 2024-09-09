using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_commerceOnlineStore.Models.DataModels.Products;

namespace E_commerceOnlineStore.Models.DataModels.Discounts
{
    /// <summary>
    /// Represents a many-to-many relationship between categories and discounts.
    /// This class maps to the "CategoryDiscounts" table in the database.
    /// </summary>
    [Table("CategoryDiscounts")]
    public class CategoryDiscount
    {
        /// <summary>
        /// Gets or sets the unique identifier for the category.
        /// This property is required and maps to the CategoryId column in the database.
        /// </summary>
        [Required]
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category associated with the discount.
        /// This navigation property provides access to the related ProductCategory entity.
        /// </summary>
        [ForeignKey(nameof(CategoryId))]
        public ProductCategory Category { get; set; } = null!;

        /// <summary>
        /// Gets or sets the unique identifier for the discount.
        /// This property is required and maps to the DiscountId column in the database.
        /// </summary>
        [Required]
        public int DiscountId { get; set; }

        /// <summary>
        /// Gets or sets the discount associated with the category.
        /// This navigation property provides access to the related Discount entity.
        /// </summary>
        [ForeignKey(nameof(DiscountId))]
        public Discount Discount { get; set; } = null!;
    }
}
