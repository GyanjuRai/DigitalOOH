using DigitalOOH.API.Models.Shared.Enum;

namespace DigitalOOH.API.Models.Shared.Response
{
    public record ResponseModel<T>
    {
        public required string Message { get; set; }
        public required string Type { get; set; }
        public T? Data { get; set; }
    }

    public record GridResponse<T>
    {
        public List<T>? Data { get; set; }
        public int TotalRows { get; set; }
    }
}
