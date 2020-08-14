using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Payments.Models
{
    public class Payments
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime Date { get; set; }
    }
}
