using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TMS.Application.Common.Interfaces.Persistence;
using TMS.Application.Departments.DTOs;
using TMS.Application.Departments.Interfaces;
using TMS.Application.Departments.Validators;
using TMS.Domain.Entities;

namespace TMS.Application.Departments.Services
{
    public class DepartmentService: IDepartmentService
    {
        private readonly ITmsDbContext _context;
        private readonly IValidator<CreateDepartmentRequest> _createValidator;
        private readonly IValidator<UpdateDepartmentRequest> _updateValidator;
        private readonly ILogger<DepartmentService> _logger;

        public DepartmentService(ITmsDbContext context, IValidator<CreateDepartmentRequest> createValidator, IValidator<UpdateDepartmentRequest> updateValidator, ILogger<DepartmentService> logger)
        {
            _context = context;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _logger = logger;
        }

        public async Task<Guid> CreateAsync(CreateDepartmentRequest req)
        {
           ValidationResult res = await _createValidator.ValidateAsync(req);
            if (!res.IsValid)
                throw new ValidationException(res.Errors);
            bool exists = await _context.Departments.AnyAsync(d => d.Name.ToLower() == req.Name.ToLower() && d.isActive);
            if (exists)
                throw new InvalidOperationException("Department already exists");
            var department = new Department
            {
                Id = Guid.NewGuid(),
                Name = req.Name,
                isActive = true
            };
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Department created. Id: {DepartmentId}", department.Id);
            return department.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var dep = await _context.Departments
                .Include(x => x.Employees)
                .FirstOrDefaultAsync(x => x.Id == id && x.isActive);
            if (dep == null)
                throw new KeyNotFoundException("Department not found");
            if (dep.Employees.Any(e => e.isActive))
                throw new InvalidOperationException("Department has active employees");
            dep.isActive = false;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Department soft deleted. Id: {DepartmentId}", id);
        }

        public async Task<List<DepartmentResponse>> GetAllAsync()
        {
            return await _context.Departments.AsNoTracking().Where(d => d.isActive).OrderBy(d => d.Name).Select(d => new DepartmentResponse { 
                Id = d.Id,
                Name = d.Name
            }).ToListAsync();
            
        }

        public async Task<DepartmentResponse> GetByIdAsync(Guid id)
        {
            var department = await _context.Departments
                .AsNoTracking()
                .Where(x => x.Id == id && x.isActive)
                .Select(x => new DepartmentResponse
                {
                    Id = x.Id,
                    Name = x.Name
                }).FirstOrDefaultAsync();
            if (department == null)
                throw new KeyNotFoundException("Department not found");
            
            return department;
        }

        public async Task UpdateAsync(UpdateDepartmentRequest req)
        {
            ValidationResult res = await _updateValidator.ValidateAsync(req);
            if (!res.IsValid)
                throw new ValidationException(res.Errors);
            var dep = await _context.Departments.FirstOrDefaultAsync(x => x.Id == req.Id && x.isActive);
            if (dep == null)
                throw new KeyNotFoundException("Department not Found");
            bool duplicate = await _context.Departments
                .AnyAsync(x => x.Id != req.Id && x.Name == req.Name && x.isActive);
            if (duplicate)
                throw new InvalidOperationException("Department name already exists");
            dep.Name = req.Name;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Department updated. Id : {DepartmentId}", dep.Id);

        }
    }
}
