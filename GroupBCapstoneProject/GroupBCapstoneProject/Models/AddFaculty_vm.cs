using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBCapstoneProject.Models
{
    public class AddFaculty_vm
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Department")]
        public string Department { get; set; }

        [DisplayName("Email Address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string EmailAddress { get; set; }

        [DisplayName("Phone Number")]
        [Phone(ErrorMessage = "Please enter a phone number without formatting anything")]
        public string PhoneNumber { get; set; }
    }
}
