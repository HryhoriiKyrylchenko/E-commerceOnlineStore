using E_commerceOnlineStore.Enums.Analytics;
using E_commerceOnlineStore.Models.DataModels.UserManagement;
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
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the customer associated with this segmentation.
        /// </summary>
        public string CustomerId { get; set; } = null!;

        /// <summary>
        /// Gets or sets the customer entity associated with this segmentation.
        /// </summary>
        public virtual Customer Customer { get; set; } = null!;

        /// <summary>
        /// Gets or sets the collection of demographic segments the customer belongs to.
        /// </summary>
        public ICollection<DemographicSegment> DemographicSegments { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of behavior segments the customer belongs to.
        /// </summary>
        public ICollection<BehaviorSegment> BehaviorSegments { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of purchase segments the customer belongs to.
        /// </summary>
        public ICollection<PurchaseSegment> PurchaseSegments { get; set; } = [];

        /// <summary>
        /// Gets or sets the date when the segmentation was recorded.
        /// </summary>
        public DateTime SegmentationDate { get; set; }

        /// <summary>
        /// Gets or sets additional data that may provide context or insights into the segmentation.
        /// </summary>
        public string? AdditionalData { get; set; }
    }
}
