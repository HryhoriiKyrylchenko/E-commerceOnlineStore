using E_commerceOnlineStore.Enums;
using E_commerceOnlineStore.Models.DataModels.Account;
using E_commerceOnlineStore.Models.DataModels.Shipping;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerceOnlineStore.Models.DataModels.Order
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
        public Customer Customer { get; set; } = null!;

        /// <summary>
        /// Gets or sets the date of the order.
        /// </summary>
        [Required]
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Gets or sets the total sum of the order.
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalOrderSum { get; set; }

        /// <summary>
        /// Gets or sets the status of the order.
        /// </summary>
        [Required]
        public OrderStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the collection of order items associated with the order.
        /// </summary>
        public virtual ICollection<OrderItem> OrderItems { get; set; } = [];

        /// <summary>
        /// Gets or sets the payment associated with the order.
        /// </summary>
        public virtual Payment? Payment { get; set; }

        /// <summary>
        /// Gets or sets the delivery information associated with the order.
        /// </summary>
        public virtual Shipment? Shipment { get; set; }

        /// <summary>
        /// Gets or sets the collection of return requests associated with the order.
        /// </summary>
        public virtual ICollection<ReturnRequest> ReturnRequests { get; set; } = [];
    }
}
