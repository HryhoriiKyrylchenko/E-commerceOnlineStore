using System.ComponentModel.DataAnnotations;

namespace E_commerceOnlineStore.Models.Account
{
    /// <summary>
    /// Represents the data required for user login.
    /// </summary>
    /// <remarks>
    /// The <see cref="LoginModel"/> class contains properties that are used when a user attempts to log in.
    /// It includes the credentials necessary to authenticate a user in the application.
    /// </remarks>
    public class LoginModel
    {
        /// <summary>
        /// Gets or sets the username of the user attempting to log in.
        /// </summary>
        /// <remarks>
        /// This property specifies the username that the user will use to authenticate and gain access to their account.
        /// </remarks>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password of the user attempting to log in.
        /// </summary>
        /// <remarks>
        /// This property specifies the password that the user will use to authenticate. It must match the password associated with the provided username.
        /// </remarks>
        public string Password { get; set; } = string.Empty;
    }

}
