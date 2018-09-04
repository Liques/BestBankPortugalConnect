using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace BestBankPortugalConnect
{
    /// <summary>
    /// Connector to the Banco Best API. With this object is possible to make bank operations.
    /// </summary>
    public class BestBankConnector
    {
        private User user;
        private Application application;

        internal User User { get { return user; } }
        internal Application Application { get { return application; } }

        /// <summary>
        /// Connector to the Banco Best API.
        /// </summary>
        /// <param name="user">Object that contains user Access Token.</param>
        /// <param name="application">Object that contain some data of the application that is connecting to the platform.</param>
        /// <example>
        /// Simple Best Bank Connector.
        /// <code>
        ///  var app = new Application(appConsumer, appSecret, Environment.Sandbox);
        ///  var user = new User(accessToken);
        ///  
        ///  var api = new BestBankConnector(user, app);
        /// </code>
        /// </example>
        public BestBankConnector(User user, Application application)
        {
            this.user = user;
            this.application = application;
        }

        /// <summary>
        /// Gets the user's accounts balance.
        /// </summary>
        /// <example>
        /// Get balances.
        /// <code>
        /// var balance = api.Balance();
        /// </code>
        /// </example>
        /// <returns>List of user accounts with balance</returns>
        public IList<BalanceInformation> Balance(AccountType accountType = AccountType.BankAccount)
        {
            var header = Headers.Operation(application, user.AccessToken);

            var client = new RestSharp.RestClient(UrlFormatter.GetEndpointUrl(application, BestBankEndpoint.Balance,accountType));

            RestRequest request = new RestRequest(Method.POST);
            client.AddDefaultHeader("Authorization", header);

            var response = client.Execute(request);

            try
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<IList<BalanceInformation>>(response.Content);
                }

                throw new BestBankAPIException(response);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Gets the user's accounts list with it's permissions.
        /// </summary>
        /// <example>
        /// Get list
        /// <code>
        /// var balance = api.Assets();
        /// </code>
        /// </example>
        /// <returns>List of user accounts with permissions of each one.</returns>
        public IList<ProductInformation> Assets()
        {
            var header = Headers.Operation(application, user.AccessToken);

            var client = new RestSharp.RestClient(UrlFormatter.GetEndpointUrl(application, BestBankEndpoint.Assets));

            RestRequest request = new RestRequest(Method.POST);
            client.AddDefaultHeader("Authorization", header);

            var response = client.Execute(request);

            try
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<IList<ProductInformation>>(response.Content);
                }

                throw new BestBankAPIException(response);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Gets the moviment of one specific user's account.
        /// </summary>
        /// <param name="accountId">Internal User's Banco Best Account ID</param>
        /// <example>
        /// Get moviments of an account>
        /// <code>
        /// var moviments = api.Moviments("0-G4ECC");
        /// </code>
        /// </example>
        /// <returns>List of Moviments</returns>
        public IList<Transaction> Transactions(string accountId, AccountType accountType = AccountType.BankAccount)
        {
            var header = Headers.Operation(application, user.AccessToken);

            var client = new RestSharp.RestClient(UrlFormatter.GetEndpointUrl(application, BestBankEndpoint.Transactions, accountType, accountId));

            RestRequest request = new RestRequest(Method.POST);
            client.AddDefaultHeader("Authorization", header);

            var response = client.Execute(request);

            try
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<IList<Transaction>>(response.Content);
                }

                throw new BestBankAPIException(response);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Starts a tranfer requeriment between one specific user's account to another account. The transfer is only complete after the user type the SMS code sent to him.
        /// </summary>
        /// <param name="accountId">Internal User's Banco Best Account ID</param>
        /// <param name="amount">Money amount</param>
        /// <param name="creditorIBAN">Account's IBAN of who is receiving the transfer</param>
        /// <param name="creditorName">Account's name of who is receiving the transfer</param>
        /// <param name="creditorMessage">A message for who is receiving the transfer</param>
        /// <param name="debitorMessage">A message for who is sending the transfer</param>
        /// <example>
        /// Make simple transfer:
        /// <code>
        /// var withdrawRequirement = api.Transfer("0-G4ECC", 10.5, "PT50006500010000000000154", "Creditor Name", "Creditor Message", "Debitor Message");
        /// </code>
        /// </example>
        /// <returns>Returns a requirement. The transfer is only confirmed after the user type the SMS code sent to him.</returns>
        public OperationRequirement Transfer(string accountId, double amount, string creditorIBAN, string creditorName, string creditorMessage, string debitorMessage, AccountType accountType = AccountType.BankAccount)
        {
            var tranferRequest = new TransferRequestModel
            {
                amount = amount.ToString(),
                currency = Variables.UniqueCurrency,
                debtor = new Debtor
                {
                    message = debitorMessage
                },
                creditor = new Creditor
                {
                    account = new Account
                    {
                        currency = Variables.UniqueCurrency,
                        value = creditorIBAN,
                        _type = Variables.UniqueAccountType
                    },
                    name = creditorName,
                    message = creditorMessage,
                }
            };

            var header = Headers.Operation(application, user.AccessToken);
            var client = new RestSharp.RestClient(UrlFormatter.GetEndpointUrl(application, BestBankEndpoint.Transfer, accountType, accountId));
            RestRequest request = new RestRequest(Method.POST);
            client.AddDefaultHeader("Authorization", header);

            request.AddJsonBody(tranferRequest);

            var response = client.Execute(request);

            try
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responsePaymentCode = JsonConvert.DeserializeObject<PaymentCodeResponseModel>(response.Content);

                    return new OperationRequirement(this, responsePaymentCode.payment_code, accountId, OperationType.Transfer);
                }

                throw new BestBankAPIException(response);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Starts a portuguese payment bill requeriment between one specific user's account to another account. The payment is only complete after the user type the SMS code sent to him.
        /// </summary>
        /// <param name="accountId">Internal User's Banco Best Account ID</param>
        /// <param name="amount">Money amount</param>
        /// <param name="entity">Entity number of the bill</param>
        /// <param name="reference">Reference number of the bill</param>
        /// <param name="creditorMessage">A message for who is receiving the payment</param>
        /// <param name="debitorMessage">A message for who is sending the payment</param>
        /// <example>
        /// Make a payment:
        /// <code>
        /// var withdrawRequirement = api.Pay("0-G4ECC", 66, "5478", "123668755", "Creditor Message", "Debitor Message");
        /// </code>
        /// </example>
        /// <returns>Returns a requirement. The payment is only confirmed after the user type the SMS code sent to him.</returns>
        public OperationRequirement Pay(string accountId, double amount, string entity, string reference, string creditorMessage, string debitorMessage, AccountType accountType = AccountType.BankAccount)
        {
            var tranferRequest = new TransferRequestModel
            {
                amount = amount.ToString(),
                currency = Variables.UniqueCurrency,
                debtor = new Debtor
                {
                    message = debitorMessage
                },
                creditor = new Creditor
                {
                    payment_bill = new PaymentBill
                    {
                        entity = entity,
                        reference = reference,
                    },
                    message = creditorMessage,
                }
            };

            var header = Headers.Operation(application, user.AccessToken);
            var client = new RestSharp.RestClient(UrlFormatter.GetEndpointUrl(application, BestBankEndpoint.Transfer, accountType, accountId));
            RestRequest request = new RestRequest(Method.POST);
            client.AddDefaultHeader("Authorization", header);

            request.AddJsonBody(tranferRequest);

            var response = client.Execute(request);

            try
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responsePaymentCode = JsonConvert.DeserializeObject<PaymentCodeResponseModel>(response.Content);

                    return new OperationRequirement(this, responsePaymentCode.payment_code, accountId, OperationType.Bill);
                }

                throw new BestBankAPIException(response);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// It confirms and completes a tranfer or a payment.
        /// </summary>
        /// <param name="accountId">Internal User's Banco Best Account ID</param>
        /// <param name="smsCode">SMS Code sent to the user. In Sandbox, the only value is '1234'.</param>
        /// <param name="paymentCode">Payment Code sent previously when the transfer requirement or the payment requirement is complete.</param>
        /// <example>
        /// Confirm a transfer or payment:
        /// <code>
        /// var withdrawRequirement = api.Pay("0-G4ECC", 66, "5478", "123668755", "Creditor Message", "Debitor Message");
        /// var smsCode = Console.ReadLine(); // Wait the user answer the code sent via SMS. In sandbox environment the only value is "1234".
        /// var isPaid = api.ConfirmOperation("0-G4ECC", smsCode, withdrawRequirement.PaymentCode);
        /// </code>
        /// </example>
        /// <example>
        /// Another way to transfer or payment:
        /// <code>
        /// var withdrawRequirement = api.Pay("0-G4ECC", 66, "5478", "123668755", "Creditor Message", "Debitor Message");
        /// var smsCode = Console.ReadLine(); // Wait the user answer the code sent via SMS. In sandbox environment the only value is "1234".
        /// var isPaid = withdrawRequirement.Confirm(smsCode);
        /// </code>
        /// </example>
        /// <returns>Confirms is the tranfer or payment was completed.</returns>
        public bool ConfirmOperation(string accountId, string smsCode, string paymentCode)
        {

            var obj = new OperationRequirement(this, accountId, accountId, OperationType.Bill);

            return obj.Confirm(smsCode);

        }
    }
}
