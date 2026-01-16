using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TMS.Application.Common.Interfaces.Persistence;
using TMS.Application.CouresCategories.DTOs;
using TMS.Application.CouresCategories.Interfaces;
using TMS.Domain.Entities;

namespace TMS.Application.CouresCategories.Services
{
    public class CourseCategoryService: ICourseCategoryService
    {
        private readonly ITmsDbContext _context;
        private readonly IValidator<CreateCourseCategoryRequest> _createValidator;
        private readonly IValidator<UpdateCourseCategoryRequest> _updateValidator;
        private readonly ILogger<CourseCategoryService> _logger;

        public CourseCategoryService(ITmsDbContext context, IValidator<CreateCourseCategoryRequest> createValidator, IValidator<UpdateCourseCategoryRequest> updateValidator, ILogger<CourseCategoryService> logger)
        {
            _context = context;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _logger = logger;
        }

        public async Task<Guid> CreateAsync(CreateCourseCategoryRequest request)
        {
            // Step 1:
            ValidationResult result = await _createValidator.ValidateAsync(request);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
            // Step 2: Find Existing
            var exists = await _context.CourseCategories
                .AnyAsync(x => x.Name == request.Name);
            if (exists)
                throw new InvalidOperationException("Course Category Already exists");
            // Step 3: 
            var courseCategory = new CourseCategory
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Code = request.Code,
                Description = request.Description,
                isActive = true
            };
            // Add & Save
            _context.CourseCategories.Add(courseCategory);
            await _context.SaveChangesAsync();
            // Logging
            _logger.LogInformation(" Course Category Created succesfully. Id: {CourseCategoryId}", courseCategory.Id);
            return courseCategory.Id;


            
        }

        public async Task DeleteAsync(Guid id)
        {
            var res = await _context.CourseCategories
                .FirstOrDefaultAsync(x => x.Id == id);
            if (res == null)
                throw new KeyNotFoundException("CourseCategory Not Found");
            bool isUsed = await _context.Courses
                .AnyAsync(x => x.CourseCategoryId == id);
            if (isUsed)
                throw new InvalidOperationException("Course Categories used in Course cannot deleted");
            res.isActive = false;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Course Category deleted successfully. Id: {CourseCategoryId}", res.Id);

        }

        public async Task<List<CourseCategoryResponse>> GetAllAsync()
        {
            return await _context.CourseCategories
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .Select(x => new CourseCategoryResponse
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();
        }

        public async Task<CourseCategoryResponse> GetByIdAsync(Guid id)
        {
            var courseCategory = await _context.CourseCategories
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new CourseCategoryResponse
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .FirstOrDefaultAsync();
            if (courseCategory == null)
                throw new KeyNotFoundException("Course Category Not Found");
            return courseCategory;
            
        }

        public async Task UpdateAsync(UpdateCourseCategoryRequest request)
        {
            ValidationResult result = await _updateValidator.ValidateAsync(request);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
            var courseCategory = await _context.CourseCategories.
                FirstOrDefaultAsync(x => x.Id == request.Id);
            if (courseCategory == null)
                throw new InvalidOperationException("Course Category not found");
            bool isUsed = await _context.CourseCategories
                .AnyAsync(x => x.Name == request.Name);
            if (isUsed)
                throw new InvalidOperationException("Course Category Name alrready Exists");
            courseCategory.Name = request.Name;
            courseCategory.Code = request.Code;
            courseCategory.Description = request.Description;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Course Category Update Succesfull. Id:{CourseCategoryId}", courseCategory.Id);
              
            
        }
    }
}
