using E_commerceOnlineStore.Models.DataModels.UserManagement;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using E_commerceOnlineStore.Models.DataModels.Common;

namespace E_commerceOnlineStore.Models.DataModels.Inventory
{
    /// <summary>
    /// Represents a physical warehouse where products are stored.
    /// </summary>
    [Table("Warehouses")]
    public class Warehouse
    {
        /// <summary>
        /// Gets or sets the warehouse ID.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the warehouse.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a description of the warehouse.
        /// </summary>
        [MaxLength(255)]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the address associated with the warehouse.
        /// </summary>
        [Required]
        public int AddressId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Address"/> associated with the warehouse.
        /// </summary>
        [ForeignKey(nameof(AddressId))]
        public virtual Address Address { get; set; } = null!;

        /// <summary>
        /// Gets or sets the collection of warehouse zones in the warehouse.
        /// </summary>
        public virtual ICollection<WarehouseZone> WarehouseZones { get; set; } = [];
    }
}
