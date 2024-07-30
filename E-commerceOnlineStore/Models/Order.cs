using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerceOnlineStore.Models
{
    /// <summary>
    /// Represents an order entity.
    /// </summary>
    [Table("Orders")]
    public class Order
    {
        /// <summary>
        /// Gets or sets the order ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the customer ID associated with the order.
        /// </summary>
        [Required]
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the customer associated with the order.
        /// </summary>
        public Customer? Customer { get; set; }

        /// <summary>
        /// Gets or sets the date of the order.
        /// </summary>
        [Required]
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Gets or sets the total amount of the order.
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the status of the order.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the collection of order items associated with the order.
        /// </summary>
        public virtual ICollection<OrderItem>? OrderItems { get; set; }

        /// <summary>
        /// Gets or sets the payment associated with the order.
        /// </summary>
        public virtual Payment? Payment { get; set; }
    }
}
