using Automarket.Domain.Entity;
using Automarket.Domain.Helpers;
using Automarket.Domain.ViewModels.ConsumableViewModel;
using Automarket.Domain.ViewModels.Order;
using Automarket.Service.Implementations;
using Automarket.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Automarket.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppointmentController (IAppointmentService appointmentService, IAccountService accountService, IHttpContextAccessor httpContextAccessor)
        {
            _appointmentService = appointmentService;            
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> GetAppointments()
        {
            var response = await _appointmentService.GetAppointments();

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

        public async Task<IActionResult> GetAppointment(long id)
        {
            var response = await _appointmentService.GetAppointment(id);

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
        public async Task<IActionResult> CreateAppointment()
        {
            if (!User.IsInRole("Admin") && !User.IsInRole("Administrator"))
            {
                return RedirectToAction("Forbidden", "Errors");
            }

            return await Task.FromResult(View());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(AppointmentViewModel appointment)
        {
            if (User.IsInRole("Admin"))
            {
                if (ModelState.IsValid)
                {
                    string userEmail = await _accountService.GetUserEmail();
                    User user = new User { Email = userEmail };

                    var response = await _appointmentService.CreateAppointment(appointment, user);

                    if (response.StatusCode == Domain.Enum.StatusCode.OK)
                    {
                        TempData["AlertMessage"] = response.Description;
                        TempData["ResponseStatus"] = response.StatusCode.ToString();
                        return RedirectToAction("GetAppointment", "Appointment", new { id = response.Data.Id });
                    }

                    TempData["AlertMessage"] = response.Description;
                    TempData["ResponseStatus"] = "Error";
                }

                return View("/Views/Home/Index.cshtml", appointment);
            }
            else
            {
                return RedirectToAction("Forbidden", "Errors");
            }
        }

        public async Task<IActionResult> DeleteAppointment(long id)
        {
            if (User.IsInRole("Admin"))
            {
                var response = await _appointmentService.DeleteAppointment(id);

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

                    return RedirectToAction("Adminpanel", "Admin");
                }
            }

            return RedirectToAction("Forbidden", "Errors");
        }

        //[HttpGet]
        //public async Task<IActionResult> EditAppointment(long id)
        //{
        //    var userEmailHelper = new GetUserEmailHelper(_httpContextAccessor);
        //    string userEmail = userEmailHelper.GetUserUserEmail();
        //    ViewBag.UserId = await _accountService.GetIdByEmail(userEmail);

        //    if (User.IsInRole("Admin") || User.IsInRole("Administrator"))
        //    {
        //        var response = await _autoServiceService.GetAppointment(id);
        //        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        //        {
        //            return View(response.Data);
        //        }
        //        else if (response.StatusCode == Domain.Enum.StatusCode.InternalServerError)
        //        {
        //            return RedirectToAction("InternalServerError", "Errors");
        //        }
        //        return RedirectToAction("Error", "Errors");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Forbidden", "Errors");
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> GetAppointment(AppointmentViewModel model)
        {
            if (User.IsInRole("Admin"))
            {
                if (ModelState.IsValid)
                {
                    var response = await _appointmentService.EditAppointment(model.Id, model);

                    if (response.StatusCode == Domain.Enum.StatusCode.OK)
                    {
                        TempData["AlertMessage"] = response.Description;
                        TempData["ResponseStatus"] = response.StatusCode.ToString();
                        ModelState.AddModelError("", response.Description);
                        return RedirectToAction("GetAppointment", "Appointment", new { id = model.Id });
                    }

                    TempData["AlertMessage"] = response.Description;
                    TempData["ResponseStatus"] = "Error";
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("Forbidden", "Errors");
            }
        }
    }
}
