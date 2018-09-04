using System;
using System.Collections.Generic;
using System.Text;

namespace BestBankPortugalConnect
{
    internal static class Variables
    {
        public const string UrlSandbox = "https://developer.open.bancobest.pt/sandbox/";
        public const string UrlLiveData = "https://api.bancobest.pt/sandbox/";
        public const string EndpointInitiate = "api/v1/OAuth/Initiate";
        public const string EndpointToken = "api/v1/OAuth/Token";
        public const string EndpointBalance = "api/v1/Operation/Balance";
        public const string EndpointAssets = "api/v1/Operation/Assets";
        public const string EndpointMoviment = "api/v1/Operation/Transactions";
        public const string EndpointPayment = "api/v1/Operation/InitiatePayment";
        public const string EndpointConfirm = "api/v1/Operation/ConfirmPayment";
        public const string UniqueCurrency = "EUR";
        public const string UniqueAccountType = "IBAN";
        public const string Version = "1.0";

    }
}
