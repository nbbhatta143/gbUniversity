using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupBCapstoneProject.Models
{
    public class Payment
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime Date { get; set; }
    }
}

    
