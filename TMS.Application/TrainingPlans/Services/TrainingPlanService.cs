using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TMS.Application.Common.Interfaces.Persistence;
using TMS.Application.TrainingPlans.DTOs;
using TMS.Application.TrainingPlans.Interfaces;
using TMS.Domain.Entities;

namespace TMS.Application.TrainingPlans.Services
{
    public class TrainingPlanService : ITrainingPlanService
    {
        private readonly ILogger<TrainingPlanService> _logger;
        private readonly ITmsDbContext _context;
        private readonly IValidator<CreateTrainingPlanRequest> _createValidator;
        private readonly IValidator<UpdateTrainingPlanRequest> _updateValidator;
        public TrainingPlanService(
            ILogger<TrainingPlanService> logger,
            ITmsDbContext context,
            IValidator<CreateTrainingPlanRequest> createValidator,
            IValidator<UpdateTrainingPlanRequest> updateValidator
        )
        {
            _logger = logger;
            _context = context;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
        public async Task<Guid> CreateAsync(CreateTrainingPlanRequest request)
        {
            ValidationResult result = await _createValidator.ValidateAsync(request);
            if(!result.IsValid)
            {
                _logger.LogWarning("Validation failed for CreateTrainingPlanRequest: {Errors}", result.Errors);
                throw new ValidationException(result.Errors);
            }
            var existingPlan = await _context.TrainingPlans
                .FirstOrDefaultAsync(tp => tp.PlanCode == request.PlanCode);
            if(existingPlan != null)
            {
                _logger.LogWarning("Training Plan with PlanCode {PlanCode} already exists.", request.PlanCode);
                throw new InvalidOperationException($"Training Plan with PlanCode {request.PlanCode} already exists.");
            }
            var trainingPlan = new TrainingPlan
            {
                TrainingCategoryId = request.TrainingCategoryId,
                PlanCode = request.PlanCode,
                PlanName = request.PlanName,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                TrainingCompany = request.TrainingCompany,
                TrainingPlace = request.TrainingPlace,
                TrainingCost = request.TrainingCost,
                ContactPerson = request.ContactPerson,
                Description = request.Description,
                isActive = true,
                CreatedAt = DateTime.UtcNow
            };
            _context.TrainingPlans.Add(trainingPlan);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Created Training Plan with ID: {TrainingPlanId}", trainingPlan.Id);
            return trainingPlan.Id;
            
        }

        public async Task DeleteAsync(Guid id)
        {
            var trainingPlan = await _context.TrainingPlans.FirstOrDefaultAsync(tp => tp.Id == id);
            if(trainingPlan == null)
            {
                _logger.LogWarning("Training Plan with ID: {TrainingPlanId} not found", id);
                throw new KeyNotFoundException($"Training Plan with ID: {id} not found");
            }
            trainingPlan.isActive = false;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Deleted Training Plan with ID: {TrainingPlanId}", id);
            
        }

        public async Task<List<TrainingPlanResponse>> GetAllAsync()
        {
            var TrainingPlans = await _context.TrainingPlans
                .Include(tp => tp.TrainingCategory)
                .Select(tp => new TrainingPlanResponse
                {
                    TrainingCategoryId = tp.TrainingCategoryId,
                    PlanCode = tp.PlanCode,
                    PlanName = tp.PlanName,
                    StartDate = tp.StartDate,
                    EndDate = tp.EndDate,
                    TrainingCompany = tp.TrainingCompany,
                    TrainingPlace = tp.TrainingPlace,
                    TrainingCost = tp.TrainingCost,
                    ContactPerson = tp.ContactPerson,
                    Description = tp.Description
                }).ToListAsync();
            _logger.LogInformation("Retrieved {Count} Training Plans", TrainingPlans.Count);
            return TrainingPlans;
        }

        public async Task<TrainingPlanResponse> GetByIdAsync(Guid id)
        {
            var trainingPlan = await _context.TrainingPlans
                .Where(tp => tp.Id == id)
                .Select(tp => new TrainingPlanResponse
                {
                    TrainingCategoryId = tp.TrainingCategoryId,
                    PlanCode = tp.PlanCode,
                    PlanName = tp.PlanName,
                    StartDate = tp.StartDate,
                    EndDate = tp.EndDate,
                    TrainingCompany = tp.TrainingCompany,
                    TrainingPlace = tp.TrainingPlace,
                    TrainingCost = tp.TrainingCost,
                    ContactPerson = tp.ContactPerson,
                    Description = tp.Description
                }).FirstOrDefaultAsync();
            if(trainingPlan == null)
            {
                _logger.LogWarning("Training Plan with ID: {TrainingPlanId} not found", id);
                throw new KeyNotFoundException($"Training Plan with ID: {id} not found");
            }

            _logger.LogInformation("Retrieved Training Plan with ID: {TrainingPlanId}", id);
            return trainingPlan;
        }

        public async Task UpdateAsync(Guid id, UpdateTrainingPlanRequest request)
        {
            ValidationResult result = await _updateValidator.ValidateAsync(request);
            if(!result.IsValid)
            {
                _logger.LogWarning("Validation failed for UpdateTrainingPlanRequest: {Errors}", result.Errors);
                throw new ValidationException(result.Errors);
            }
            var trainingPlan = await _context.TrainingPlans.FirstOrDefaultAsync(tp => tp.Id == id);
            if(trainingPlan == null)
            {
                _logger.LogWarning("Training Plan with ID: {TrainingPlanId} not found", id);
                throw new KeyNotFoundException($"Training Plan with ID: {id} not found");
            }
            trainingPlan.TrainingCategoryId = request.TrainingCategoryId;
            trainingPlan.PlanCode = request.PlanCode;
            trainingPlan.PlanName = request.PlanName;
            trainingPlan.StartDate = request.StartDate;
            trainingPlan.EndDate = request.EndDate;
            trainingPlan.TrainingCompany = request.TrainingCompany;
            trainingPlan.TrainingPlace = request.TrainingPlace;
            trainingPlan.TrainingCost = request.TrainingCost;
            trainingPlan.ContactPerson = request.ContactPerson;
            trainingPlan.Description = request.Description;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Updated Training Plan with ID: {TrainingPlanId}", id);
        }
    }
}