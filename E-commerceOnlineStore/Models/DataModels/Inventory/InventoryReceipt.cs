using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace E_commerceOnlineStore.Models.DataModels.Inventory
{
    /// <summary>
    /// Represents an inbound shipment of inventory items from a supplier.
    /// </summary>
    [Table("InventoryReceipts")]
    public class InventoryReceipt
    {
        /// <summary>
        /// Gets or sets the unique identifier for the inventory receipt.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the inventory receipt was created.
        /// </summary>
        [Required]
        public DateTime ReceiptDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the supplier for this receipt.
        /// </summary>
        [Required]
        public int SupplierId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Supplier"/> associated with this inventory receipt.
        /// </summary>
        [ForeignKey(nameof(SupplierId))]
        public virtual Supplier Supplier { get; set; } = null!;

        /// <summary>
        /// Gets or sets the delivery cost associated with this receipt.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal DeliveryCost { get; set; }

        /// <summary>
        /// Gets or sets the collection of inventory transactions associated with this receipt.
        /// </summary>
        public virtual ICollection<InventoryTransaction> InventoryTransactions { get; set; } = [];
    }
}
