using Api.Domain.DTOs;

namespace Api.Domain.Managers
{
    public interface ILoginManager
    {
        Task<LoginResultDTO> SignIn(LoginPayloadDTO payload);
    }
}