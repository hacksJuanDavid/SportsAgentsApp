using System.Text;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
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
    // Add all services, repositories, validators, mappings , authenticate
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();

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

    public static IServiceCollection AddAuthenticate(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure strongly typed settings objects
        var appSettingsSection = configuration.GetSection("AppSettings").GetSection("SecretKey");
        // KeyBytes
        if (appSettingsSection.Value != null)
        {
            var keyBytes = Encoding.ASCII.GetBytes(appSettingsSection.Value);

            // Configure jwt authentication

            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = false;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    // Validate the JWT Issuer (iss) claim
                    ValidateIssuer = false,
                    // Validate the JWT Audience (aud) claim
                    ValidateAudience = false,
                    // Validate the token expiry
                    ValidateLifetime = true,
                    // Validate the token signing key
                    ValidateIssuerSigningKey = true,
                    // Set the token signing key
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
                };
            });
        }

        return services;
    }
}