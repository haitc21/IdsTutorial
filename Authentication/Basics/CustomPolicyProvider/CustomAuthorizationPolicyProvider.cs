using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basics.CustomPolicyProvider
{
    public static class DynamicPolycies
    {
        public static string Level = "Level";
        public static string Rank = "Rank";

        public static IEnumerable<string> Get()
        {
            yield return Level;
            yield return Rank;
        }
    }
    public static class DynamicPolycyFactory
    {
        public static AuthorizationPolicyBuilder Create(string policyName)
        {
            var parts = policyName.Split('.');
            var policyType = parts.First();
            var policyValue = parts.Last();

            switch (policyType)
            {
                case "Rank":
                    return new AuthorizationPolicyBuilder();
                case "Level":
                    return new AuthorizationPolicyBuilder();
                default:
                    return null;


            }
            return null;
        }
    }
    public class CustomAuthorizationPolicyProvider
        : DefaultAuthorizationPolicyProvider
    {
        public CustomAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
            : base(options)
        {

        }

        public override Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            foreach (var p in DynamicPolycies.Get())
            {
                if (policyName.StartsWith(p))
                {
                    var policy = new AuthorizationPolicyBuilder().Build();
                    return Task.FromResult(policy);
                }
            }
            return base.GetPolicyAsync(policyName);
        }
    }
}
