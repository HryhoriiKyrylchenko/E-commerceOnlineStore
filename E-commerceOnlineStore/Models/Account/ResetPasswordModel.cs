namespace E_commerceOnlineStore.Models.Account
{
    /// <summary>
    /// Represents the data model used for resetting a user's password.
    /// </summary>
    public class ResetPasswordModel
    {
        /// <summary>
        /// Gets or sets the email address of the user requesting the password reset.
        /// This property is initialized to an empty string.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the token used to validate the password reset request.
        /// This property is initialized to an empty string.
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the new password that the user wants to set.
        /// This property is initialized to an empty string.
        /// </summary>
        public string NewPassword { get; set; } = string.Empty;
    }

}
