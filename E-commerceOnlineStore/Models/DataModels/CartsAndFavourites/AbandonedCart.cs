using E_commerceOnlineStore.Models.DataModels.UserManagement;

namespace E_commerceOnlineStore.Models.DataModels.CartsAndFavourites
{
    /// <summary>
    /// Represents a shopping cart that has been abandoned by a user without completing a purchase.
    /// Tracks the cart's status and reminder notifications sent to the user.
    /// </summary>
    public class AbandonedCart
    {
        /// <summary>
        /// Gets or sets the unique identifier for the abandoned cart record.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the associated shopping cart that was abandoned.
        /// </summary>
        public int ShoppingCartId { get; set; }

        /// <summary>
        /// Gets or sets the associated shopping cart.
        /// </summary>
        public virtual ShoppingCart ShoppingCart { get; set; } = null!;

        /// <summary>
        /// Gets or sets the date and time when the cart was abandoned.
        /// </summary>
        public DateTime AbandonedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when a reminder was sent to the user about the abandoned cart.
        /// Nullable, as a reminder may not have been sent yet.
        /// </summary>
        public DateTime? ReminderSentAt { get; set; }

        /// <summary>
        /// Gets or sets the count of reminders sent to the user regarding the abandoned cart.
        /// </summary>
        public int ReminderCount { get; set; }
    }
}
