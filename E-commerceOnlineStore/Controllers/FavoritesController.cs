using E_commerceOnlineStore.Models.DataModels.CartsAndFavourites;
using E_commerceOnlineStore.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace E_commerceOnlineStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritesController : ControllerBase
    {
        //private readonly IFavoritesService _wishlistService;

        //public FavoritesController(IFavoritesService wishlistService)
        //{
        //    _wishlistService = wishlistService;
        //}

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Favorite>>> GetWishlist()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var userId = User.Identity.Name;
        //        var wishlist = await _wishlistService.GetWishlistAsync(userId);
        //        return Ok(wishlist);
        //    }
        //    else
        //    {
        //        var wishlist = GetWishlistFromCookies();
        //        return Ok(wishlist);
        //    }
        //}

        //[HttpPost]
        //public async Task<ActionResult<Favorite>> AddToWishlist([FromBody] Favorite item)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        item.UserId = User.Identity.Name;
        //        item.DateAdded = DateTime.UtcNow;
        //        var createdItem = await _wishlistService.AddToWishlistAsync(item);
        //        return CreatedAtAction(nameof(GetWishlist), new { id = createdItem.Id }, createdItem);
        //    }
        //    else
        //    {
        //        var wishlist = GetWishlistFromCookies();
        //        wishlist.Add(item);
        //        SaveWishlistToCookies(wishlist);
        //        return Ok(item);
        //    }
        //}

        //[HttpDelete("{productId}")]
        //public async Task<IActionResult> RemoveFromWishlist(int productId)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var userId = User.Identity.Name;
        //        var removed = await _wishlistService.RemoveFromWishlistAsync(userId, productId);
        //        if (!removed)
        //        {
        //            return NotFound();
        //        }

        //        return NoContent();
        //    }
        //    else
        //    {
        //        var wishlist = GetWishlistFromCookies();
        //        var itemToRemove = wishlist.FirstOrDefault(w => w.ProductId == productId);
        //        if (itemToRemove != null)
        //        {
        //            wishlist.Remove(itemToRemove);
        //            SaveWishlistToCookies(wishlist);
        //        }
        //        return NoContent();
        //    }
        //}

        //[Authorize]
        //[HttpPost("migrate")]
        //public async Task<IActionResult> MigrateWishlist()
        //{
        //    var wishlistFromCookies = GetWishlistFromCookies();
        //    var userId = User.Identity.Name;

        //    await _wishlistService.MigrateWishlistAsync(userId, wishlistFromCookies);
        //    ClearWishlistCookies();
        //    return Ok();
        //}

        //private List<Favorite> GetWishlistFromCookies()
        //{
        //    var cookie = Request.Cookies["wishlist"];
        //    return cookie != null
        //        ? JsonSerializer.Deserialize<List<Favorite>>(cookie) ?? new List<Favorite>()
        //        : new List<Favorite>();
        //}

        //private void SaveWishlistToCookies(List<Favorite> wishlist)
        //{
        //    var cookieOptions = new CookieOptions
        //    {
        //        Expires = DateTime.UtcNow.AddDays(30),
        //        HttpOnly = true
        //    };
        //    var cookieValue = JsonSerializer.Serialize(wishlist);
        //    Response.Cookies.Append("wishlist", cookieValue, cookieOptions);
        //}

        //private void ClearWishlistCookies()
        //{
        //    Response.Cookies.Delete("wishlist");
        //}
    }
}
