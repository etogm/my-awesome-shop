using MyAwesomeShop.Shared.WebApi;
using MyAwesomeShop.Shared.Infrastructure.EventBus;
using MyAwesomeShop.Catalog.Application.IntegrationEvents;
using MyAwesomeShop.Shared.Application.IntegrationEvent;

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

var eventBus = app.Services.GetRequiredService<IEventBus>();

eventBus.SubscribeAsync<ProductUpdatedIntegrationEvent>();

app.UseCors(policy =>
{
    policy.AllowAnyHeader();
    policy.AllowAnyMethod();
    policy.AllowAnyOrigin();
});

app.MapHub<ProductHub>("/producthub");
app.Run();
