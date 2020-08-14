using GroupBCapstoneProject.Data;
using GroupBCapstoneProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBCapstoneProject.Controllers.Helpers
{
    public class DataIndexManager
    {
        private readonly AppDbContext _context;
        public DataIndexManager(AppDbContext context)
        {
            _context = context;
        }
        public List<DataIndex_vm> GetDatabaseInfo()
        {
            List<Faculty> faculty = GetListOfFaculty();
            List<Student> students = GetListOfStudents();
            List<Course> courses = GetListOfCourses();
            DataIndex_vm dataIndex = new DataIndex_vm(students, faculty, courses);
            List<DataIndex_vm> dataIndices = new List<DataIndex_vm>
            {
                dataIndex
            };
            return dataIndices;
        }
        public List<Faculty> GetListOfFaculty()
        {
            var faculty = _context.Faculty
                .Select(f => new Faculty
                {
                    ID = f.ID,
                    FirstName = f.FirstName,
                    LastName = f.LastName,
                    Department = f.Department,
                    EmailAddress = f.EmailAddress,
                    PhoneNumber = f.PhoneNumber
                })
                .ToList();
            return faculty;
        }

        public List<Student> GetListOfStudents()
        {
            var students = _context.Students
                .Select(s => new Student
                {
                    ID = s.ID,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Balance = s.Balance,
                    Major = s.Major,
                    EmailAddress = s.EmailAddress,
                    PhoneNumber = s.PhoneNumber
                })
                .ToList();
            return students;
        }

        public List<Course> GetListOfCourses()
        {
            var courses = _context.Courses
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
            return courses;
        }
    }
}
