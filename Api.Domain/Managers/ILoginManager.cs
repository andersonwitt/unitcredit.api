using Domain.DTOs;

namespace Domain.Managers
{
    public interface ILoginManager
    {
        Task<LoginResultDTO> SignIn(LoginPayloadDTO payload);
    }
}