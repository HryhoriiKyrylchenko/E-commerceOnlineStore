using E_commerceOnlineStore.Models.DataModels.Products;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_commerceOnlineStore.Models.DataModels.Inventory
{
    /// <summary>
    /// Represents the stock of a specific product in a warehouse.
    /// </summary>
    [Table("InventoryItems")]
    public class InventoryItem
    {
        /// <summary>
        /// Gets or sets the zone inventory item ID.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the product variant Id associated with this inventory item.
        /// </summary>
        [Required]
        public int ProductVariantId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the product variant associated with this inventory item.
        /// </summary>
        [ForeignKey(nameof(ProductVariantId))]
        public virtual ProductVariant ProductVariant { get; set; } = null!;

        /// <summary>
        /// Gets or sets the stock quantity for the inventory item.
        /// The stock is required and cannot be a negative value.
        /// </summary>
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative.")]
        public int Stock { get; set; }

        /// <summary>
        /// Gets or sets the value of a single item in stock.
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }

        /// <summary>
        /// Gets or sets the minimum stock level for reordering.
        /// </summary>
        public int ReorderLevel { get; set; } = 0;

        /// <summary>
        /// Gets or sets the collection of inventory item zones.
        /// </summary>
        public virtual ICollection<InventoryItemZone> InventoryItemZones { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of inventory movements.
        /// </summary>
        public virtual ICollection<InventoryMovement> InventoryMovements { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of inventory transactions for this item.
        /// </summary>
        public virtual ICollection<InventoryTransaction> Transactions { get; set; } = [];
    }
}
