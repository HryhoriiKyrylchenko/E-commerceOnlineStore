using E_commerceOnlineStore.Data;
using E_commerceOnlineStore.Models.DataModels.CartsAndFavourites;
using Microsoft.EntityFrameworkCore;
using System;

namespace E_commerceOnlineStore.Services.Data
{
    public class FavoritesService(ApplicationDbContext context) : IFavoritesService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IEnumerable<ProductFavorite>> GetWishlistAsync(string userId)
        {
            return await _context.Favorites.Where(f => f.CustomerId == userId).ToListAsync();
        }

        public async Task<ProductFavorite?> AddToWishlistAsync(ProductFavorite item)
        {
            _context.Favorites.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> RemoveFromWishlistAsync(string userId, int productId)
        {
            //var item = await _context.Favorites.FirstOrDefaultAsync(w => w.UserId == userId && w.ProductId == productId);
            //if (item == null)
            //{
            //    return false;
            //}

            //_context.WishlistItems.Remove(item);
            //await _context.SaveChangesAsync();
            return true;
        }

        public async Task MigrateWishlistAsync(string userId, List<ProductFavorite> wishlistFromCookies)
        {
            //foreach (var item in wishlistFromCookies)
            //{
            //    item.UserId = userId;
            //    item.DateAdded = DateTime.UtcNow;
            //    _context.WishlistItems.Add(item);
            //}

            //await _context.SaveChangesAsync();
        }
    }
}
