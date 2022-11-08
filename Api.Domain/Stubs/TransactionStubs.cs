using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.DTOs;
using Api.Domain.Entities;

namespace Api.Domain.Stubs
{
    public class TransactionStubs
    {
        public static TransactionEntity GetTransactionEntity() => new TransactionEntity
        {
            Id = Guid.NewGuid(),
            Description = "A beautiful name",
            CreateAt = DateTime.UtcNow,
            UpdateAt = DateTime.UtcNow,
            Type = Enums.EnumTransactionType.Credit,
            FromId = Guid.NewGuid(),
            ToId = Guid.NewGuid(),
            Total = 135m,
        };

        public static TransactionDTO GetTransactionDTO() => new TransactionDTO
        {
            Id = Guid.NewGuid(),
            Description = "A beautiful name",
            CreateAt = DateTime.UtcNow,
            UpdateAt = DateTime.UtcNow,
            Type = Enums.EnumTransactionType.Credit,
            FromId = Guid.NewGuid(),
            ToId = Guid.NewGuid(),
            Total = 135m,
        };

    }
}