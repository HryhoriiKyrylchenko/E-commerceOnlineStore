namespace E_commerceOnlineStore.Models.RequestModels.Account
{
    /// <summary>
    /// Represents a request model for revoking a JWT refresh token.
    /// </summary>
    public class RevokeTokenRequestModel
    {
        /// <summary>
        /// Gets or sets the JWT refresh token that needs to be revoked.
        /// </summary>
        public string Token { get; set; } = string.Empty;
    }
}
