namespace E_commerceOnlineStore.Models.Account
{
    /// <summary>
    /// Represents the data model used for initiating a password reset request.
    /// </summary>
    public class ForgotPasswordModel
    {
        /// <summary>
        /// Gets or sets the email address of the user who has forgotten their password.
        /// This property is initialized to an empty string.
        /// </summary>
        public string Email { get; set; } = string.Empty;
    }

}
