using BancoBest.Tools;
using System;
using Xunit;

namespace BancoBestAPI.xUnitTest
{
    public class AuthTest
    {
        [Fact]
        public void IsCorrectSignature()
        {
            var basestring = "auth_consumer_key%3D835a1cfe04e53298929bd4fa59ab12a1b004bab5eea1edbdcc980741df181af1%26oauth_timestamp%3D2527679516%26oauth_version%3D1.0%26oauth_consumer_secret%3Da1d9193d271d4b8de55fa88f2c7b6d4c4264f0d8384838a35d6b7ee20c92b479";
            var secret = "a1d9193d271d4b8de55fa88f2c7b6d4c4264f0d8384838a35d6b7ee20c92b479";

            var signature = Signature.GetSignature(basestring, secret);

            Assert.Equal("A54c1HMRCQUdmkeTlvcCeQwEnlygDa2AVD7JbTO8uNo%3d", signature);
        }
    }
}
