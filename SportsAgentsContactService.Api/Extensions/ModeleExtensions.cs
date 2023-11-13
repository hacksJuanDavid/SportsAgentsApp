using System.Text;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
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
    // Add all the services, repositories, validators, mappings and ValidationJwtTokenService 
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IValidationJwtTokenService, ValidationJwtTokenService>();
        services.AddScoped<IContactService, ContactService>();
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IContactRepository, ContactRepository>();
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