using System.Security.Claims;
using Api.Domain.DTOs;

namespace Api.Domain.Managers
{
    public interface IClaimsManager
    {
        UserSessionDTO GetUserSession(ClaimsPrincipal claims);
    }
}