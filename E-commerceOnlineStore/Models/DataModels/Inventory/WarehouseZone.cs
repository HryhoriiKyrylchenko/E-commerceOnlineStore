using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_commerceOnlineStore.Models.DataModels.Inventory
{
    /// <summary>
    /// Represents a warehouse zone or section.
    /// </summary>
    [Table("WarehouseZones")]
    public class WarehouseZone
    {
        /// <summary>
        /// Gets or sets the warehouse zone ID.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the warehouse zone.
        /// </summary>
        [Required]
        public string ZoneName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the warehouse Id associated with this zone.
        /// </summary>
        [Required]
        public int WarehouseId { get; set; }

        /// <summary>
        /// Gets or sets the warehouse associated with this zone.
        /// </summary>
        [ForeignKey(nameof(WarehouseId))]
        public virtual Warehouse Warehouse { get; set; } = null!;

        /// <summary>
        /// Gets or sets the collection of zone positions in this zone.
        /// </summary>
        public virtual ICollection<ZonePosition> ZonePositions { get; set; } = [];
    }
}
