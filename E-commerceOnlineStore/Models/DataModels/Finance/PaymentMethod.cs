using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_commerceOnlineStore.Models.DataModels.Account;

namespace E_commerceOnlineStore.Models.DataModels.Finance
{
    /// <summary>
    /// Represents a payment method that can be used for transactions.
    /// </summary>
    [Table("PaymentMethods")]
    public class PaymentMethod
    {
        /// <summary>
        /// Gets or sets the unique identifier for the payment method.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the payment method.
        /// This property is initialized to an empty string to avoid null values.
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the payment method.
        /// This property is nullable to accommodate cases where no description is provided.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the collection of payments associated with this shipping method.
        /// </summary>
        public virtual ICollection<Payment> Payments { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of customers associated with this payment method.
        /// </summary>
        public virtual ICollection<Customer> Customers { get; set; } = [];
    }

}
