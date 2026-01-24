using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TMS.Application.Auth.Interfaces;
using TMS.Application.Auth.Services;
using TMS.Application.CouresCategories.DTOs;
using TMS.Application.CouresCategories.Interfaces;
using TMS.Application.CouresCategories.Services;
using TMS.Application.CouresCategories.Validators;
using TMS.Application.Courses.DTOs;
using TMS.Application.Courses.Interfaces;
using TMS.Application.Courses.Services;
using TMS.Application.Courses.Validators;
using TMS.Application.Departments.DTOs;
using TMS.Application.Departments.Interfaces;
using TMS.Application.Departments.Services;
using TMS.Application.Departments.Validators;
using TMS.Application.Employees.DTOs;
using TMS.Application.Employees.Interfaces;
using TMS.Application.Employees.Services;
using TMS.Application.Employees.Validators;
using TMS.Application.Enrollments.DTOs;
using TMS.Application.Enrollments.Interfaces;
using TMS.Application.Enrollments.Services;
using TMS.Application.Enrollments.Validators;
using TMS.Application.JobTitles.DTOs;
using TMS.Application.JobTitles.Interfaces;
using TMS.Application.JobTitles.Services;
using TMS.Application.JobTitles.Validators;
using TMS.Application.TrainingCategories.DTOs;
using TMS.Application.TrainingCategories.Interfaces;
using TMS.Application.TrainingCategories.Services;
using TMS.Application.TrainingCategories.Validators;
using TMS.Application.TrainingClasses.DTOs;
using TMS.Application.TrainingClasses.Interfaces;
using TMS.Application.TrainingClasses.Services;
using TMS.Application.TrainingClasses.Validators;
using TMS.Application.TrainingPlans.DTOs;
using TMS.Application.TrainingPlans.Interfaces;
using TMS.Application.TrainingPlans.Services;
using TMS.Application.TrainingPlans.Validators;

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
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<ITrainingClassService, TrainingClassService>();
            services.AddScoped<ITrainingCategoryService, TrainingCategoryService>();
            services.AddScoped<ITrainingPlanService, TrainingPlanService>();

            services.AddScoped<IValidator<CreateEmployeeRequest>, CreateEmployeeRequestValidator>();
            services.AddScoped<IValidator<UpdateEmployeeRequest>, UpdateEmployeeRequestValidator>();
            services.AddScoped<IValidator<CreateDepartmentRequest>, CreateDepartmentRequestValidator>();
            services.AddScoped<IValidator<UpdateDepartmentRequest>, UpdateDepartmentRequestValidator>();
            services.AddScoped<IValidator<CreateJobTitleRequest>, CreateJobTitleValidator>();
            services.AddScoped<IValidator<UpdateJobTitleRequest>, UpdateJobTitleValidator>();
            services.AddScoped<IValidator<CreateCourseCategoryRequest>, CreateCourseCategoryRequestValidator>();
            services.AddScoped<IValidator<UpdateCourseCategoryRequest>, UpdateCourseCategoryRequestValidator>();
            services.AddScoped<IValidator<CreateCourseRequest>, CreateCourseRequestValidator>();
            services.AddScoped<IValidator<UpdateCourseRequest>, UpdateCourseRequestValidator>();
            services.AddScoped<IValidator<CreateEnrollmentRequest>, CreateEnrollmentRequestValidator>();
            services.AddScoped<IValidator<CreateTrainingCategoryRequest>, CreateTrainingCategoryRequestValidator>();
            services.AddScoped<IValidator<UpdateTrainingCategoryRequest>, UpdateTrainingCategoryRequestValidator>();
            services.AddScoped<IValidator<CreateTrainingPlanRequest>, CreateTrainingPlanRequestValidator>();
            services.AddScoped<IValidator<UpdateTrainingPlanRequest>, UpdateTrainingPlanRequestValidator>();
            
            services.AddScoped<IValidator<CreateTrainingClassRequest>, CreateTrainingClassRequestValidator>();
            services.AddScoped<IValidator<UpdateTrainingClassRequest>, UpdateTrainingClassRequestValidator>();
            
            return services;
        }
    }
}
