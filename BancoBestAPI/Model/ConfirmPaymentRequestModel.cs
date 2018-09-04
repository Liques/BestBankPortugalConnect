using System;
using System.Collections.Generic;
using System.Text;

namespace BestBankPortugalConnect
{
    internal class ConfirmPaymentRequestModel
    {
        public string payment_code { get; set; }
        public string sms_code { get; set; }
    }
}
