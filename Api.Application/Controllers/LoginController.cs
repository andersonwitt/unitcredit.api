using Domain.DTOs;
using Domain.Managers;
using Microsoft.AspNetCore.Mvc;

namespace unitcredit.api.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginManager _loginManager;

        public LoginController(ILoginManager loginManager)
        {
            _loginManager = loginManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] LoginPayloadDTO payload)
        {
            var result = await _loginManager.SignIn(payload);

            return Ok(result);
        }
    }
}