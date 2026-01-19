using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TMS.Application.Common.Interfaces.Persistence;
using TMS.Application.Enrollments.DTOs;
using TMS.Application.Enrollments.Interfaces;
using TMS.Domain.Entities;

namespace TMS.Application.Enrollments.Services
{
    public class EnrollmentService: IEnrollmentService
    {
        private readonly ITmsDbContext _context;
        private readonly IValidator<CreateEnrollmentRequest> _createValidator;
        private readonly ILogger<EnrollmentService> _logger;

        public EnrollmentService(ITmsDbContext context, IValidator<CreateEnrollmentRequest> createValidator, ILogger<EnrollmentService> logger)
        {
            _context = context;
            _createValidator = createValidator;
            _logger = logger;
        }

        public async Task<Guid> CreateAsync(CreateEnrollmentRequest request)
        {
            //ValidationResult res = await _context.Enrollments.ValidateAsync(request);
            //var existed = await _context.Employees
            //    .FirstOrDefaultAsync(x => x.Id == request.EmployeeId);
            //if (existed != null) {
            //    throw new InvalidOperationException("Employee are already enrollment");
            //}
            var enrollment = new Enrollment
            {
                Status = request.Status,
                EnrolledAt = DateTime.UtcNow,
                EmployeeId = request.EmployeeId,
                TrainingClassId = request.TrainingClassId,
                isActive = true
            };
            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Create Enrollment");
            return enrollment.Id;



            
        }

        public Task<List<ResponseEnrollment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseEnrollment> GetByIdAsync()
        {
            throw new NotImplementedException();
        }
    }
}
