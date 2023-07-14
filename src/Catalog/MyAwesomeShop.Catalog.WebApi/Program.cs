using MyAwesomeShop.Catalog.Application;
using MyAwesomeShop.Catalog.Infrastructure;
using MyAwesomeShop.Catalog.WebApi;
using MyAwesomeShop.Catalog.WebApi.Grpc;
using MyAwesomeShop.Shared.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseCustomLogger();

builder.Services.AddCatalogInfrastructure(builder.Configuration);
builder.Services.AddCatalogApplication();
builder.Services.AddCatalogWebApi();

builder.Services.AddGrpc();

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

var app = builder.Build();

app.MapGrpcService<GrpcCatalogService>();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseWebApiSwagger();
app.MapControllers();

app.Run();
