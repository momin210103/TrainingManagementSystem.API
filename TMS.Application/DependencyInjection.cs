using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TMS.Application.Auth.Interfaces;
using TMS.Application.Auth.Services;
using TMS.Application.CouresCategories.DTOs;
using TMS.Application.CouresCategories.Interfaces;
using TMS.Application.CouresCategories.Services;
using TMS.Application.CouresCategories.Validators;
using TMS.Application.Departments.DTOs;
using TMS.Application.Departments.Interfaces;
using TMS.Application.Departments.Services;
using TMS.Application.Departments.Validators;
using TMS.Application.Employees.DTOs;
using TMS.Application.Employees.Interfaces;
using TMS.Application.Employees.Services;
using TMS.Application.Employees.Validators;
using TMS.Application.JobTitles.DTOs;
using TMS.Application.JobTitles.Interfaces;
using TMS.Application.JobTitles.Services;
using TMS.Application.JobTitles.Validators;

namespace TMS.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJobTitleService, JobTitleService>();
            services.AddScoped<ICourseCategoryService, CourseCategoryService>();

            services.AddScoped<IValidator<CreateEmployeeRequest>, CreateEmployeeRequestValidator>();
            services.AddScoped<IValidator<UpdateEmployeeRequest>, UpdateEmployeeRequestValidator>();
            services.AddScoped<IValidator<CreateDepartmentRequest>, CreateDepartmentRequestValidator>();
            services.AddScoped<IValidator<UpdateDepartmentRequest>, UpdateDepartmentRequestValidator>();
            services.AddScoped<IValidator<CreateJobTitleRequest>, CreateJobTitleValidator>();
            services.AddScoped<IValidator<UpdateJobTitleRequest>, UpdateJobTitleValidator>();
            services.AddScoped<IValidator<CreateCourseCategoryRequest>, CreateCourseCategoryRequestValidator>();
            services.AddScoped<IValidator<UpdateCourseCategoryRequest>, UpdateCourseCategoryRequestValidator>();
            
            return services;
        }
    }
}
