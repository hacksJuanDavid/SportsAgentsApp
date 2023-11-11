using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using SportsAgentsUserService.Api.Dto;
using SportsAgentsUserService.Api.Mappers;
using SportsAgentsUserService.Api.Validators;
using SportsAgentsUserService.Application.Interfaces;
using SportsAgentsUserService.Application.Services;
using SportsAgentsUserService.Domain.Interfaces.Repositories;
using SportsAgentsUserService.Infrastructure.Repositories;

namespace SportsAgentsUserService.Api.Extensions;

public static class ModuleExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
        services.AddScoped<IValidator<UserDto>, UserValidator>();

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