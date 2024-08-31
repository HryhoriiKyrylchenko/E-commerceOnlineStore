using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_commerceOnlineStore.Models
{
    /// <summary>
    /// Represents a coupon entity.
    /// </summary>
    [Table("Coupons")]
    public class Coupon
    {
        /// <summary>
        /// Gets or sets the coupon ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the coupon code.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the discount percentage (0-100).
        /// </summary>
        [Range(0, 100, ErrorMessage = "Discount percentage must be between 0 and 100.")]
        public decimal? Percentage { get; set; }

        /// <summary>
        /// Gets or sets the discount amount.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Amount { get; set; }

        /// <summary>
        /// Gets or sets the minimum order amount required to apply the coupon.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal? MinimumOrderAmount { get; set; }

        /// <summary>
        /// Gets or sets the start date of the coupon.
        /// </summary>
        [Required]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the coupon.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of uses for the coupon.
        /// </summary>
        public int? MaxUses { get; set; }

        /// <summary>
        /// Gets or sets the number of times the coupon has been used.
        /// </summary>
        public int TimesUsed { get; set; } = 0;

        /// <summary>
        /// Gets or sets whether the coupon is active.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Gets or sets the collection of customer coupons associated with the coupon.
        /// </summary>
        public virtual ICollection<CustomerCoupon> CustomersCoupons { get; set; } = [];
    }
}
