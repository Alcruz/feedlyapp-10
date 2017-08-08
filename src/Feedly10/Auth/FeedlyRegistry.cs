namespace App.Services.OAuth
{
	public static class FeedlyRegistry
	{
		public static string BaseUrl = "https://sandbox7.feedly.com";
		public static string AuthCodeUrl = $"{BaseUrl}/v3/auth/auth";
		public static string RedirectAuthUrl = $"http://localhost";
		public static string AuthTokenUrl = $"{BaseUrl}/v3/auth/token";
	}
}