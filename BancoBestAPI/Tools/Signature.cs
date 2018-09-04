using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BestBankPortugalConnect.Tools
{
    /// <summary>
    /// Generates a HMAC SHA256 signature based on the Base String. It's needed to validate a request in OAuth 2.0
    /// </summary>
    public static class Signature
    {
        /// <summary>
        /// Generate the HMAC SHA256 signature based on the Base String.
        /// </summary>
        /// <param name="basestring">Base String</param>
        /// <param name="oauth_consumer_secret">Aplication Consumer Secret Key</param>
        /// <returns>HMAC SHA256 signature coded in Base64 and URL Coded</returns>
        public static string GetSignature(string basestring, string oauth_consumer_secret)
        {
            // Code based on this page: http://obp.sckhoo.com/obpwalkthrough/Page2_obtainrequesttoken.aspx

            var enc = Encoding.ASCII;

            HMACSHA256 hmac = new HMACSHA256(enc.GetBytes(oauth_consumer_secret));
            hmac.Initialize();
            byte[] buffer = enc.GetBytes(basestring);
            string hmacsha256 = BitConverter.ToString(hmac.ComputeHash(buffer)).Replace("-", "")
                .ToLower();
            byte[] resultantArray = new byte[hmacsha256.Length / 2];
            for (int i = 0; i < resultantArray.Length; i++)
            {
                resultantArray[i] = Convert.ToByte(hmacsha256.Substring(i * 2, 2), 16);
            }
            string base64 = Convert.ToBase64String(resultantArray);
            return HttpUtility.UrlEncode(base64);

        }
    }
}
