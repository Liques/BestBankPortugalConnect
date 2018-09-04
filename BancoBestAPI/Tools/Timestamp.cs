using System;
using System.Collections.Generic;
using System.Text;

namespace BestBankPortugalConnect.Tools
{
    /// <summary>
    /// Helper to work with linux timestamp.
    /// </summary>
    public static class Timestamp
    {
        /// <summary>
        /// Get the current UTC Timestamp.
        /// </summary>
        /// <returns>UTC Timestamp</returns>
        public static string Now()
        {
            return Math.Round( (double)new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds()).ToString();
        }
    }
}
