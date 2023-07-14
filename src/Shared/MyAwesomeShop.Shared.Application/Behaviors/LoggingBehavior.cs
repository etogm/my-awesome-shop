using System.Reflection;

using MediatR;

using Microsoft.Extensions.Logging;

namespace MyAwesomeShop.Shared.Application.Behaviors;

internal sealed class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        //Request
        _logger.LogInformation("{request} | Handling {requestProps}", typeof(TRequest).Name, request);

        var response = await next();

        //Response
        _logger.LogInformation("{request} | Handled {responseProps}", typeof(TRequest).Name, response);

        return response;
    }
}
