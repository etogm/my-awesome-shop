using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MyAwesomeShop.Shared.WebApi;

public static class SwaggerExtensions
{
    public static IServiceCollection AddWebApiSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    public static IApplicationBuilder UseWebApiSwagger(this IApplicationBuilder app)
    {
        app.UseWebApiSwagger();
        app.UseSwaggerUI();

        return app;
    }
}
