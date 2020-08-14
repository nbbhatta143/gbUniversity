using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBCapstoneProject.Models
{
    public class AddCourse_vm
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [DisplayName("Course Number")]
        [MaxLength(5, ErrorMessage ="Cannot be longer than 5 characters")]
        [Required]
        public string CourseNumber { get; set; }

        [DisplayName("Section Number")]
        [MaxLength(20, ErrorMessage = "Cannot be longer than 20 characters")]
        [Required]
        public string SectionNumber { get; set; }

        [DisplayName("Capacity")]
        [Range(0, 1000, ErrorMessage ="Must between 0 and 1000")]
        public int Capacity { get; set; }

        [DisplayName("Students Enrolled")]
        public int Enrollment { get; set; }

        [DisplayName("Select Faculty")]
        public int? FacultyID { get; set; }

        [DisplayName("Start Time")]
        public DateTime? StartTime { get; set; }

        [DisplayName("End Time")]
        public DateTime? EndTime { get; set; }

        [DisplayName("Credit Hours")]
        [Range(0, 10, ErrorMessage = "Must be between 0 and 10")]
        public int CreditHours { get; set; }

        [DisplayName("Building Name")]
        [MaxLength(30, ErrorMessage = "Cannot be longer than 30 characters")]
        public string BuildingName { get; set; }

        [DisplayName("Building Number")]
        [MaxLength(10, ErrorMessage = "Cannot be longer than 10 characters")]
        public string BuildingNumber { get; set; }

        [DisplayName("Meets On Monday")]
        public bool MeetsOnMonday { get; set; }

        [DisplayName("Meets On Tuesday")]
        public bool MeetsOnTuesday { get; set; }

        [DisplayName("Meets On Wednesday")]
        public bool MeetsOnWednesday { get; set; }

        [DisplayName("Meets On Thursday")]
        public bool MeetsOnThursday { get; set; }

        [DisplayName("Meets On Friday")]
        public bool MeetsOnFriday { get; set; }

        [DisplayName("Meets On Saturday")]
        public bool MeetsOnSaturday { get; set; }
    }
}
