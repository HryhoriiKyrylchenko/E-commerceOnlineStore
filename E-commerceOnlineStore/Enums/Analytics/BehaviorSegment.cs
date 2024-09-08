namespace E_commerceOnlineStore.Enums.Analytics
{
    /// <summary>
    /// Represents different behavioral segments used for customer segmentation based on purchasing behavior.
    /// </summary>
    public enum BehaviorSegment
    {
        /// <summary>
        /// Represents customers who make purchases infrequently.
        /// </summary>
        InfrequentBuyer,

        /// <summary>
        /// Represents customers who make frequent purchases.
        /// </summary>
        FrequentBuyer,

        /// <summary>
        /// Represents customers who spend a high amount on purchases.
        /// </summary>
        HighSpender,

        /// <summary>
        /// Represents customers who spend a lower amount on purchases.
        /// </summary>
        LowSpender,

        /// <summary>
        /// Represents loyal customers who consistently make purchases.
        /// </summary>
        LoyalCustomer,

        /// <summary>
        /// Represents new customers who have recently made their first purchase.
        /// </summary>
        NewCustomer
    }
}
