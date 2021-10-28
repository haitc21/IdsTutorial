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

        [Authorize( Policy = "NgaySinh")]
        public IActionResult SecretPoliCyDob()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult SecretPolicyRole()
        {
            return View();
        }

        public IActionResult AccessDenie()
        {
            return View();
        }



        public IActionResult Authenticate()
        {
            var haidzClaims = new List<Claim>()
            {
                 new Claim(ClaimTypes.Name,"Hai dz"),
                 new Claim(ClaimTypes.Email,"Haidz@gmail.com"),
                 new Claim("HaiDz","Hai dz vo doi"),
                 //
                 
                 new Claim(ClaimTypes.DateOfBirth,"21/12/1995"),
                 new Claim(ClaimTypes.Role,"Admin")
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
