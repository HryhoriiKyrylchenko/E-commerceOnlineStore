using E_commerceOnlineStore.Models.DataModels.Support;
using System.ComponentModel.DataAnnotations;

namespace E_commerceOnlineStore.Models.DataModels.UserManagement
{
    /// <summary>
    /// Represents an employee who can handle support tickets and perform actions within the application.
    /// </summary>
    public class Employee : ApplicationUser
    {
        /// <summary>
        /// Gets or sets the position or job title of the employee.
        /// </summary>
        public string? Position { get; set; }

        /// <summary>
        /// Gets or sets whether the user is active.
        /// </summary>
        [Required]
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Gets or sets the collection of ticket histories associated with the employee.
        /// This includes records of actions taken by the employee on support tickets.
        /// </summary>
        public virtual ICollection<TicketHistory> TicketHistories { get; set; } = [];
    }

}
