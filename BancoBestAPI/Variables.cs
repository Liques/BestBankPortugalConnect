using System;
using System.Collections.Generic;
using System.Text;

namespace BestBankPortugalConnect
{
    static class Variables
    {
        public const string UrlSandbox = "http://localhost/BestWebAPI/";
        public const string UrlLiveData = "https://wlp.bancobest.pt/apimanager/";
        public const string EndpointInitiate = "api/v1/OAuth/Initiate";
        public const string EndpointGetAccessToken = "api/v1/OAuth/GetAccessToken";
        public const string EndpointBalance = "api/v1/Operation/Balance";
        public const string EndpointMoviment = "api/v1/Operation/Moviments";

        public const string Version = "1.0";

    }
}
