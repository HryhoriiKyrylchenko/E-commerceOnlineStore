using System.ComponentModel.DataAnnotations;

namespace E_commerceOnlineStore.Models.RequestModels.Account
{
    /// <summary>
    /// Represents the data model used for resetting a user's password.
    /// </summary>
    public class ResetPasswordModel
    {
        /// <summary>
        /// Gets or sets the email address of the user requesting the password reset.
        /// This property is initialized to an empty string.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the token used to validate the password reset request.
        /// This property is initialized to an empty string.
        /// </summary>

        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the new password that the user wants to set.
        /// This property is initialized to an empty string.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password confirmation that the user wants to set.
        /// This property is initialized to an empty string.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

}
