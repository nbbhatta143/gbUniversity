using GroupBCapstoneProject.AuthorizationRequirements;
using GroupBCapstoneProject.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBCapstoneProject.Controllers.Helpers
{
    public class RegistrationManager
    {
        private readonly AppDbContext _context;
        private readonly decimal hourlyTuition = 300;

        public RegistrationManager(AppDbContext context)
        {
            _context = context;
        }
        public List<CourseForRegistration> GetListOfCoursesForRegistration()
        {
            var courses = from course in _context.Courses
                          join faculty in _context.Faculty on course.FacultyID equals faculty.ID
                          select new CourseForRegistration
                          {
                              ID = course.ID,
                              CourseNumber = course.CourseNumber,
                              SectionNumber = course.SectionNumber,
                              AvailableSeats = course.Capacity - course.Enrollment,
                              Faculty = $"{faculty.FirstName} {faculty.LastName}",
                              StartTime = course.StartTime,
                              EndTime = course.EndTime,
                              CreditHours = course.CreditHours,
                              BuildingName = course.BuildingName,
                              BuildingNumber = course.BuildingNumber,
                              MeetsOnMonday = course.MeetsOnMonday,
                              MeetsOnTuesday = course.MeetsOnTuesday,
                              MeetsOnWednesday = course.MeetsOnWednesday,
                              MeetsOnThursday = course.MeetsOnThursday,
                              MeetsOnFriday = course.MeetsOnFriday,
                              MeetsOnSaturday = course.MeetsOnSaturday
                          };
                            
             return courses.ToList();
        }

        public int GetCourseIDFromSectionNumber(string sectionNumber)
        {
            var courseID = _context.Courses
                .Where(c => c.SectionNumber.Equals(sectionNumber))
                .Select(c => c.ID)
                .ToArray();

            return courseID[0];
        }

        public Course GetCourseByCourseID(int courseID)
        {
            var courses = _context.Courses
                .Where(c => c.ID.Equals(courseID))
                 .Select(c => new Course
                 {
                     ID = c.ID,
                     CourseNumber = c.CourseNumber,
                     SectionNumber = c.SectionNumber,
                     Capacity = c.Capacity,
                     Enrollment = c.Enrollment,
                     FacultyID = c.FacultyID,
                     StartTime = c.StartTime,
                     EndTime = c.EndTime,
                     CreditHours = c.CreditHours,
                     BuildingName = c.BuildingName,
                     BuildingNumber = c.BuildingNumber,
                     MeetsOnMonday = c.MeetsOnMonday,
                     MeetsOnTuesday = c.MeetsOnTuesday,
                     MeetsOnWednesday = c.MeetsOnWednesday,
                     MeetsOnThursday = c.MeetsOnThursday,
                     MeetsOnFriday = c.MeetsOnFriday,
                     MeetsOnSaturday = c.MeetsOnSaturday
                 })
                 .ToList();

                
            return courses[0];
        }

        public int GetStudentIDFromUserID(string userID)
        {
            var studentID = from user in _context.AspNetUsers
                            join student in _context.Students on user.Id equals student.AspNetUserID
                            where user.Id.Equals(userID)
                            select student.ID;

            int[] studentIDs = studentID.ToArray();

            // really should add some error catching for when this inevitably fails.

            return studentIDs[0];             
        }

        public void SubtractBalanceFromStudent(int studentID, decimal amountToBePaid)
        {
            Student student = GetStudentByStudentID(studentID);
            student.Balance -= amountToBePaid;
            _context.Update(student);
        } 

        public bool IsStudentAlreadyEnrolled(int studentID, int courseID)
        {
            var enrollments = _context.Enrollments
                .Where(e => e.StudentID.Equals(studentID) && e.CourseID.Equals(courseID))
                .Select(e => e.CourseID)
                .ToList();

            if (enrollments.Count == 0)
            {
                return false;
            }

            return true;
        }

        public int GetFacultyIDFromUserID(string userID)
        {
            var facultyID = from user in _context.AspNetUsers
                            join faculty in _context.Faculty on user.Id equals faculty.AspNetUserID
                            where user.Id.Equals(userID)
                            select faculty.ID;

            int[] facultyIDs = facultyID.ToArray();

            // really should add some error catching for when this inevitably fails.

            return facultyIDs[0];
        }

        async public Task AddBalanceToStudent(int courseID, string userID)
        {
            int creditHours = FindCreditHoursByCourseID(courseID);
            int studentID = GetStudentIDFromUserID(userID);
            Student student = GetStudentByStudentID(studentID);
            decimal balance = CalculateBalance(creditHours);
            student.Balance += balance;
            _context.Update(student);
            await _context.SaveChangesAsync();
        }

        private int FindCreditHoursByCourseID(int courseID)
        {
            var creditHours = _context.Courses
                .Where(c => c.ID.Equals(courseID))
                .Select(c => c.CreditHours)
                .ToList();

            return creditHours[0];
        }
        private decimal CalculateBalance(int creditHours)
        {
            return creditHours * hourlyTuition;
        }

        public Student GetStudentByStudentID(int studentID)
        {
            var students = _context.Students
                .Where(s => s.ID.Equals(studentID))
                .Select(s => new Student
                {
                    ID = s.ID,
                    AspNetUserID = s.AspNetUserID,
                    Balance = s.Balance,
                    EmailAddress = s.EmailAddress,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Major = s.Major,
                    PhoneNumber = s.PhoneNumber
                })
                .ToList();

            return students[0];
        }

        public List<CourseForRegistration> GetStudentEnrollments(int studentID)
        {
            var enrollments = from enrollment in _context.Enrollments
                              join course in _context.Courses on enrollment.CourseID equals course.ID
                              join faculty in _context.Faculty on course.FacultyID equals faculty.ID
                              where studentID.Equals(enrollment.StudentID)
                              select new CourseForRegistration
                              {
                                  ID = course.ID,
                                  CourseNumber = course.CourseNumber,
                                  SectionNumber = course.SectionNumber,
                                  Faculty = $"{faculty.FirstName} {faculty.LastName}",
                                  StartTime = course.StartTime,
                                  EndTime = course.EndTime,
                                  CreditHours = course.CreditHours,
                                  BuildingName = course.BuildingName,
                                  BuildingNumber = course.BuildingNumber,
                                  MeetsOnMonday = course.MeetsOnMonday,
                                  MeetsOnTuesday = course.MeetsOnTuesday,
                                  MeetsOnWednesday = course.MeetsOnWednesday,
                                  MeetsOnThursday = course.MeetsOnThursday,
                                  MeetsOnFriday = course.MeetsOnFriday,
                                  MeetsOnSaturday = course.MeetsOnSaturday
                              };
            List<CourseForRegistration> courses = enrollments.ToList();
            if (courses != null)
            {
                return courses;
            }

            return new List<CourseForRegistration>();
        }

        public void DropClass(int courseID, string userID)
        {
            int studentID = GetStudentIDFromUserID(userID);
            var enrollmentToDrop = _context.Enrollments.FirstOrDefault(x => x.CourseID == courseID && x.StudentID == studentID);
            _context.Remove(enrollmentToDrop);
            _context.SaveChanges();
            int creditHours = FindCreditHoursByCourseID(courseID);
            Student student = GetStudentByStudentID(studentID);
            decimal balance = CalculateBalance(creditHours);
            PaymentManager manager = new PaymentManager(_context);
            manager.SubtractBalanceFromStudent(userID, balance);
            Course course = GetCourseByCourseID(courseID);
            course.Enrollment -= 1;
            _context.Update(course);
            _context.SaveChanges();
        }

        public Faculty GetFacultyByFacultyID(int facultyID)
        {
            var faculty = _context.Faculty
                .Where(s => s.ID.Equals(facultyID))
                .Select(s => new Faculty
                {
                    ID = s.ID,
                    AspNetUserID = s.AspNetUserID,
                    Balance = s.Balance,
                    Department = s.Department,
                    EmailAddress = s.EmailAddress,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    PhoneNumber = s.PhoneNumber
                })
                .ToList();

            return faculty[0];
        }

        public List<CourseForRegistration> GetFacultyCourses(int facultyID)
        {
            var assignedClasses = from  course in _context.Courses
                              join faculty in _context.Faculty on course.FacultyID equals faculty.ID
                              where facultyID.Equals(course.FacultyID)
                              select new CourseForRegistration
                              {
                                  ID = course.ID,
                                  CourseNumber = course.CourseNumber,
                                  SectionNumber = course.SectionNumber,
                                  Faculty = $"{faculty.FirstName} {faculty.LastName}",
                                  StartTime = course.StartTime,
                                  EndTime = course.EndTime,
                                  CreditHours = course.CreditHours,
                                  BuildingName = course.BuildingName,
                                  BuildingNumber = course.BuildingNumber,
                                  MeetsOnMonday = course.MeetsOnMonday,
                                  MeetsOnTuesday = course.MeetsOnTuesday,
                                  MeetsOnWednesday = course.MeetsOnWednesday,
                                  MeetsOnThursday = course.MeetsOnThursday,
                                  MeetsOnFriday = course.MeetsOnFriday,
                                  MeetsOnSaturday = course.MeetsOnSaturday
                              };
            List<CourseForRegistration> courses = assignedClasses.ToList();
            if (courses != null)
            {
                return courses;
            }

            return new List<CourseForRegistration>();
        }
    }
}
