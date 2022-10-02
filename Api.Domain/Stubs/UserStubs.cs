using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        };

        public static UserDTO GetUserDTO() => new UserDTO
        {
            Id = Guid.NewGuid(),
            Name = "A beautiful name",
            Email = "lalaland@lala.com",
            CreateAt = DateTime.UtcNow,
            StudentId = new Random().Next(1000, 9999).ToString(),
            UpdateAt = DateTime.UtcNow,
        };

    }
}