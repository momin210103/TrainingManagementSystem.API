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

builder.Services.AddSwaggerWithJwt();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

//Middleware
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
