using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerceOnlineStore.Models
{
    /// <summary>
    /// Represents a category entity.
    /// </summary>
    [Table("Categories")]
    [Index(nameof(Name), IsUnique = true)]
    public class Category
    {
        /// <summary>
        /// Gets or sets the category ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the category.
        /// </summary>
        [MaxLength(255)]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the ID of the parent category.
        /// Null if it's a root category.
        /// </summary>
        public int? ParentCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the parent category.
        /// </summary>
        [ForeignKey(nameof(ParentCategoryId))]
        public virtual Category? ParentCategory { get; set; }

        /// <summary>
        /// Gets or sets the display order of the category.
        /// </summary>
        public int DisplayOrder { get; set; } = 0;

        /// <summary>
        /// Gets or sets the level of the category in the hierarchy.
        /// </summary>
        public int Level { get; set; } = 0;

        /// <summary>
        /// Gets or sets the collection of subcategories associated with this category.
        /// </summary>
        public virtual ICollection<Category> SubCategories { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of products associated with the category.
        /// </summary>
        public virtual ICollection<Product> Products { get; set; } = [];
    }
}
