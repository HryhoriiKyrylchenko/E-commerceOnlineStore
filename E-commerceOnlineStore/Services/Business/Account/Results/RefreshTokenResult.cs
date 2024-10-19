namespace E_commerceOnlineStore.Services.Business.Account.Results
{
    /// <summary>
    /// Represents the result of a refresh token operation.
    /// </summary>
    /// <param name="Succeeded">Indicates whether the operation was successful.</param>
    /// <param name="NewToken">The newly generated access token.</param>
    /// <param name="NewRefreshToken">The newly generated refresh token.</param>
    public record RefreshTokenResult(bool Succeeded, string NewToken, string NewRefreshToken);
}
