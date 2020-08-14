using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts;
using AuthorizeNet.Api.Controllers.Bases;
using AuthorizeNet.Api.Contracts.V1;
using GroupBCapstoneProject.Data;

namespace GroupBCapstoneProject.Models
{
    public class PaymentProcesses
    {
        // These are default api login id and transaction key .
        // You can create your own keys by signing up for a sandbox account here: https://sandbox.authorize.net/

        string apiLoginId = "4B3s6BkFe3";
        string transactionKey = "7uKGfk2Z2k8U7j3b";

        public TransactionResponse ChargeCredit(PaymentModel payment)
        {
            // determine run Environment to SANDBOX for developemnt level
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;


            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = apiLoginId,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = transactionKey,
            };
            var creditCard = new creditCardType
            {
                cardNumber = payment.CardNumber,
                expirationDate = payment.Month + payment.Year,
                cardCode = payment.CardCode,
            };
            var bilingAddress = new customerAddressType
            {
                firstName = payment.FirstName,
                lastName = payment.LastName,
                city = payment.Address1,
                address = payment.Address2,
                zip = payment.PostCode
            };
            //standard api call to retrieve response
            var paymentType = new paymentType { Item = creditCard };

            //getting payment that student paying
            var studentAmount = new PaymentModel();

            var studentID = new Student();
            //Add line Items you pay to obtain these
            
            var lineItems = new lineItemType[1];
            lineItems[0] = new lineItemType { itemId =studentID.ID.ToString(), name = "Tution Fees", quantity = 1, unitPrice = studentAmount.Amount,taxRate=7.5M,totalAmount=studentAmount.Amount};
           
            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),// charge the card
                amount = Convert.ToDecimal(payment.Amount),
                payment = paymentType,
                billTo = bilingAddress,
                lineItems = lineItems
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };

            // instantiate the contoller that will call the service
            var controller = new createTransactionController(request);
            controller.Execute();

            var resCode = controller.GetResultCode();
            var resAll = controller.GetResults();

            //get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();

            

            //validate 
            TransactionResponse result = new TransactionResponse();

            if (response != null)
            {
                result.ResultCode = response.messages.resultCode;
                if (response.messages.resultCode == messageTypeEnum.Ok)
                {
                    if (response.transactionResponse.messages != null)
                    {
                        result.TransId = response.transactionResponse.transId;
                        result.ResponseCode = response.transactionResponse.responseCode;
                        result.MessageCode = response.transactionResponse.messages[0].code;
                        result.Description = response.transactionResponse.messages[0].description;
                        result.AuthCode = response.transactionResponse.authCode;
                        result.AmountPaid = payment.Amount;
                    }
                    else
                    {
                        if (response.transactionResponse.errors !=null)
                        {
                            result.ErrorCode = response.transactionResponse.errors[0].errorCode;
                            result.ErrorText = response.transactionResponse.errors[0].errorText;
                        }
                    }
                }
                else
                {
                    if (response.transactionResponse != null && response.transactionResponse.errors != null)
                    {
                        result.ErrorCode = response.transactionResponse.errors[0].errorCode;
                        result.ErrorText = response.transactionResponse.errors[0].errorText;
                    }
                    else
                    {
                        result.ErrorCode = response.messages.message[0].code;
                        result.ErrorText = response.messages.message[0].text;
                    }
                }
            }
            else
            {
                //result.errorCode = "NONE";
                //result.errorText = "Failed Transaction, Unknown Error";

                ANetApiResponse errorResponse = controller.GetErrorResponse();
                result.ErrorText = errorResponse.messages.message[0].text;
                result.ErrorCode = errorResponse.messages.message[0].code;
                result.ResultCode = messageTypeEnum.Error;
            }
            return result;
        }
    }
}
