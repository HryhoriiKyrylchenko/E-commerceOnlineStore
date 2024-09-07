using E_commerceOnlineStore.Models.DataModels.CartsAndWishlists;

namespace E_commerceOnlineStore.Services.Data
{
    public interface IWishlistService
    {
        Task<IEnumerable<Favorite>> GetWishlistAsync(string userId);
        Task<Favorite?> AddToWishlistAsync(Favorite item);
        Task<bool> RemoveFromWishlistAsync(string userId, int productId);
        Task MigrateWishlistAsync(string userId, List<Favorite> wishlistFromCookies);
    }
}
