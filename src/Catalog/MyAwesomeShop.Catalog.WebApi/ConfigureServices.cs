using MyAwesomeShop.Shared.WebApi;

namespace MyAwesomeShop.Catalog.WebApi;

public static class ConfigureServices
{
    public static IServiceCollection AddCatalogWebApi(this IServiceCollection services)
    {
        services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
        });
        services.AddControllers(options =>
        {
            options.SuppressAsyncSuffixInActionNames = false;
        });
        services.AddProblemDetails();
        services.AddWebApiSwagger();

        return services;
    }

    public static IApplicationBuilder UseCatalogWebApi(this IApplicationBuilder app, IHostEnvironment env)
    {
        app.UseWebApiSwagger();

        app.UseExceptionHandler();
        app.UseStatusCodePages();

        return app;
    }
}
