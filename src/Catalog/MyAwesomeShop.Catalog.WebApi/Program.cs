using MyAwesomeShop.Catalog.Application;
using MyAwesomeShop.Catalog.Infrastructure;
using MyAwesomeShop.Catalog.WebApi;

using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    builder.Services.AddCatalogInfrastructure(builder.Configuration);
    builder.Services.AddCatalogApplication();
    builder.Services.AddCatalogWebApi();

    var app = builder.Build();

    app.UseCatalogInfrastructure();
    app.UseWebApi(builder.Environment);
    app.MapControllers();

    app.Run();
}
catch (HostAbortedException)
{
    Log.Information("Host aborted");
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
