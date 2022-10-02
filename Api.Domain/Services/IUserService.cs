using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.DTOs;

namespace Domain.Services
{
    public interface IUserService
    {
        Task<UserDTO> GetByLogin(LoginPayloadDTO payload);
    }
}