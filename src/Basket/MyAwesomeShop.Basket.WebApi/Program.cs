using MyAwesomeShop.Basket;
using MyAwesomeShop.Basket.Repositories;
using MyAwesomeShop.Shared.Infrastructure.PubSub;
using MyAwesomeShop.Shared.WebApi;

using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebApiSwagger();

// Infra
builder.Services.AddScoped<IBasketRepository, RedisBasketRepository>();
builder.Services.AddSingleton(provider =>
{
    return ConnectionMultiplexer.Connect(builder.Configuration["RedisOptions:Configuration"]!);
});
builder.Services.AddPubSub(options =>
{
    options.Configuration = builder.Configuration["RedisOptions:Configuration"]!;
});

var app = builder.Build();

app.UseBasketSub();
app.MapBasketWebApi();

app.Run();
