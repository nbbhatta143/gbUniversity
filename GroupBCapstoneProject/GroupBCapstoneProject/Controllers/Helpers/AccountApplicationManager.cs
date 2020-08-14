using GroupBCapstoneProject.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBCapstoneProject.Controllers.Helpers
{
    public class AccountApplicationManager
    {
        private readonly AppDbContext _context;

        public AccountApplicationManager(AppDbContext context)
        {
            _context = context;
        }
        public string MakeUsername(string firstName, string lastName)
        {
            return MakeUsernameString(firstName.Trim().ToLower(), lastName.Trim().ToLower());
        }

        private string MakeUsernameString(string firstName, string lastName)
        {
            char firstLetter = firstName[0];
            string username = String.Join("", firstLetter, lastName);
            int numberToAddToUsername = 0;
            while (IsUsernameAvailable(username) == false)
            {
                numberToAddToUsername++;

                if (numberToAddToUsername == 1)
                {
                    username = String.Join("", username, numberToAddToUsername);
                }
                if (numberToAddToUsername < 10)
                {
                    username = username.Remove(username.Length - 1) + numberToAddToUsername;
                }
                if (numberToAddToUsername > 10)
                {
                    username = username.Remove(username.Length - 2) + numberToAddToUsername;
                }
            }

            return username;
        }

        private bool IsUsernameAvailable(string userName)
        {
            var userNames = _context.AspNetUsers
                .Where(u => u.UserName.Equals(userName))
                .Select(u => u.UserName)
                .ToList();

            if (userNames.Count == 0)
            {
                return true;
            }

            return false;
        }
    }
}
