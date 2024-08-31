using E_commerceOnlineStore.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace E_commerceOnlineStore.Models
{
    /// <summary>
    /// Represents a customer entity.
    /// </summary>
    [Table("Customers")]
    public class Customer : ApplicationUser
    {
        /// <summary>
        /// Gets or sets the unique identifier provided by Google for the user.
        /// This property is nullable to accommodate users who may not use Google for authentication.
        /// </summary>
        public string? GoogleId { get; set; }

        /// <summary>
        /// Gets or sets the collection of orders associated with the user.
        /// This collection is initialized to an empty list to avoid null reference issues.
        /// </summary>
        public virtual ICollection<Order> Orders { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of products in the user's wishlist.
        /// This collection is initialized to an empty list to avoid null reference issues.
        /// </summary>
        public virtual ICollection<Product> Wishlist { get; set; } = [];

        /// <summary>
        /// Gets or sets the ID of the last used payment method.
        /// </summary>
        public int? LastPaymentMethodId { get; set; }

        /// <summary>
        /// Gets or sets the last used payment method.
        /// </summary>
        [ForeignKey(nameof(LastPaymentMethodId))]
        public virtual PaymentMethod? LastPaymentMethod { get; set; }

        /// <summary>
        /// Gets or sets the ID of the last used delivery method.
        /// </summary>
        public int? LastShippingMethodId { get; set; }

        /// <summary>
        /// Gets or sets the last used delivery method.
        /// </summary>
        [ForeignKey(nameof(LastShippingMethodId))]
        public virtual ShippingMethod? LastShippingMethod { get; set; }

        /// <summary>
        /// Gets or sets the collection of customer coupons associated with the coupon.
        /// </summary>
        public virtual ICollection<CustomerCoupon> CustomersCoupons { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of product reviews associated with the coupon.
        /// </summary>
        public virtual ICollection<ProductReview> ProductReviews { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of shopping carts associated with the coupon.
        /// </summary>
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of wishlist items associated with the customer.
        /// </summary>
        public virtual ICollection<WishlistItem> WishlistItems { get; set; } = [];
    }
}
