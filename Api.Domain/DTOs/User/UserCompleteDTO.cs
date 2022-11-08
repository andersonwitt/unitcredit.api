using Api.Domain.Enums;
using Domain.DTOs;

namespace Api.Domain.DTOs
{
    public class UserCompleteDTO : BaseDTO
    {
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string StudentId { get; set; } = "";
        public string Password { get; set; } = "";
        public decimal Balance { get; set; }
        public EnumUserType Type { get; set; }
    }
}