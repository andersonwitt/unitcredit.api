using Api.Domain.Entities;
using Api.Domain.Repository;
using Data.Context;
using Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations
{
    public class TransactionImplementation : BaseRepository<TransactionEntity>, ITransactionRepository
    {
        private DbSet<TransactionEntity> _dataset;
        public TransactionImplementation(UnitContext context) : base(context)
        {
            _dataset = context.Set<TransactionEntity>();
        }

        public async Task<List<TransactionEntity>> SelectByUserId(Guid userId)
        {
            return await _dataset
                .Where(t => t.FromId == userId ||
                            t.ToId == userId)
                .ToListAsync();
        }
    }
}