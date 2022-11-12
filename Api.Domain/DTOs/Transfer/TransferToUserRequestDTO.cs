namespace Api.Domain.DTOs
{
    public class TransferToUserRequestDTO
    {
        public decimal Total { get; set; }
        public Guid ToId { get; set; }
    }
}