using System;
using System.Collections.Generic;
using System.Text;

namespace BancoBestAPI.Tools
{
    public static class Timestamp
    {
        public static string Now()
        {
            return Math.Round( (double)new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds()).ToString();
        }
    }
}
