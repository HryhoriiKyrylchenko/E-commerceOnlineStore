using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using E_commerceOnlineStore.Enums;

namespace E_commerceOnlineStore.Models
{
    [Table("Notifications")]
    public class Notification
    {
        /// <summary>
        /// Gets or sets the notification ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user ID associated with the notification.
        /// </summary>
        [Required]
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the customer associated with this entity.
        /// This navigation property provides access to the related <see cref="Customer"/> entity.
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        /// <summary>
        /// Gets or sets the type of the notification.
        /// </summary>
        [Required]
        public NotificationType Type { get; set; }

        /// <summary>
        /// Gets or sets the content of the notification.
        /// </summary>
        [Required]
        [MaxLength(1000)]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the notification was created.
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets whether the notification has been read.
        /// </summary>
        public bool IsRead { get; set; } = false;
    }
}
