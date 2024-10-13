using E_commerceOnlineStore.Enums.Newsletter;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using E_commerceOnlineStore.Models.DataModels.UserManagement;

namespace E_commerceOnlineStore.Models.DataModels.Newsletter
{
    /// <summary>
    /// Represents a customer's subscription to newsletters.
    /// </summary>
    [Table("NewsletterSubscriptions")]
    public class NewsletterSubscription
    {
        /// <summary>
        /// Gets or sets the subscription ID.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the customer ID associated with this subscription.
        /// </summary>
        [Required]
        public string CustomerId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the customer associated with this subscription.
        /// </summary>
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer Customer { get; set; } = null!;

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
        /// Gets or sets the type of newsletter the user is subscribed to.
        /// </summary>
        [Required]
        public NewsletterType SubscriptionType { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the subscription was created.
        /// </summary>
        [Required]
        public DateTime SubscriptionDate { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the subscription was last updated.
        /// </summary>
        public DateTime? LastUpdatedDate { get; set; }
    }
}
