using Automarket.Domain.Entity;
using Automarket.Domain.Helpers;
using Automarket.Domain.ViewModels.Account;
using Automarket.Domain.ViewModels.AdminPanel;
using Automarket.Service.Implementations;
using Automarket.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Automarket.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAutoServiceService _autoServiceService;
        private readonly IAccountService _accountService;
        private readonly IConsumableService _consumableService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminController(IAutoServiceService autoServiceService, IAccountService accountService, 
            IHttpContextAccessor httpContextAccessor, IConsumableService consumableService)
        {
            _autoServiceService = autoServiceService;
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
            _consumableService = consumableService;

        }

        public async Task<IActionResult> Adminpanel()
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Forbidden", "Errors");
            }

            var serviceResponse = await _autoServiceService.GetAppointments();
            var userResponse = await _accountService.GetUsers();
            var itemResponse = await _consumableService.GetItems();

            if (userResponse.StatusCode == Domain.Enum.StatusCode.OK && itemResponse.StatusCode == Domain.Enum.StatusCode.OK &&
                serviceResponse.StatusCode == Domain.Enum.StatusCode.OK)
            {
                var viewModel = new AdminPanelViewModel
                {
                    Appointments = serviceResponse.Data,
                    Users = userResponse.Data,
                    Items = itemResponse.Data
                };

                return View(viewModel);
            }
            else if (serviceResponse.StatusCode == Domain.Enum.StatusCode.InternalServerError || 
                        userResponse.StatusCode == Domain.Enum.StatusCode.InternalServerError ||
                        itemResponse.StatusCode == Domain.Enum.StatusCode.InternalServerError)
            {
                return RedirectToAction("InternalServerError", "Errors");
            }

            return RedirectToAction("Error", "Errors");
        }


        [HttpGet]
        public async Task<IActionResult> CreateAccount()
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Forbidden", "Errors");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(AccountViewModel user)
        {
            if (User.IsInRole("Admin"))
            {
                if (ModelState.IsValid)
                {
                    var response = await _accountService.CreateAccount(user);

                    if (response.StatusCode == Domain.Enum.StatusCode.OK)
                    {
                        TempData["AlertMessage"] = response.Description;
                        TempData["ResponseStatus"] = response.StatusCode.ToString();
                        ModelState.AddModelError("", response.Description);
                        return RedirectToActionPermanent("Profile", "Account", new { id = response.Data.Id });
                    }

                    TempData["AlertMessage"] = response.Description;
                    TempData["ResponseStatus"] = "Error";
                }
                return View(user);
            }
            else
            {
                return RedirectToAction("Forbidden", "Errors");
            }
        }

        public async Task<IActionResult> DeleteAccount(long id)
        {
            if (User.IsInRole("Admin"))
            {
                var response = await _accountService.DeleteAccount(id);                              

                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    TempData["AlertMessage"] = response.Description;
                    TempData["ResponseStatus"] = response.StatusCode.ToString();
                    return RedirectToAction("Adminpanel", "Admin");
                }
                else
                {
                    var referer = Request.Headers["Referer"].ToString();
                    TempData["AlertMessage"] = response.Description;
                    TempData["ResponseStatus"] = "Error";
                    return Redirect(referer);
                }
            }

            return RedirectToAction("Forbidden", "Errors");
        }
    }
}
