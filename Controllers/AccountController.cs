using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using javora.Models.Database;
using javora.Models.View;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace javora.Controllers
{
    public class AccountController : Controller
    {

        private readonly SignInManager<User> _signInManager;
        public AccountController(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View(new Login());
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                var res = await _signInManager.PasswordSignInAsync(model.Log, model.Password, true, false);
                //await _signInManager.SignInAsync(user, false);
                if (res.Succeeded)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Bab data");
                    
                }
            }
            return View(new Login());


        }
    }
}