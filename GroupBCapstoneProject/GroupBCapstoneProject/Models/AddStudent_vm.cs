using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBCapstoneProject.Models
{
    public class AddStudent_vm
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [DisplayName("First Name")]
        [Required]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required]
        public string LastName { get; set; }

        [DisplayName("Balance")]
        [Required]
        [Range(0, 1000000,
            ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public decimal Balance { get; set; }

        [DisplayName("Major")]
        [Required]
        public string Major { get; set; }

        [DisplayName("Email Address")]
        [Required]
        [EmailAddress(ErrorMessage ="Please enter a valid email address")]
        public string EmailAddress { get; set; }

        [DisplayName("Phone Number")]
        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage ="Invalid Phone Number")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage ="Invalid Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
