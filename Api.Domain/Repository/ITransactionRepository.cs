using Api.Domain.Entities;
using Domain.Repository;

namespace Api.Domain.Repository
{
    public interface ITransactionRepository : IRepository<TransactionEntity>
    {
        Task<List<TransactionEntity>> SelectByUserId(Guid userId);
    }
}