using System;
using System.Collections.Generic;
using System.Text;

namespace BestBankPortugalConnect
{
    /// <summary>
    /// User representation
    /// </summary>
    public class User
    {
        private string accessToken;
        /// <summary>
        /// User's Access Token
        /// </summary>
        public string AccessToken
        {
            get { return accessToken; }
        }

        /// <summary>
        /// Banco Best user
        /// </summary>
        /// <param name="accessToken">User's Access Token</param>
        /// <example>
        /// If the user access token is already know
        /// <code>
        /// User user = new User("dzfa324cc23gh2jg3423679hxnzb");
        /// </code>
        /// </example>
        /// See <see cref="AuthorizationFlow.GetUserAccessToken(Application, string, string)"/> if you have no access token.
        public User(string accessToken)
        {
            this.accessToken = accessToken;
        }
    }
}
