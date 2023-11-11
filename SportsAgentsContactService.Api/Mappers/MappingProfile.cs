using AutoMapper;
using SportsAgentsContactService.Api.Dto;
using SportsAgentsContactService.Domain.Entities;

namespace SportsAgentsContactService.Api.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Contact, ContactDto>();
        CreateMap<ContactDto, Contact>();
    }
}