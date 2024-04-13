using Automarket.Domain.Helpers;
using Automarket.Domain.ViewModels.Account;
using Automarket.Domain.ViewModels.ConsumableViewModel;
using Automarket.Service.Implementations;
using Automarket.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Automarket.Controllers
{
    public class ConsumableController : Controller
    {
        private readonly IConsumableService _consumableService;
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ConsumableController (IConsumableService consumableService, IAccountService accountService, IHttpContextAccessor httpContextAccessor)
        {
            _consumableService = consumableService;
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var response = await _consumableService.GetItems();

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

        [HttpGet]
        public async Task<IActionResult> GetItem(long id)
        {
            var response = await _consumableService.GetItem(id);

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

        [HttpGet]
        public async Task<IActionResult> CreateItem()
        {
            if (!User.IsInRole("Admin") && !User.IsInRole("Administrator"))
            {
                return RedirectToAction("Forbidden", "Errors");
            }

            return await Task.FromResult(View());
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem(ConsumableViewModel item)
        {
            if (User.IsInRole("Admin"))
            {
                if (ModelState.IsValid)
                {
                    var response = await _consumableService.CreateItem(item);

                    if (response.StatusCode == Domain.Enum.StatusCode.OK)
                    {
                        TempData["AlertMessage"] = response.Description;
                        TempData["ResponseStatus"] = response.StatusCode.ToString();
                        ModelState.AddModelError("", response.Description);
                        return RedirectToActionPermanent("GetItem", "Consumable", new { id = response.Data.Id });
                    }

                    TempData["AlertMessage"] = response.Description;
                    TempData["ResponseStatus"] = "Error";
                }
                return View(item);
            }
            else
            {
                return RedirectToAction("Forbidden", "Errors");
            }
        }

        public async Task<IActionResult> DeleteItem(long id)
        {
            if (User.IsInRole("Admin"))
            {
                var response = await _consumableService.DeleteItem(id);

                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    TempData["AlertMessage"] = response.Description;
                    TempData["ResponseStatus"] = response.StatusCode.ToString();

                    var referer = Request.Headers["Referer"].ToString();

                    if (!string.IsNullOrEmpty(referer) && referer.Contains("Adminpanel"))
                    {
                        return Redirect(referer);
                    }
                    else
                    {
                        return RedirectToAction("GetItems", "Consumable");
                    }
                }
                else
                {
                    var referer = Request.Headers["Referer"].ToString();
                    TempData["AlertMessage"] = response.Description;
                    TempData["ResponseStatus"] = "Error";

                    if (!string.IsNullOrEmpty(referer) && referer.Contains("Adminpanel"))
                    {
                        return Redirect(referer);
                    }
                    else
                    {
                        return RedirectToAction("GetItems", "Consumable");
                    }
                }
            }

            return RedirectToAction("Forbidden", "Errors");
        }


        [HttpGet]
        public async Task<IActionResult> EditItem(long id)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Administrator"))
            {
                var response = await _consumableService.GetItem(id);
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
            else
            {
                return RedirectToAction("Forbidden", "Errors");
            }
        }


        [HttpPost]
        public async Task<IActionResult> EditItem(ConsumableViewModel item)
        {
            if (User.IsInRole("Admin"))
            {
                if (ModelState.IsValid)
                {
                    var response = await _consumableService.EditItem(item.Id, item);

                    if (response.StatusCode == Domain.Enum.StatusCode.OK)
                    {
                        TempData["AlertMessage"] = response.Description;
                        TempData["ResponseStatus"] = response.StatusCode.ToString();
                        ModelState.AddModelError("", response.Description);
                        return RedirectToAction("GetItem", "Consumable", new { id = item.Id });
                    }

                    TempData["AlertMessage"] = response.Description;
                    TempData["ResponseStatus"] = "Error";
                }
                return View(item);
            }
            else
            {
                return RedirectToAction("Forbidden", "Errors");
            }
        }
    }
}
