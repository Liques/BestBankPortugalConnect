using BestBankPortugalConnect.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace BestBankPortugalConnect
{
    internal static class Headers
    {
        public static string Login(Application application, string redirectUrl, int response_type, string scope, string state)
        {

            var timeStamp = Timestamp.Now();

            List<KeyValuePair<string, string>> oauthparameters = new List<KeyValuePair<string, string>>();
            oauthparameters.Add(new KeyValuePair<string, string>("oauth_consumer_key", application.ConsumerKey));
            oauthparameters.Add(new KeyValuePair<string, string>("oauth_timestamp", timeStamp));
            oauthparameters.Add(new KeyValuePair<string, string>("oauth_version", Variables.Version));
            oauthparameters.Add(new KeyValuePair<string, string>
                ("oauth_consumer_secret", application.ConsumerSecret));
            oauthparameters.Add(new KeyValuePair<string, string>("redirect_uri", String.Format(@"""{0}""", redirectUrl)));
            oauthparameters.Add(new KeyValuePair<string, string>("response_type", response_type.ToString()));
            oauthparameters.Add(new KeyValuePair<string, string>("scope", scope));
            oauthparameters.Add(new KeyValuePair<string, string>("state", state));

            var basestring = BaseString.Transform(oauthparameters);
            var signature = Signature.GetSignature(basestring, application.ConsumerSecret);

            var header = String.Format(@"OAuth oauth_consumer_key={0},oauth_timestamp={1},oauth_version=1.0,redirect_uri={2},response_type={3},scope={4},state={5},oauth_signature={6}",
                application.ConsumerKey,
                timeStamp,
                String.Format(@"""{0}""", redirectUrl),
                response_type,
                scope,
                state,
                signature);

            return header;
        }

        public static string AccessToken(Application application, string temporaryCode, string grand_type)
        {

            var timeStamp = Timestamp.Now();

            List<KeyValuePair<string, string>> oauthparameters = new List<KeyValuePair<string, string>>();
            oauthparameters.Add(new KeyValuePair<string, string>("code", temporaryCode));
            oauthparameters.Add(new KeyValuePair<string, string>("grand_type", grand_type));
            oauthparameters.Add(new KeyValuePair<string, string>("oauth_consumer_key", application.ConsumerKey));
            oauthparameters.Add(new KeyValuePair<string, string>("oauth_timestamp", timeStamp));
            oauthparameters.Add(new KeyValuePair<string, string>("oauth_version", Variables.Version));
            oauthparameters.Add(new KeyValuePair<string, string>
                ("oauth_consumer_secret", application.ConsumerSecret));
            oauthparameters.Add(new KeyValuePair<string, string>("redirect_uri", "none"));

            var basestring = BaseString.Transform(oauthparameters);
            var signature = Signature.GetSignature(basestring, application.ConsumerSecret);

            var header = String.Format(@"OAuth code={0},grand_type={1},oauth_consumer_key={2},oauth_timestamp={3},oauth_version=1.0,redirect_uri={4},oauth_signature={5}",
                temporaryCode,
                grand_type,
                application.ConsumerKey,
                timeStamp,
                "none",
                signature);

            return header;
        }


        public static string Operation(Application application, string accessToken)
        {
            var timeStamp = Timestamp.Now();

            List<KeyValuePair<string, string>> oauthparameters = new List<KeyValuePair<string, string>>();
            oauthparameters.Add(new KeyValuePair<string, string>("access_token", accessToken));
            oauthparameters.Add(new KeyValuePair<string, string>("oauth_consumer_key", application.ConsumerKey));
            oauthparameters.Add(new KeyValuePair<string, string>("oauth_timestamp", timeStamp));
            oauthparameters.Add(new KeyValuePair<string, string>("oauth_version", Variables.Version));
            oauthparameters.Add(new KeyValuePair<string, string>
                ("oauth_consumer_secret", application.ConsumerSecret));

            var basestring = BaseString.Transform(oauthparameters);
            var signature = Signature.GetSignature(basestring, application.ConsumerSecret);

            var header = String.Format(@"OAuth access_token={0},oauth_consumer_key={1},oauth_timestamp={2},oauth_version=1.0,oauth_signature={3}",
                accessToken,
                application.ConsumerKey,
                timeStamp,
                signature);

            return header;
        }
    }
}
