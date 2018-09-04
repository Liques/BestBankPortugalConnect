using System;
using System.Collections.Generic;
using System.Text;

namespace BestBankPortugalConnect
{

    public class Account
    {
        public string _type { get; set; }
        public string currency { get; set; }
        public string value { get; set; }
    }

    public class PaymentBill
    {
        public string entity { get; set; }
        public string reference { get; set; }
    }


    public class PaymentGovernmentBill
    {
        public string reference { get; set; }
    }

    public class Creditor
    {
        public Account account { get; set; }
        public PaymentBill payment_bill { get; set; }
        public string message { get; set; }
        public string name { get; set; }
    }

    public class Debtor
    {
        public string message { get; set; }
    }

    internal class TransferRequestModel
    {
        public string amount { get; set; }
        public Creditor creditor { get; set; }
        public string currency { get; set; }
        public Debtor debtor { get; set; }
    }
}
