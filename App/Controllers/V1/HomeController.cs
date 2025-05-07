using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers.V1
{
    [ApiController]
    [Route("/")]
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
        
        [HttpGet("Admin")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> AdminAsync()
        {
            return View();
        }
        
        [HttpGet("Privacy")]
        [Authorize]
        public async Task<IActionResult> PrivacyAsync()
        {
            return View();
        }
    }
}
