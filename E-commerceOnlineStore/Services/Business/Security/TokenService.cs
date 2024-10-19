using E_commerceOnlineStore.Data;
using E_commerceOnlineStore.Models.DataModels.UserManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace E_commerceOnlineStore.Services.Business.Security
{
    /// <summary>
    /// Provides functionality to generate JSON Web Tokens (JWT) for application users.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="TokenService"/> class.
    /// </remarks>
    /// <param name="configuration">The configuration used to access settings for JWT token generation.</param>
    public class TokenService(IConfiguration configuration, UserManager<ApplicationUser> userManager, ApplicationDbContext context) : ITokenService
    {
        private readonly IConfiguration _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        private readonly UserManager<ApplicationUser> _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        private readonly ApplicationDbContext _context = context;

        /// <summary>
        /// Generates a JWT for the specified user.
        /// </summary>
        /// <param name="user">The application user for whom the JWT is generated.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the generated JWT.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="user"/> parameter is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the JWT key or issuer is not found in the configuration, or if the user's username is null.</exception>
        public async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            ArgumentNullException.ThrowIfNull(user);

            var key = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not found in configuration.");
            var issuer = _configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT Issuer not found in configuration.");
            var audience = _configuration["Jwt:Audience"] ?? throw new InvalidOperationException("JWT Audience not found in configuration.");

            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role));

            return await Task.Run(() =>
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? throw new InvalidOperationException("Username cannot be null")),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email ?? throw new InvalidOperationException("Email cannot be null")),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }
                .Concat(roleClaims);

                var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var creds = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: creds);

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

        /// <summary>
        /// Generates an email confirmation token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom the email confirmation token is being generated.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the email confirmation token.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the user parameter is null.</exception>
        public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            // Check if the user object is null to prevent NullReferenceException
            ArgumentNullException.ThrowIfNull(user, nameof(user));

            // Generate and return the email confirmation token using UserManager
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }


        /// <summary>
        /// Generates a new refresh token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom the refresh token is being generated.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the newly generated refresh token as a string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="user"/> parameter is null.</exception>
        public async Task<string> GenerateRefreshTokenAsync(ApplicationUser user)
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                UserId = user.Id,
                Expiration = DateTime.UtcNow.AddDays(7)
            };

            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return refreshToken.Token;
        }

        /// <summary>
        /// Retrieves a refresh token by its value.
        /// </summary>
        /// <param name="token">The value of the refresh token to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="RefreshToken"/> object if found; otherwise, <c>null</c>.</returns>
        /// <exception cref="ArgumentException">Thrown when the <paramref name="token"/> parameter is invalid.</exception>
        public async Task<RefreshToken?> GetRefreshTokenAsync(string token)
        {
            return await _context.RefreshTokens
                .SingleOrDefaultAsync(t => t.Token == token);
        }

        /// <summary>
        /// Revokes a refresh token, marking it as unusable.
        /// </summary>
        /// <param name="token">The value of the refresh token to revoke.</param>
        /// <returns>A task that represents the asynchronous operation. It completes when the token has been revoked.</returns>
        /// <exception cref="ArgumentException">Thrown when the <paramref name="token"/> parameter is invalid.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the token cannot be revoked, for example, if it does not exist.</exception>
        public async Task RevokeRefreshTokenAsync(string token)
        {
            var refreshToken = await GetRefreshTokenAsync(token);
            if (refreshToken != null)
            {
                try
                {
                    refreshToken.IsRevoked = true;
                    _context.RefreshTokens.Update(refreshToken);
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    throw;
                }
            }
        }

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
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var confKey = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not found in configuration.");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(confKey));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateLifetime = false, // Important: Do not validate the token's expiration
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        /// <summary>
        /// Determines whether the specified refresh token has expired.
        /// </summary>
        /// <param name="token">The refresh token to check.</param>
        /// <returns><c>true</c> if the token has expired; otherwise, <c>false</c>.</returns>
        public bool IsRefreshTokenExpired(RefreshToken token)
        {
            return DateTime.Now >= token.Expiration;
        }

        /// <summary>
        /// Determines whether the specified refresh token is active.
        /// </summary>
        /// <param name="token">The refresh token to check.</param>
        /// <returns><c>true</c> if the token is active; otherwise, <c>false</c>.</returns>
        public bool IsRefreshTokenActive(RefreshToken token)
        {
            return !IsRefreshTokenExpired(token) && !token.IsRevoked;
        }

        /// <summary>
        /// Retrieves all refresh tokens associated with the specified user.
        /// </summary>
        /// <param name="user">The user whose refresh tokens are to be retrieved.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of refresh tokens associated with the specified user.</returns>
        public async Task<IEnumerable<RefreshToken>> GetUserAllRefreshTokensAsync(ApplicationUser user)
        {
            // Query the database for refresh tokens where the UserId matches the provided user's ID
            return await _context.RefreshTokens
                .Where(t => t.UserId == user.Id)
                .ToListAsync();
        }
    }
}
