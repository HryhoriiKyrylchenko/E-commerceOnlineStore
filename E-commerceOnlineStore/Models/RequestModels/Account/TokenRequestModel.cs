namespace E_commerceOnlineStore.Models.RequestModels.Account
{
    /// <summary>
    /// Represents a request model for refreshing a JWT token.
    /// </summary>
    public class TokenRequestModel
    {
        /// <summary>
        /// Gets or sets the JWT token that needs to be refreshed.
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the refresh token used to generate a new JWT token.
        /// </summary>
        public string RefreshToken { get; set; } = string.Empty;
    }

}
