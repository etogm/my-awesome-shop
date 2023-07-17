using Hellang.Middleware.ProblemDetails.Mvc;

using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MyAwesomeShop.Shared.WebApi;

public static class WebApiExtensions
{
    public static void AddInternalDefaultPolicy(this CorsOptions options, string gatewayUri)
    {
        options.AddDefaultPolicy(policy => policy.WithOrigins(gatewayUri).AllowAnyHeader().AllowAnyMethod());
    }

    public static IServiceCollection AddWebApi(this IServiceCollection services, IHostEnvironment env)
    {
        services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
        });
        services.AddCustomProblemDetails(env);
        services.AddControllers(options =>
        {
            options.SuppressAsyncSuffixInActionNames = false;
        }).AddProblemDetailsConventions();

        return services;
    }
}
