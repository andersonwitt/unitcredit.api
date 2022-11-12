using Api.Domain.Enums;

namespace Api.Domain.DTOs
{
    public class TransferToUserPayloadDTO : TransferToUserRequestDTO
    {
        public Guid FromId { get; set; }
        public EnumTransactionType TransactionType { get; set; }
    }
}