using Azure.Storage.Blobs.Models;
using E_commerceOnlineStore.Models.DataModels.UserManagement;
using E_commerceOnlineStore.Models.RequestModels.Account;
using E_commerceOnlineStore.Services.Business.Account.Results;
using E_commerceOnlineStore.Services.Business.Security;
using E_commerceOnlineStore.Services.Data.User;
using E_commerceOnlineStore.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ClientModel.Primitives;

namespace E_commerceOnlineStore.Services.Business.Account
{
    /// <summary>
    /// Service for handling authentication operations, including user login, token refresh, 
    /// and token revocation.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="AuthService"/> class.
    /// </remarks>
    /// <param name="userDataService">Service for user data operations.</param>
    /// <param name="passwordResetService">Service for password reset operations.</param>
    /// <param name="tokenService">Service for token management operations.</param>
    public class AuthService(IUserDataService userDataService,
                       IPasswordResetService passwordResetService,
                       ITokenService tokenService) : IAuthService
    {
        private readonly IUserDataService _userDataService = userDataService;
        private readonly IPasswordResetService _passwordResetService = passwordResetService;
        private readonly ITokenService _tokenService = tokenService;

        /// <summary>
        /// Authenticates a user and generates access and refresh tokens.
        /// </summary>
        /// <param name="model">The login model containing user credentials.</param>
        /// <param name="modelState">The state of the model validation.</param>
        /// <returns>A <see cref="LoginResult"/> containing the authentication result, 
        /// access token, and refresh token.</returns>
        /// <exception cref="ArgumentException">Thrown when the model state is invalid.</exception>
        public async Task<LoginResult> LoginAsync(LoginModel model, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                throw new ArgumentException("Invalid model state.");
            }

            var userResult = await _userDataService.GetUserByEmailAsync(model.UserEmail);
            if (!userResult.Succeeded 
                || userResult.Data == null 
                || !await _passwordResetService.CheckPasswordAsync(userResult.Data, model.Password))
            {
                return new LoginResult
                (
                    Succeeded : false,
                    Token : string.Empty,
                    RefreshToken : string.Empty
                );
            }

            var token = await _tokenService.GenerateTokenAsync(userResult.Data); 
            var refreshToken = await _tokenService.GenerateRefreshTokenAsync(userResult.Data); 

            return new LoginResult
            (
                Succeeded : true,
                Token : token,
                RefreshToken : refreshToken
            );
        }

        /// <summary>
        /// Refreshes an access token using a valid refresh token.
        /// </summary>
        /// <param name="model">The token request model containing the refresh token.</param>
        /// <param name="modelState">The state of the model validation.</param>
        /// <returns>A <see cref="RefreshTokenResult"/> containing the new tokens if the refresh was successful.</returns>
        /// <exception cref="ArgumentException">Thrown when the model state is invalid or the token is invalid.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the user identity name is null.</exception>
        public async Task<RefreshTokenResult> RefreshTokenAsync(TokenRequestModel model, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                throw new ArgumentException("Invalid model state.");
            }

            var principal = _tokenService.GetPrincipalFromExpiredToken(model.Token) ?? throw new ArgumentException("Invalid token");
            
            var userId = principal.Identity?.Name
                ?? throw new InvalidOperationException("The user identity name cannot be null. Ensure that the user is authenticated.");

            var userResult = await _userDataService.GetUserByIdAsync(userId);
            if (!userResult.Succeeded || userResult.Data == null)
            {
                throw new ArgumentException("Invalid refresh token. User was not found");
            }

            var oldToken = await _tokenService.GetRefreshTokenAsync(model.RefreshToken);

            if (oldToken == null || !_tokenService.IsRefreshTokenActive(oldToken))
            {
                throw new ArgumentException("Invalid refresh token");
            }

            var newToken = await _tokenService.GenerateTokenAsync(userResult.Data);
            var newRefreshToken = await _tokenService.GenerateRefreshTokenAsync(userResult.Data);

            await _tokenService.RevokeRefreshTokenAsync(oldToken.Token);

            return new RefreshTokenResult
            (
                Succeeded: true,
                NewToken: newToken,
                NewRefreshToken: newRefreshToken
            );
        }

        /// <summary>
        /// Revokes a specified refresh token.
        /// </summary>
        /// <param name="token">The refresh token to revoke.</param>
        /// <returns>An <see cref="OperationResult{string}"/> indicating the result of the revocation.</returns>
        public async Task<OperationResult<string>> RevokeTokenAsync(string token)
        {
            var refreshToken = await _tokenService.GetRefreshTokenAsync(token);

            if (refreshToken == null || !_tokenService.IsRefreshTokenActive(refreshToken))
            {
                return OperationResult<string>.FailureResult(["Invalid token"]);
            }

            try
            {
                await _tokenService.RevokeRefreshTokenAsync(token);
                return OperationResult<string>.SuccessResult(token);
            }
            catch (Exception ex)
            {
                return OperationResult<string>.FailureResult([ex.Message]);
            }
        }
    }
}
