using E_commerceOnlineStore.Enums.Support;
using E_commerceOnlineStore.Models.DataModels.UserManagement;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_commerceOnlineStore.Models.DataModels.Support
{
    /// <summary>
    /// Represents a support ticket submitted by a customer, including details about the issue, its status, and history of actions taken on it.
    /// </summary>
    [Table("SupportTickets")]
    public class SupportTicket
    {
        /// <summary>
        /// Gets or sets the unique identifier for the support ticket.
        /// </summary>
        [Key] 
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the customer who submitted the ticket.
        /// </summary>
        [Required]
        public string CustomerId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the <see cref="Customer"/> associated with this ticket.
        /// </summary>
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer Customer { get; set; } = null!;

        /// <summary>
        /// Gets or sets the subject of the support ticket.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the detailed description of the issue or request in the support ticket.
        /// </summary>
        [Required]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the priority level of the support ticket.
        /// </summary>
        [Required]
        public TicketPriority Priority { get; set; }

        /// <summary>
        /// Gets or sets the category of the support ticket.
        /// </summary>
        [Required]
        public TicketCategory Category { get; set; }

        /// <summary>
        /// Gets or sets the current status of the support ticket.
        /// </summary>
        [Required]
        public TicketStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the support ticket was created.
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the support ticket was last updated.
        /// This can be null if the ticket has not been updated since creation.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the support ticket was closed.
        /// This can be null if the ticket is still open.
        /// </summary>
        public DateTime? ClosedAt { get; set; }

        /// <summary>
        /// Gets or sets the response provided by the employee handling the ticket.
        /// </summary>
        public string Response { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the collection of history records associated with this support ticket.
        /// </summary>
        public virtual ICollection<TicketHistory> TicketHistories { get; set; } = [];
    }
}
