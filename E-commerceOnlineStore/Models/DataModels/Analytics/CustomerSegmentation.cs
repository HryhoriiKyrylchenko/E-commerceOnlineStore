using E_commerceOnlineStore.Enums.Analytics;
using E_commerceOnlineStore.Models.DataModels.UserManagement;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerceOnlineStore.Models.DataModels.Analytics
{
    /// <summary>
    /// Represents the segmentation of a customer based on demographic, behavioral, and purchase characteristics.
    /// </summary>
    [Table("CustomerSegmentations")]
    public class CustomerSegmentation
    {
        /// <summary>
        /// Gets or sets the unique identifier for the customer segmentation entry.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the customer associated with this segmentation.
        /// </summary>
        [Required]
        [MaxLength(36)]
        public string CustomerId { get; set; } = null!;

        /// <summary>
        /// Gets or sets the customer entity associated with this segmentation.
        /// </summary>
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer Customer { get; set; } = null!;

        /// <summary>
        /// Gets or sets the collection of demographic segments the customer belongs to.
        /// </summary>
        [Required]
        public ICollection<DemographicSegment> DemographicSegments { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of behavior segments the customer belongs to.
        /// </summary>
        [Required]
        public ICollection<BehaviorSegment> BehaviorSegments { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of purchase segments the customer belongs to.
        /// </summary>
        [Required]
        public ICollection<PurchaseSegment> PurchaseSegments { get; set; } = [];

        /// <summary>
        /// Gets or sets the date when the segmentation was recorded.
        /// </summary>
        [Required]
        public DateTime SegmentationDate { get; set; }

        /// <summary>
        /// Gets or sets additional data that may provide context or insights into the segmentation.
        /// </summary>
        [MaxLength(1000)]
        public string? AdditionalData { get; set; }
    }
}
