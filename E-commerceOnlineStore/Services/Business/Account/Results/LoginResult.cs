namespace E_commerceOnlineStore.Services.Business.Account.Results
{
    /// <summary>
    /// Represents the result of a login operation.
    /// </summary>
    /// <param name="Succeeded">Indicates whether the login operation was successful.</param>
    /// <param name="Token">The access token generated upon successful login.</param>
    /// <param name="RefreshToken">The refresh token generated upon successful login.</param>
    public record LoginResult(bool Succeeded, string Token, string RefreshToken);
}
