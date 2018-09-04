using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace BestBankPortugalConnect.Tools
{
    /// <summary>
    /// Base String helper. 
    /// </summary>
    public static class BaseString
    {
        /// <summary>
        /// Method that generates a Base String needed to make the OAuth 2.0 flow
        /// </summary>
        /// <param name="oauthparameters">List of Parameters</param>
        /// <returns>Base String</returns>
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

            basestring = Regex.Replace(basestring, "%[a-f0-9]{2}", m => m.Value.ToUpper());

            return basestring;
        }

    }
}
