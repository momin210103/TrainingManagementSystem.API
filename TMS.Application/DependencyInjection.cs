using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TMS.Application.Departments.DTOs;
using TMS.Application.Departments.Interfaces;
using TMS.Application.Departments.Services;
using TMS.Application.Departments.Validators;
using TMS.Application.Employees.DTOs;
using TMS.Application.Employees.Interfaces;
using TMS.Application.Employees.Services;
using TMS.Application.Employees.Validators;

namespace TMS.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
           
            services.AddScoped<IValidator<CreateEmployeeRequest>, CreateEmployeeRequestValidator>();
            services.AddScoped<IValidator<UpdateEmployeeRequest>, UpdateEmployeeRequestValidator>();
            services.AddScoped<IValidator<CreateDepartmentRequest>, CreateDepartmentRequestValidator>();
            services.AddScoped<IValidator<UpdateDepartmentRequest>, UpdateDepartmentRequestValidator>();
            return services;
        }
    }
}
