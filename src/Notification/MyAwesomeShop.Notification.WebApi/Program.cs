using MyAwesomeShop.Shared.WebApi;
using MyAwesomeShop.Shared.Infrastructure.EventBus;
using MyAwesomeShop.Catalog.Application.IntegrationEvents;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseCustomLogger();

builder.Services.AddCors();

builder.Services.AddEventBus(options =>
{
    options.Configuration = builder.Configuration["RedisOptions:Connection"]!;
});

builder.Services.AddIntegrationEventHandler<ProductUpdatedIntegrationEvent, ProductUpdatedHandler>();

builder.Services.AddSignalR();

var app = builder.Build();

app.UseCors(policy => policy.AllowAnyOrigin());

app.MapHub<ProductHub>("/producthub");
app.Run();
