using MyAwesomeShop.Shared.WebApi;
using MyAwesomeShop.Shared.Infrastructure.EventBus;
using MyAwesomeShop.Catalog.Application.IntegrationEvents;
using MyAwesomeShop.Shared.Application.IntegrationEvent;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseCustomLogger();

builder.Services.AddCors(options => options.AddInternalDefaultPolicy(builder.Configuration["GatewayUri"]!));
builder.Services.AddSignalR();

builder.Services.AddEventBus(options =>
{
    options.Configuration = builder.Configuration["RedisOptions:Connection"]!;
});
builder.Services.AddIntegrationEventHandler<ProductUpdatedIntegrationEvent, ProductUpdatedHandler>();

var app = builder.Build();

var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.SubscribeAsync<ProductUpdatedIntegrationEvent>();

app.UseCors();

app.MapHub<ProductHub>("/producthub");
app.Run();
