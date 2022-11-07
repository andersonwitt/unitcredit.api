using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.DTOs;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Domain.Repository;
using Domain.Services;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        public async Task<UserDTO> GetByLogin(LoginPayloadDTO payload)
        {
            var result = await _userRepository.SelectByLogin(payload);

            return _mapper.Map<UserDTO>(result);
        }

        public async Task<List<UserDTO>> Get()
        {
            var result = await _userRepository.SelectAsync();

            return _mapper.Map<List<UserDTO>>(result);
        }

        public async Task<UserDTO> Get(Guid id)
        {
            var result = await _userRepository.SelectAsync(id);

            return _mapper.Map<UserDTO>(result);
        }

        public async Task<UserDTO?> Post(UserCompleteDTO payload)
        {
            var entity = _mapper.Map<UserEntity>(payload);
            var result = await _userRepository.InsertAsync(entity);

            return _mapper.Map<UserDTO>(result);
        }

        public async Task<UserDTO?> Put(UserCompleteDTO payload)
        {
            var entity = _mapper.Map<UserEntity>(payload);
            var result = await _userRepository.UpdateAsync(entity);

            return _mapper.Map<UserDTO>(result);
        }
    }
}