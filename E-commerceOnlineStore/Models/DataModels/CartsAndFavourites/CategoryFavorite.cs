using E_commerceOnlineStore.Models.DataModels.Products;
using E_commerceOnlineStore.Models.DataModels.UserManagement;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_commerceOnlineStore.Models.DataModels.CartsAndFavourites
{
    /// <summary>
    /// Represents a category that a customer has marked as a favorite.
    /// </summary>
    [Table("CategoryFavorites")]
    public class CategoryFavorite
    {
        /// <summary>
        /// Gets or sets the unique identifier for the favorite category entry.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the customer who marked the category as a favorite.
        /// </summary>
        [Required]
        public string CustomerId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the customer who has marked the category as a favorite.
        /// </summary>
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer Customer { get; set; } = null!;

        /// <summary>
        /// Gets or sets the unique identifier of the category that has been marked as a favorite.
        /// </summary>
        [Required]
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category that has been marked as a favorite.
        /// </summary>
        [ForeignKey(nameof(CategoryId))]
        public virtual ProductCategory Category { get; set; } = null!;

        /// <summary>
        /// Gets or sets the date and time when the category was added to the favorites.
        /// </summary>
        [Required]
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
    }

}
