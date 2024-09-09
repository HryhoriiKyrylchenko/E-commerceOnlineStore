using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using E_commerceOnlineStore.Enums.Account;

namespace E_commerceOnlineStore.Models.DataModels.UserManagement
{
    /// <summary>
    /// Represents a user's subscription to the newsletter.
    /// </summary>
    [Table("NewsletterSubscriptions")]
    public class NewsletterSubscription
    {
        /// <summary>
        /// Gets or sets the unique identifier for the newsletter subscription.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the subscription was created.
        /// </summary>
        [Required]
        public DateTime SubscriptionDate { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the subscription was last updated.
        /// </summary>
        public DateTime? LastUpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the subscription is active.
        /// </summary>
        [Required]
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether the subscription is confirmed.
        /// This is useful for managing double opt-in subscriptions.
        /// </summary>
        public bool IsConfirmed { get; set; } = false;

        /// <summary>
        /// Gets or sets the identifier of the user who owns this subscription.
        /// This is a foreign key linking to the <see cref="ApplicationUser"/> entity.
        /// </summary>
        [Required]
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the associated <see cref="ApplicationUser"/> for this subscription.
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        /// <summary>
        /// Gets or sets the types of newsletters the user is subscribed to.
        /// </summary>
        [NotMapped]
        public ICollection<NewsletterType> SubscriptionTypes { get; set; } = [];
    }
}
