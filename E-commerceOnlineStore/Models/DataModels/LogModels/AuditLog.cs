using E_commerceOnlineStore.Models.DataModels.UserManagement;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using E_commerceOnlineStore.Enums.LogEnums;
using Microsoft.EntityFrameworkCore;
using System;

namespace E_commerceOnlineStore.Models.DataModels.LogModels
{
    /// <summary>
    /// Represents an audit log of data changes within the system.
    /// </summary>
    [Table("AuditLogs")]
    [Index(nameof(EntityName), nameof(EntityId), nameof(ActionDate))]
    public class AuditLog
    {
        /// <summary>
        /// Gets or sets the audit log ID.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the entity name where changes occurred.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string EntityName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the primary key of the entity where changes occurred.
        /// </summary>
        [Required]
        public string EntityId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of action (Insert, Update, Delete).
        /// </summary>
        [Required]
        public ActionType ActionType { get; set; }

        /// <summary>
        /// Gets or sets the old values before the change.
        /// </summary>
        public string? OldValues { get; set; }

        /// <summary>
        /// Gets or sets the new values after the change.
        /// </summary>
        public string? NewValues { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who performed the action.
        /// </summary>
        [Required]
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user associated with this audit log.
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        /// <summary>
        /// Gets or sets the timestamp when the action occurred.
        /// </summary>
        [Required]
        public DateTime ActionDate { get; set; }
    }
}
