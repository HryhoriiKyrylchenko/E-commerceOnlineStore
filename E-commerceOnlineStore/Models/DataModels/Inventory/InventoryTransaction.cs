using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using E_commerceOnlineStore.Enums.Inventory;

namespace E_commerceOnlineStore.Models.DataModels.Inventory
{
    /// <summary>
    /// Represents a transaction that alters the stock level of an inventory item.
    /// </summary>
    [Table("InventoryTransactions")]
    public class InventoryTransaction
    {
        /// <summary>
        /// Gets or sets the inventory transaction ID.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the inventory receipt that the transaction is associated with.
        /// </summary>
        [Required]
        public int InventoryReceiptId { get; set; }

        /// <summary>
        /// Gets or sets the inventory receipt that the transaction is associated with.
        /// </summary>
        [ForeignKey(nameof(InventoryReceiptId))]
        public virtual InventoryReceipt InventoryReceipt { get; set; } = null!;

        /// <summary>
        /// Gets or sets the ID of the inventory item that the transaction is associated with.
        /// </summary>
        [Required]
        public int InventoryItemId { get; set; }

        /// <summary>
        /// Gets or sets the inventory item that the transaction is associated with.
        /// </summary>
        [ForeignKey(nameof(InventoryItemId))]
        public virtual InventoryItem InventoryItem { get; set; } = null!;

        /// <summary>
        /// Gets or sets the type of the transaction (e.g., "Received", "Sold", "Adjusted").
        /// </summary>
        [Required]
        public InventoryTransactionType TransactionType { get; set; }

        /// <summary>
        /// Gets or sets the quantity of items involved in the transaction.
        /// </summary>
        [Required]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the transaction occurred.
        /// </summary>
        [Required]
        public DateTime TransactionDate { get; set; }
    }
}
