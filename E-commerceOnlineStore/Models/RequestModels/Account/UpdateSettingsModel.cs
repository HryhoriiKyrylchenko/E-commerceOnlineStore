namespace E_commerceOnlineStore.Models.RequestModels.Account
{
    /// <summary>
    /// Represents the model used to update user settings such as preferred language and time zone.
    /// </summary>
    public class UpdateSettingsModel
    {
        /// <summary>
        /// Gets or sets the preferred language of the user.
        /// </summary>
        public string? PreferredLanguage { get; set; }

        /// <summary>
        /// Gets or sets the time zone of the user.
        /// </summary>
        public string? TimeZone { get; set; }
    }

}
