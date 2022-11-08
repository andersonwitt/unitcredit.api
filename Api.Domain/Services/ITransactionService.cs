using Api.Domain.DTOs;

namespace Api.Domain.Services
{
    public interface ITransactionService
    {
        Task<TransactionDTO> Get(Guid id);
        Task<List<TransactionDTO>> GetByUserId(Guid userId);
        Task<List<TransactionDTO>> Get();
        Task<TransactionDTO> Post(TransactionDTO payload);
        Task<TransactionDTO> Put(TransactionDTO payload);
        Task<bool> Delete(Guid id);
    }
}