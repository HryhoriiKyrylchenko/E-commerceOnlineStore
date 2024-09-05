using E_commerceOnlineStore.Models.DataModels.CartsAndWishlists;

namespace E_commerceOnlineStore.Services.Data
{
    public interface IWishlistService
    {
        Task<IEnumerable<WishlistItem>> GetWishlistAsync(string userId);
        Task<WishlistItem?> AddToWishlistAsync(WishlistItem item);
        Task<bool> RemoveFromWishlistAsync(string userId, int productId);
        Task MigrateWishlistAsync(string userId, List<WishlistItem> wishlistFromCookies);
    }
}
