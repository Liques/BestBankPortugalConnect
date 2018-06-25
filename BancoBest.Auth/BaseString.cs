using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BancoBestAPI.Tools
{
    public static class BaseString
    {

        public static string Transform(List<KeyValuePair<string, string>> oauthparameters)
        {
            var signtature = oauthparameters.Single(s => s.Key == "oauth_consumer_secret").Value;
            oauthparameters.RemoveAll(r => r.Key == "oauth_consumer_secret");
            // Code based on this page: http://obp.sckhoo.com/obpwalkthrough/Page2_obtainrequesttoken.aspx

            // Sort the OAuth parameters on the key
            oauthparameters.Sort((x, y) => x.Key.CompareTo(y.Key));

            string basestring = String.Empty;

            // Construct the Base String
            foreach (KeyValuePair<string, string> pair in oauthparameters)
            {
                basestring += pair.Key + "%3D" + HttpUtility.UrlEncode(pair.Value) + "%26";
            }
            basestring += "oauth_consumer_secret%3D" + signtature;

            return basestring;
        }

    }
}
