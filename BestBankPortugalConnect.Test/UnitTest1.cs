using BestBankPortugalConnect.Tools;
using System;
using Xunit;

namespace BestBankPortugalConnect.Test
{
    public class MainTest
    {

        string appConsumer = "set here";
        string appSecret = "set here";
        string accessToken = "set here";

        [Fact]
        public void IsCorrectSignature()
        {
            var basestring = "auth_consumer_key%3D835a1cfe04e53298929bd4fa59ab12a1b004bab5eea1edbdcc980741df181af1%26oauth_timestamp%3D2527679516%26oauth_version%3D1.0%26oauth_consumer_secret%3Da1d9193d271d4b8de55fa88f2c7b6d4c4264f0d8384838a35d6b7ee20c92b479";
            var secret = "a1d9193d271d4b8de55fa88f2c7b6d4c4264f0d8384838a35d6b7ee20c92b479";

            var signature = Signature.GetSignature(basestring, secret);

            Assert.Equal("A54c1HMRCQUdmkeTlvcCeQwEnlygDa2AVD7JbTO8uNo%3d", signature);
        }



        [Fact]
        public void GetAuthorizationURL()
        {
            var app = new Application(appConsumer, appSecret, Environment.Sandbox);

            var url = AuthorizationFlow.GetBankLoginUrl(app, @"http:\\www.httpbin.org\get");

            Assert.StartsWith("http", url.AbsoluteUri);
        }



        [Fact]
        public void GetAccessToken()
        {
            var app = new Application(appConsumer, appSecret, Environment.Sandbox);

            var accessToken = AuthorizationFlow.GetUserAccessToken(app, "058f485de95d49d7b7ab7165919f0379");

            Assert.NotNull(accessToken);
        }



        [Fact]
        public void GetAssets()
        {
            var app = new Application(appConsumer, appSecret, Environment.Sandbox);
            var user = new User(accessToken);

            var api = new BestBankConnector(user, app);

            var assets = api.Assets();

            Assert.NotNull(assets);
        }

        [Fact]
        public void GetBalanceOfBankAccount()
        {
            var app = new Application(appConsumer, appSecret, Environment.Sandbox);
            var user = new User(accessToken);

            var api = new BestBankConnector(user, app);

            var balance = api.Balance(AccountType.BankAccount);

            Assert.NotNull(balance);
        }

        [Fact]
        public void GetBalanceOfCreditCard()
        {
            var app = new Application(appConsumer, appSecret, Environment.Sandbox);
            var user = new User(accessToken);

            var api = new BestBankConnector(user, app);

            var balance = api.Balance(AccountType.CreditCard);

            Assert.NotNull(balance);
        }


        [Fact]
        public void GetMovimentsOfBankAccount()
        {
            var app = new Application(appConsumer, appSecret, Environment.Sandbox);
            var user = new User(accessToken);

            var api = new BestBankConnector(user, app);

            var moviments = api.Transactions("0-MDAwMDAwMDAx", AccountType.BankAccount);

            Assert.NotNull(moviments);
        }

        [Fact]
        public void TransferAndConfirmFromBankAccount()
        {
            var app = new Application(appConsumer, appSecret, Environment.Sandbox);
            var user = new User(accessToken);

            var api = new BestBankConnector(user, app);

            var withdrawRequirement = api.Transfer("0-MDAwMDAwMDEx", 10.5, "PT50006500010000000000154", "Creditor Name", "Creditor Message", "Debitor Message");

            var isPaid = withdrawRequirement.Confirm("1234");

            Assert.True(isPaid);
        }


        [Fact]
        public void Payment()
        {
            var app = new Application(appConsumer, appSecret, Environment.Sandbox);
            var user = new User(accessToken);

            var api = new BestBankConnector(user, app);

            var withdrawRequirement = api.Pay("0-MDAwMDAwMDAx", 10.5, "5478", "193668755", "Creditor Message", "Debitor Message");

            var isPaid = withdrawRequirement.Confirm("1234");

            Assert.True(isPaid);
        }
    }
}
