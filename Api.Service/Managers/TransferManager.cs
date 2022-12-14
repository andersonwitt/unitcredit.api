using Api.Domain.DTO;
using Api.Domain.DTOs;
using Api.Domain.Managers;
using Api.Domain.Services;
using Domain.Services;

namespace Api.Service.Managers
{
    public class TransferManager : ITransferManager
    {
        private readonly ITransactionService _transactionService;
        private readonly IUserService _userService;
        public TransferManager(ITransactionService transactionService, IUserService userService)
        {
            _transactionService = transactionService;
            _userService = userService;
        }


        public async Task<BaseResultDTO> TransferToUser(TransferToUserPayloadDTO payload)
        {
            var result = new BaseResultDTO()
            {
                IsValid = false,
                Message = "",
            };

            try
            {
                var from = await _userService.Get(payload.FromId);
                var to = await _userService.Get(payload.ToId);
                bool isNotAdminUser = from.Type != Domain.Enums.EnumUserType.ADMIN;

                if (isNotAdminUser)
                {
                    if (from.Balance < payload.Total)
                    {
                        throw new Exception("Saldo insuficiente!");
                    }

                    from.Balance -= payload.Total;
                }

                await _transactionService.Post(new TransactionDTO()
                {
                    FromId = from.Id,
                    ToId = to.Id,
                    Type = payload.TransactionType,
                    Total = payload.Total,
                });

                if (isNotAdminUser)
                {
                    await _userService.Put(from);
                }

                result.IsValid = true;
            }
            catch (Exception ex)
            {
                if (ex.Source == "Service")
                {
                    result.Message = ex.Message;
                }
                else
                {
                    result.Message = "Erro ao fazer transferĂȘncia.";
                }

                result.IsValid = false;
                Console.WriteLine(ex.Message);
            }

            return result;
        }
    }
}