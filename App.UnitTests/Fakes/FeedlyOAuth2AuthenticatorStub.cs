using System.Threading.Tasks;
using App.Services.OAuth;

namespace App.UnitTests.Fakes
{
    public class FeedlyOAuth2AuthenticatorStub: FeedlyOAuth2Authenticator
    {
        public override Task<OAuthToken> Authenticate(string authCode)
        {
            return null;
        }

        public override Task<string> RequestAuthCode()
        {
            return Task.FromResult<string>(null);
        }
    }
}
