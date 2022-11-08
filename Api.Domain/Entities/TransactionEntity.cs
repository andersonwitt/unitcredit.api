using System.ComponentModel.DataAnnotations.Schema;
using Api.Domain.Enums;
using Domain.Entities;

namespace Api.Domain.Entities
{
    public class TransactionEntity : BaseEntity
    {
        public string Description { get; set; } = "";
        public decimal Total { get; set; }
        public EnumTransactionType Type { get; set; }
        public Guid FromId { get; set; }
        public Guid ToId { get; set; }

        [ForeignKey("FromId")]
        public UserEntity? From { get; set; }

        [ForeignKey("ToId")]
        public UserEntity? To { get; set; }
    }
}