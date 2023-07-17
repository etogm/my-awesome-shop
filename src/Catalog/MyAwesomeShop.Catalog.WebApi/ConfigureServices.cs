using Hellang.Middleware.ProblemDetails;
using Hellang.Middleware.ProblemDetails.Mvc;

using MyAwesomeShop.Shared.WebApi;

namespace MyAwesomeShop.Catalog.WebApi;

public static class ConfigureServices
{
    public static IServiceCollection AddCatalogWebApi(this IServiceCollection services, IHostEnvironment env)
    {
        services.AddCors();

        services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
        });
        services.AddCustomProblemDetails(env);
        services.AddControllers(options =>
        {
            options.SuppressAsyncSuffixInActionNames = false;
        }).AddProblemDetailsConventions();
        services.AddWebApiSwagger();

        return services;
    }
}
