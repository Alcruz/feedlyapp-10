namespace App.Services.OAuth
{
    public class OAuthToken
    {
        public string Id { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int Second { get; set; }
        public string Plan { get; set; }
        public string State { get; set; }
    }
}