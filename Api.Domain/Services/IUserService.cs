using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.DTOs;

namespace Api.Domain.Services
{
    public interface IUserService
    {
        Task<UserDTO> GetByLogin(LoginPayloadDTO payload);
    }
}