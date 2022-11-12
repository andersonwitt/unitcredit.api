namespace Api.Domain.DTO
{
    public class BaseResultDTO
    {
        public bool IsValid { get; set; }
        public string Message { get; set; } = "";
    }
}