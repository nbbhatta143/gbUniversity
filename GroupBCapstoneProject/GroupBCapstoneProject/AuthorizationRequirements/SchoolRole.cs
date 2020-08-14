using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static GroupBCapstoneProject.AuthorizationRequirements.AppClaimsPrincipalFactory;

namespace GroupBCapstoneProject.AuthorizationRequirements
{
    public class SchoolRole : IAuthorizationRequirement
    {
        public string RoleInSchool { get; }

        public SchoolRole(string roleInSchool)
        {
            RoleInSchool = roleInSchool;
        }

    }

}
