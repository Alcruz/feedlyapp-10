using App.Utils;
using System;

namespace App.Services.OAuth
{
	public class OAuthToken
	{
		public string Id { get; set; }
		public string AccessToken { get; set; }
		public string RefreshToken { get; set; }
		public int ExpiresIn { get; set; }
		public string Plan { get; set; }
		public string State { get; set; }
		public string TokenType { get; set; }
		public DateTimeOffset CreatedAt { get; set; }


		public bool HasExpire()
		{
			Assert.IsNotNull(CreatedAt, nameof(CreatedAt));

			var diff = DateTimeOffset.UtcNow - CreatedAt.UtcDateTime;
			return diff.TotalSeconds > ExpiresIn;
		}
	}
}