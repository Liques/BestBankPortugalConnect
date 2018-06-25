using System;
using System.Collections.Generic;
using System.Text;

namespace BancoBestAPI
{
    public class Application
    {
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public Environment Environment { get; set; }
    }

    public enum Environment
    {
        Sandbox,
        Production
    }
}
