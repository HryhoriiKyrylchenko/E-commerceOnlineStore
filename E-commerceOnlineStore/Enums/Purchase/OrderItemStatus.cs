namespace E_commerceOnlineStore.Enums.Purchase
{
    /// <summary>
    /// Represents the status of an item within an order.
    /// </summary>
    public enum OrderItemStatus
    {
        /// <summary>
        /// The order item has been created but is yet to be processed.
        /// </summary>
        Pending,

        /// <summary>
        /// The order item has been shipped to the customer.
        /// </summary>
        Shipped,

        /// <summary>
        /// The order item has been canceled and will not be fulfilled.
        /// </summary>
        Canceled
    }

}
