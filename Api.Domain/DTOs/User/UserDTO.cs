using Api.Domain.Enums;

namespace Domain.DTOs
{
    public class UserDTO : BaseDTO
    {
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string StudentId { get; set; } = "";
        public EnumUserType Type { get; set; }
    }
}