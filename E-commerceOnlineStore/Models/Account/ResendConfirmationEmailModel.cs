namespace E_commerceOnlineStore.Models.Account
{
    /// <summary>
    /// Model class representing the data required to resend a confirmation email.
    /// </summary>
    public class ResendConfirmationEmailModel
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user to whom the confirmation email will be sent.
        /// </summary>
        public string UserId { get; set; } = string.Empty;
    }
}
