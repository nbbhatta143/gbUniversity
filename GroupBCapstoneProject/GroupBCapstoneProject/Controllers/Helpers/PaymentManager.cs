using GroupBCapstoneProject.AuthorizationRequirements;
using GroupBCapstoneProject.Data;
using GroupBCapstoneProject.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace GroupBCapstoneProject.Controllers.Helpers
{
    public class PaymentManager
    {
        private readonly AppDbContext _context;
        public PaymentManager(AppDbContext context)
        {
            _context = context;

        }

        public void SubtractBalanceFromStudent(string userID, decimal amountToBePaid)
        {
            RegistrationManager manager = new RegistrationManager(_context);
            int studentID = manager.GetStudentIDFromUserID(userID);
            Student student = manager.GetStudentByStudentID(studentID);
            student.Balance -= amountToBePaid;
            _context.Update(student);
            _context.SaveChanges();
        }

        public void CreatePaymentForDatabase(TransactionResponse response, int studentID)
        {
            Payment payment = new Payment()
            {
                StudentID = studentID,
                AmountPaid = response.AmountPaid,
                Date = DateTime.Now,
            };
            _context.Add(payment);
            _context.SaveChanges();
        }

        public List<PaymentWithNames> GetPaymentsWithNames()
        {
            var payments = from payment in _context.Payments
                           join student in _context.Students on payment.StudentID equals student.ID
                           select new PaymentWithNames
                           {
                               ID = payment.ID,
                               FirstName = student.FirstName,
                               LastName = student.LastName,
                               AmountPaid = payment.AmountPaid,
                               Date = payment.Date
                           };

            return payments.ToList();
        }

        public List<Student> GetStudents()
        {
            var students = _context.Students
                .Select(s => new Student
                {
                    ID = s.ID,
                    AspNetUserID = s.AspNetUserID,
                    Balance = s.Balance,
                    EmailAddress = s.EmailAddress,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Major = s.Major,
                    PhoneNumber = s.PhoneNumber
                })
                .ToList();
            return students;
        }

        public List<Faculty> GetFaculty()
        {
            var faculty = _context.Faculty
                .Select(s => new Faculty
                {
                    ID = s.ID,
                    AspNetUserID = s.AspNetUserID,
                    Balance = s.Balance,
                    EmailAddress = s.EmailAddress,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Department = s.Department,
                    PhoneNumber = s.PhoneNumber
                })
                .ToList();
            return faculty;
        }
    }
}
