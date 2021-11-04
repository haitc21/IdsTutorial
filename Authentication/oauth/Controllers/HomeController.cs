using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace oauth.Controllers
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
        public IActionResult Authen()
        {
            var claims = new[] 
            {
                new Claim(JwtRegisteredClaimNames.Sub, "some_id"),
                new Claim("granny","cookie")
            };

            var secretBytes = Encoding.UTF8.GetBytes(Constants.Secret);

            // https://docs.microsoft.com/en-us/dotnet/api/system.identitymodel.tokens.symmetricsecuritykey?view=dotnet-plat-ext-5.0
            var key = new SymmetricSecurityKey(secretBytes);

            var algorithm = SecurityAlgorithms.HmacSha256;
            // https://docs.microsoft.com/en-us/dotnet/api/system.identitymodel.tokens.securitykey?view=dotnet-plat-ext-5.0
            var signingCredentials = new SigningCredentials(key, algorithm);

            var tokent = new JwtSecurityToken(
                Constants.Issuer,
                Constants.Audiance,
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(1),
                signingCredentials
                );

            return RedirectToAction("Index");
        }
    }
}
