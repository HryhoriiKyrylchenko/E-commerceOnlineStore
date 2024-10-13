using E_commerceOnlineStore.Models.DataModels.Products;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerceOnlineStore.Models.DataModels.Analytics
{
    /// <summary>
    /// Represents sales analytics data for a specific product variant.
    /// </summary>
    [Table("SalesAnalytics")]
    public class SalesAnalytics
    {
        /// <summary>
        /// Gets or sets the unique identifier for the sales analytics entry.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier for the related product variant.
        /// </summary>
        [Required]
        public int ProductVariantId { get; set; }

        /// <summary>
        /// Gets or sets the related product variant for which the analytics data is recorded.
        /// </summary>
        [ForeignKey(nameof(ProductVariantId))]
        public virtual ProductVariant ProductVariant { get; set; } = null!;

        /// <summary>
        /// Gets or sets the number of units sold for the product variant.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Units sold must be a positive number.")]
        public int UnitsSold { get; set; }

        /// <summary>
        /// Gets or sets the total revenue generated from the sales of the product variant.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total revenue must be a positive value.")]
        public decimal TotalRevenue { get; set; }

        /// <summary>
        /// Gets or sets the average selling price of the product variant.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Average selling price must be a positive value.")]
        public decimal AverageSellingPrice { get; set; }

        /// <summary>
        /// Gets or sets the date for which this sales analytics data applies (e.g., daily, monthly, yearly).
        /// </summary>
        [Required]
        public DateTime AnalysisDate { get; set; }

        /// <summary>
        /// Gets or sets any additional data or context, such as discounts, returns, etc.
        /// </summary>
        [MaxLength(1000, ErrorMessage = "Additional data cannot exceed 1000 characters.")]
        public string? AdditionalData { get; set; }
    }
}
