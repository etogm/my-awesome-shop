using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using MyAwesomeShop.Shared.Security.Data;
using MyAwesomeShop.Shared.Security.Domain;

namespace MyAwesomeShop.Shared.Security;

public static class SecurityExtensions
{
    public static IMvcBuilder AddSecurityControllers(this IMvcBuilder builder)
    {
        builder.AddApplicationPart(typeof(SecurityExtensions).Assembly);

        return builder;
    }

    public static IServiceCollection AddSecurity<TContext>(this IServiceCollection services, IConfigurationSection jwtSection) where TContext : SecurityDbContext
    {
        services.AddOptions();

        services
            .AddIdentityCore<User>()
            .AddRoles<Role>()
            .AddEntityFrameworkStores<TContext>();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 3;
            options.Password.RequiredUniqueChars = 1;
        });

        services.Configure<SecurityOptions>(jwtSection);

        var jwtOptions = jwtSection.Get<SecurityOptions>();

        if (jwtOptions == null)
            throw new ArgumentNullException(nameof(jwtSection));

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    ClockSkew = jwtOptions.Expiry,
                    IssuerSigningKey = jwtOptions.GetSigningKey()
                };
            });

        services.AddAuthorization();
        
        return services;
    }

    public static void UseSecurity(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}
