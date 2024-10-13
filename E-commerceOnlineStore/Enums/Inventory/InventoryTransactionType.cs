namespace E_commerceOnlineStore.Enums.Inventory
{
    /// <summary>
    /// Represents the types of inventory transactions that can occur in the system.
    /// </summary>
    public enum InventoryTransactionType
    {
        /// <summary>
        /// Inventory items have been received, increasing the stock.
        /// </summary>
        Received,

        /// <summary>
        /// Inventory items have been sold, decreasing the stock.
        /// </summary>
        Sold,

        /// <summary>
        /// Inventory stock has been adjusted (e.g., due to corrections or discrepancies).
        /// </summary>
        Adjusted,

        /// <summary>
        /// Inventory items have been returned, typically increasing the stock.
        /// </summary>
        Returned
    }
}
