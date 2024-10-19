namespace E_commerceOnlineStore.Enums.UserManagement
{
    /// <summary>
    /// Represents the different roles a user can have within the application.
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// Represents a user with administrative privileges, 
        /// capable of managing system settings and users.
        /// </summary>
        Administrator,

        /// <summary>
        /// Represents a user responsible for managing business operations, 
        /// including overseeing sales and marketing strategies.
        /// </summary>
        BusinessManager,

        /// <summary>
        /// Represents a user in charge of sales activities, 
        /// focusing on customer relations and sales performance.
        /// </summary>
        SalesManager,

        /// <summary>
        /// Represents an employee working in the store, 
        /// assisting customers and handling sales transactions.
        /// </summary>
        StoreEmployee,

        /// <summary>
        /// Represents a user managing warehouse operations, 
        /// overseeing inventory management and logistics.
        /// </summary>
        WarehouseManager,

        /// <summary>
        /// Represents an employee working in the warehouse, 
        /// assisting with inventory handling and storage.
        /// </summary>
        WarehouseEmployee,

        /// <summary>
        /// Represents a user who is a customer of the application, 
        /// able to browse products and make purchases.
        /// </summary>
        Customer
    }

}
