using System;
using System.Collections.Generic;
using System.Text;
using BancoBestAPI;
using BancoBestAPI.Tools;
using RestSharp.Authenticators;
using RestSharp;
using BestBankPortugalConnect;
using Newtonsoft.Json;

namespace BestBankPortugalConnect
{
    public static class AuthorizationFlow
    {

        public static Uri GetBankLoginUrl(Application application, string redirectUrl, string state = "none", int response_type = 200, string scope = "none")
        {
            var header = Headers.Login(application, redirectUrl, response_type, scope, state);

            var client = new RestSharp.RestClient(application.ServerUrl + Variables.EndpointInitiate);

            RestRequest request = new RestRequest(Method.POST);
            client.AddDefaultHeader("Authorization", header);

            var response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var loginUrl = JsonConvert.DeserializeObject<LoginResponseModel>(response.Content).LoginUrl;

                return new Uri(loginUrl);
            }

            throw new BestBankAPIException(response);

        }

        public static User GetUserAccessToken(Application application, string temporaryCode, string grand_type = "none")
        {
            var header = Headers.AccessToken(application, temporaryCode, grand_type);

            var client = new RestSharp.RestClient(application.ServerUrl + Variables.EndpointGetAccessToken);

            RestRequest request = new RestRequest(Method.POST);
            client.AddDefaultHeader("Authorization", header);

            var response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseTokenObj = JsonConvert.DeserializeObject<RequestTokenResponseModel>(response.Content);

                return new User(responseTokenObj.access_token);
            }

            throw new BestBankAPIException(response);

        }

    }
}
