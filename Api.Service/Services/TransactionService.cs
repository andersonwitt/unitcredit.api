using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.DTOs;
using Api.Domain.Entities;
using Api.Domain.Repository;
using Api.Domain.Services;
using AutoMapper;

namespace Api.Service.Services
{
    public class TransactionService : ITransactionService
    {
        private IMapper _mapper;
        private ITransactionRepository _repository;

        public TransactionService(ITransactionRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<TransactionDTO> Get(Guid id)
        {
            var entity = await _repository.SelectAsync(id);

            return _mapper.Map<TransactionDTO>(entity);
        }

        public async Task<List<TransactionDTO>> Get()
        {
            var entity = await _repository.SelectAsync();

            return _mapper.Map<List<TransactionDTO>>(entity);
        }

        public async Task<TransactionDTO> Post(TransactionDTO payload)
        {
            var entity = _mapper.Map<TransactionEntity>(payload);

            var result = await _repository.InsertAsync(entity);

            return _mapper.Map<TransactionDTO>(result);
        }

        public async Task<TransactionDTO> Put(TransactionDTO payload)
        {
            var entity = _mapper.Map<TransactionEntity>(payload);

            var result = await _repository.UpdateAsync(entity);

            return _mapper.Map<TransactionDTO>(result);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<List<TransactionDTO>> GetByUserId(Guid userId)
        {
            var entity = await _repository.SelectByUserId(userId);

            return _mapper.Map<List<TransactionDTO>>(entity);
        }
    }
}