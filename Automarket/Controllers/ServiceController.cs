using Automarket.Domain.Helpers;
using Automarket.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Automarket.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountService _accountService;

        public ServiceController(IHttpContextAccessor httpContextAccessor, IAccountService accountService)
        {
            _httpContextAccessor = httpContextAccessor;
            _accountService = accountService;
        }

        public async Task<IActionResult> Services()
        {
            return await Task.FromResult(View());
        }

        public async Task<IActionResult> EngineService()
        {
            return await Task.FromResult(View("ServicesList/EngineService"));
        }


        public async Task<IActionResult> ClutchService()
        {
            return await Task.FromResult(View("ServicesList/ClutchService"));
        }

        public async Task<IActionResult> ComputerService()
        {
            return await Task.FromResult(View("ServicesList/ComputerService"));
        }

        public async Task<IActionResult> ChassisService()
        {
            return await Task.FromResult(View("ServicesList/ChassisService"));
        }

        public async Task<IActionResult> TurbineService()
        {
            return await Task.FromResult(View("ServicesList/TurbineService"));
        }

        public async Task<IActionResult> AlignmentService()
        {
            return await Task.FromResult(View("ServicesList/AlignmentService"));
        }

        public async Task<IActionResult> ElectricityService()
        {
            return await Task.FromResult(View("ServicesList/ElectricityService"));
        }

        public async Task<IActionResult> RackService()
        {
            return await Task.FromResult(View("ServicesList/RackService"));
        }
    }
}
