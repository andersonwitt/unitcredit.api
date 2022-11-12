using System.Security.Claims;
using Api.Domain.DTOs;
using Api.Domain.Enums;
using Api.Domain.Managers;

namespace Api.Service.Managers
{
    public class ClaimsManager : IClaimsManager
    {
        public UserSessionDTO GetUserSession(ClaimsPrincipal claims)
        {
            var result = new UserSessionDTO();

            result.UserType = (EnumUserType)Convert.ToInt32(claims.FindFirst(c => c.Type == "UserType")?.Value);
            result.UserId = new Guid(claims.FindFirst(c => c.Type == "id")?.Value ?? "");

            return result;
        }
    }
}