using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.DTOs;
using Domain.Managers;
using Domain.Services;

namespace Service.Managers
{
    public class LoginManager : ILoginManager
    {
        private IUserService _userService;

        public LoginManager(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<LoginResultDTO> SignIn(LoginPayloadDTO payload)
        {
            var result = new LoginResultDTO()
            {
                IsAuthenticated = false,
            };

            var user = await _userService.GetByLogin(payload);

            if (user != null)
            {
                result.IsAuthenticated = true;
            }

            return result;
        }
    }
}