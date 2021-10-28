using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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
        public async Task<IActionResult> Dowloads()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Dowloads", "test.xlsx");
            byte[] fileBytes = null;
            fileBytes = System.IO.File.ReadAllBytes(path);
            if (fileBytes == null)
                return NotFound();
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            //using (var stream = System.IO.File.OpenRead(path))
            //{
            //    if (stream == null)
            //        return NotFound();
            //    return File(stream, "application/octet-stream");
            //}
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
