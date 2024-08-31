namespace E_commerceOnlineStore.Enums
{
    /// <summary>
    /// Represents the status of a payment in the system.
    /// </summary>
    public enum PaymentStatus
    {
        /// <summary>
        /// The payment is pending and has not yet been completed.
        /// </summary>
        Pending,

        /// <summary>
        /// The payment has been successfully completed.
        /// </summary>
        Completed,

        /// <summary>
        /// The payment attempt failed.
        /// </summary>
        Failed,

        /// <summary>
        /// The payment was refunded to the customer.
        /// </summary>
        Refunded
    }
}
