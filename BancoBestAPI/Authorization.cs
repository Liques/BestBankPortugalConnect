using System;
using System.Collections.Generic;
using System.Text;
using BancoBestAPI;
using BancoBestAPI.Tools;
using RestSharp.Authenticators;
using RestSharp;

namespace BancoBestAPI
{
    public class Authorization
    {
        Application application = null;

        public Authorization(Application application)
        {
            this.application = application;
        }

        public string GetBankLoginUrl(string redirectUrl, string state = "none", int response_type = 200, string scope = "none")
        {

            List<KeyValuePair<string, string>> oauthparameters = new List<KeyValuePair<string, string>>();
            oauthparameters.Add(new KeyValuePair<string, string>("oauth_consumer_key", application.ConsumerKey));
            oauthparameters.Add(new KeyValuePair<string, string>("oauth_timestamp", Timestamp.Now()));
            oauthparameters.Add(new KeyValuePair<string, string>("oauth_version", Variables.Version));
            oauthparameters.Add(new KeyValuePair<string, string>
                ("oauth_consumer_secret", application.ConsumerSecret));

            var basestring = BaseString.Transform(oauthparameters);
            var signature = Signature.GetSignature(basestring, application.ConsumerSecret);

            var header = String.Format(@"oauth_consumer_key={0},oauth_timestamp={1},oauth_version=1.0,redirect_uri=""{2}"",response_type = {3},scope = {4},state = {5},oauth_signature ={6}", 
                application.ConsumerKey, 
                Timestamp.Now(), 
                redirectUrl, 
                response_type, 
                scope, 
                state, 
                signature);


            var client = new RestSharp.RestClient(Variables.UrlSandbox + Variables.EndpointInitiate);

            RestRequest request = new RestRequest( Method.POST );
            client.Authenticator = new HttpBasicAuthenticator("OAuth", header);
        }
    }
}
