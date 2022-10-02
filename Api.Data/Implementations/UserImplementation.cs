using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Context;
using Data.Repository;
using Domain.DTOs;
using Domain.Entities;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Data.Implementations
{
    public class UserImplementation : BaseRepository<UserEntity>, IUserRepository
    {
        private DbSet<UserEntity> _dataset;
        public UserImplementation(UnitContext context) : base(context)
        {
            _dataset = context.Set<UserEntity>();
        }

        public async Task<UserEntity> SelectByLogin(LoginPayloadDTO payload) =>
             await _dataset
                .FirstOrDefaultAsync(user =>
                                        user.StudentId == payload.StudentId &&
                                        user.Password == payload.Password);
    }
}