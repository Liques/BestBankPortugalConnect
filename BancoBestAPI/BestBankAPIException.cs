using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace BestBankPortugalConnect
{
    /// <summary>
    /// Specific API Exception
    /// </summary>
    public class BestBankAPIException : System.Exception
    {
        /// <summary>
        /// Json message error raw
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        /// Specific API Exception
        /// </summary>
        /// <param name="response"></param>
        public BestBankAPIException(IRestResponse response) : base(JsonConvert.DeserializeObject<ErrorResponseModel>(response.Content).Message)
        {
            this.Response = response.Content;
        }
    }

    internal class ErrorResponseModel
    {
        public string Message { get; set; }
        public DateTime ErrorDate { get; set; }
        public string ErrorID { get; set; }
    }
}
