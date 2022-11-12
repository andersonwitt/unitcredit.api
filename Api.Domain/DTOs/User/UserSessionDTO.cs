using Api.Domain.Enums;

namespace Api.Domain.DTOs
{
    public class UserSessionDTO
    {
        public Guid UserId { get; set; }
        public EnumUserType UserType { get; set; }
        public bool IsAdmin { get { return this.UserType == EnumUserType.ADMIN; } }
        public bool IsCommerce { get { return this.UserType == EnumUserType.COMMERCE; } }
        public bool IsTeacher { get { return this.UserType == EnumUserType.TEACHER; } }
        public bool IsStudent { get { return this.UserType == EnumUserType.STUDENT; } }
    }
}