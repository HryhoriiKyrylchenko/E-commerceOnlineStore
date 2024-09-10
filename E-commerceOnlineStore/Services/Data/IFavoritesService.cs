using E_commerceOnlineStore.Models.DataModels.CartsAndFavourites;

namespace E_commerceOnlineStore.Services.Data
{
    public interface IFavoritesService
    {
        Task<IEnumerable<ProductFavorite>> GetWishlistAsync(string userId);
        Task<ProductFavorite?> AddToWishlistAsync(ProductFavorite item);
        Task<bool> RemoveFromWishlistAsync(string userId, int productId);
        Task MigrateWishlistAsync(string userId, List<ProductFavorite> wishlistFromCookies);
    }
}
