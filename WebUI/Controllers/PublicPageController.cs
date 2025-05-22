using Application.Interfaces;
using Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class PublicPageController : Controller
    {

        public PublicPageController()
        {
            
        }

        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10)
        {
            return View();
        }

    }
}
