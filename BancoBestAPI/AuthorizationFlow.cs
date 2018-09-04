using System;
using System.Collections.Generic;
using System.Text;
using RestSharp.Authenticators;
using RestSharp;
using BestBankPortugalConnect;
using Newtonsoft.Json;

namespace BestBankPortugalConnect
{
    /// <summary>
    /// This class helps to make the painful Auth 2.0 authorization flow faster and easy.
    /// </summary>
    public static class AuthorizationFlow
    {
        /// <summary>
        /// This method is the first step of the authorizaton flow. It will return the Banco Best user login url where the user will allow the application to connect to his own account.
        /// </summary>
        /// <example>
        /// <code>
        /// AuthorizationFlow.GetBankLoginUrl(myApplication,"http://www.myapplication.com/returnLogin");
        /// </code>
        /// </example>
        /// <param name="application">Application representation</param>
        /// <param name="redirectUrl">URL where the user will be redirected after succesful login to the application.</param>
        /// <param name="state">Custom application variable that will be returned with the 'redirectUrl' as param.</param>
        /// <param name="response_type">Response Type Code</param>
        /// <param name="scope">Scope</param>
        /// <returns>Login URL at the Banco Best website where the application must redirect the user.</returns>
        public static Uri GetBankLoginUrl(Application application, string redirectUrl, string state = "none", int response_type = 200, string scope = "none")
        {
            var header = Headers.Login(application, redirectUrl, response_type, scope, state);

            var client = new RestSharp.RestClient(UrlFormatter.GetEndpointUrl(application, BestBankEndpoint.Initiate));

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

        /// <summary>
        /// This is the second step of the authorization flow. Get the User object containing his access token.
        /// See <see cref="AuthorizationFlow.GetBankLoginUrl(Application, string, string, int, string)"/> to start the first step.
        /// </summary>
        /// <param name="application">Application representation</param>
        /// <param name="temporaryCode">
        /// Parameters returned after the user return to the application.
        /// See <see cref="AuthorizationFlow.GetBankLoginUrl(Application, string, string, int, string)"/> if you have no temporary code.
        /// </param>
        /// <param name="grand_type">Grand type</param>
        /// <returns>User object containing his access token and ready to start to make operations.</returns>
        /// See <see cref="AuthorizationFlow.GetBankLoginUrl(Application, string, string, int, string)"/> to make the first step.
        public static User GetUserAccessToken(Application application, string temporaryCode, string grand_type = "none")
        {
            var header = Headers.AccessToken(application, temporaryCode, grand_type);

            var client = new RestSharp.RestClient(UrlFormatter.GetEndpointUrl(application, BestBankEndpoint.Token));

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
