namespace E_commerceOnlineStore.Enums.LogEnums
{
    /// <summary>
    /// Represents the different types of events that can occur within the system.
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// Indicates an informational event that provides general information or status updates.
        /// </summary>
        Information,

        /// <summary>
        /// Indicates a warning event that alerts users to potential issues or conditions that may need attention.
        /// </summary>
        Warning,

        /// <summary>
        /// Indicates an error event that signals a problem or failure that requires immediate attention.
        /// </summary>
        Error,

        /// <summary>
        /// Indicates an event that does not fall into the predefined categories and requires a custom or unspecified type.
        /// </summary>
        Other
    }
}
