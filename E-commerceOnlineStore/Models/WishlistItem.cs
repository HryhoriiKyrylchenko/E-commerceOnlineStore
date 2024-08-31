using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerceOnlineStore.Models
{
    /// <summary>
    /// Represents an item in a user's wishlist.
    /// </summary>
    [Table("WishlistItem")]
    public class WishlistItem
    {
        /// <summary>
        /// Gets or sets the unique identifier for the wishlist item.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user who added the item to the wishlist.
        /// </summary>
        [Required]
        [ForeignKey("Customer")]
        public string CustomerId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the <see cref="Customer"/> who owns the wishlist item.
        /// </summary>
        public virtual Customer Customer { get; set; } = null!;

        /// <summary>
        /// Gets or sets the unique identifier of the product that is added to the wishlist.
        /// </summary>
        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Product"/> that is added to the wishlist.
        /// </summary>
        public virtual Product Product { get; set; } = null!;

        /// <summary>
        /// Gets or sets the date and time when the item was added to the wishlist.
        /// The default value is set to the current UTC date and time.
        /// </summary>
        [Required]
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
    }
}
