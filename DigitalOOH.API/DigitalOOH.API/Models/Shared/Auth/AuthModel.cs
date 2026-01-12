namespace DigitalOOH.API.Models.Shared.Auth
{
    public record JwtConfig
    {
        public string SecretKey { get; init; } = "";
        public string Issuer { get; init; } = "";
        public string Audience { get; init; } = "";

        public int AccessTokenExpirationMin { get; set; } = 60;
        public int AccessTokenClockSkewMin { get; set; } = 4;

    }
    public record JwtResult
    {
        public string? Token { get; init; }
        public DateTime? ExpireAt { get; init; }
    }
}
