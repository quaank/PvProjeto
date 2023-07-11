using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoPv.Areas.Identity.Data;
using ProjetoPv.Models;
using System.Data;
using System.Diagnostics;
using static System.Formats.Asn1.AsnWriter;

namespace ProjetoPv.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<ProjetoPvUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        

        public HomeController( RoleManager<IdentityRole> roleManager,UserManager<ProjetoPvUser> userManager, ILogger<HomeController> logger)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users
                .Select(user => new {
                    Id = user.Id,
                    Username = user.UserName,
                    Role = _userManager.GetRolesAsync(user).Result.FirstOrDefault()
                }).ToListAsync();

            return View(users);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Edit(string id)
        {
            // Get the user
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Get the list of user roles
            var userRoles = await _userManager.GetRolesAsync(user);

            // Get the list of all roles
            var allRoles = await _roleManager.Roles.ToListAsync();

            // Build the view model
            var model = new ChangeRoleViewModel
            {
                UserId = user.Id,
                UserEmail = user.Email,
                UserRole = userRoles.FirstOrDefault(),
                AllRoles = allRoles
            };

            // Send the model to the view
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(string id, ChangeRoleViewModel model)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var currentRole = userRoles.FirstOrDefault();

                if (currentRole != null && !currentRole.Equals(model.UserRole))
                {
                    await _userManager.RemoveFromRoleAsync(user, currentRole);
                }
                if (await _roleManager.RoleExistsAsync(model.UserRole))
                {
                    await _userManager.AddToRoleAsync(user, model.UserRole);
                }
            }

            return RedirectToAction("Index");
        }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}