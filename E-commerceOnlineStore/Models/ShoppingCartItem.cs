﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_commerceOnlineStore.Models
{
    /// <summary>
    /// Represents an item in the shopping cart.
    /// </summary>
    [Table("ShoppingCartItems")]
    public class ShoppingCartItem
    {
        /// <summary>
        /// Gets or sets the shopping cart item ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the shopping cart ID associated with the item.
        /// </summary>
        [Required]
        public int ShoppingCartId { get; set; }

        /// <summary>
        /// Gets or sets the shopping cart associated with the item.
        /// </summary>
        [ForeignKey(nameof(ShoppingCartId))]
        public virtual ShoppingCart ShoppingCart { get; set; } = null!;

        /// <summary>
        /// Gets or sets the product ID associated with the item.
        /// </summary>
        [Required]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product associated with the item.
        /// </summary>
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; } = null!;

        /// <summary>
        /// Gets or sets the quantity of the product in the shopping cart.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the date when the item was added to the cart.
        /// </summary>
        [Required]
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the price of the product at the time it was added to the cart.
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
