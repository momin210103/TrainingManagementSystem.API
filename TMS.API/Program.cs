using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TMS.API.Extensions;
using TMS.API.Middlewares;
using TMS.Application;
using TMS.Application.Employees.DTOs;
using TMS.Application.Employees.Validators;
using TMS.Infrastructure;
using TMS.Infrastructure.Persistence;
using TMS.Infrastructure.Persistence.Data.Seed;

var builder = WebApplication.CreateBuilder(args);


Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
builder.Host.UseSerilog();
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSeeders();

builder.Services.AddSwaggerWithJwt();
var app = builder.Build();

// Initialize and seed database
using (var scope = app.Services.CreateScope()) {
    var seeder = scope.ServiceProvider.GetRequiredService<MasterSeeder>();
    await seeder.SeedAllAsync();
}

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json","TMS.API Open");
            options.RoutePrefix = string.Empty;
        });
    }

// app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

//Middleware
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
