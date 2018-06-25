using System;

namespace BancoBestAPI
{
    public class BancoBestAPI
    {
        private User user;
        private Application application;

        public BancoBestAPI(User user, Application application)
        {

        }

        public static string GetOAuthLoginUrl(Application application)
        {
            throw new NotImplementedException();
        }

        public static string GetAccessToken(Application application)
        {
            throw new NotImplementedException();
        }
    }
}
