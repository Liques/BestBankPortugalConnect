using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace BestBankPortugalConnect
{
    /// <summary>
    /// Bank Account information summary.
    /// </summary>
    public class ProductInformation
    {
        /// <summary>
        /// Bank Account internal ID
        /// </summary>
        /// <value>
        /// Bank Account internal ID
        /// </value>
        public string ID { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        /// <value>
        /// Currency
        /// </value>
        public string Currency { get; set; }

        /// <summary>
        /// Account Number
        /// </summary>
        /// <value>
        /// Account Number
        /// </value>
        public AccountNumber AccountNumber { get; set; }

        /// <summary>
        /// Account Type
        /// </summary>
        /// <value>
        /// Account Type
        /// </value>
        public string Type { get; set; }
    }

    /// <summary>
    /// Permission Type
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Permission
    {
        /// <summary>
        /// Permission to get the user bank account balance.
        /// </summary>
        BALANCE = 1,
        /// <summary>
        /// Permission to get the user bank account transactions list.
        /// </summary>
        TRANSACTIONS = 2,
        /// <summary>
        /// Permission to make transfers.
        /// </summary>
        TRANSFERS = 3,
        /// <summary>
        /// Permission to make payments.
        /// </summary>
        PAYMENTS = 4,
    }
}
