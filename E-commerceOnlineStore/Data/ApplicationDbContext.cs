using E_commerceOnlineStore.Models.DataModels.Account;
using E_commerceOnlineStore.Models.DataModels.CartsAndWishlists;
using E_commerceOnlineStore.Models.DataModels.Common;
using E_commerceOnlineStore.Models.DataModels.Discounts;
using E_commerceOnlineStore.Models.DataModels.Finance;
using E_commerceOnlineStore.Models.DataModels.Notifications;
using E_commerceOnlineStore.Models.DataModels.Purchase;
using E_commerceOnlineStore.Models.DataModels.Products;
using E_commerceOnlineStore.Models.DataModels.Shipping;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using E_commerceOnlineStore.Models.DataModels.Configuration;
using E_commerceOnlineStore.Models.DataModels.LogModels;

namespace E_commerceOnlineStore.Data
{
    /// <summary>
    /// Represents the application's database context.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            // Ensures that the database for the context exists.
            Database.EnsureCreated();
        }

        /// <summary>
        /// Gets or sets the addresses in the database.
        /// </summary>
        public DbSet<Address> Addresses { get; set; }

        /// <summary>
        /// Gets or sets the users in the database.
        /// </summary>
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        /// <summary>
        /// Gets or sets the categories in the database.
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the categories and discounts in the database.
        /// </summary>
        public DbSet<CategoryDiscount> CategoriesDiscounts { get; set; }

        /// <summary>
        /// Gets or sets the coupons in the database.
        /// </summary>
        public DbSet<Coupon> Coupons { get; set; }

        /// <summary>
        /// Gets or sets the customers in the database.
        /// </summary>
        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// Gets or sets the customers related with coupons in the database.
        /// </summary>
        public DbSet<CustomerCoupon> CustomersCoupons { get; set; }

        /// <summary>
        /// Gets or sets the deliveries in the database.
        /// </summary>
        public DbSet<Shipment> Shipments { get; set; }

        /// <summary>
        /// Gets or sets the methods of the delivery in the database.
        /// </summary>
        public DbSet<ShippingMethod> ShippingMethods { get; set; }

        /// <summary>
        /// Gets or sets the discounts in the database.
        /// </summary>
        public DbSet<Discount> Discounts { get; set; }

        /// <summary>
        /// Gets or sets the employees in the database.
        /// </summary>
        public DbSet<Customer> Employees { get; set; }

        /// <summary>
        /// Gets or sets the notifications in the database.
        /// </summary>
        public DbSet<Notification> Notifications { get; set; }

        /// <summary>
        /// Gets or sets the newsletter subscriptions in the database.
        /// </summary>
        public DbSet<NewsletterSubscription> NewsletterSubscriptions { get; set; }

        /// <summary>
        /// Gets or sets the orders in the database.
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Gets or sets the order items in the database.
        /// </summary>
        public DbSet<OrderItem> OrderItems { get; set; }

        /// <summary>
        /// Gets or sets the payments in the database.
        /// </summary>
        public DbSet<Payment> Payments { get; set; }

        /// <summary>
        /// Gets or sets the payment methods in the database.
        /// </summary>
        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        /// <summary>
        /// Gets or sets the products in the database.
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Represents the collection of product bundles in the database.
        /// </summary>
        public DbSet<ProductBundle> ProductBundles { get; set; }

        /// <summary>
        /// Represents the collection of items that belong to product bundles in the database.
        /// </summary>
        public DbSet<ProductBundleItem> ProductBundleItems { get; set; }

        /// <summary>
        /// Gets or sets the product discounts in the database.
        /// </summary>
        public DbSet<ProductDiscount> ProductsDiscounts { get; set; }

        /// <summary>
        /// Gets or sets the product variant discounts in the database.
        /// </summary>
        public DbSet<ProductVariantDiscount> ProductVariantsDiscounts { get; set; }

        /// <summary>
        /// Gets or sets the product images in the database.
        /// </summary>
        public DbSet<ProductImage> ProductImages { get; set; }

        /// <summary>
        /// Gets or sets the product reviews in the database.
        /// </summary>
        public DbSet<ProductReview> ProductReviews { get; set; }

        /// <summary>
        /// Gets or sets the product variants in the database.
        /// </summary>
        public DbSet<ProductVariant> ProductVariants { get; set; }

        /// <summary>
        /// Gets or sets the product variant attributes in the database.
        /// </summary>
        public DbSet<ProductVariantAttribute> ProductVariantAttributes { get; set; }

        /// <summary>
        /// Gets or sets the products and tags in the database.
        /// </summary>
        public DbSet<ProductTag> ProductsTags { get; set; }

        /// <summary>
        /// Gets or sets the refresh token in the database.
        /// </summary>
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        /// <summary>
        /// Gets or sets the refund in the database.
        /// </summary>
        public DbSet<Refund> Refunds { get; set; }

        /// <summary>
        /// Gets or sets the return requests in the database.
        /// </summary>
        public DbSet<ReturnRequest> ReturnRequests { get; set; }

        /// <summary>
        /// Gets or sets the return request items in the database.
        /// </summary>
        public DbSet<ReturnRequestItem> ReturnRequestItems { get; set; }

        /// <summary>
        /// Gets or sets the shopping carts in the database.
        /// </summary>
        public DbSet<Settings> Settings { get; set; }

        /// <summary>
        /// Gets or sets the shopping carts in the database.
        /// </summary>
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        /// <summary>
        /// Gets or sets the shopping cart items in the database.
        /// </summary>
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        /// <summary>
        /// Gets or sets the tags in the database.
        /// </summary>
        public DbSet<Tag> Tags { get; set; }

        /// <summary>
        /// Gets or sets the taxes in the database.
        /// </summary>
        public DbSet<Tax> Taxes { get; set; }

        /// <summary>
        /// Gets or sets the transaction in the database.
        /// </summary>
        public DbSet<Transaction> Transactions { get; set; }

        /// <summary>
        /// Gets or sets the wishlist item in the database.
        /// </summary>`
        public DbSet<Favorite> Favorites { get; set; }

        /// <summary>
        /// Gets or sets the user activity log in the database.
        /// </summary>
        public DbSet<UserActivityLog> UserActivityLogs { get; set; }

        /// <summary>
        /// Gets or sets the audit log in the database.
        /// </summary>
        public DbSet<AuditLog> AuditLogs { get; set; }

        /// <summary>
        /// Gets or sets the event log in the database.
        /// </summary>
        public DbSet<EventLog> EventLogs { get; set; }

        /// <summary>
        /// Configures the model that was discovered by convention from the entity types exposed in <see cref="DbSet{TEntity}"/> properties on this context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Name = "Manager", NormalizedName = "MANAGER" },
                new IdentityRole { Name = "Customer", NormalizedName = "CUSTOMER" }
            );
        }
    }
}
