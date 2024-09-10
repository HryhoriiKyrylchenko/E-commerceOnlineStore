using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_commerceOnlineStore.Enums.Products;
using E_commerceOnlineStore.Models.DataModels.UserManagement;
using E_commerceOnlineStore.Models.DataModels.Products;

namespace E_commerceOnlineStore.Models.DataModels.CartsAndFavourites
{
    /// <summary>
    /// Represents a product that a customer has marked as a favorite.
    /// </summary>
    [Table("ProductFavorites")]
    public class ProductFavorite
    {
        /// <summary>
        /// Gets or sets the unique identifier for the favorite entry.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the customer who marked the product as a favorite.
        /// </summary>
        [Required]
        public string CustomerId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the customer who has marked the product as a favorite.
        /// </summary>
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer Customer { get; set; } = null!;

        /// <summary>
        /// Gets or sets the unique identifier of the product variant that has been marked as a favorite.
        /// </summary>
        [Required]
        public int ProductVariantId { get; set; }

        /// <summary>
        /// Gets or sets the product variant that has been marked as a favorite.
        /// </summary>
        [ForeignKey(nameof(ProductVariantId))]
        public virtual ProductVariant ProductVariant { get; set; } = null!;

        /// <summary>
        /// Gets or sets the date and time when the product was added to the favorites.
        /// </summary>
        [Required]
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
    }
}
