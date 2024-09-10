using E_commerceOnlineStore.Enums;
using E_commerceOnlineStore.Models.DataModels.Analytics;
using E_commerceOnlineStore.Models.DataModels.CartsAndFavourites;
using E_commerceOnlineStore.Models.DataModels.Discounts;
using E_commerceOnlineStore.Models.DataModels.Finance;
using E_commerceOnlineStore.Models.DataModels.Products;
using E_commerceOnlineStore.Models.DataModels.Purchase;
using E_commerceOnlineStore.Models.DataModels.Shipping;
using E_commerceOnlineStore.Models.DataModels.Support;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace E_commerceOnlineStore.Models.DataModels.UserManagement
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
        /// Gets or sets the collection of customer payment methods associated with the coupon.
        /// </summary>
        public virtual ICollection<CustomerPaymentMethod> CustomerPaymentMethods { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of customer shipping methods associated with the coupon.
        /// </summary>
        public virtual ICollection<CustomerShippingMethod> CustomerShippingMethods { get; set; } = [];

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
        /// Gets or sets the collection of favorite products associated with the customer.
        /// </summary>
        public virtual ICollection<ProductFavorite> ProductFavorites { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of favorite categories associated with the customer.
        /// </summary>
        public virtual ICollection<CategoryFavorite> CategoryFavorites { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of support tickets associated with the customer.
        /// </summary>
        public virtual ICollection<SupportTicket> SupportTickets { get; set; } = [];

        /// <summary>
        /// Gets or sets the customer segmentation associated with the customer.
        /// </summary>
        public virtual CustomerSegmentation? CustomerSegmentation { get; set; }
    }
}
