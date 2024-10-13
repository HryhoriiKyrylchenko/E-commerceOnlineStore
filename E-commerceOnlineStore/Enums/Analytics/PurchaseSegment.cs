namespace E_commerceOnlineStore.Enums.Analytics
{
    /// <summary>
    /// Represents different segments based on the type of products customers typically purchase and their buying behavior.
    /// </summary>
    public enum PurchaseSegment
    {
        /// <summary>
        /// Represents customers who primarily buy budget-friendly products.
        /// </summary>
        BudgetProducts,

        /// <summary>
        /// Represents customers who typically purchase premium or high-end products.
        /// </summary>
        PremiumProducts,

        /// <summary>
        /// Represents customers who prefer to shop when discounts are available.
        /// </summary>
        DiscountShopper,

        /// <summary>
        /// Represents customers who tend to pay full price for products and do not wait for discounts.
        /// </summary>
        FullPriceShopper,

        /// <summary>
        /// Represents customers who make occasional purchases.
        /// </summary>
        OccasionalBuyer,

        /// <summary>
        /// Represents customers who frequently make purchases.
        /// </summary>
        FrequentBuyer
    }
}
