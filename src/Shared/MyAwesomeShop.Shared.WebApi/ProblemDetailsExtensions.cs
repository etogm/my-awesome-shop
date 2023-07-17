using FluentValidation;

using Hellang.Middleware.ProblemDetails;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MyAwesomeShop.Shared.WebApi;

public static class ProblemDetailsExtensions
{
    public static IServiceCollection AddCustomProblemDetails(this IServiceCollection services, IHostEnvironment env)
    {
        services.AddProblemDetails(options =>
        {
            options.IncludeExceptionDetails = (ctx, ex) => env.IsDevelopment();
            options.MapFluentValidationException();
            options.Rethrow<NotSupportedException>();
            options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);
            options.MapToStatusCode<HttpRequestException>(StatusCodes.Status503ServiceUnavailable);
            options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
        });

        return services;
    }

}

public static class ProblemDetailsOptionsExtensions
{
    public static void MapFluentValidationException(this Hellang.Middleware.ProblemDetails.ProblemDetailsOptions options) =>
        options.Map<ValidationException>((ctx, ex) =>
        {
            var factory = ctx.RequestServices.GetRequiredService<ProblemDetailsFactory>();

            var errors = ex.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    x => x.Key,
                    x => x.Select(x => x.ErrorMessage).ToArray());

            return factory.CreateValidationProblemDetails(ctx, errors);
        });
}