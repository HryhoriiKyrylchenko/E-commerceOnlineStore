namespace E_commerceOnlineStore.Models.RequestModels.Account
{
    /// <summary>
    /// Represents the model used to change a user's email address.
    /// </summary>
    public class ChangeEmailModel
    {
        /// <summary>
        /// Gets or sets the user's current email address.
        /// </summary>
        public string CurrentEmail { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the new email address that the user wishes to set.
        /// </summary>
        public string NewEmail { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password for verifying the user's identity.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
