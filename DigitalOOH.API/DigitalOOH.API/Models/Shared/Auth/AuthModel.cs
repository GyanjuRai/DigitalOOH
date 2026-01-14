namespace DigitalOOH.API.Models.Shared.Auth
{
    public record JwtConfig
    {
        public string SecretKey { get; set; } = "";
        public string Issuer { get; set; } = "";
        public string Audience { get; set; } = "";

        public int AccessTokenExpirationMin { get; set; } = 60;
        public int AccessTokenClockSkewMin { get; set; } = 4;

    }
    public record JwtResponse
    {
        public string? Token { get; set; }
    }
}
