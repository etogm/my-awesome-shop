using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

using AspNet.Security.OAuth.GitHub;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using MyAwesomeShop.Catalog.Application.Abstractions;
using MyAwesomeShop.Catalog.Infrastructure.Data;

namespace MyAwesomeShop.Catalog.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddCatalogInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CatalogContext>(options =>
        {
            options
                .UseNpgsql(configuration.GetConnectionString("CatalogDB"))
                .UseSnakeCaseNamingConvention();
        });
        services.AddScoped<ICatalogContext>(provider => provider.GetRequiredService<CatalogContext>());

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.IncludeErrorDetails = true;
                options.Events = new JwtBearerEvents()
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        // Ensure we always have an error and error description.
                        if (string.IsNullOrEmpty(context.Error))
                            context.Error = "invalid_token";
                        if (string.IsNullOrEmpty(context.ErrorDescription))
                            context.ErrorDescription = "This request requires a valid JWT access token to be provided";

                        // Add some extra context for expired tokens.
                        if (context.AuthenticateFailure != null && context.AuthenticateFailure.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            var authenticationException = context.AuthenticateFailure as SecurityTokenExpiredException;
                            context.Response.Headers.Add("x-token-expired", authenticationException.Expires.ToString("o"));
                            context.ErrorDescription = $"The token expired on {authenticationException.Expires.ToString("o")}";
                        }

                        return context.Response.WriteAsync(JsonSerializer.Serialize(new
                        {
                            error = context.Error,
                            error_description = context.ErrorDescription
                        }));
                    }
                };
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://github.com",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:ClientSecret"]!))
                };
            });
        //.AddGitHub(options =>
        //{
        //    options.ClientId = "4859a02eb577a7db4e4b";
        //    options.ClientSecret = "50606605031cad9bee3e110b0d0d59ed347df778";
        //    options.SignInScheme = JwtBearerDefaults.AuthenticationScheme;
        //    options.SaveTokens = true;
        //});

        return services;
    }
    //public static Task ValidateToken(MessageReceivedContext context)
    //{
    //    try
    //    {
    //        context.Token = GetToken(context.Request);

    //        var tokenHandler = new JwtSecurityTokenHandler();
    //        tokenHandler.ValidateToken(context.Token, context.Options.TokenValidationParameters, out var validatedToken);

    //        var jwtSecurityToken = validatedToken as JwtSecurityToken;

    //        context.Principal = new ClaimsPrincipal();

    //        var claimsIdentity = new ClaimsIdentity(jwtSecurityToken.Claims.ToList(),
    //                "JwtBearerToken", ClaimTypes.NameIdentifier, ClaimTypes.Role);
    //        context.Principal.AddIdentity(claimsIdentity);

    //        context.Success();

    //        return Task.CompletedTask;
    //    }
    //    catch (Exception e)
    //    {
    //        context.Fail(e);
    //    }

    //    return Task.CompletedTask;
    //}

    public static IApplicationBuilder UseCatalogInfrastructure(this IApplicationBuilder app)
    {
        app.UseAuthentication();

        return app;
    }
}
