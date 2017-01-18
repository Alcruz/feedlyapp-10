using System.Threading.Tasks;
using App.Services.OAuth;

namespace App.UnitTests.Fakes
{
    public class FeedlyOAuth2AuthenticatorStub: FeedlyOAuth2Authenticator
    {
        public override Task<OAuthToken> Auth(string authCode)
        {
            return base.Auth(authCode);
        }

        public override Task<string> RequestAuthCode(string username)
        {
            return base.RequestAuthCode(username);
        }
    }
}
