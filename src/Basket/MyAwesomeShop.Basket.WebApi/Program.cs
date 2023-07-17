using Hellang.Middleware.ProblemDetails;
using Hellang.Middleware.ProblemDetails.Mvc;

using MyAwesomeShop.Basket;
using MyAwesomeShop.Basket.BasketFeature;
using MyAwesomeShop.Catalog.Application.Abstractions;
using MyAwesomeShop.Catalog.Application.IntegrationEvents;
using MyAwesomeShop.Shared.Application;
using MyAwesomeShop.Shared.Infrastructure.EventBus;
using MyAwesomeShop.Shared.WebApi;

using Refit;

using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseCustomLogger();

builder.Services.AddCors();

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});
builder.Services.AddCustomProblemDetails(builder.Environment);
builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
}).AddProblemDetailsConventions();

builder.Services.AddMapper();
builder.Services.AddWebApiSwagger();
builder.Services.AddRefitClient<ICatalogQueryService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["Uris:Catalog"]!));

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

app.UseCors(policy =>
{
    policy.AllowAnyHeader();
    policy.AllowAnyMethod();
    policy.AllowAnyOrigin();
});

app.UseBasketSubscriptions();

app.UseProblemDetails();
app.UseWebApiSwagger();
app.MapBasketWebApi();

app.Run();
