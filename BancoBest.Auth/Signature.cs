using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BancoBestAPI.Tools
{
    public static class Signature
    {
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
