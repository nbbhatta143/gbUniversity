using GroupBCapstoneProject.AuthorizationRequirements;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GroupBCapstoneProject.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Enrollment>()
                .HasKey(e => new { e.CourseID, e.StudentID });
        }

        public DbSet<GroupBCapstoneProject.Data.Student> Students { get; set; }
        public DbSet<GroupBCapstoneProject.Data.Faculty> Faculty { get; set; }
        public DbSet<GroupBCapstoneProject.Data.Course> Courses { get; set; }
        public DbSet<GroupBCapstoneProject.Data.Enrollment> Enrollments { get; set; }
        public DbSet<GroupBCapstoneProject.Data.CourseForRegistration> CourseForRegistrations { get; set; }
        public DbSet<GroupBCapstoneProject.AuthorizationRequirements.ApplicationUser> AspNetUsers { get; set; }
        public DbSet<GroupBCapstoneProject.Models.Payment> Payments { get; set; }
        public DbSet<GroupBCapstoneProject.Models.PaymentWithNames> PaymentsWithNames { get; set; }

    }
}
