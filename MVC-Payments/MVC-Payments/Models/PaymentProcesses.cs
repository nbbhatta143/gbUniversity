using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts;
using AuthorizeNet.Api.Controllers.Bases;
using AuthorizeNet.Api.Contracts.V1;

namespace MVC_Payments.Models
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

            var studentID = new Students();
            //Add line Items you pay to obtain these
            
            var lineItems = new lineItemType[1];
            lineItems[0] = new lineItemType { itemId =studentID.ID.ToString(), name = "Tution Fees", quantity = 1, unitPrice = studentAmount.Amount,taxRate=7.5M,totalAmount=studentAmount.Amount};
           
            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),// charge the card
                amount = Convert.ToDecimal(studentAmount.Amount),
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
                result.resultCode = response.messages.resultCode;
                if (response.messages.resultCode == messageTypeEnum.Ok)
                {
                    if (response.transactionResponse.messages != null)
                    {
                        result.transId = response.transactionResponse.transId;
                        result.responseCode = response.transactionResponse.responseCode;
                        result.messageCode = response.transactionResponse.messages[0].code;
                        result.description = response.transactionResponse.messages[0].description;
                        result.authCode = response.transactionResponse.authCode;
                    }
                    else
                    {
                        if (response.transactionResponse.errors !=null)
                        {
                            result.errorCode = response.transactionResponse.errors[0].errorCode;
                            result.errorText = response.transactionResponse.errors[0].errorText;
                        }
                    }
                }
                else
                {
                    if (response.transactionResponse != null && response.transactionResponse.errors != null)
                    {
                        result.errorCode = response.transactionResponse.errors[0].errorCode;
                        result.errorText = response.transactionResponse.errors[0].errorText;
                    }
                    else
                    {
                        result.errorCode = response.messages.message[0].code;
                        result.errorText = response.messages.message[0].text;
                    }
                }
            }
            else
            {
                //result.errorCode = "NONE";
                //result.errorText = "Failed Transaction, Unknown Error";

                ANetApiResponse errorResponse = controller.GetErrorResponse();
                result.errorText = errorResponse.messages.message[0].text;
                result.errorCode = errorResponse.messages.message[0].code;
                result.resultCode = messageTypeEnum.Error;
            }
            return result;
        }
    }
}
