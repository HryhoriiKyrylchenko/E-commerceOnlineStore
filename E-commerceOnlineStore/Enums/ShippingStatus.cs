namespace E_commerceOnlineStore.Enums
{
    /// <summary>
    /// Represents the various stages of the shipping process for an order.
    /// </summary>
    public enum ShippingStatus
    {
        /// <summary>
        /// The delivery is pending and has not yet begun.
        /// </summary>
        Pending,

        /// <summary>
        /// The order is being prepared for shipment.
        /// </summary>
        Preparing,

        /// <summary>
        /// The order has been shipped from the warehouse.
        /// </summary>
        Shipped,

        /// <summary>
        /// The order is currently in transit to its destination.
        /// </summary>
        InTransit,

        /// <summary>
        /// The order is out for delivery and will be delivered soon.
        /// </summary>
        OutForDelivery,

        /// <summary>
        /// The order has been delivered to the customer.
        /// </summary>
        Delivered,

        /// <summary>
        /// The delivery has been cancelled and will not proceed.
        /// </summary>
        Cancelled,

        /// <summary>
        /// The order is being returned by the customer.
        /// </summary>
        Returning,

        /// <summary>
        /// The order has been successfully returned to the warehouse or seller.
        /// </summary>
        Returned
    }
}
