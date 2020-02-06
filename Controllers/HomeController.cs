using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using javora.Models;
using Microsoft.AspNetCore.Identity;
using javora.Models.Database;

namespace javora.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _user;
        private readonly RoleManager<IdentityRole> _role;

        public HomeController(ILogger<HomeController> logger, UserManager<User> user, RoleManager<IdentityRole> role)
        {
            _logger = logger;
            _user = user;
            _role = role;
        }

        public async Task<IActionResult> Index()
        {
            //var user = new User { UserName = "Admin" };
            //var res = await _user.CreateAsync(user, "!QA2ws3ed");
            //IdentityResult result = await _role.CreateAsync(new IdentityRole("admin"));
            //var user = await _user.FindByIdAsync("082902d3-bb72-4a4b-9b0e-4b1f5b5009a3");
            //IdentityResult result = await _user.AddToRoleAsync(user, "admin");

            return View();
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
