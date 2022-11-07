using Api.Domain.DTOs;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userService.Get();

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}", Name = "GetWithId")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var result = await _userService.Get(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserCompleteDTO payload)
        {
            var result = await _userService.Post(payload);

            if (result == null) return BadRequest();

            return Created(new Uri(Url.Link("GetWithId", new { id = result.Id })), result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserCompleteDTO payload)
        {
            var result = await _userService.Put(payload);

            if (result == null) return BadRequest();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userService.Delete(id);

            return Ok(result);
        }
    }
}