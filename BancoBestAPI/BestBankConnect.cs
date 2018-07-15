using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace BestBankPortugalConnect
{
    public class BancoBestAPI
    {
        private User user;
        private Application application;

        public BancoBestAPI(User user, Application application)
        {
            this.user = user;
            this.application = application;
        }

        public IList<BalanceInformation> Balance()
        {
            var header = Headers.Operation(application, user.AccessToken);

            var client = new RestSharp.RestClient(application.ServerUrl + Variables.EndpointBalance);

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

        public IList<Moviment> Moviments(string accountId)
        {
            var header = Headers.Operation(application, user.AccessToken);

            var client = new RestSharp.RestClient(application.ServerUrl + Variables.EndpointBalance + "/" + accountId);

            RestRequest request = new RestRequest(Method.POST);
            client.AddDefaultHeader("Authorization", header);

            var response = client.Execute(request);

            try
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<IList<Moviment>>(response.Content);
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
