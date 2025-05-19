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
        private readonly ILogger<HomeController> _logger;

        public HomeController(IVehicleService vehicleService, ILogger<HomeController> logger)
        {
            _vehicleService = vehicleService;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10)
        {
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
