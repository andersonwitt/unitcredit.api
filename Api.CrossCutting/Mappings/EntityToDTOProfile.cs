using Api.Domain.DTOs;
using Api.Domain.Entities;
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

            CreateMap<TransactionDTO, TransactionEntity>()
                .ReverseMap();
        }
    }
}