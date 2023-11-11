using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using SportsAgentsContactService.Api.Dto;
using SportsAgentsContactService.Api.Mappers;
using SportsAgentsContactService.Api.Validators;
using SportsAgentsContactService.Application.Interfaces;
using SportsAgentsContactService.Application.Services;
using SportsAgentsContactService.Domain.Interfaces.Repositories;
using SportsAgentsContactService.Infrastructure.Repositories;

namespace SportsAgentsContactService.Api.Extensions;

public static class ModuleExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IContactService,ContactService>();
        return services;
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IContactRepository,ContactRepository>();
        return services;
    }
    
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
        services.AddScoped<IValidator<ContactDto>, ContactValidator>();
        return services;
    }
    
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        // Auto Mapper Configurations
        var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }
    
}