using E_commerceOnlineStore.Models.DataModels.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_commerceOnlineStore.Models.DataModels.Finance
{
    /// <summary>
    /// Represents a tax entity for different locations and product categories.
    /// </summary>
    [Table("Taxes")]
    public class Tax
    {
        /// <summary>
        /// Gets or sets the tax ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the country where the tax is applicable.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Country { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the state or region where the tax is applicable (if applicable).
        /// </summary>
        [MaxLength(100)]
        public string? State { get; set; }

        /// <summary>
        /// Gets or sets the city where the tax is applicable (if applicable).
        /// </summary>
        [MaxLength(100)]
        public string? City { get; set; }

        /// <summary>
        /// Gets or sets the category ID for which this tax is applicable (if applicable).
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category for which this tax is applicable (if applicable).
        /// </summary>
        [ForeignKey(nameof(CategoryId))]
        public virtual Category? Category { get; set; }

        /// <summary>
        /// Gets or sets the tax rate as a percentage.
        /// </summary>
        [Required]
        [Range(0, 100)]
        public decimal Rate { get; set; }

        /// <summary>
        /// Gets or sets the effective date for the tax rate.
        /// </summary>
        [Required]
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// Gets or sets the expiry date for the tax rate (if applicable).
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets whether this tax is the default for its location or category.
        /// </summary>
        public bool IsDefault { get; set; } = false;
    }
}
