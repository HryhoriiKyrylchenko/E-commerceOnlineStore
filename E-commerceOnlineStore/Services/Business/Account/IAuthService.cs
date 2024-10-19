using E_commerceOnlineStore.Models.RequestModels.Account;
using E_commerceOnlineStore.Services.Business.Account.Results;
using E_commerceOnlineStore.Utilities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ClientModel.Primitives;

namespace E_commerceOnlineStore.Services.Business.Account
{
    /// <summary>
    /// Interface for authentication services, providing methods for user login, token management, and revocation.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Authenticates a user with the provided login model and returns the login result.
        /// </summary>
        /// <param name="model">The login model containing user credentials.</param>
        /// <param name="modelState">The state of the model to validate.</param>
        /// <returns>A task representing the asynchronous operation, with a <see cref="LoginResult"/> containing the authentication outcome.</returns>
        Task<LoginResult> LoginAsync(LoginModel model, ModelStateDictionary modelState);

        /// <summary>
        /// Refreshes an authentication token based on the provided token request model.
        /// </summary>
        /// <param name="model">The token request model containing the refresh token and related information.</param>
        /// <param name="modelState">The state of the model to validate.</param>
        /// <returns>A task representing the asynchronous operation, with a <see cref="RefreshTokenResult"/> containing the new token or failure information.</returns>
        Task<RefreshTokenResult> RefreshTokenAsync(TokenRequestModel model, ModelStateDictionary modelState);

        /// <summary>
        /// Revokes the specified token, invalidating it for future use.
        /// </summary>
        /// <param name="token">The token to revoke.</param>
        /// <returns>A task representing the asynchronous operation, with an <see cref="OperationResult{string}"/> indicating success or failure.</returns>
        Task<OperationResult<string>> RevokeTokenAsync(string token);
    }

}
