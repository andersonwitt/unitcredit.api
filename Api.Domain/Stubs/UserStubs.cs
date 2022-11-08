using Api.Domain.DTOs;
using Domain.DTOs;
using Domain.Entities;

namespace Domain.Stubs
{
    public class UserStubs
    {
        public static UserEntity GetUserEntity() => new UserEntity
        {
            Id = Guid.NewGuid(),
            Name = "A beautiful name",
            Email = "lalaland@lala.com",
            Password = "123456",
            CreateAt = DateTime.UtcNow,
            StudentId = new Random().Next(1000, 9999).ToString(),
            UpdateAt = DateTime.UtcNow,
            Type = Api.Domain.Enums.EnumUserType.ADMIN,
            Balance = 15m,
        };

        public static UserDTO GetUserDTO() => new UserDTO
        {
            Id = Guid.NewGuid(),
            Name = "A beautiful name",
            Email = "lalaland@lala.com",
            CreateAt = DateTime.UtcNow,
            StudentId = new Random().Next(1000, 9999).ToString(),
            UpdateAt = DateTime.UtcNow,
            Type = Api.Domain.Enums.EnumUserType.ADMIN,
        };

        public static UserCompleteDTO GetUserCompleteDTO() => new UserCompleteDTO()
        {
            Id = Guid.NewGuid(),
            Name = "A beautiful name",
            Email = "lalaland@lala.com",
            CreateAt = DateTime.UtcNow,
            StudentId = new Random().Next(1000, 9999).ToString(),
            UpdateAt = DateTime.UtcNow,
            Type = Api.Domain.Enums.EnumUserType.ADMIN,
            Password = "123456",
            Balance = 15m,
        };

    }
}