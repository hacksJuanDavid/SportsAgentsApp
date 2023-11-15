using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SportsAgentsUserService.Api.Extensions;
using SportsAgentsUserService.Api.Middleware;
using SportsAgentsUserService.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

// Add the DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("CnnStr")!);
});

// Add services to the container.
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errorDetails = context.ConstructErrorMessages();
        return new BadRequestObjectResult(errorDetails);
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
      {

          s.SwaggerDoc("v1", new OpenApiInfo { Title = " User API", Version = "v1" });

          s.AddSecurityDefinition("Bearer",

            new OpenApiSecurityScheme

            {

                Scheme = "Bearer",

                In = ParameterLocation.Header,

                Description = "Please enter into field the word 'Bearer' following by space and JWT",

                Name = "Authorization",

                Type = SecuritySchemeType.ApiKey

            });

          s.AddSecurityRequirement(new OpenApiSecurityRequirement

        {

          {

            new OpenApiSecurityScheme

            {

              Reference = new OpenApiReference

              {

                Type = ReferenceType.SecurityScheme,

                Id = "Bearer"

              }

            },

            Array.Empty<string>()

            }

          });

      });

// Add Modules
builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddValidators();
builder.Services.AddMappings();
builder.Services.AddAuthenticate(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add the Exception Middleware 
app.UseExceptionMiddleware();

app.UseHttpsRedirection();

app.UseCors(corsPolicyBuilder => corsPolicyBuilder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();