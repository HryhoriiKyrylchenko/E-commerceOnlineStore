using E_commerceOnlineStore.Models.DataModels.Account;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using E_commerceOnlineStore.Enums.LogEnums;

namespace E_commerceOnlineStore.Models.DataModels.LogModels
{
    /// <summary>
    /// Represents a log of user activities within the system.
    /// </summary>
    [Table("UserActivityLogs")]
    public class UserActivityLog
    {
        /// <summary>
        /// Gets or sets the log ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who performed the activity.
        /// </summary>
        [Required]
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user associated with this activity.
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        /// <summary>
        /// Gets or sets the type of the activity (e.g., login, order creation).
        /// </summary>
        [Required]
        public ActivityType ActivityType { get; set; }

        /// <summary>
        /// Gets or sets the description of the activity.
        /// </summary>
        [MaxLength(500)]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the IP address from where the activity was performed.
        /// </summary>
        [MaxLength(50)]
        public string? IPAddress { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the activity occurred.
        /// </summary>
        [Required]
        public DateTime ActivityDate { get; set; }
    }
}
