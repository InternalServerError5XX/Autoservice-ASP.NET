using Automarket.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Automarket.Domain.ViewModels.Account;
using Automarket.Service.Implementations;
using Automarket.Domain.Entity;
using Automarket.Domain.Helpers;

namespace Automarket.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IAccountService accountService, IHttpContextAccessor httpContextAccessor)
        {
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;

        }

        [HttpGet]
        public IActionResult Signup() => View();

        [HttpPost]
        public async Task<IActionResult> Signup(SignupViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.Signup(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.Login(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(model);
        }               

        [HttpGet]
        public async Task<IActionResult> Profile(long id)
        {
            var response = await _accountService.GetProfile(id);
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
        public async Task<IActionResult> ChangeProfile(long id)
        {
            var userId = await _accountService.GetIdByEmail();

            if (userId.Data == id || User.IsInRole("Admin"))
            {
                var response = await _accountService.GetProfile(id);
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
        public async Task<IActionResult> ChangeProfile(AccountViewModel user)
        {
            var userId = await _accountService.GetIdByEmail();


            if (userId.StatusCode == Domain.Enum.StatusCode.OK)
            {
                if (userId.Data == user.Id || User.IsInRole("Admin"))
                {
                    if (ModelState.IsValid)
                    {
                        var response = await _accountService.EditAccount(user.Id, user);

                        if (response.StatusCode == Domain.Enum.StatusCode.OK)
                        {                           
                            TempData["AlertMessage"] = response.Description;
                            TempData["ResponseStatus"] = response.StatusCode.ToString();
                            ModelState.AddModelError("", response.Description);
                            return RedirectToAction("Profile", "Account", new { id = user.Id });
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
            else if (userId.StatusCode == Domain.Enum.StatusCode.InternalServerError)
            {
                return RedirectToAction("InternalServerError", "Errors");
            }
            return RedirectToAction("Error", "Errors");
        }


        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}