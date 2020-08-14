using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupBCapstoneProject.AuthorizationRequirements;
using GroupBCapstoneProject.Controllers.Helpers;
using GroupBCapstoneProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GroupBCapstoneProject.Controllers
{
    [Authorize(Policy = "IsFaculty")]
    public class FacultyController : Controller
    {
        
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public FacultyController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        async public Task<IActionResult> Index()
        {
            var userID = _userManager.GetUserId(User);
            ApplicationUser currentUser = await _userManager.FindByIdAsync(userID);
            if (currentUser.CompletedRegistration == false)
            {
                return RedirectToAction("GetFacultyInfo");
            }
            RegistrationManager manager = new RegistrationManager(_context);
            int facultyID = manager.GetFacultyIDFromUserID(userID);
            Faculty faculty = manager.GetFacultyByFacultyID(facultyID);
            ViewData["FacultyBalance"] = faculty.Balance;
            List<CourseForRegistration> courses = manager.GetFacultyCourses(facultyID);
            ViewBag.facultyCourses = courses;

            return View();
        }

        [HttpPost]
        public IActionResult Index(string btnSubmit)
        {
            switch (btnSubmit)
            {
                case "Sign up to teach a class":
                    return RedirectToAction("RegisterForCourse");
                default:
                    break;
            }
            return View();
        }

        public IActionResult GetFacultyInfo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetFacultyInfo([Bind("ID,FirstName,LastName,Department,EmailAddress,PhoneNumber")] Faculty faculty)
        {
            var userID = _userManager.GetUserId(User);
            faculty.AspNetUserID = userID;


            if (ModelState.IsValid)
            {
                ApplicationUser currentUser = await _userManager.FindByIdAsync(userID);
                currentUser.CompletedRegistration = true;
                _context.Add(faculty);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View("FailedToCreate");
        }

        public IActionResult RegisterForCourse()
        {
            RegistrationManager manager = new RegistrationManager(_context);
            ViewBag.listOfCoursesForRegistration = manager.GetListOfCoursesForRegistration();
            return View();
        }

        [HttpPost]
        async public Task<IActionResult> RegisterForCourse(int courseID)
        {

            var userID = _userManager.GetUserId(User);
            RegistrationManager manager = new RegistrationManager(_context);
            int facultyID = manager.GetFacultyIDFromUserID(userID);
            Course course = manager.GetCourseByCourseID(courseID);
            
            course.FacultyID = facultyID;
            _context.Update(course);

            Faculty faculty = manager.GetFacultyByFacultyID(facultyID);
            faculty.Balance += course.CreditHours * 2000;
            _context.Update(faculty);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
    
}