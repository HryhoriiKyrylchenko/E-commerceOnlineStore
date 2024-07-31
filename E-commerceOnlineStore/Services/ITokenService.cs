using E_commerceOnlineStore.Models;

namespace E_commerceOnlineStore.Services
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
    }
}
