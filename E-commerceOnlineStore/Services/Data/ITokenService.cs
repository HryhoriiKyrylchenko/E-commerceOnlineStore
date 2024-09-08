using E_commerceOnlineStore.Models.DataModels.UserManagement;
using System.Security.Claims;

namespace E_commerceOnlineStore.Services.Data
{
    /// <summary>
    /// Defines methods for generating JSON Web Tokens (JWT) for application users.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generates a JWT for the specified user.
        /// </summary>
        /// <param name="user">The application user for whom the JWT is generated.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the generated JWT.</returns>
        Task<string> GenerateTokenAsync(ApplicationUser user);

        /// <summary>
        /// Generates a password reset token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom to generate the token.</param>
        /// <returns>The password reset token.</returns>
        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);

        /// <summary>
        /// Generates a new refresh token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom the refresh token is being generated.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the newly generated refresh token as a string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="user"/> parameter is null.</exception>
        Task<string> GenerateRefreshTokenAsync(ApplicationUser user);

        /// <summary>
        /// Generates an email confirmation token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom the email confirmation token is being generated.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the email confirmation token.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the user parameter is null.</exception>
        Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);

        /// <summary>
        /// Retrieves a refresh token by its value.
        /// </summary>
        /// <param name="token">The value of the refresh token to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="RefreshToken"/> object if found; otherwise, <c>null</c>.</returns>
        /// <exception cref="ArgumentException">Thrown when the <paramref name="token"/> parameter is invalid.</exception>
        Task<RefreshToken?> GetRefreshTokenAsync(string token);

        /// <summary>
        /// Revokes a refresh token, marking it as unusable.
        /// </summary>
        /// <param name="token">The value of the refresh token to revoke.</param>
        /// <returns>A task that represents the asynchronous operation. It completes when the token has been revoked.</returns>
        /// <exception cref="ArgumentException">Thrown when the <paramref name="token"/> parameter is invalid.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the token cannot be revoked, for example, if it does not exist.</exception>
        Task RevokeRefreshTokenAsync(string token);

        /// <summary>
        /// Extracts a <see cref="ClaimsPrincipal"/> from an expired JWT token.
        /// </summary>
        /// <param name="token">The expired JWT token from which the <see cref="ClaimsPrincipal"/> is to be extracted.</param>
        /// <returns>A <see cref="ClaimsPrincipal"/> representing the user information encoded in the token.</returns>
        /// <exception cref="SecurityTokenException">Thrown when the token is invalid or the algorithm used is not supported.</exception>
        /// <remarks>
        /// This method does not validate the expiration of the token, allowing for extraction of the principal even if the token is expired.
        /// It ensures that the token uses the expected algorithm (HMAC SHA-256) and has a valid signing key.
        /// </remarks>
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

        /// <summary>
        /// Determines whether the specified refresh token has expired.
        /// </summary>
        /// <param name="token">The refresh token to check.</param>
        /// <returns><c>true</c> if the token has expired; otherwise, <c>false</c>.</returns>
        bool IsRefreshTokenExpired(RefreshToken token);

        /// <summary>
        /// Determines whether the specified refresh token is active.
        /// </summary>
        /// <param name="token">The refresh token to check.</param>
        /// <returns><c>true</c> if the token is active; otherwise, <c>false</c>.</returns>
        bool IsRefreshTokenActive(RefreshToken token);

        /// <summary>
        /// Retrieves all refresh tokens associated with the specified user.
        /// </summary>
        /// <param name="user">The user whose refresh tokens are to be retrieved.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of refresh tokens associated with the specified user.</returns>
        Task<IEnumerable<RefreshToken>> GetUserAllRefreshTokensAsync(ApplicationUser user);
    }
}
