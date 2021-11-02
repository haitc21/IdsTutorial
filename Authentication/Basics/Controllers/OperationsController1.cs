using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basics.Controllers
{
    public class OperationsController1 : Controller
    {
        private readonly IAuthorizationService _authoService;

        public OperationsController1(IAuthorizationService authoService)
        {
            _authoService = authoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Open()
        {
            //  cookieJar là resoure lấy từ DB
            var cookieJar = new CookieJar();
            var requirement = new OperationAuthorizationRequirement()
            {
                Name = CookieJarOperators.Open
            };
            await _authoService.AuthorizeAsync(User, cookieJar, requirement);
            return View();
        }
    }

    public class CookieJarAuthorizationHandler
        : AuthorizationHandler<OperationAuthorizationRequirement, CookieJar>
    {
        // OperationAuthorizationRequirement 
        // gioong y như CustomRequireClaim viết ở bài 3
        // nó được định nghãi sẵn nên lấy ra dùng
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            CookieJar cookieJar)
        {
            if (requirement.Name == CookieJarOperators.Look)
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    context.Succeed(requirement);
                }
            }
            else if (requirement.Name == CookieJarOperators.ComeNear)
            {
                if (context.User.HasClaim("friend", "Good"))
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// Kiểu như tên các Action
    /// Vd: CRUD
    /// </summary>
    public static class CookieJarOperators
    {
        public static string Open = "Open";
        public static string TakeCookie = "TakeCookie";
        public static string ComeNear = "ComeNear";
        public static string Look = "Look";
    }
    public class CookieJar
    {
        public string Name { get; set; }
    }
}
