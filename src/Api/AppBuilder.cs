using Api.Common;
using Api.Common.Middleware;
using Api.Common.MinimalAPI;
using App.Localization;
using Application.Modules.Authentication.Commands;
using Application.Modules.Authentication.DTOs.Request;
using Domain;
using Domain.Abstraction;
using Domain.Abstraction.Security;
using Domain.Enumerations;
using DomainEvent;
using DomainEvent.Abstraction;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Security;
using Infrastructure.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Reflection;
using System.Text.Json;

namespace Api
{
    internal static class AppBuilder
    {
        internal static void BuildProject(this WebApplicationBuilder builder)
        {
            AppSettings appSettings = builder.Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>()
                ?? throw new Exception(nameof(AuthenticationConfig));

            builder.Services.AddSingleton(appSettings);
            builder.Services.AddSingleton<IDomainEventDispatcher, MediatREventDispatcher>();
            builder.Services.AddDbContext<Database>((serviceProvider, options) =>
            {
                var connectionString = builder.Configuration.GetConnectionString(nameof(Database));
                options.UseSqlServer(connectionString).ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
            });

            builder.Services.AddScoped<DbContext>(sp => sp.GetRequiredService<Database>());

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(SignInCommand).Assembly);
            });

            builder.Services.AddValidatorsFromAssemblyContaining<SignInRequestDTO>();

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();

            builder.Services.AddSingleton<IAuthenticationManager, AuthenticationManager>();
            builder.Services.AddSingleton<IHttpContextService, HttpContextService>();
            builder.Services.AddSingleton<IPasswordGenerator, PasswordGenerator>();
            builder.Services.AddScoped<IUserManager, UserManager>();


            string croatianCulture = CultureExtensions.AsString(CultureType.Croatian);
            string englishCulture = CultureExtensions.AsString(CultureType.English);

            builder.Services.AddLocalizator(options =>
            {
                options.ReturnKey = true;
                options.ThrowIfNotPresent = false;
                options.DefaultUICulture = croatianCulture;
                options.SupportedUICultures = [croatianCulture, englishCulture];
                options.FolderName = "Localization";
                options.TTLDays = 1;
            });

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>
            {
                new(croatianCulture),
                new(englishCulture),
            };
                options.DefaultRequestCulture = new RequestCulture(culture: croatianCulture, uiCulture: croatianCulture);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
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
            }).AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(EnvironmentConstants.DEVELOPMENT, builder =>
                {
                    builder.SetIsOriginAllowed(_ => true)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            builder.Services.Configure<JsonOptions>(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.WriteIndented = true;
            });

            builder.Services
                     .AddEndpointsApiExplorer()
                     .AddRouting(options => options.LowercaseUrls = true)
                     .AddHttpContextAccessor();
        }

        internal static void BuildApp(this WebApplication app)
        {
            app.UseMiddleware<CultureMiddleware>();
            app.UseMiddleware<AuthenticationMiddleware>();

            app.LoadEndpointModules(Assembly.GetExecutingAssembly());

            if (app.Environment.IsDevelopment())
            {
                app.UseCors(EnvironmentConstants.DEVELOPMENT);
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); });
            }

            app.UseHttpsRedirection();

            app.UseExceptionHandler(exceptionHandlerApp => exceptionHandlerApp.Run(async context =>
            {
                var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                var logger = context.RequestServices.GetService<ILogger<Program>>() ?? throw new Exception("Logger is null.");
                await ExceptionHandler.HandleExceptionAsync(context, exception, logger);
            }));
        }
    }
}
