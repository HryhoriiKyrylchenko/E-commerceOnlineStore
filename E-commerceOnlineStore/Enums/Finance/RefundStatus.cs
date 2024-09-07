namespace E_commerceOnlineStore.Enums.Finance
{
    /// <summary>
    /// Represents the different statuses of a refund request.
    /// </summary>
    public enum RefundStatus
    {
        /// <summary>
        /// Indicates that the refund request is pending and awaiting processing.
        /// </summary>
        Pending,

        /// <summary>
        /// Indicates that the refund request has been processed and completed.
        /// </summary>
        Processed,

        /// <summary>
        /// Indicates that the refund request has been rejected.
        /// </summary>
        Rejected
    }

}
