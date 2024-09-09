using E_commerceOnlineStore.Models.DataModels.UserManagement;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_commerceOnlineStore.Models.DataModels.Purchase
{
    /// <summary>
    /// Represents a return note entity.
    /// </summary>
    [Table("ReturnNotes")]
    public class ReturnNote
    {
        /// <summary>
        /// Gets or sets the return note ID.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the return request this note is associated with.
        /// </summary>
        [Required]
        public int ReturnRequestId { get; set; }

        /// <summary>
        /// Gets or sets the associated return request.
        /// </summary>
        [ForeignKey(nameof(ReturnRequestId))]
        public virtual ReturnRequest ReturnRequest { get; set; } = null!;

        /// <summary>
        /// Gets or sets the ID of the user who created the note.
        /// </summary>
        [Required]
        public string CreatedById { get; set; } = null!;

        /// <summary>
        /// Gets or sets the user who created the note.
        /// </summary>
        [ForeignKey(nameof(CreatedById))]
        public virtual ApplicationUser CreatedBy { get; set; } = null!;

        /// <summary>
        /// Gets or sets the content of the note.
        /// </summary>
        [Required]
        [MaxLength(1000)]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date when the note was created.
        /// </summary>
        [Required]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }

}
