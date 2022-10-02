using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.DTOs;
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

        public async Task<UserDTO> GetByLogin(LoginPayloadDTO payload)
        {
            var result = await _userRepository.SelectByLogin(payload);

            return _mapper.Map<UserDTO>(result);
        }
    }
}