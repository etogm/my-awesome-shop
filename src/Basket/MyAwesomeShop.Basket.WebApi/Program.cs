using Hellang.Middleware.ProblemDetails;

using MyAwesomeShop.Basket;
using MyAwesomeShop.Shared.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseCustomLogger();

builder.Services.AddBasketInfrastructure(builder.Configuration);
builder.Services.AddBasketApplication();
builder.Services.AddBasketWebApi(builder.Configuration, builder.Environment);

var app = builder.Build();

app.UseCors();

app.UseBasketSubscriptions();

app.UseProblemDetails();
app.UseWebApiSwagger();
app.MapBasketWebApi();

app.Run();
