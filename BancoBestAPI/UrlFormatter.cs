using System;
using System.Collections.Generic;
using System.Text;

namespace BestBankPortugalConnect
{
    internal enum BestBankEndpoint
    {
        Initiate,
        Token,
        Assets,
        Balance,
        Transactions,
        Transfer,
        ConfirmPayment,
    }

    internal static class UrlFormatter
    {
        public static string GetEndpointUrl(Application application, BestBankEndpoint endpoint, AccountType? accountType = null, string optionalParameter = null)
        {
            var url = new StringBuilder();
            url.Append(application.ServerUrl);
            url.Append(GetEndpointURL(endpoint));
            url.Append("/");

            string accountTypeParam = null;

            if (accountType.HasValue && accountType.Value == AccountType.BankAccount)
                accountTypeParam = "Account";
            else if (accountType.HasValue && accountType.Value == AccountType.CreditCard)
                accountTypeParam = "CreditCard";
            else
                accountTypeParam = String.Empty;

            url.Append(accountTypeParam);
            url.Append("/");
            url.Append(optionalParameter);


            return url.ToString();
        }

        private static string GetEndpointURL(BestBankEndpoint endpoint)
        {
            switch (endpoint)
            {
                case BestBankEndpoint.Initiate:
                    return Variables.EndpointInitiate;

                case BestBankEndpoint.Token:
                    return Variables.EndpointToken;

                case BestBankEndpoint.Assets:
                    return Variables.EndpointAssets;

                case BestBankEndpoint.Balance:
                    return Variables.EndpointBalance;

                case BestBankEndpoint.Transactions:
                    return Variables.EndpointMoviment;

                case BestBankEndpoint.Transfer:
                    return Variables.EndpointPayment;
                    
                case BestBankEndpoint.ConfirmPayment:
                    return Variables.EndpointConfirm;
            }

            throw new ArgumentException("Internal development or package error.");
        }
    }
}
