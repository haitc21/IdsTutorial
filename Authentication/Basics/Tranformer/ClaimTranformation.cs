using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Basics.Tranformer
{
    /// <summary>
    /// Sau khi authenticate thi se tu dong vao day mỗi khi vào controller or api
    /// </summary>
    public class ClaimTranformation : IClaimsTransformation
    {
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var hasFriendClaim = principal.Claims.Any(x => x.Type == "friend");
            if (!hasFriendClaim)
            {
                // cách này k ăn thua
               var IdentityClaim = (ClaimsIdentity)principal.Identity;
                IdentityClaim.AddClaim(new Claim("friend", "Bad"));
            }
            return Task.FromResult(principal);
        }
    }
}
