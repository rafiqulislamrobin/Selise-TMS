using Application.Common.infra;
using Application.features.Customer.Get;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using WebApplication1.Helpers.Entensions;

var builder = WebApplication.CreateBuilder(args);

// Serilog setup
builder.Host.UseSerilog((_, conf) =>
{
    conf
        .MinimumLevel.Debug()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        .Enrich.FromLogContext()
        .WriteTo.Console();
});

builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dummy-project API", Version = "v1" });
});
builder.Services
    .AddDbContext<AppDbContext>(opts =>
    {
        opts.UseNpgsql(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly("WebApplication1");
                })
            .EnableSensitiveDataLogging();
    })
    .AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>();

builder.Services.AddScoped<IRepository, CustomerRepository>();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetCustomerHandler).Assembly);

});
builder.Services.AddHostedService<MigrationService>();

var app = builder.Build();

// Enable Swagger middleware
if (true)
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dummy-project API");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();