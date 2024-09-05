namespace E_commerceOnlineStore.Models.RequestModels.Account
{
    /// <summary>
    /// Represents the data model used for assigning a role to a user.
    /// </summary>
    public class AssignRoleModel
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user to whom the role is to be assigned.
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the role to be assigned to the user.
        /// </summary>
        public string RoleName { get; set; } = string.Empty;
    }
}
