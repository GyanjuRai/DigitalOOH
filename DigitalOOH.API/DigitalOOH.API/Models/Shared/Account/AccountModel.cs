using System.ComponentModel.DataAnnotations;

namespace DigitalOOH.API.Models.Shared.Account
{
    public record UserLoginParam
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
    public record UserInfoResponse
    {
        public required Guid Id { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? Token { get; set; }
        public DateTime? ExpireAt { get; set; }
    }

}
