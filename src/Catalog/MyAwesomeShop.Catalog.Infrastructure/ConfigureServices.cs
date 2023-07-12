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
using MyAwesomeShop.Shared.Infrastructure.PubSub;

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

        services.AddPubSub(options =>
        {
            options.Configuration = "localhost:6379";
        });

        return services;
    }

    public static IApplicationBuilder UseCatalogInfrastructure(this IApplicationBuilder app)
    {
        return app;
    }
}
