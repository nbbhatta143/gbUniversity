using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupBCapstoneProject.AuthorizationRequirements;
using GroupBCapstoneProject.Controllers.Helpers;
using GroupBCapstoneProject.Data;
using GroupBCapstoneProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account.Manage;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GroupBCapstoneProject.Controllers
{
    [Authorize(Policy = "IsStudent")]
    public class PaymentsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public PaymentsController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public ActionResult Index(string errorMessage)
        {
            var userID = _userManager.GetUserId(User);
            RegistrationManager manager = new RegistrationManager(_context);
            int studentID = manager.GetStudentIDFromUserID(userID);
            Student student = manager.GetStudentByStudentID(studentID);
            ViewData["StudentBalance"] = student.Balance;


            if (String.IsNullOrEmpty(errorMessage))
            {
                return View();
            }
            ViewData["ErrorMessage"] = errorMessage;
            return View();
        }

        [HttpPost]
        public ActionResult Index(PaymentModel payment)
        {
            var tempUserID = _userManager.GetUserId(User);
            RegistrationManager manager = new RegistrationManager(_context);
            int tempStudentID = manager.GetStudentIDFromUserID(tempUserID);
            Student student = manager.GetStudentByStudentID(tempStudentID);
            ViewData["StudentBalance"] = student.Balance;

            if (string.IsNullOrWhiteSpace(payment.FirstName) && string.IsNullOrWhiteSpace(payment.LastName) && string.IsNullOrWhiteSpace(payment.Address1) && string.IsNullOrWhiteSpace(payment.Address2) &&
                string.IsNullOrWhiteSpace(payment.Month) && string.IsNullOrWhiteSpace(payment.Year) && string.IsNullOrWhiteSpace(payment.PostCode) && string.IsNullOrWhiteSpace(payment.CardCode))
            {
                ModelState.AddModelError("CardNumber", "Cannot be empty and card number has to be 14 digits or greater");
            }
            if (!ModelState.IsValid)
            {
                View(payment);
            }

            TransactionResponse result = new PaymentProcesses().ChargeCredit(payment);

            if (result != null && result.ResultCode == AuthorizeNet.Api.Contracts.V1.messageTypeEnum.Ok)
            {
                PaymentManager paymentManager = new PaymentManager(_context);
                string userID = _userManager.GetUserId(User);
                paymentManager.SubtractBalanceFromStudent(userID, result.AmountPaid);

                RegistrationManager registrationManager = new RegistrationManager(_context);
                int studentID = registrationManager.GetStudentIDFromUserID(userID);
                paymentManager.CreatePaymentForDatabase(result, studentID);

                TransactionResponse viewmodel = new TransactionResponse()
                {
                    TransId = result.TransId,
                    ResultCode = result.ResultCode,
                    ResponseCode = result.ResponseCode,
                    MessageCode = result.MessageCode,
                    AuthCode = result.AuthCode,
                    Description = result.Description,
                    AmountPaid = result.AmountPaid,
                };

                List<TransactionResponse> viewmodelList = new List<TransactionResponse>
                {
                    viewmodel
                };
                return RedirectToAction("TransactionResponse", new { viewmodelList = JsonConvert.SerializeObject(viewmodelList) });
            }
            else
            {
                PaymentModel model = new PaymentModel();

                TransactionResponse transaction = new TransactionResponse();

                if (transaction.ErrorCode == "6" || transaction.ErrorCode == "78" || transaction.ErrorCode == "316" || transaction.ErrorCode == "112")
                {
                    model.CardNumber = transaction.ErrorText;
                    return View("Index", model.CardNumber);
                }
                else
                {
                    return View("Index", model.CardNumber);
                }

            }
        }

        [HttpGet]
        public IActionResult TransactionResponse(string viewmodelList)
        {
            List<TransactionResponse> response = JsonConvert.DeserializeObject<List<TransactionResponse>>(viewmodelList);
            string userID = _userManager.GetUserId(User);
            RegistrationManager manager = new RegistrationManager(_context);
            int studentID = manager.GetStudentIDFromUserID(userID);
            Student student = manager.GetStudentByStudentID(studentID);
            ViewData["StudentName"] = String.Join(" ", student.FirstName, student.LastName);
            ViewData["StudentBalance"] = student.Balance;
            return View("TransactionResponse", response);
        }
    }
}
