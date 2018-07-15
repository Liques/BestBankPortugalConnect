using System;
using System.Collections.Generic;
using System.Text;

namespace BestBankPortugalConnect
{
    public class Application
    {
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public Environment Environment { get; set; }
        internal string ServerUrl { get; set; }

        public Application(string consumerKey, string consumerSecret, Environment environment)
        {
            this.ConsumerKey = consumerKey;
            this.ConsumerSecret = consumerSecret;
            this.Environment = environment;

            if (this.Environment == Environment.Production)
                this.ServerUrl = Variables.UrlLiveData;
            else
                this.ServerUrl = Variables.UrlSandbox;
        }
    }

    public enum Environment
    {
        Sandbox,
        Production
    }
}
