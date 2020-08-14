using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GroupBCapstoneProject.Models
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
        [MinLength(3)]
        [MaxLength(4)]
        public string CardCode { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }
    public class TransactionResponse
    {
        public AuthorizeNet.Api.Contracts.V1.messageTypeEnum ResultCode { get; set; }
        public string  TransId { get; set; }
        public string ResponseCode { get; set; }
        public string MessageCode { get; set; }
        public string AuthCode { get; set; }
        public string Description { get; set; }
        public decimal AmountPaid { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorText { get; set; }
    }
}
