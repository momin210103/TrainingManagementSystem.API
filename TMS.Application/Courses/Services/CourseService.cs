using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TMS.Application.Common.Interfaces.Persistence;
using TMS.Application.Courses.DTOs;
using TMS.Application.Courses.Interfaces;
using TMS.Domain.Entities;

namespace TMS.Application.Courses.Services
{
    public class CourseService: ICourseService
    {
        private readonly ILogger<CourseService> _logger;
        private readonly ITmsDbContext _context;
        private readonly IValidator<CreateCourseRequest> _createValidator;
        private readonly IValidator<UpdateCourseRequest> _updateValidator;
        public CourseService(
            ITmsDbContext context,
            IValidator<CreateCourseRequest> createValidator,
            IValidator<UpdateCourseRequest> updateValidator,
            ILogger<CourseService> logger)
        {
            _context = context;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _logger = logger;
        }

        public async Task<Guid> CreateAsync(CreateCourseRequest request)
        {

            ValidationResult validationResult = await _createValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var categoryExists = await _context.CourseCategories
                .AnyAsync(x => x.Id == request.CourseCategoryId);
            if(!categoryExists)
                throw new KeyNotFoundException("Course Category not found.");
            var existed = await _context.Courses
                .AnyAsync(x => x.CourseCode == request.CourseCode);
            if(existed)
                throw new InvalidOperationException("Course with the same code already exists.");

            var course = new Course
            {
                Id = Guid.NewGuid(),
                CourseCategoryId = request.CourseCategoryId,
                CourseCode = request.CourseCode,
                Title = request.Title,
                Description = request.Description,
                DurationHours = request.DurationHours,
                isActive = true
            };
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Course {CourseId} created successfully.", course.Id);
            return course.Id;



        }

        public async Task DeleteAsync(Guid id)
        {
            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == id);
            if (course == null)
                throw new KeyNotFoundException("Course not found.");
            var res = await _context.CourseCategories
                .AnyAsync(cc => cc.Id == id);
            if (res)
                throw new InvalidOperationException("Cannot delete course as it is associated with a course category.");
            course.isActive = false;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Course {CourseId} deleted successfully.", id);
        }

        public async Task<List<CourseResponse>> GetAllAsync()
        {
            var courses = await _context.Courses
                .Include(c => c.CourseCategory)
                .Select(c => new CourseResponse
                {
                    Id = c.Id,
                    CourseCategoryId = c.CourseCategoryId,
                    CourseCode = c.CourseCode,
                    Title = c.Title,
                    Description = c.Description,
                    DurationHours = c.DurationHours
                })
                .ToListAsync();
            _logger.LogInformation("Retrieved {Count} courses.", courses.Count);
            return courses;

        }

        public async Task<CourseResponse> GetByIdAsync(Guid id)
        {
            var course = await _context.Courses
                .Include(c => c.CourseCategory)
                .Where(c => c.Id == id)
                .Select(c => new CourseResponse
                {
                    Id = c.Id,
                    CourseCategoryId = c.CourseCategoryId,
                    CourseCode = c.CourseCode,
                    Title = c.Title,
                    Description = c.Description,
                    DurationHours = c.DurationHours
                })
                .FirstOrDefaultAsync();
            if (course == null){
                throw new KeyNotFoundException("Course not found.");
            }
            _logger.LogInformation("Course {CourseId} retrieved successfully.", id);
            return course;
        }

        public async Task UpdateAsync(UpdateCourseRequest request)
        {
            ValidationResult validationResult = await _updateValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == request.Id);
            if (course == null)
                throw new KeyNotFoundException("Course not found.");
            bool isDuplicate = await _context.Courses
                .AnyAsync(c => c.CourseCode == request.CourseCode && c.Id != request.Id);
            if(isDuplicate)
                throw new InvalidOperationException("Another course with the same code already exists.");
            course.CourseCategoryId = request.CourseCategoryId;
            course.CourseCode = request.CourseCode;
            course.Title = request.Title;
            course.Description = request.Description;
            course.DurationHours = request.DurationHours;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Course {CourseId} updated successfully.", request.Id);



        }
    }
}
