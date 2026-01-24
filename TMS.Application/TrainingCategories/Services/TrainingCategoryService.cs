
using System.Runtime.CompilerServices;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TMS.Application.Common.Interfaces.Persistence;
using TMS.Application.TrainingCategories.DTOs;
using TMS.Application.TrainingCategories.Interfaces;
using TMS.Domain.Entities;

namespace TMS.Application.TrainingCategories.Services
{
    public class TrainingCategoryService : ITrainingCategoryService
    {
        private readonly ITmsDbContext _context;
        private readonly ILogger<TrainingCategoryService> _logger;
        private readonly IValidator<CreateTrainingCategoryRequest> _createValidator;
        private readonly IValidator<UpdateTrainingCategoryRequest> _updateValidator;
        public TrainingCategoryService(
            ITmsDbContext context,
            ILogger<TrainingCategoryService> logger,
            IValidator<CreateTrainingCategoryRequest> createValidator,
            IValidator<UpdateTrainingCategoryRequest> updateValidator)
        {
            _context = context;
            _logger = logger;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
        public async Task<Guid> CreateAsync(CreateTrainingCategoryRequest request)
        {
            ValidationResult result = await _createValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors); 
            }
            var exists = await _context.TrainingCategories
                .AnyAsync(tc => tc.Code == request.Code);
            if (exists)
            {
                throw new ValidationException($"A training category with code '{request.Code}' already exists.");   
            }
            var trainingCategory = new TrainingCategory
            {
                Id = Guid.NewGuid(),
                Code = request.Code,
                Name = request.Name,
                Description = request.Description,
                isActive = true
            };
            await _context.TrainingCategories.AddAsync(trainingCategory);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Created new training category with ID {TrainingCategoryId}", trainingCategory.Id);
            return trainingCategory.Id;

        }

        public async Task DeleteAsync(Guid id)
        {
            var trainingCategory = await _context.TrainingCategories
                .FirstOrDefaultAsync(tc => tc.Id == id);
            if (trainingCategory == null)
            {
                throw new KeyNotFoundException($"Training category with ID {id} not found.");
            }
            trainingCategory.isActive = false;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Deactivated training category with ID {TrainingCategoryId}", id);


        }

        public async Task<List<TrainingCategoryResponse>> GetAllAsync()
        {
            var trainingCategories = await _context.TrainingCategories
                .Select(tc => new TrainingCategoryResponse
                {
                    Id = tc.Id,
                    Name = tc.Name
                }).ToListAsync();
                _logger.LogInformation("Retrieved all training categories. Count: {Count}", trainingCategories.Count);
            return trainingCategories;
        }

        public async Task<TrainingCategoryResponse> GetByIdAsync(Guid id)
        {
            var trainingCategory = await _context.TrainingCategories
                .Where(tc => tc.Id == id)
                .Select(tc => new TrainingCategoryResponse
                {
                    Id = tc.Id,
                    Name = tc.Name
                }).FirstOrDefaultAsync();
            if (trainingCategory == null)
            {
                throw new KeyNotFoundException($"Training category with ID {id} not found.");
            }
            _logger.LogInformation("Retrieved training category with ID {TrainingCategoryId}", id);
            return trainingCategory;
            
        }

        public async Task UpdateAsync(Guid id, UpdateTrainingCategoryRequest request)
        {
            var trainingCategory = await _context.TrainingCategories
                .FirstOrDefaultAsync(tc => tc.Id == id);
            if (trainingCategory == null)
            {
                throw new KeyNotFoundException($"Training category with ID {id} not found.");
            }
            ValidationResult result = await _updateValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            bool isDuplicate = await _context.TrainingCategories
                .AnyAsync(tc => tc.Code == request.Code && tc.Id != id);
            if (isDuplicate)
            {
                throw new ValidationException($"A training category with code '{request.Code}' already exists.");
            }
            trainingCategory.Code = request.Code;
            trainingCategory.Name = request.Name;
            trainingCategory.Description = request.Description;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Updated training category with ID {TrainingCategoryId}", id);   

        }
    }
}