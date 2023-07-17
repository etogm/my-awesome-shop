using Hellang.Middleware.ProblemDetails;

using MyAwesomeShop.Catalog.Application;
using MyAwesomeShop.Catalog.Infrastructure;
using MyAwesomeShop.Catalog.WebApi;
using MyAwesomeShop.Shared.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseCustomLogger();

builder.Services.AddCatalogInfrastructure(builder.Configuration);
builder.Services.AddCatalogApplication();
builder.Services.AddCatalogWebApi(builder.Environment);

var app = builder.Build();

app.UseCors(policy =>
{
    policy.AllowAnyHeader();
    policy.AllowAnyMethod();
    policy.AllowAnyOrigin();
});

app.UseProblemDetails();
app.UseWebApiSwagger();
app.MapControllers();

app.Run();
