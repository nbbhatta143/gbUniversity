using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupBCapstoneProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using GroupBCapstoneProject.AuthorizationRequirements;
using GroupBCapstoneProject.Controllers.Helpers;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account.Manage;

namespace GroupBCapstoneProject.Controllers
{
    [Authorize(Policy = "IsStudent")]
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
    
        public StudentController(AppDbContext context, UserManager<ApplicationUser> userManager)
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
                return RedirectToAction("GetStudentInfo");
            }
            RegistrationManager manager = new RegistrationManager(_context);
            int studentID = manager.GetStudentIDFromUserID(userID);
            Student student = manager.GetStudentByStudentID(studentID);
            ViewData["StudentBalance"] = student.Balance;
            List<CourseForRegistration> courses = manager.GetStudentEnrollments(studentID);
            ViewBag.studentEnrollments = courses;
            return View();
        }

        [HttpPost]
        public IActionResult Index(string btnSubmit) 
        {
            switch (btnSubmit)
            {
                case "Sign up for a class":
                    return RedirectToAction("RegisterForCourse");
                case "Drop a class":
                    return RedirectToAction("DropCourse");
                case "Make a Payment":
                    return RedirectToAction("Index", "Payments");
                default:
                    break;
            }
            return View();
        }

        public IActionResult GetStudentInfo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetStudentInfo([Bind("ID,FirstName,LastName,Major,EmailAddress,PhoneNumber")] Student student)
        {
            student.Balance = 0;
            var userID = _userManager.GetUserId(User);
            student.AspNetUserID = userID;
            
            
            if (ModelState.IsValid)
            {
                ApplicationUser currentUser = await _userManager.FindByIdAsync(userID);
                currentUser.CompletedRegistration = true;
                _context.Add(student);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            return View("FailedToCreate");
        }

        public IActionResult RegisterForCourse(string message)
        {
            RegistrationManager manager = new RegistrationManager(_context);
            ViewBag.listOfCoursesForRegistration = manager.GetListOfCoursesForRegistration();
            if (String.IsNullOrEmpty(message))
            {
                return View();
            } else
            {
                ViewData["ErrorMessage"] = message;
            }
            
            return View();
        }

        [HttpPost]
        async public Task<IActionResult> RegisterForCourse(int courseID)
        {
            var userID = _userManager.GetUserId(User);
            RegistrationManager manager = new RegistrationManager(_context);         
            int studentID = manager.GetStudentIDFromUserID(userID);

            if (manager.IsStudentAlreadyEnrolled(studentID, courseID))
            {
                return RedirectToAction("RegisterForCourse", new { message = "You're already enrolled in this class" });
            }

            Course course = manager.GetCourseByCourseID(courseID);
            if (course.Capacity == course.Enrollment)
            {
                return RedirectToAction("RegisterForCourse", new { message = "Sorry, that class is already full." });
            }

            else
            {
                await manager.AddBalanceToStudent(courseID, userID);
                Enrollment enrollment = new Enrollment()
                {
                    CourseID = courseID,
                    StudentID = studentID,
                    Date = DateTime.Now,
                };

                
                course.Enrollment++;
                _context.Update(course);


                _context.Add(enrollment);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
        }

        public IActionResult DropCourse()
        {
            RegistrationManager manager = new RegistrationManager(_context);
            string userID = _userManager.GetUserId(User);
            int studentID = manager.GetStudentIDFromUserID(userID);
            List<CourseForRegistration> courses = manager.GetStudentEnrollments(studentID);
            ViewBag.studentEnrollments = courses;
            return View();
        }

        [HttpPost]
        public IActionResult DropCourse(int courseID)
        {
            RegistrationManager manager = new RegistrationManager(_context);
            string userID = _userManager.GetUserId(User);
            manager.DropClass(courseID, userID);
            return RedirectToAction("Index");
        }
    }
}
