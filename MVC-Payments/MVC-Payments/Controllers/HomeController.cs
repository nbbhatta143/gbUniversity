using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC_Payments.Models;
using AuthorizeNet.Api.Controllers.Bases;
using AuthorizeNet.Api.Contracts.V1;

namespace MVC_Payments.Controllers
{
    public class HomeController : Controller
    {
  
        public ActionResult Index(string errorMessage)
        {
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
            if(string.IsNullOrWhiteSpace(payment.FirstName)&&string.IsNullOrWhiteSpace(payment.LastName)&&string.IsNullOrWhiteSpace(payment.Address1)&&string.IsNullOrWhiteSpace(payment.Address2)&&
                string.IsNullOrWhiteSpace(payment.Month)&&string.IsNullOrWhiteSpace(payment.Year)&&string.IsNullOrWhiteSpace(payment.PostCode)&&string.IsNullOrWhiteSpace(payment.CardCode))
            {
                ModelState.AddModelError("CardNumber", "Cannot be empty and card number has to be 14 digits or greater");
            }
            if (!ModelState.IsValid)
            {
                View(payment);
            }

            TransactionResponse result = new PaymentProcesses().ChargeCredit(payment);

            if (result != null && result.resultCode == AuthorizeNet.Api.Contracts.V1.messageTypeEnum.Ok)
            {
                
                TransactionResponse viewmodel = new TransactionResponse()
                {
                    transId = result.transId,
                    resultCode = result.resultCode,
                    responseCode = result.responseCode,
                    messageCode = result.messageCode,
                    authCode = result.authCode,
                    description = result.description,
                };

                List<TransactionResponse> viewmodelList = new List<TransactionResponse>
                {
                    viewmodel
                };
                return View("TransactionResponse", viewmodelList);
            }
            else
            {
                PaymentModel model = new PaymentModel();

                TransactionResponse transaction = new TransactionResponse();
                
                if (transaction.errorCode == "6" && transaction.errorCode == "78" && transaction.errorCode == "316"&& transaction.errorCode== "112")
                {
                    model.CardNumber = transaction.errorText;
                    return View("Index", model.CardNumber);
                }
                else 
                {
                    return View("Index", model.CardNumber);
                }
                
            }
        }
    }
}
