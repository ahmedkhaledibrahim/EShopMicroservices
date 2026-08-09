namespace Users.Services.Settings
{
    public class JwtSettings
    {
        public int ExpiryMinutes { get; set; }
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
