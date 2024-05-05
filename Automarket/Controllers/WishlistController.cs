using Automarket.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Automarket.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController (IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        public async Task<IActionResult> GetWishlist()
        {
            var response = await _wishlistService.GetWishlist();

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            else if (response.StatusCode == Domain.Enum.StatusCode.InternalServerError)
            {
                return RedirectToAction("InternalServerError", "Errors");
            }
            return RedirectToAction("Error", "Errors");
        }

        public async Task<IActionResult> DeleteFromWishlist(long id)
        {
            var returnUrl = Request.Headers["Referer"].ToString();
            var response = await _wishlistService.DeleteFromWishlist(id);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Redirect(returnUrl);
            }
            else if (response.StatusCode == Domain.Enum.StatusCode.InternalServerError)
            {
                return RedirectToAction("InternalServerError", "Errors");
            }

            return RedirectToAction("Error", "Errors");
        }

        public async Task<IActionResult> AddToWishlist(long id)
        {
            var returnUrl = Request.Headers["Referer"].ToString();
            var response = await _wishlistService.AddToWishlist(id);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Redirect(returnUrl);
            }
            else if (response.StatusCode == Domain.Enum.StatusCode.InternalServerError)
            {
                return RedirectToAction("InternalServerError", "Errors");
            }

            return RedirectToAction("Error", "Errors");
        }

    }
}
