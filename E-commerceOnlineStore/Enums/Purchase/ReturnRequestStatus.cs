namespace E_commerceOnlineStore.Enums.Purchase
{
    /// <summary>
    /// Represents the status of a return request in the system.
    /// </summary>
    public enum ReturnRequestStatus
    {
        /// <summary>
        /// The return request has been submitted and is awaiting review.
        /// </summary>
        Pending,

        /// <summary>
        /// The return request has been reviewed and approved.
        /// </summary>
        Approved,

        /// <summary>
        /// The return request has been reviewed and rejected.
        /// </summary>
        Rejected,

        /// <summary>
        /// The return request has been processed and completed.
        /// </summary>
        Completed
    }
}
