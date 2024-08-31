namespace E_commerceOnlineStore.Enums
{
    /// <summary>
    /// Represents the various stages of an order in the system.
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// The order has been placed but not yet processed.
        /// </summary>
        Pending,

        /// <summary>
        /// The order is currently being processed.
        /// </summary>
        Processing,

        /// <summary>
        /// The order has been shipped to the customer.
        /// </summary>
        Shipped,

        /// <summary>
        /// The order has been delivered to the customer.
        /// </summary>
        Delivered,

        /// <summary>
        /// The order has been canceled.
        /// </summary>
        Canceled
    }
}
