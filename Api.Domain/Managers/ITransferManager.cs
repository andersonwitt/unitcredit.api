using Api.Domain.DTO;
using Api.Domain.DTOs;

namespace Api.Domain.Managers
{
    public interface ITransferManager
    {
        Task<BaseResultDTO> TransferToUser(TransferToUserPayloadDTO payload);
    }
}