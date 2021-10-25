using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Basics.Controllers
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


        public IActionResult Authenticate()
        {
            var haidzClaims = new List<Claim>()
            {
                 new Claim(ClaimTypes.Name,"Hai dz"),
                 new Claim(ClaimTypes.Email,"Haidz@gmail.com"),
                 new Claim("HaiDz","Hai dz vo doi")
            };

            var bangLaiClaims = new List<Claim>()
            {
                 new Claim("Bang lai xe","A"),
                 new Claim("Bang lai may aby","K"),
            };

            var haidzIdentity = new ClaimsIdentity(haidzClaims, "Hai dz identity");
            var bangLaiIdentity = new ClaimsIdentity(bangLaiClaims, "Bang lai identity");

            var userPrincipals = new ClaimsPrincipal(new[] { haidzIdentity, bangLaiIdentity });

            HttpContext.SignInAsync(userPrincipals);
            return RedirectToAction("Index");
        }
    }
}
