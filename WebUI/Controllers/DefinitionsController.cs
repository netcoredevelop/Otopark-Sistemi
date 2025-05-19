using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebUI.Auth;

namespace WebUI.Controllers
{
    [Authorize]
    public class DefinitionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
} 