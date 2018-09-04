using System;
using System.Collections.Generic;
using System.Text;

namespace BestBankPortugalConnect
{
    /// <summary>
    /// Application representation
    /// </summary>
    public class Application
    {
        /// <summary>
        /// Application Consumer Key
        /// </summary>
        public string ConsumerKey { get; set; }
        /// <summary>
        /// Application Consumer Secret
        /// </summary>
        public string ConsumerSecret { get; set; }
        /// <summary>
        /// Enviroment (Sandbox or Production)
        /// </summary>
        public Environment Environment { get; set; }
        internal string ServerUrl { get; set; }

        /// <summary>
        /// Application representation
        /// </summary>
        /// <param name="consumerKey">Application Consumer Key</param>
        /// <param name="consumerSecret">Application Consumer Secret</param>
        /// <param name="environment">Enviroment (Sandbox or Production)</param>
        /// <example>
        /// Simple application object:
        /// <code>
        /// new Application()
        /// {
        ///     ConsumerKey = "735a1cfe04e53298929bd4fa59ab12a1b004bab5eea1edbdcc980741df181af1",
        ///     ConsumerSecret = "765a1cfe04e53298929bd4fa59ab12a1b004bab5eea1edbdcc980741df181af1",
        ///     Environment = Environment.Sandbox,
        /// }
        /// </code>
        /// </example>
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

    /// <summary>
    /// API Environment
    /// </summary>
    public enum Environment
    {
        /// <summary>
        /// Sandbox Environment
        /// </summary>
        Sandbox,
        /// <summary>
        /// Production Environment
        /// </summary>
        Production
    }
}
