using Application.Interfaces;
using Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IBranchService _branchService;
        private readonly ILogger<HomeController> _logger;
        private readonly IVehicleTypeService _vehicleTypeService;

        public HomeController(IVehicleService vehicleService,IBranchService branchService, ILogger<HomeController> logger, IVehicleTypeService vehicleTypeService)
        {
            _vehicleService = vehicleService;
            _branchService = branchService;
            _logger = logger;
            _vehicleTypeService = vehicleTypeService;
        }

        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10)
        {
            ViewBag.Branches = await _branchService.GetAllAsync();
            var vehicles = await _vehicleService.GetPagedAsync(pageIndex, pageSize);
            return View(vehicles);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
