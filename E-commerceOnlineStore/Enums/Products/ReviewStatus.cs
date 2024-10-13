namespace E_commerceOnlineStore.Enums.Products
{
    /// <summary>
    /// Represents the different statuses of a review in the system.
    /// </summary>
    public enum ReviewStatus
    {
        /// <summary>
        /// The review is pending and has not yet been evaluated.
        /// </summary>
        Pending,

        /// <summary>
        /// The review has been approved and is considered valid.
        /// </summary>
        Approved,

        /// <summary>
        /// The review has been rejected and will not be published.
        /// </summary>
        Rejected,

        /// <summary>
        /// The review has been deleted and is no longer available.
        /// </summary>
        Deleted
    }

}
