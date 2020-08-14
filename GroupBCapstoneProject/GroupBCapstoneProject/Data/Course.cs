using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBCapstoneProject.Data
{
    public class Course
    {
        public int ID { get; set; }
        public string CourseNumber { get; set; }
        public string SectionNumber { get; set; }
        public int Capacity { get; set; }
        public int Enrollment { get; set; }
        public int? FacultyID { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int CreditHours { get; set; }
        public string BuildingName { get; set; }
        public string BuildingNumber { get; set; }
        public bool MeetsOnMonday { get; set; }
        public bool MeetsOnTuesday { get; set; }
        public bool MeetsOnWednesday { get; set; }
        public bool MeetsOnThursday { get; set; }
        public bool MeetsOnFriday { get; set; }
        public bool MeetsOnSaturday { get; set; }

    }
}
