using E_commerceOnlineStore.Data;
using E_commerceOnlineStore.Models.DataModels.CartsAndWishlists;
using Microsoft.EntityFrameworkCore;
using System;

namespace E_commerceOnlineStore.Services.Data
{
    public class WishlistService : IWishlistService
    {
        private readonly ApplicationDbContext _context;

        public WishlistService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Favorite>> GetWishlistAsync(string userId)
        {
            return await _context.WishlistItems.Where(w => w.UserId == userId).ToListAsync();
        }

        public async Task<Favorite?> AddToWishlistAsync(Favorite item)
        {
            _context.WishlistItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> RemoveFromWishlistAsync(string userId, int productId)
        {
            var item = await _context.WishlistItems.FirstOrDefaultAsync(w => w.UserId == userId && w.ProductId == productId);
            if (item == null)
            {
                return false;
            }

            _context.WishlistItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task MigrateWishlistAsync(string userId, List<Favorite> wishlistFromCookies)
        {
            foreach (var item in wishlistFromCookies)
            {
                item.UserId = userId;
                item.DateAdded = DateTime.UtcNow;
                _context.WishlistItems.Add(item);
            }

            await _context.SaveChangesAsync();
        }
    }
}
