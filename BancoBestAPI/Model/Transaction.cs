using System;
using System.Collections.Generic;
using System.Text;

namespace BestBankPortugalConnect
{
    /// <summary>
    /// Represents a single transaction data
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Balance on the time of the transaction
        /// </summary>
        public double Balance { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Date in timestamp
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        /// Amount
        /// </summary>
        public double Amount { get; set; }
    }
}
