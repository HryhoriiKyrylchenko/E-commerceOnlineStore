namespace E_commerceOnlineStore.Models.RequestModels.Account
{
    /// <summary>
    /// Represents the model used for confirming a user's email address.
    /// </summary>
    public class ConfirmEmailModel
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user whose email is being confirmed.
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the token used to confirm the user's email address.
        /// </summary>
        public string Token { get; set; } = string.Empty;
    }

}
