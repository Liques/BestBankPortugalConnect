using System;
using System.Collections.Generic;
using System.Text;

namespace BestBankPortugalConnect
{
    /// <summary>
    /// Balance Information of a specific account
    /// </summary>
    public class BalanceInformation
    {
        /// <summary>
        /// Internal User's Banco Best Account ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Balance
        /// </summary>
        public string Balance { get; set; }
        /// <summary>
        /// Balance Avaliable
        /// </summary>
        public string BalanceAvailable { get; set; }
        /// <summary>
        /// Account Type
        /// </summary>
        public string AccountType { get; set; }
        /// <summary>
        /// Product
        /// </summary>
        public string Product { get; set; }
        /// <summary>
        /// Account Number
        /// </summary>
        public AccountNumber AccountNumber { get; set; }

    }
}
