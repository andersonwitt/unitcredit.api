using Api.Domain.Enums;
using Domain.DTOs;

namespace Api.Domain.DTOs
{
    public class TransactionDTO: BaseDTO
    {
        public string Description { get; set; } = "";
        public decimal Total { get; set; }
        public EnumTransactionType Type { get; set; }
        public Guid FromId { get; set; }
        public Guid ToId { get; set; }
        public UserDTO? From { get; set; }
        public UserDTO? To { get; set; }
    }
}