namespace E_commerceOnlineStore.Models.RequestModels.Account
{
    /// <summary>
    /// Represents the model used to change a user's password.
    /// </summary>
    public class ChangePasswordModel
    {
        /// <summary>
        /// Gets or sets the user's current password.
        /// </summary>
        public string CurrentPassword { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the new password that the user wishes to set.
        /// </summary>
        public string NewPassword { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the confirmation of the new password.
        /// </summary>
        public string ConfirmPassword { get; set; } = string.Empty;
    }

}
