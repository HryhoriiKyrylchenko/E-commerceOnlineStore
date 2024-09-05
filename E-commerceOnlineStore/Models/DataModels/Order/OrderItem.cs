using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_commerceOnlineStore.Models.DataModels.Product;

namespace E_commerceOnlineStore.Models.DataModels.Order
{
    /// <summary>
    /// Represents an order item entity.
    /// </summary>
    [Table("OrderItems")]
    public class OrderItem
    {
        /// <summary>
        /// Gets or sets the order item ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the order ID associated with the order item.
        /// </summary>
        [Required]
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the order associated with the order item.
        /// </summary>
        public Order Order { get; set; } = null!;

        /// <summary>
        /// Gets or sets the product ID associated with the order item.
        /// </summary>
        [Required]
        public int ProductVariantId { get; set; }

        /// <summary>
        /// Gets or sets the product variant associated with the order item.
        /// </summary>
        public ProductVariant ProductVariant { get; set; } = null!;

        /// <summary>
        /// Gets or sets the quantity of the product in the order item.
        /// </summary>
        [Required]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product in the order item.
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalUnitPrice { get; set; }
    }
}
