using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TMS.Application.Common.Interfaces.Persistence;
using TMS.Application.JobTitles.DTOs;
using TMS.Application.JobTitles.Interfaces;
using TMS.Application.JobTitles.Validators;
using TMS.Domain.Entities;

namespace TMS.Application.JobTitles.Services
{
    public class JobTitleService: IJobTitleService
    {
        private readonly ITmsDbContext _context;
        private readonly IValidator<CreateJobTitleRequest> _createValidator;
        private readonly IValidator<UpdateJobTitleRequest> _updateValidator;
        private readonly ILogger<JobTitleService> _logger;

        public JobTitleService(ITmsDbContext context, IValidator<CreateJobTitleRequest> createValidator, IValidator<UpdateJobTitleRequest> updateValidator, ILogger<JobTitleService> logger)
        {
            _context = context;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _logger = logger;
        }

        public async Task<Guid> CreateAsync(CreateJobTitleRequest request)
        {
            // Step 1: Validation Check
            ValidationResult res = await _createValidator.ValidateAsync(request);
            if (!res.IsValid)
                throw new ValidationException(res.Errors);
            // Step 2: Duplicate Check
            bool exists = await _context.JobTitles
                .AnyAsync(x => x.Name == request.Name);
            if (exists)
                throw new InvalidOperationException("JobTitle already exists");
            // Step 3: Create JobTitle
            var jobTitle = new JobTitle
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                isActive = true
            };
            // Step 4: Save to Database
            _context.JobTitles.Add(jobTitle);
            await _context.SaveChangesAsync();
            // Step 5: Logging
            _logger.LogInformation("JobTitle creadted. Id: {JobTitleId}", jobTitle.Id);
           
            return jobTitle.Id;

        }

        public async Task DeleteAsync(Guid id)
        {
            var res = await _context.JobTitles
                .FirstOrDefaultAsync(x => x.Id == id);
            if (res == null)
                throw new KeyNotFoundException("JobTitle Not Found");
            bool used = await _context.Employees
                .AnyAsync(x => x.JobTitleId == id);
            if (used)
                throw new InvalidOperationException("Job Title used in Employee");
            res.isActive = false;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Job Title Soft Deleted. Id :{JobTitleId}", res.Id);

        }

        public async Task<List<JobTitleResponse>> GetAllAsync()
        {
            return await _context.JobTitles
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .Select(x => new JobTitleResponse
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();
        }

        public async Task<JobTitleResponse> GetByIdAsync(Guid id)
        {
            var res = await _context.JobTitles
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new JobTitleResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .FirstOrDefaultAsync();
            if (res == null)
                throw new KeyNotFoundException("Job Title Not Found");
            return res;

        }

        public async Task UpdateAsync(UpdateJobTitleRequest request)
        {
            // Step 1: Check Validate
            ValidationResult res = await _updateValidator.ValidateAsync(request);
            if (!res.IsValid)
                throw new ValidationException(res.Errors);
            // Step 2: Find Which is update
            var jobTitle = await _context.JobTitles
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            if (jobTitle == null)
                throw new KeyNotFoundException("Job Title Not Found");
            // step 3: Check duplicate
            bool duplicate = await _context.JobTitles
                .AnyAsync(x => x.Id != request.Id && x.Name == request.Name);
            if (duplicate)
                throw new InvalidOperationException("Job Title Already Exists");
            jobTitle.Name = request.Name;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Job Title Update Succcesfully");
                
        }
    }
}
