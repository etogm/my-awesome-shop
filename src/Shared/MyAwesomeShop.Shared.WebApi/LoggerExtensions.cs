using Microsoft.Extensions.Hosting;

using Serilog;

namespace MyAwesomeShop.Shared.WebApi;

public static class LoggerExtensions
{
    public static IHostBuilder UseCustomLogger(this IHostBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(Environment.CurrentDirectory + "/Logs/log-", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        return builder.UseSerilog();
    }
}
