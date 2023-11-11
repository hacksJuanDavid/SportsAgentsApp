using AutoMapper;
using SportsAgentsUserService.Api.Dto;
using SportsAgentsUserService.Domain.Entities;

namespace SportsAgentsUserService.Api.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
    }
}