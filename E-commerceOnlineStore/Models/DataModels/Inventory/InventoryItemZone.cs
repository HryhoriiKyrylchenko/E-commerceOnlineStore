using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_commerceOnlineStore.Models.DataModels.Inventory
{
    /// <summary>
    /// Represents the relationship between an inventory item and a zone position.
    /// </summary>
    [Table("InventoryItemZones")]
    public class InventoryItemZone
    {
        /// <summary>
        /// Gets or sets the inventory item ID.
        /// </summary>
        [Key]
        public int InventoryItemId { get; set; }

        /// <summary>
        /// Gets or sets the inventory item associated with this zone.
        /// </summary>
        [ForeignKey(nameof(InventoryItemId))]
        public virtual InventoryItem InventoryItem { get; set; } = null!;

        /// <summary>
        /// Gets or sets the zone position ID.
        /// </summary>
        public int ZonePositionId { get; set; }

        /// <summary>
        /// Gets or sets the zone position associated with this inventory item.
        /// </summary>
        [ForeignKey(nameof(ZonePositionId))]
        public virtual ZonePosition ZonePosition { get; set; } = null!;

        /// <summary>
        /// Gets or sets the quantity of inventory items in this zone position.
        /// </summary>
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative.")]
        public int Quantity { get; set; }
    }
}
