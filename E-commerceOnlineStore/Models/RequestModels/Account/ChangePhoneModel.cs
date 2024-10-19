namespace E_commerceOnlineStore.Models.RequestModels.Account
{
    /// <summary>
    /// Represents the model used to change the user's phone number.
    /// </summary>
    public class ChangePhoneModel
    {
        /// <summary>
        /// Gets or sets the new phone number for the user.
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;
    }

}
