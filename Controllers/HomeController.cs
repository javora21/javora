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
using javora.Models.View;

namespace javora.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _user;
        private readonly RoleManager<IdentityRole> _role;
        private readonly JavoraContext db;

        public HomeController(ILogger<HomeController> logger, UserManager<User> user, RoleManager<IdentityRole> role, JavoraContext db)
        {
            _logger = logger;
            _user = user;
            _role = role;
            this.db = db;
        }

        public async Task<IActionResult> Index()
        {
            //var user = new User { UserName = "Admin" };
            //await _user.CreateAsync(user, "!QA2ws3ed");
            //await _role.CreateAsync(new IdentityRole("admin"));
            //var us = db.Users.FirstOrDefault();
            //var us = new UserContext().Users.FirstOrDefault();

            //var result = await _user.AddToRoleAsync(us, "admin");
            var skip = db.News.Count() - 3 > 0 ? db.News.Count() - 3 : 0;
             var list = db.News.Skip(skip).Select(x=> new NewsModel { 
             Guid = x.NewsGuid,
             Title = x.Title,
             Description = x.Description,
             MainImagePath = x.Images.Where(y=>y.IsMain).FirstOrDefault().Puth
             }).ToList();

            return View(list);
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
