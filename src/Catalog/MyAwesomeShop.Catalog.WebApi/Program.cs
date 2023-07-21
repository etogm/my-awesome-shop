using Hellang.Middleware.ProblemDetails;

using MyAwesomeShop.Catalog.Application;
using MyAwesomeShop.Catalog.Infrastructure;
using MyAwesomeShop.Catalog.WebApi;
using MyAwesomeShop.Shared.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseCustomLogger();

builder.Services.AddCatalogInfrastructure(options => builder.Configuration.GetRequiredSection("InfrastructureOptions").Bind(options));
builder.Services.AddCatalogApplication();
builder.Services.AddCatalogWebApi(builder.Configuration, builder.Environment);

var app = builder.Build();

app.UseCors();
app.UseProblemDetails();
app.MapControllers();
app.UseWebApiSwagger();

app.Run();
