using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVC_Payments.Models;

namespace MVC_Payments.Data
{
    public class MVC_PaymentsContext : DbContext
    {
        public MVC_PaymentsContext (DbContextOptions<MVC_PaymentsContext> options)
            : base(options)
        {
        }

        public DbSet<MVC_Payments.Models.Students> Students { get; set; }

        public DbSet<MVC_Payments.Models.Payments> Payments { get; set; }
    }
}
