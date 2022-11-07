using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.DTOs;
using Domain.DTOs;

namespace Domain.Services
{
    public interface IUserService
    {
        Task<UserDTO> GetByLogin(LoginPayloadDTO payload);
        Task<List<UserDTO>> Get();
        Task<UserDTO> Get(Guid id);
        Task<UserDTO?> Post(UserCompleteDTO payload);
        Task<UserDTO?> Put(UserCompleteDTO payload);
        Task<bool> Delete(Guid id);
    }
}