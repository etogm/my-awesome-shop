using MyAwesomeShop.Basket;
using MyAwesomeShop.Basket.Application;
using MyAwesomeShop.Basket.Repositories;
using MyAwesomeShop.Catalog.Application.IntegrationEvents;
using MyAwesomeShop.Shared.Application;
using MyAwesomeShop.Shared.Infrastructure.EventBus;
using MyAwesomeShop.Shared.WebApi;

using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseCustomLogger();

builder.Services.AddCors();

builder.Services.AddMapper();
builder.Services.AddWebApiSwagger();

builder.Services.Configure<BasketOptions>(builder.Configuration.GetSection("BasketOptions"));
builder.Services.AddScoped<IBasketRepository, RedisBasketRepository>();
builder.Services.AddSingleton(provider =>
{
    return ConnectionMultiplexer.Connect(builder.Configuration["BasketOptions:Connection"]!);
});

builder.Services.AddEventBus(options =>
{
    options.Configuration = builder.Configuration["RedisOptions:Connection"]!;
});

builder.Services.AddIntegrationEventHandler<ProductUpdatedIntegrationEvent, ProductUpdatedHandler>();

var app = builder.Build();

app.UseCors(policy => policy.AllowAnyOrigin());

app.UseWebApiSwagger();
app.UseBasketSub();
app.MapBasketWebApi();

app.Run();
