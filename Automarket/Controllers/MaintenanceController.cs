﻿using Automarket.Domain.Entity;
using Automarket.Domain.ViewModels.AutoService;
using Automarket.Domain.ViewModels.Order;
using Automarket.Service.Implementations;
using Automarket.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Automarket.Controllers
{
    public class MaintenanceController : Controller
    {
        private readonly IMaintenanceService _maintenanceService;

        public MaintenanceController(IMaintenanceService maintenanceService)
        {
            _maintenanceService = maintenanceService;
        }

        public async Task<IActionResult> GetMaintenances()
        {
            var response = await _maintenanceService.GetMaintenances();

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

        public async Task<IActionResult> GetMaintenance(long id)
        {
            var response = await _maintenanceService.GetMaintenance(id);

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
        public async Task<IActionResult> CreateMaintenance()
        {
            if (!User.IsInRole("Admin") && !User.IsInRole("Administrator"))
            {
                return RedirectToAction("Forbidden", "Errors");
            }

            return await Task.FromResult(View());
        }

        [HttpPost]
        public async Task<IActionResult> CreateMaintenance(MaintenanceViewModel maintenance)
        {
            if (User.IsInRole("Admin"))
            {
                if (ModelState.IsValid)
                {
                    var response = await _maintenanceService.CreateMaintenance(maintenance);

                    if (response.StatusCode == Domain.Enum.StatusCode.OK)
                    {
                        TempData["AlertMessage"] = response.Description;
                        TempData["ResponseStatus"] = response.StatusCode.ToString();
                        return RedirectToAction("GetMaintenance", "Maintenance", new { id = response.Data.Id });
                    }

                    TempData["AlertMessage"] = response.Description;
                    TempData["ResponseStatus"] = "Error";
                }

                return View(maintenance);
            }
            else
            {
                return RedirectToAction("Forbidden", "Errors");
            }
        }

        public async Task<IActionResult> DeleteMaintenance(long id)
        {
            if (User.IsInRole("Admin"))
            {
                var response = await _maintenanceService.DeleteMaintenance(id);

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

        [HttpPost]
        public async Task<IActionResult> GetMaintenance(MaintenanceViewModel maintenance)
        {
            if (User.IsInRole("Admin"))
            {
                if (ModelState.IsValid)
                {
                    var response = await _maintenanceService.EditMaintenance(maintenance.Id, maintenance);

                    if (response.StatusCode == Domain.Enum.StatusCode.OK)
                    {
                        TempData["AlertMessage"] = response.Description;
                        TempData["ResponseStatus"] = response.StatusCode.ToString();
                        ModelState.AddModelError("", response.Description);
                        return RedirectToAction("GetMaintenance", "Maintenance", new { id = maintenance.Id });
                    }

                    TempData["AlertMessage"] = response.Description;
                    TempData["ResponseStatus"] = "Error";
                }
                return View(maintenance);
            }
            else
            {
                return RedirectToAction("Forbidden", "Errors");
            }
        }
    }
}