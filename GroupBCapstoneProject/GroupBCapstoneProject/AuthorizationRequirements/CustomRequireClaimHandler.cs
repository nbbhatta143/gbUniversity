using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBCapstoneProject.AuthorizationRequirements
{
    public class CustomRequireClaimHandler : AuthorizationHandler<SchoolRole>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            SchoolRole requirement)
        {

            bool isAdmin = context.User.HasClaim(x => x.Value.Equals("Admin"));

            bool isAuthorized = context.User.HasClaim(x => x.Value.Equals(requirement.RoleInSchool));

            if (isAdmin || isAuthorized)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
