using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_commerceOnlineStore.Models.DataModels.UserManagement;

namespace E_commerceOnlineStore.Models.DataModels.Discounts
{
    /// <summary>
    /// Represents the many-to-many relationship between customers and coupons.
    /// </summary>
    [Table("CustomersCoupons")]
    public class CustomerCoupon
    {
        /// <summary>
        /// Gets or sets the customer ID.
        /// </summary>
        [Required]
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the customer associated with the coupon.
        /// </summary>
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer Customer { get; set; } = null!;

        /// <summary>
        /// Gets or sets the Coupon ID.
        /// </summary>
        [Required]
        public int CouponId { get; set; }

        /// <summary>
        /// Gets or sets the coupon associated with the customer.
        /// </summary>
        [ForeignKey(nameof(CouponId))]
        public virtual Coupon Coupon { get; set; } = null!;
    }
}
