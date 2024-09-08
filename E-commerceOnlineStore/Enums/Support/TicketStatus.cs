namespace E_commerceOnlineStore.Enums.Support
{
    /// <summary>
    /// Represents the different statuses a support ticket can have.
    /// </summary>
    public enum TicketStatus
    {
        /// <summary>
        /// Indicates that the ticket is newly created and has not been reviewed yet.
        /// </summary>
        New,

        /// <summary>
        /// Indicates that the ticket is currently being worked on.
        /// </summary>
        InProgress,

        /// <summary>
        /// Indicates that the ticket is on hold and no action is being taken at the moment.
        /// </summary>
        OnHold,

        /// <summary>
        /// Indicates that the issue described in the ticket has been resolved.
        /// </summary>
        Resolved,

        /// <summary>
        /// Indicates that the ticket has been closed and no further action is needed.
        /// </summary>
        Closed
    }
}
