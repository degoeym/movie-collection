using System.Text.Json.Serialization;
using Components;
using Data;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Operations;
using Routers;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MovieCollectionContext>(opt => opt.UseNpgsql(connectionString).UseSnakeCaseNamingConvention());
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<IMovieOperations, MovieOperations>();
builder.Services.AddScoped<RouterBase, MovieRouter>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JsonOptions>(opt => opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var app = builder.Build();
var dbContext = app.Services.GetRequiredService<MovieCollectionContext>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider.GetServices<RouterBase>();

    foreach (var service in services)
    {
        service.AddRoutes(app);
    }

    app.Run();
}
