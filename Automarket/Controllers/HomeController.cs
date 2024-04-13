using Automarket.DAL.Interfaces;
using Automarket.Domain.Entity;
using Automarket.Domain.Helpers;
using Automarket.Models;
using Automarket.Service.Implementations;
using Automarket.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Automarket.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return await Task.FromResult(View());
        }

        public async Task<IActionResult> About()
        {
            return await Task.FromResult(View());
        }
    }
}