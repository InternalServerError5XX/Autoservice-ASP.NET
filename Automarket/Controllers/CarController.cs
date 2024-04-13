using Automarket.DAL.Interfaces;
using Automarket.Domain.Entity;
using Automarket.Domain.Enum;
using Automarket.Domain.Helpers;
using Automarket.Domain.ViewModels.Car;
using Automarket.Service.Implementations;
using Automarket.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Automarket.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CarController(ICarService carService, IAccountService accountService, IHttpContextAccessor httpContextAccessor)
        {
            _carService = carService;
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            var response = await _carService.GetCars();           
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                TempData["AlertMessage"] = response.Description;
                TempData["ResponseStatus"] = response.StatusCode.ToString();
                return View(response.Data);
            }
            else if (response.StatusCode == Domain.Enum.StatusCode.InternalServerError)
            {
                return RedirectToAction("InternalServerError", "Errors");
            }
            return RedirectToAction("Error", "Errors");
        }

        //[HttpGet]
        //public async Task<IActionResult> GetCars(string request)
        //{
        //    var response = await _carService.GetCars();
        //    if (response.StatusCode == Domain.Enum.StatusCode.OK)
        //    {
        //        return View(response.Data.ToList());
        //    }
        //    return RedirectToAction("Error");
        //}

        [HttpGet]
        public async Task<IActionResult> GetCar(long id)
        {
            var response = await _carService.GetCar(id);
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

        public async Task<IActionResult> Delete (long id)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Forbidden", "Errors");
            }

            var response = await _carService.DeleteCar(id);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                TempData["AlertMessage"] = response.Description;
                TempData["ResponseStatus"] = response.StatusCode.ToString();
                return RedirectToAction("GetCars");
            }
            else if (response.StatusCode == Domain.Enum.StatusCode.InternalServerError)
            {
                TempData["AlertMessage"] = response.Description;
            }
            return RedirectToAction("Error", "Errors");
        }

        [HttpGet]
        public async Task<IActionResult> Save(long id)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Forbidden", "Errors");
            }
            if (id == 0)
            {
                return View();
            }

            var response = await _carService.GetCar(id);

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

        [HttpPost]
        public async Task<IActionResult> Save(CarViewModel carViewModel)
        {       
            if (!ModelState.IsValid)
            {
                if (carViewModel.Id == 0)
                {
                    var response = await _carService.CreateCar(carViewModel);

                    if (response.StatusCode == Domain.Enum.StatusCode.OK)
                    {
                        TempData["AlertMessage"] = response.Description;
                    }
                    else if (response.StatusCode == Domain.Enum.StatusCode.InternalServerError)
                    {
                        return RedirectToAction("InternalServerError", "Errors");
                    }
                    return RedirectToAction("Error", "Errors");
                }
                else
                {
                    var response = await _carService.Edit(carViewModel.Id, carViewModel);

                    if (response.StatusCode == Domain.Enum.StatusCode.OK)
                    {
                        TempData["AlertMessage"] = response.Description;
                    }
                    else if (response.StatusCode == Domain.Enum.StatusCode.InternalServerError)
                    {
                        return RedirectToAction("InternalServerError", "Errors");
                    }
                    return RedirectToAction("Error", "Errors");
                }
            }
            return RedirectToAction("GetCars");
        }

        [HttpPost]
        public JsonResult GetTypes()
        {
            var types = _carService.GetTypes();
            return Json(types.Data);
        }
    }
}
