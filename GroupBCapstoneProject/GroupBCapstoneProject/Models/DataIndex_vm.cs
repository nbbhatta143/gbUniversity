using GroupBCapstoneProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBCapstoneProject.Models
{
    public class DataIndex_vm
    {
        public List<Student> Students { get; }
        public List<Faculty> Faculty { get; }
        public List<Course> Courses { get; }

        public DataIndex_vm()
        {
            Students = new List<Student>();
            Faculty = new List<Faculty>();
            Courses = new List<Course>();
        }

        public DataIndex_vm(List<Student> students, List<Faculty> faculty, List<Course> courses)
        {
            Students = students;
            Faculty = faculty;
            Courses = courses;
        }
    }
}
