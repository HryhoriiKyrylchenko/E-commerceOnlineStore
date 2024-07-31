using E_commerceOnlineStore.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_commerceOnlineStore.Services
{
    /// <summary>
    /// Provides functionality to generate JSON Web Tokens (JWT) for application users.
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration used to access settings for JWT token generation.</param>
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Generates a JWT for the specified user.
        /// </summary>
        /// <param name="user">The application user for whom the JWT is generated.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the generated JWT.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="user"/> parameter is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the JWT key or issuer is not found in the configuration, or if the user's username is null.</exception>
        public async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            // Ensure the user object is not null
            ArgumentNullException.ThrowIfNull(user);

            // Ensure the configuration values are not null
            var key = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not found in configuration.");
            var issuer = _configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT Issuer not found in configuration.");

            return await Task.Run(() =>
            {
                // Create claims based on user information
                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? throw new InvalidOperationException("Username cannot be null")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                // Create a symmetric security key and signing credentials
                var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var creds = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

                // Create the JWT token with the specified claims, issuer, audience, and expiration
                var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: issuer,
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: creds);

                // Serialize the token to a string and return it
                return new JwtSecurityTokenHandler().WriteToken(token);
            });
        }
    }
}
