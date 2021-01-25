using Banking.Api.OAuth2;
using Xunit;

namespace Banking.Api.Tests.OAuth2
{
    public class OAuth2ClientTests
    {
        [Fact]
        public void RequestAccessToken()
        { 
            var oAuth2Client = new OAuth2Client();

            oAuth2Client.RequestAccessToken();
        }
    }
}
