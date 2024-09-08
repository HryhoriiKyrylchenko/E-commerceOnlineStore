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
        [ForeignKey("Customer")]
        public string CustomerId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the <see cref="Customer"/> who owns the favorite item.
        /// </summary>
        public virtual Customer Customer { get; set; } = null!;


        /// <summary>
        /// Gets or sets the date and time when the item was added to the favorite.
        /// The default value is set to the current UTC date and time.
        /// </summary>
        [Required]
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the unique identifier of the favourite type that is added to the favorite.
        /// </summary>
        [Required]
        public FavoriteType FavoriteType { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the product that is added to the favorite.
        /// </summary>
        [Required]
        [ForeignKey("ProductVariant")]
        public int ProductVariantId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Product"/> that is added to the favorite.
        /// </summary>
        public virtual ProductVariant ProductVariant { get; set; } = null!;

        /// <summary>
        /// Gets or sets the unique identifier of the ProductCategory that is added to the favorite.
        /// </summary>
        [Required]
        [ForeignKey("ProductVariant")]
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Category"/> that is added to the favorite.
        /// </summary>
        public virtual ProductCategory Category { get; set; } = null!;
    }
}
