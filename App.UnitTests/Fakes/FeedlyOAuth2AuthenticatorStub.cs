using System.Threading.Tasks;
using App.Services.OAuth;

namespace App.UnitTests.Fakes
{
    public class FeedlyOAuth2AuthenticatorStub: FeedlyOAuth2Authenticator
    {
        public override Task<OAuthToken> Authenticate(string emailAccount, string authCode)
        {
            return null;
        }

        public override Task<string> RequestAuthCode(string emailAccount)
        {
            return Task.FromResult<string>(null);
        }
    }
}
