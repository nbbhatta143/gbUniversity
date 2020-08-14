using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBCapstoneProject.Data
{
    public class Faculty
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public decimal Balance { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string AspNetUserID { get; set; }
    }
}
