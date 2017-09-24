namespace Feedly10.App.Feedly
{
	public static class FeedlyRegistry
	{
		public static string BaseUrl = "https://sandbox7.feedly.com";

		public static string AuthCodeUrl = $"{BaseUrl}/v3/auth/auth";
		public static string AuthRedirectUrl = $"http://localhost";
		public static string AuthTokenUrl = $"{BaseUrl}/v3/auth/token";

		public static string Categories = $"{BaseUrl}/v3/categories";
		public static string Subcriptions = $"{BaseUrl}/v3/subscriptions";
		public static string Streams = $"{BaseUrl}/v3/streams";
		public static string StreamContent = BaseUrl + "/v3/streams/{0}/contents";

		public static string ScopeSubscription = "https://cloud.feedly.com/subscriptions";
	}
}