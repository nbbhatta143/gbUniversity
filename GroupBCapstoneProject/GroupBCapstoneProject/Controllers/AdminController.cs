using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupBCapstoneProject.AuthorizationRequirements;
using GroupBCapstoneProject.Controllers.Helpers;
using GroupBCapstoneProject.Data;
using GroupBCapstoneProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GroupBCapstoneProject.Controllers
{
    [Authorize(Policy = "IsAdmin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;
        public AdminController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;

        }

        
        public IActionResult Index()
        {
            DataIndexManager manager = new DataIndexManager(_context);
            ViewBag.listOfStudents = manager.GetListOfStudents();
            ViewBag.listOfFaculty = manager.GetListOfFaculty();
            ViewBag.listOfCourses = manager.GetListOfCourses();
            List<DataIndex_vm> dataIndices = manager.GetDatabaseInfo();
            return View(dataIndices);
        }

        [HttpPost]
        public IActionResult Index(string btnSubmit)
        {
            switch (btnSubmit)
            {
                case "Add Course":
                    DataIndexManager manager = new DataIndexManager(_context);
                    ViewBag.listOfFaculty = manager.GetListOfFaculty();
                    return View("AddCourse");
                case "General Journal":
                    return RedirectToAction("ViewPayments");
                case "Accounts Receivable":
                    return RedirectToAction("ViewStudentBalances");
                case "Accounts Payable":
                    return RedirectToAction("ViewFacultyBalances");
                case "Create Account":
                    return RedirectToAction("Register");
                default:
                    break;
            }

            return View("FailedToCreate");
        }
           
        public IActionResult ViewPayments()
        {
            PaymentManager manager = new PaymentManager(_context);
            ViewBag.listOfPayments = manager.GetPaymentsWithNames();
            return View();
        }

        public IActionResult ViewStudentBalances()
        {
            PaymentManager manager = new PaymentManager(_context);
            ViewBag.listOfStudents = manager.GetStudents();
            return View();
        }

        public IActionResult ViewFacultyBalances()
        {
            PaymentManager manager = new PaymentManager(_context);
            ViewBag.listOfFaculty = manager.GetFaculty();
            return View();
        }

        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStudent([Bind("ID,FirstName,LastName,Balance,Major,EmailAddress,PhoneNumber")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View("FailedToCreate");
        }

        public IActionResult AddFaculty()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFaculty([Bind("ID,FirstName,LastName,Department,EmailAddress,PhoneNumber")] Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(faculty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View("FailedToCreate");
        }

        public IActionResult AddCourse()
        {
            DataIndexManager manager = new DataIndexManager(_context);
            ViewBag.listOfFaculty = manager.GetListOfFaculty();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCourse([Bind("ID,CourseNumber,SectionNumber,Capacity,Enrollment,FacultyID,StartTime,EndTime,CreditHours,BuildingName,BuildingNumber," +
            "MeetsOnMonday,MeetsOnTuesday,MeetsOnWednesday,MeetsOnThursday,MeetsOnFriday,MeetsOnSaturday")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View("FailedToCreate");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password, string roleInSchool)
        {

            var user = new ApplicationUser
            {
                UserName = username,
                RoleInSchool = roleInSchool,
                CompletedRegistration = false,
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);

                if (signInResult.Succeeded && user.RoleInSchool.Equals("Student"))
                {
                    return RedirectToAction("GetStudentInfo", "Student");
                }

                if (signInResult.Succeeded && user.RoleInSchool.Equals("Faculty"))
                {
                    return RedirectToAction("GetFacultyInfo", "Faculty");
                }

                return RedirectToAction("Index", "Admin");
            }

            return RedirectToAction("Index");
        }


    }
}
