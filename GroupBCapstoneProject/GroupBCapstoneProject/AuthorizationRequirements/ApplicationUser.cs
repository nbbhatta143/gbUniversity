using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace GroupBCapstoneProject.AuthorizationRequirements
{
    public class ApplicationUser : IdentityUser
    {
        public string RoleInSchool { get; set; }
        public bool CompletedRegistration { get; set; }

        public ApplicationUser()
        {

        }
    }
}
