using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace BestBankPortugalConnect
{
    /// <summary>
    /// Account Number details.
    /// </summary>
    public class AccountNumber
    {
        /// <summary>
        /// Account Number Type
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public AccountNumberType Type { get; set; }
        /// <summary>
        /// Account number value.
        /// <example>
        /// IBAN Value: 
        /// PT51006500010000000000454
        /// </example>
        /// <example>
        /// Credit Card Value: 
        /// 5247729000000004
        /// </example>
        /// </summary>
        public string Value { get; set; }
    }

    /// <summary>
    /// Account Number Type
    /// </summary>
    public enum AccountNumberType
    {
        /// <summary>
        /// Credit Card Number
        /// </summary>
        CreditCard = 1,
        /// <summary>
        /// IBAN
        /// </summary>
        IBAN = 2,
    }
}
