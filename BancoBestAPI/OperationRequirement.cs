using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace BestBankPortugalConnect
{
    /// <summary>
    /// Operation Type
    /// </summary>
    public enum OperationType
    {
        /// <summary>
        /// Any Operation
        /// </summary>
        Any = 1,
        /// <summary>
        /// Transfer operation
        /// </summary>
        Transfer = 2,
        /// <summary>
        /// Bill operation
        /// </summary>
        Bill = 3,
    }

    /// <summary>
    /// Requirement of a transfer or payment.
    /// </summary>
    public class OperationRequirement
    {
        private BestBankConnector api;
        private string accountID;

        private OperationType type;
        /// <summary>
        /// Operation Type
        /// </summary>
        public OperationType Type
        {
            get { return type; }
        }

        private string paymentCode;
        /// <summary>
        /// Code returned when the requirement is successful
        /// </summary>
        public string PaymentCode
        {
            get { return paymentCode; }
        }

        private bool isConfirmed;
        /// <summary>
        /// If requirement transfer or payment has been complete
        /// </summary>
        public bool IsConfirmed
        {
            get { return isConfirmed; }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="OperationRequirement"/> class.
        /// </summary>
        /// <param name="api">The API.</param>
        /// <param name="paymentCode">The payment code.</param>
        /// <param name="accountID">The account identifier.</param>
        internal OperationRequirement(BestBankConnector api, string paymentCode, string accountID)
        {
            this.paymentCode = paymentCode;
            this.accountID = accountID;
            this.api = api;
        }

        internal OperationRequirement(BestBankConnector api, string paymentCode, string accountID, OperationType type)
        {
            this.paymentCode = paymentCode;
            this.accountID = accountID;
            this.api = api;
            this.type = type;
        }

        /// <summary>
        /// Confirm and complete the transfer or the payment.
        /// </summary>
        /// <param name="smsCode">SMS Code sent to the user. In Sandbox, the only value is '1234'.</param>
        /// <returns>
        /// Confirms is the tranfer or payment was completed.
        /// </returns>
        /// <exception cref="BestBankAPIException"></exception>
        public bool Confirm(string smsCode)
        {
            if (this.isConfirmed)
            {
                return true;
            }

            var confirmRequest = new ConfirmPaymentRequestModel
            {
                payment_code = paymentCode,
                sms_code = smsCode
            };
            
            var header = Headers.Operation(api.Application, api.User.AccessToken);
            var client = new RestSharp.RestClient(UrlFormatter.GetEndpointUrl(api.Application, BestBankEndpoint.ConfirmPayment));
            RestRequest request = new RestRequest(Method.POST);
            client.AddDefaultHeader("Authorization", header);

            request.AddJsonBody(confirmRequest);

            var response = client.Execute(request);

            try
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }

                throw new BestBankAPIException(response);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
