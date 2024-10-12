using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using E_commerceOnlineStore.Models.DataModels.Products;
using E_commerceOnlineStore.Common.Attributes.Validation;

namespace E_commerceOnlineStore.Models.DataModels.Discounts
{
    /// <summary>
    /// Represents a discount entity.
    /// </summary>
    [Table("Discounts")]
    public class Discount
    {
        /// <summary>
        /// Gets or sets the discount ID.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the discount name.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the discount description.
        /// </summary>
        [MaxLength(255)]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the discount percentage (0-100).
        /// </summary>
        [Range(0, 100, ErrorMessage = "Discount percentage must be between 0 and 100.")]
        public decimal Percentage { get; set; }

        /// <summary>
        /// Gets or sets the discount amount.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Amount { get; set; }

        /// <summary>
        /// Gets or sets the start date of the discount.
        /// </summary>
        [Required]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the discount.
        /// </summary>
        [DateGreaterThan(nameof(StartDate), ErrorMessage = "End date must be greater than start date.")]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets whether the discount is active.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Gets or sets whether the discount is actually active based on the current date, in addition to manual status.
        /// This property is not stored in the database and is calculated on the fly.
        /// </summary>
        [NotMapped]
        public bool IsCurrentlyActive => IsActive &&
                                         (EndDate == null || EndDate > DateTime.UtcNow) &&
                                         (StartDate <= DateTime.UtcNow);

        /// <summary>
        /// Gets or sets the collection of products discounts associated with the discount.
        /// </summary>
        public virtual ICollection<ProductDiscount> ProductsDiscounts { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of product variants discounts associated with the discount.
        /// </summary>
        public virtual ICollection<ProductVariantDiscount> ProductsVariantDiscounts { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of category discounts associated with the discount.
        /// </summary>
        public virtual ICollection<CategoryDiscount> CategoryDiscounts { get; set; } = [];
    }
}
