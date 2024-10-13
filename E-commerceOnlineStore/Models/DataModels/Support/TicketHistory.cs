using E_commerceOnlineStore.Models.DataModels.UserManagement;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_commerceOnlineStore.Models.DataModels.Support
{
    /// <summary>
    /// Represents a record of actions taken on a support ticket for historical tracking.
    /// </summary>
    [Table("TicketHistories")]
    public class TicketHistory
    {
        /// <summary>
        /// Gets or sets the unique identifier for the ticket history record.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the support ticket that this history record is associated with.
        /// </summary>
        [Required]
        public int TicketId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="SupportTicket"/> associated with this history record.
        /// </summary>
        [ForeignKey(nameof(TicketId))]
        public virtual SupportTicket Ticket { get; set; } = null!;

        /// <summary>
        /// Gets or sets the identifier of the employee who performed the action recorded in this history entry.
        /// </summary>
        [Required]
        public string EmployeeId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the <see cref="Employee"/> who performed the action recorded in this history entry.
        /// </summary>
        [ForeignKey(nameof(EmployeeId))]
        public virtual Employee Employee { get; set; } = null!;

        /// <summary>
        /// Gets or sets the date and time when the action was performed.
        /// </summary>
        [Required]
        public DateTime ActionDate { get; set; }

        /// <summary>
        /// Gets or sets a description of the action performed.
        /// </summary>
        [Required]
        [MaxLength(1000)]
        public string ActionDescription { get; set; } = string.Empty;
    }
}
