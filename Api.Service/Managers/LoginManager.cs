using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Domain.DTOs;
using Domain.Managers;
using Domain.Services;
using Microsoft.IdentityModel.Tokens;

namespace Service.Managers
{
    public class LoginManager : ILoginManager
    {
        private IUserService _userService;

        public LoginManager(IUserService userService)
        {
            _userService = userService;
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("Key")));

            var signing = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = Environment.GetEnvironmentVariable("Issuer"),
                Audience = Environment.GetEnvironmentVariable("Audience"),
                SigningCredentials = signing,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private LoginResultDTO SuccessObject(DateTime createDate, DateTime expirationDate, string token, UserDTO user)
        {
            return new LoginResultDTO()
            {
                IsAuthenticated = true,
                AuthorizationType = "Bearer",
                CreateDate = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                ExpirationDate = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = token,
                UserName = user.StudentId,
                Name = user.Name,
                Message = "Usuário logado com sucesso"
            };
        }

        public async Task<LoginResultDTO> SignIn(LoginPayloadDTO payload)
        {
            if (payload != null && !string.IsNullOrWhiteSpace(payload.StudentId))
            {
                var user = await _userService.GetByLogin(payload);

                if (user == null)
                {
                    return new LoginResultDTO()
                    {
                        IsAuthenticated = false,
                        Message = "Falha ao autenticar",
                    };
                }
                else
                {
                    var identity = new ClaimsIdentity(
                        new GenericIdentity(user.StudentId),
                        new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.StudentId),
                            new Claim("Name", user.Name),
                            new Claim("UserType", ((int)user.Type).ToString()),
                            new Claim("id", user.Id.ToString()),
                        }
                    );

                    var createDate = DateTime.Now;
                    var expirationDate = createDate + TimeSpan.FromSeconds(Convert.ToInt32(Environment.GetEnvironmentVariable("Seconds")));

                    var handler = new JwtSecurityTokenHandler();
                    string token = CreateToken(identity, createDate, expirationDate, handler);
                    return SuccessObject(createDate, expirationDate, token, user);
                }
            }
            else
            {
                return new LoginResultDTO()
                {
                    IsAuthenticated = false,
                    Message = "Falha ao autenticar"
                };
            }
        }
    }
}