using E_commerceOnlineStore.Models.DataModels.UserManagement;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using E_commerceOnlineStore.Enums.LogEnums;
using Microsoft.EntityFrameworkCore;

namespace E_commerceOnlineStore.Models.DataModels.LogModels
{
    /// <summary>
    /// Represents an event log for tracking system events.
    /// </summary>
    [Table("EventLogs")]
    [Index(nameof(EventType), nameof(EventDate))]
    public class EventLog
    {
        /// <summary>
        /// Gets or sets the event log ID.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the event type (e.g., Error, Information, Warning).
        /// </summary>
        [Required]
        public EventType EventType { get; set; }

        /// <summary>
        /// Gets or sets the event message.
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the stack trace (if the event is an error).
        /// </summary>
        public string? StackTrace { get; set; }

        /// <summary>
        /// Gets or sets the user ID associated with the event (if applicable).
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// Gets or sets the user associated with this event log.
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser? User { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the event occurred.
        /// </summary>
        [Required]
        public DateTime EventDate { get; set; }
    }
}
