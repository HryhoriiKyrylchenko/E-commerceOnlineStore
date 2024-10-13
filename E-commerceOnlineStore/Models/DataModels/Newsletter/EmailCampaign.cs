using E_commerceOnlineStore.Enums.Newsletter;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_commerceOnlineStore.Models.DataModels.Newsletter
{
    /// <summary>
    /// Represents an email campaign for newsletters.
    /// </summary>
    [Table("EmailCampaigns")]
    public class EmailCampaign
    {
        /// <summary>
        /// Gets or sets the campaign ID.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the subject of the email campaign.
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the body content of the email campaign.
        /// </summary>
        [Required]
        public string Body { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date when the campaign was created.
        /// </summary>
        [Required]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the scheduled date for sending the campaign.
        /// </summary>
        public DateTime? ScheduledDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the campaign has been sent.
        /// </summary>
        public bool IsSent { get; set; } = false;

        /// <summary>
        /// Gets or sets the type of newsletter this campaign is associated with.
        /// </summary>
        [Required]
        public NewsletterType NewsletterType { get; set; }
    }
}
