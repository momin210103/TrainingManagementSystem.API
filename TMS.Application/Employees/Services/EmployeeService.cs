using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TMS.Application.Common.Interfaces.Persistence;
using TMS.Application.Employees.DTOs;
using TMS.Application.Employees.Interfaces;
using TMS.Application.Employees.Validators;
using TMS.Domain.Entities;

namespace TMS.Application.Employees.Services
{
    public class EmployeeService: IEmployeeService
    {
        private readonly ITmsDbContext _context;
        private readonly IValidator<CreateEmployeeRequest> _validator;
        private readonly IValidator<UpdateEmployeeRequest> _updateValidator;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(ITmsDbContext context,IValidator<CreateEmployeeRequest> validator,IValidator<UpdateEmployeeRequest> updateValidator,ILogger<EmployeeService> logger)
        {
            _context = context;
            _validator = validator;
            _updateValidator = updateValidator;
            _logger = logger;
        }

        public async Task<Guid> CreateAsync(CreateEmployeeRequest request)
        {
            
            // Basic Validation (Service Level)
            //if (string.IsNullOrWhiteSpace(request.EmployeeCode))
            //    throw new ArgumentException("EmployeeCode id Required");
            //if (string.IsNullOrWhiteSpace(request.Email))
            //    throw new ArgumentException("Email is Required");
            //if (string.IsNullOrWhiteSpace(request.FullName))
            //    throw new ArgumentException("Name is Required");
            //! Another way
            //var validator = new CreateEmployeeRequestValidator();
            //var result = validator.Validate(request);
            //if (!result.IsValid)
            //    throw new ValidationException(result.Errors);

            // Another way Production level

            ValidationResult result = await _validator.ValidateAsync(request);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
            // Step 1: Validate Result
            if (request.DepartmentId.HasValue)
            {
                bool departmentExists = await _context.Departments.AnyAsync(d => d.Id == request.DepartmentId);
                if (!departmentExists)
                    throw new KeyNotFoundException("Department not found");
            }
            if (request.JobTitleId.HasValue)
            {
                bool jobTitleExists = await _context.JobTitles.AnyAsync(j => j.Id == request.JobTitleId);
                if (!jobTitleExists)
                    throw new KeyNotFoundException("Job Title not found");
            }
            

            // Business Rule

            var exists = await _context.Employees.AnyAsync(x => x.EmployeeCode == request.EmployeeCode || x.Email == request.Email);
            if (exists)
                throw new InvalidOperationException("Same Code or Email already exists");
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                FullName = request.FullName,
                EmployeeCode = request.EmployeeCode,
                Email = request.Email,
                DepartmentId = request.DepartmentId,
                JobTitleId = request.JobTitleId,
                isActive = true
                
            };
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Creating employee. Code: {EmployeeCode},Email:{Email}",
                request.EmployeeCode,
                request.Email);
            return employee.Id;
        }

        

        public async Task<List<EmployeeResponse>> GetAllAsync()
        {
            return await _context.Employees
                 .AsNoTracking()
                 .OrderBy(x => x.FullName)
                 .Select(x => new EmployeeResponse
                 {
                     Id = x.Id,
                     EmployeeCode = x.EmployeeCode,
                     FullName = x.FullName,
                     Email = x.Email,
                     DepartmentId = x.DepartmentId,
                     JobTitleId = x.JobTitleId
                 })
                 .ToListAsync();

        }

        public async Task<EmployeeResponse> GetByIdAsync(Guid id)
        {
            var employee = await _context.Employees
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new EmployeeResponse
                {
                    Id = x.Id,
                    EmployeeCode = x.EmployeeCode,
                    FullName = x.FullName,
                    Email = x.Email,
                    DepartmentId = x.DepartmentId,
                    JobTitleId = x.JobTitleId
                })
                .FirstOrDefaultAsync();
            if (employee == null)
                throw new KeyNotFoundException("Employee not found");

            return employee;

        }

        public async Task UpdateAsync(UpdateEmployeeRequest request)
        {
            await _updateValidator.ValidateAndThrowAsync(request);
            var employee = await _context.Employees
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.isActive == true);
            if (employee == null)
                throw new KeyNotFoundException("Employee Not found");
            bool duplicate = await _context.Employees.AnyAsync(x => x.Id != request.Id && (x.EmployeeCode == request.EmployeeCode || x.Email == request.Email));
            if (duplicate)
                throw new InvalidOperationException("EmployeeCode or Email already exisits");
            employee.EmployeeCode = request.EmployeeCode;
            employee.FullName = request.FullName;
            employee.Email = request.Email;
            employee.DepartmentId = request.DepartmentId;
            employee.JobTitleId = request.JobTitleId;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Employee Updated. Id: {EmployeeId}", employee.Id);
        }
        public async Task DeleteAsync(Guid id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id && x.isActive == true);
            if (employee == null)
                throw new KeyNotFoundException("Employee not found");
            employee.isActive = false;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Employee soft deleted. Id : {EmployeeId}", id);
        }
    }
}
