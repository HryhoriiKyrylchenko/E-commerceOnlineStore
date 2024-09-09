using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_commerceOnlineStore.Enums.Products;
using E_commerceOnlineStore.Models.DataModels.UserManagement;
using E_commerceOnlineStore.Models.DataModels.Products;

namespace E_commerceOnlineStore.Models.DataModels.CartsAndFavourites
{
    /// <summary>
    /// Represents an item in a user's favorite.
    /// </summary>
    [Table("Favorites")]
    public class Favorite
    {
        /// <summary>
        /// Gets or sets the unique identifier for the favorite item.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user who added the item to the favorite.
        /// </summary>
        [Required]
        public string CustomerId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the <see cref="Customer"/> who owns the favorite item.
        /// </summary>
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer Customer { get; set; } = null!;

        /// <summary>
        /// Gets or sets the date and time when the item was added to the favorite.
        /// The default value is set to the current UTC date and time.
        /// </summary>
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the unique identifier of the favourite type that is added to the favorite.
        /// </summary>
        [Required]
        public FavoriteType FavoriteType { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the product variant that is added to the favorite.
        /// This is required if the FavoriteType is Product.
        /// </summary>
        public int? ProductVariantId { get; set; }

        /// <summary>
        /// Gets or sets the related product variant if the item is a product.
        /// </summary>
        [ForeignKey(nameof(ProductVariantId))]
        public virtual ProductVariant? ProductVariant { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the category that is added to the favorite.
        /// This is required if the FavoriteType is Category.
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the related category if the item is a category.
        /// </summary>
        [ForeignKey(nameof(CategoryId))]
        public virtual ProductCategory? Category { get; set; }
    }
}
