using Microsoft.EntityFrameworkCore;

using MyAwesomeShop.Shared.Security;
using MyAwesomeShop.Shared.Security.Data;
using MyAwesomeShop.Shared.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseCustomLogger();

builder.Services.AddDbContext<SecurityDbContext>(options =>
    options
        .UseNpgsql(
            builder.Configuration.GetConnectionString("Security"),
                options =>
                {
                    options.MigrationsHistoryTable("security_migrations_history", "public");
                    options.MigrationsAssembly("MyAwesomeShop.ApiGateways.Web");
                }));

builder.Services.AddSecurity<SecurityDbContext>(builder.Configuration.GetSection("SecurityOptions"));

builder.Services.AddWebApiSwagger();

builder.Services.AddCors(options => 
    options.AddDefaultPolicy(policy => 
        policy.WithOrigins(builder.Configuration["FrontUri"]!).AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services
    .AddControllers()
    .AddSecurityControllers();

var app = builder.Build();

app.UseWebApiSwagger();
app.UseCors();

app.UseSecurity();

app.MapReverseProxy();
app.MapControllers();

app.Run();
