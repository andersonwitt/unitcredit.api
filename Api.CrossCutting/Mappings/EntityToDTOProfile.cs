using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.DTOs;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;

namespace CrossCutting.Mappings
{
    public class EntityToDTOProfile : Profile
    {
        public EntityToDTOProfile()
        {
            CreateMap<UserDTO, UserEntity>()
                .ReverseMap();

            CreateMap<UserCompleteDTO, UserEntity>()
                .ReverseMap();

            CreateMap<UserDTO, UserCompleteDTO>()
                .ReverseMap();
        }
    }
}