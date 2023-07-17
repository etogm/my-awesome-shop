using MyAwesomeShop.Shared.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseCustomLogger();

builder.Services.AddCors(options => 
    options.AddDefaultPolicy(policy => 
        policy.WithOrigins(builder.Configuration["FrontUri"]!).AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.UseCors();
app.MapReverseProxy();
app.Run();
