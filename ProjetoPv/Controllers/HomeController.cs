    using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoPv.Areas.Identity.Data;
using ProjetoPv.Models;
using System.Diagnostics;
using static System.Formats.Asn1.AsnWriter;

namespace ProjetoPv.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<ProjetoPvUser> _userManager;
         

        public HomeController(UserManager<ProjetoPvUser> userManager, ILogger<HomeController> logger)
        {
            _logger = logger;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
           
            ViewData["Id"] = new SelectList(_userManager.Users, "Email");
            return View(_userManager.Users.ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}