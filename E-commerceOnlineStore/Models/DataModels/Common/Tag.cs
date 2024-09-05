using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_commerceOnlineStore.Models.DataModels.Common
{
    /// <summary>
    /// Represents a tag entity.
    /// </summary>
    [Table("Tags")]
    public class Tag
    {
        /// <summary>
        /// Gets or sets the tag ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the tag.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the collection of products tags associated with the tag.
        /// </summary>
        public virtual ICollection<ProductTag> ProductsTags { get; set; } = [];
    }
}
