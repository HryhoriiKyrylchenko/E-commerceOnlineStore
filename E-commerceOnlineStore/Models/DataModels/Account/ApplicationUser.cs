﻿using E_commerceOnlineStore.Enums.Account;
using E_commerceOnlineStore.Models.DataModels.Notifications;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerceOnlineStore.Models.DataModels.Account
{
    /// <summary>
    /// Represents an application user.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the user's first name.
        /// </summary>
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's last name.
        /// </summary>
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's date of birth.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the user's gender.
        /// </summary>
        [MaxLength(10)]
        public GenderType? Gender { get; set; }

        /// <summary>
        /// Gets or sets the URL of the user's profile picture.
        /// </summary>
        public string? ProfilePictureUrl { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was created. 
        /// The default value is set to the current UTC date and time.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the date and time when the entity was last updated. 
        /// This value is nullable and can be null if the entity has not been updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the user's preferred language for application localization.
        /// This property is nullable to accommodate users who may not have a preferred language set.
        /// </summary>
        public string? PreferredLanguage { get; set; }

        /// <summary>
        /// Gets or sets the user's time zone to adjust dates and times according to their local time.
        /// This property is nullable to accommodate users who may not have a time zone set.
        /// </summary>
        public string? TimeZone { get; set; }

        /// <summary>
        /// Gets or sets whether the user is active.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Gets or sets user addresses.
        /// </summary>
        public virtual ICollection<Address> Addresses { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of refresh tokens associated with the user.
        /// </summary>
        /// <remarks>
        /// This collection is typically used to store multiple refresh tokens for the user, 
        /// allowing for the management of issued tokens for security purposes.
        /// </remarks>
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of notifications associated with the user.
        /// </summary>
        public virtual ICollection<Notification> Notifications { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of newsletter subscriptions associated with the user.
        /// </summary>
        public virtual ICollection<NewsletterSubscription> NewsletterSubscriptions { get; set; } = [];
    }
}
