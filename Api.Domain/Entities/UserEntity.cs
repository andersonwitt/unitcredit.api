using Api.Domain.Enums;

namespace Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string StudentId { get; set; } = "";
        public decimal Balance { get; set; }
        public EnumUserType Type { get; set; }
    }
}