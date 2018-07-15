using System;
using System.Collections.Generic;
using System.Text;

namespace BestBankPortugalConnect
{
    public class User
    {
        private string accessToken;

        public string AccessToken
        {
            get { return accessToken; }
        }

        public User(string accessToken)
        {
            this.accessToken = accessToken;
        }
    }
}
