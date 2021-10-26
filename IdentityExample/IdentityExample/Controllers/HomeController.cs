using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityExample.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Login(string username, string password)
        {
            return RedirectToAction("Index");
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Register(string username, string password)
        {
            return RedirectToAction("Index");
        }
    }
}
