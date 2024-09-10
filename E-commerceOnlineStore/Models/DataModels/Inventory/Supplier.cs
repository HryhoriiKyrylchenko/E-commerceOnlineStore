using E_commerceOnlineStore.Models.DataModels.Shipping;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_commerceOnlineStore.Models.DataModels.Inventory
{
    /// <summary>
    /// Represents a supplier who provides products to the warehouse.
    /// </summary>
    [Table("Suppliers")]
    public class Supplier
    {
        /// <summary>
        /// Gets or sets the unique identifier for the supplier.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the supplier.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a description of the supplier.
        /// </summary>
        [MaxLength(1000)]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the collection of inventory receipts from this supplier.
        /// </summary>
        public virtual ICollection<InventoryReceipt> InventoryReceipt { get; set; } = [];
    }

}
