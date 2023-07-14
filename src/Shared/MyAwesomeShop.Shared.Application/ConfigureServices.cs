using FluentValidation;

using Mapster;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using MyAwesomeShop.Shared.Application.Behaviors;

namespace MyAwesomeShop.Shared.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings
            .NewConfig<string, Guid>()
            .Map(dest => dest, src => Guid.Parse(src));

        TypeAdapterConfig.GlobalSettings
            .NewConfig<string, Guid?>()
            .Map(dest => dest, src => Guid.Parse(src));

        TypeAdapterConfig.GlobalSettings
            .NewConfig<Guid, string?>()
            .Map(dest => dest, src => src.ToString());

        TypeAdapterConfig.GlobalSettings
            .NewConfig<Guid?, string?>()
            .Map(dest => dest, src => src.ToString());

        return services;
    }

    public static IServiceCollection AddMessaging(this IServiceCollection services, Type type)
    {
        services.AddValidatorsFromAssemblyContaining(type);

        services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(type.Assembly))
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        return services;
    }
}
