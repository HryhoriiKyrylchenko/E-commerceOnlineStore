using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_commerceOnlineStore.Models.DataModels.Inventory
{
    /// <summary>
    /// Represents a position within a warehouse zone where an item is stored.
    /// </summary>
    [Table("ZonePositions")]
    public class ZonePosition
    {
        /// <summary>
        /// Gets or sets the zone pozition ID.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the zone Id where the inventory item is located.
        /// </summary>
        public int? ZoneId { get; set; }

        /// <summary>
        /// Gets or sets the zone where the inventory item is located.
        /// </summary>
        [ForeignKey(nameof(ZoneId))]
        public virtual WarehouseZone? Zone { get; set; }

        /// <summary>
        /// Gets or sets the aisle where the position is located.
        /// </summary>
        [MaxLength(10)]
        public string? Aisle { get; set; }

        /// <summary>
        /// Gets or sets the shelf number within the aisle.
        /// </summary>
        [MaxLength(10)]
        public string? Shelf { get; set; }

        /// <summary>
        /// Gets or sets the bin or specific location within the shelf.
        /// </summary>
        [MaxLength(10)]
        public string? Bin { get; set; }

        /// <summary>
        /// Gets or sets the collection of inventory item zones.
        /// </summary>
        public virtual ICollection<InventoryItemZone> InventoryItemZones { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of inventory movements where this zone was the source.
        /// </summary>
        [InverseProperty("SourceZonePosition")]
        public virtual ICollection<InventoryMovement> MovementsFromThisZone { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of inventory movements where this zone was the destination.
        /// </summary>
        [InverseProperty("DestinationZonePosition")]
        public virtual ICollection<InventoryMovement> MovementsToThisZone { get; set; } = [];
    }
}
