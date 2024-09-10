using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_commerceOnlineStore.Models.DataModels.Inventory
{
    /// <summary>
    /// Represents the movement of inventory items between different zones within the warehouse.
    /// </summary>
    [Table("InventoryMovements")]
    public class InventoryMovement
    {
        /// <summary>
        /// Gets or sets the inventory movement ID.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the item being moved.
        /// </summary>
        [Required]
        public int InventoryItemId { get; set; }

        /// <summary>
        /// Gets or sets the item being moved.
        /// </summary>
        [ForeignKey(nameof(InventoryItemId))]
        public virtual InventoryItem InventoryItem { get; set; } = null!;

        /// <summary>
        /// Gets or sets the ID of the zone from which the item is moved.
        /// This value can be null for newly received stock.
        /// </summary>
        public int? SourceZonePositionId { get; set; }

        /// <summary>
        /// Gets or sets the zone from which the item is moved.
        /// This value can be null for newly received stock.
        /// </summary>
        [ForeignKey(nameof(SourceZonePositionId))]
        [InverseProperty("MovementsFromThisZone")]
        public virtual ZonePosition? SourceZonePosition { get; set; }

        /// <summary>
        /// Gets or sets the ID of the zone to which the item is moved.
        /// </summary>
        [Required]
        public int DestinationZonePositionId { get; set; }

        /// <summary>
        /// Gets or sets the zone to which the item is moved.
        /// </summary>
        [ForeignKey(nameof(DestinationZonePositionId))]
        [InverseProperty("MovementsToThisZone")]
        public virtual ZonePosition DestinationZonePosition { get; set; } = null!;

        /// <summary>
        /// Gets or sets the quantity of items moved.
        /// </summary>
        [Required]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the movement occurred.
        /// </summary>
        [Required]
        public DateTime MovementDate { get; set; }
    }
}
