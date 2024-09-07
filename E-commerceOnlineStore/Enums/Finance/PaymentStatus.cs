namespace E_commerceOnlineStore.Enums.Finance
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
    }
}
