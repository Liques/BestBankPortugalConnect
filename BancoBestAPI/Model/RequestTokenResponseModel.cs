using System;
using System.Collections.Generic;
using System.Text;

namespace BestBankPortugalConnect
{
    internal class RequestTokenResponseModel
    {
        public string access_token { get; set; }
        public double expires_in { get; set; }
    }
}
