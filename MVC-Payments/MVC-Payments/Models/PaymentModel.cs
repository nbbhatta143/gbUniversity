using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC_Payments.Models
{
    public class PaymentModel
    {
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("City")]
        public string Address1 { get; set; }
        [DisplayName("Address")]
        public string Address2 { get; set; }
        [DisplayName("Zip Code")]
        public string PostCode { get; set; }
        [DataType(DataType.Currency)]
        [Range(1,10000,ErrorMessage ="Can't be Negative number or > $10,000")]
        public decimal Amount { get; set; }
        [CreditCard]
        [DisplayName("Card Number")]
        public string CardNumber { get; set; }
        [DisplayName("CVV")]        
        public string CardCode { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }
    public class TransactionResponse
    {
        public AuthorizeNet.Api.Contracts.V1.messageTypeEnum resultCode { get; set; }
        public string  transId { get; set; }
        public string responseCode { get; set; }
        public string messageCode { get; set; }
        public string authCode { get; set; }
        public string description { get; set; }
        public string errorCode { get; set; }
        public string errorText { get; set; }
    }
}
