﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_commerceOnlineStore.Models
{
    /// <summary>
    /// Represents an item in a return request.
    /// </summary>
    [Table("ReturnRequestItems")]
    public class ReturnRequestItem
    {
        /// <summary>
        /// Gets or sets the return request item ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the return request ID associated with this item.
        /// </summary>
        [Required]
        public int ReturnRequestId { get; set; }

        /// <summary>
        /// Gets or sets the return request associated with this item.
        /// </summary>
        [ForeignKey(nameof(ReturnRequestId))]
        public virtual ReturnRequest ReturnRequest { get; set; } = null!;

        /// <summary>
        /// Gets or sets the product ID being returned.
        /// </summary>
        [Required]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product being returned.
        /// </summary>
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; } = null!;

        /// <summary>
        /// Gets or sets the quantity of the product being returned.
        /// </summary>
        [Required]
        public int Quantity { get; set; }
    }
}
