using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Lab10_AntonyHuayhua.Api.Configuration;
using Lab10_AntonyHuayhua.Application.Port.Services;
using Lab10_AntonyHuayhua.Application.UseCases.Response;
using Lab10_AntonyHuayhua.Application.UseCases.Role;
using Lab10_AntonyHuayhua.Application.UseCases.Ticket;
using Lab10_AntonyHuayhua.Application.UseCases.User;
using Lab10_AntonyHuayhua.Application.UseCases.UserRole;
using Lab10_AntonyHuayhua.Infrastructure.Configuration;

namespace Lab10_AntonyHuayhua.Api.Configuration;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
         
        services.AddControllers();
        services.AddInfrastructureServices(configuration);
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUser, CurrentUser>();

        services.AddScoped<CreateUserUseCase>();
        services.AddScoped<GetUserUseCase>();
        services.AddScoped<ListUsersUseCase>();
        services.AddScoped<UpdateUserUseCase>();
        services.AddScoped<DeleteUserUseCase>();
        services.AddScoped<LoginUseCase>();

        services.AddScoped<CreateRoleUseCase>();
        services.AddScoped<GetRoleUseCase>();
        services.AddScoped<ListRolesUseCase>();
        services.AddScoped<UpdateRoleUseCase>();
        services.AddScoped<DeleteRoleUseCase>();

        services.AddScoped<AssignUserRoleUseCase>();
        services.AddScoped<RemoveUserRoleUseCase>();
        services.AddScoped<GetRoleAssignmentUseCase>();
        services.AddScoped<ListRoleAssignmentsUseCase>();

        services.AddScoped<CreateTicketUseCase>();
        services.AddScoped<GetTicketUseCase>();
        services.AddScoped<ListTicketsUseCase>();
        services.AddScoped<UpdateTicketUseCase>();
        services.AddScoped<DeleteTicketUseCase>();
        services.AddScoped<ChangeTicketStatusUseCase>();

        services.AddScoped<CreateResponseUseCase>();
        services.AddScoped<GetResponseUseCase>();
        services.AddScoped<ListResponsesByTicketUseCase>();
        services.AddScoped<UpdateResponseUseCase>();
        services.AddScoped<DeleteResponseUseCase>();

        var jwt = configuration.GetSection("Jwt").Get<JwtSettings>()
            ?? throw new InvalidOperationException("Falta la configuraci?n Jwt.");
        var secret = jwt.Secret;
        var issuer = jwt.Issuer;
        var audience = jwt.Audience;

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateLifetime = true
            };
        });

        services.AddAuthorization();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Lab10-AntonyHuayhua - Ticketera API",
                Version = "v1",
                Description = "API de tickets con arquitectura hexagonal, PostgreSQL y JWT."
            });
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Ingresa tu token JWT en formato Bearer"
            };

            c.AddSecurityDefinition("Bearer", securityScheme);
            c.AddSecurityRequirement(doc => new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecuritySchemeReference("Bearer", doc),
                    new List<string>()
                }
            });
        });

        return services;
    }
}
