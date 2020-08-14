using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GroupBCapstoneProject.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using GroupBCapstoneProject.Data;
using Microsoft.AspNetCore.Identity;
using GroupBCapstoneProject.AuthorizationRequirements;
using System.IO;

namespace GroupBCapstoneProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(
            ILogger<HomeController> logger,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Faq()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();           
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Athletics()
        {
            return View();
        }

        public IActionResult Academics()
        {
            return View();
        }
        public IActionResult Events()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }

        public IActionResult SportsEvents()
        {
            return View();
        }

        public IActionResult Apply()
        {
            return RedirectToAction("RegisterStudent", "Account");
        } 

        public IActionResult FinancialAid()
        {
            return View();
        }

        public async Task<IActionResult> Login(string message)
        {
            if (_signInManager.IsSignedIn(User))
            {
                string userName = _userManager.GetUserName(User);
                var user = await _userManager.FindByNameAsync(userName);

                if (user.RoleInSchool.Equals("Student"))
                {
                    if (user.CompletedRegistration)
                    {
                        return RedirectToAction("Index", "Student");
                    }
                    else
                    {
                        return RedirectToAction("GetStudentInfo", "Student");
                    }

                }

                if (user.RoleInSchool.Equals("Faculty"))
                {
                    if (user.CompletedRegistration)
                    {
                        return RedirectToAction("Index", "Faculty");
                    }
                    else
                    {
                        return RedirectToAction("GetFacultyInfo", "Faculty");
                    }
                }

                if (user.RoleInSchool.Equals("Admin"))
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            
            if (String.IsNullOrEmpty(message))
            {
                return View();
            }

            ViewData["ErrorMessage"] = message;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);

                if (signInResult.Succeeded && user.RoleInSchool.Equals("Student"))
                {
                    if (user.CompletedRegistration)
                    {
                        return RedirectToAction("Index", "Student");
                    } else
                    {
                        return RedirectToAction("GetStudentInfo", "Student");
                    }
                    
                }            

                if (signInResult.Succeeded && user.RoleInSchool.Equals("Faculty"))
                {
                    if (user.CompletedRegistration)
                    {
                        return RedirectToAction("Index", "Faculty");
                    }
                    else
                    {
                        return RedirectToAction("GetFacultyInfo", "Faculty");
                    }
                }

                if (signInResult.Succeeded && user.RoleInSchool.Equals("Admin"))
                {
                    return RedirectToAction("Index", "Admin");
                }
                return RedirectToAction("Login", new { message = "The username or password was incorrect" });
            }

            
            return RedirectToAction("Login", new { message = "The username or password was incorrect" });
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       
    }
}
