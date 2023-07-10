using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;

using MyAwesomeShop.Shared.Application.Exceptions;

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
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    public static IApplicationBuilder UseWebApi(this IApplicationBuilder app, IHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseProblemDetails(env);
        app.UseStatusCodePages();

        return app;
    }
}

public static class ProblemDetailsExtensions
{
    public static IApplicationBuilder UseProblemDetails(this IApplicationBuilder app, IHostEnvironment env)
    {
        app.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(async context =>
            {
                var problemDetailsService = context.RequestServices.GetService<IProblemDetailsService>();
                var problemDetailsFactory = context.RequestServices.GetService<ProblemDetailsFactory>();

                if (problemDetailsService == null || problemDetailsFactory == null)
                {
                    return;
                }

                var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                var exception = exceptionHandlerFeature?.Error as MyAwesomeShopException;

                if (exception == null)
                {
                    return;
                }

                var problemDetails = problemDetailsFactory.CreateProblemDetails(
                    context,
                    context.Response.StatusCode,
                    exception.Title,
                    "about:blank",
                    exception.Message);

                if (env.IsDevelopment())
                {
                    problemDetails.Extensions.Add("exception", exception);
                }

                await problemDetailsService.WriteAsync(new ProblemDetailsContext
                {
                    HttpContext = context,
                    ProblemDetails = problemDetails,
                });
            });
        });

        return app;
    }
}

