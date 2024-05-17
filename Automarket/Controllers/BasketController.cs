using Automarket.Service.Implementations;
using Automarket.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Automarket.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<IActionResult> GetBasket()
        {
            var response = await _basketService.GetBasket();

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

        public async Task<IActionResult> DeleteFromBasket(long id)
        {
            var returnUrl = Request.Headers["Referer"].ToString();
            var response = await _basketService.DeleteFromBasket(id);

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

        public async Task<IActionResult> AddToBasket(long id)
        {
            var returnUrl = Request.Headers["Referer"].ToString();
            var response = await _basketService.AddToBasket(id);

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

        public async Task<IActionResult> Plus(long id)
        {
            var response = await _basketService.Plus(id);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetBasket", "Basket");
            }
            else if (response.StatusCode == Domain.Enum.StatusCode.InternalServerError)
            {
                TempData["AlertMessage"] = response.Description;
                TempData["ResponseStatus"] = response.StatusCode.ToString();
                ModelState.AddModelError("", response.Description);
                return RedirectToActionPermanent("GetBasket", "Basket");
            }
            return RedirectToAction("Error", "Errors");
        }

        public async Task<IActionResult> Minus(long id)
        {
            var response = await _basketService.Minus(id);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetBasket", "Basket");
            }
            else if (response.StatusCode == Domain.Enum.StatusCode.InternalServerError)
            {
                TempData["AlertMessage"] = response.Description;
                TempData["ResponseStatus"] = response.StatusCode.ToString();
                ModelState.AddModelError("", response.Description);
                return RedirectToActionPermanent("GetBasket", "Basket");
            }
            return RedirectToAction("Error", "Errors");
        }
    }
}
