using E_commerceOnlineStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_commerceOnlineStore.Services
{
    /// <summary>
    /// Provides functionality to generate JSON Web Tokens (JWT) for application users.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="TokenService"/> class.
    /// </remarks>
    /// <param name="configuration">The configuration used to access settings for JWT token generation.</param>
    public class TokenService(IConfiguration configuration, UserManager<ApplicationUser> userManager) : ITokenService
    {
        private readonly IConfiguration _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        private readonly UserManager<ApplicationUser> _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));

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
            var audience = _configuration["Jwt:Audience"] ?? throw new InvalidOperationException("JWT Audience not found in configuration.");

            // Retrieve roles for the user
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role));

            return await Task.Run(() =>
            {
                // Create claims based on user information
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName ?? throw new InvalidOperationException("Username cannot be null")),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email ?? throw new InvalidOperationException("Email cannot be null")),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }
                .Concat(roleClaims);

                // Create a symmetric security key and signing credentials
                var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var creds = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

                // Create the JWT token with the specified claims, issuer, audience, and expiration
                var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(10),
                    signingCredentials: creds);

                // Serialize the token to a string and return it
                return new JwtSecurityTokenHandler().WriteToken(token);
            });
        }

        /// <summary>
        /// Generates a password reset token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom to generate the token.</param>
        /// <returns>The password reset token.</returns>
        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            ArgumentNullException.ThrowIfNull(user);

            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }
    }
}
