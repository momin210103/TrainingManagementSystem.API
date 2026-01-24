using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TMS.Application.Common.Interfaces.Persistence;
using TMS.Application.TrainingClasses.DTOs;
using TMS.Application.TrainingClasses.Interfaces;
using TMS.Domain.Entities;

namespace TMS.Application.TrainingClasses.Services
{
    public class TrainingClassService : ITrainingClassService
    {
        private readonly ITmsDbContext _Context;
        private readonly ILogger<TrainingClassService> _Logger;
        private readonly IValidator<CreateTrainingClassRequest> _CreateValidator;
        private readonly IValidator<UpdateTrainingClassRequest> _UpdateValidator;

        public TrainingClassService(ITmsDbContext context, ILogger<TrainingClassService> logger, IValidator<CreateTrainingClassRequest> createValidator, IValidator<UpdateTrainingClassRequest> updateValidator)
        {
            _Context = context;
            _Logger = logger;
            _CreateValidator = createValidator;
            _UpdateValidator = updateValidator;
        }

        public async Task<Guid> CreateAsync(CreateTrainingClassRequest request)
        {
            ValidationResult result = await _CreateValidator.ValidateAsync(request);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
            if (request.CourseId != Guid.Empty)
            {
                bool courseExists = await _Context.Courses.AnyAsync(c => c.Id == request.CourseId);
                if (!courseExists)
                    throw new KeyNotFoundException("Course not found");
            }
            if(request.TrainingPlanId != Guid.Empty)
            {
                bool trainingPlanExists = await _Context.TrainingPlans.AnyAsync(tp => tp.Id == request.TrainingPlanId);
                if (!trainingPlanExists)
                    throw new KeyNotFoundException("Training Plan not found");
            }
            
            var TrainingClass = new TrainingClass
            {
                Id = Guid.NewGuid(),
                CourseId = request.CourseId,
                TrainingPlanId = request.TrainingPlanId,
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Capacity = request.Capacity,
                Status = request.Status,
                isActive = true
            };
            _Context.TrainingClasses.Add(TrainingClass);
            await _Context.SaveChangesAsync();
            _Logger.LogInformation("Training Class {TrainingClassId} created successfully.", TrainingClass.Id);
            return TrainingClass.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var trainingClass = await _Context.TrainingClasses.FirstOrDefaultAsync(tc => tc.Id == id);
            if (trainingClass == null)
                throw new KeyNotFoundException("Training Class not found");
            trainingClass.isActive = false;
            _Context.TrainingClasses.Update(trainingClass);
            await _Context.SaveChangesAsync();
            _Logger.LogInformation("Training Class {TrainingClassId} deleted successfully.", id);
        }

        public async Task<List<TrainingClassResponse>> GetAllAsync()
        {
            var trainingClasses = await _Context.TrainingClasses
                .Select(tc => new TrainingClassResponse
                {
                    Id = tc.Id,
                    CourseId = tc.CourseId,
                    TrainingPlanId = tc.TrainingPlanId,
                    Name = tc.Name,
                    StartDate = tc.StartDate,
                    EndDate = tc.EndDate,
                    Capacity = tc.Capacity,
                    Status = tc.Status
                }).ToListAsync();
            _Logger.LogInformation("Retrieved {Count} training classes.", trainingClasses.Count);
            return trainingClasses;
        }

        public async Task<TrainingClassResponse> GetByIdAsync(Guid id)
        {
            var TrainingClass = await _Context.TrainingClasses
                .Where(tc => tc.Id == id)
                .Select(tc => new TrainingClassResponse
                {
                    Id = tc.Id,
                    CourseId = tc.CourseId,
                    TrainingPlanId = tc.TrainingPlanId,
                    Name = tc.Name,
                    StartDate = tc.StartDate,
                    EndDate = tc.EndDate,
                    Capacity = tc.Capacity,
                    Status = tc.Status
                }).FirstOrDefaultAsync();
            if (TrainingClass == null)
                throw new KeyNotFoundException("Training Class not found");
            _Logger.LogInformation("Retrieved training class with Id: {TrainingClassId}", id);
            return TrainingClass;
        }

        public async Task UpdateAsync(UpdateTrainingClassRequest request)
        {
            ValidationResult result = await _UpdateValidator.ValidateAsync(request);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
            var res = await _Context.TrainingClasses.FirstOrDefaultAsync(tc => tc.Id == request.Id);
            if (res == null)
                throw new KeyNotFoundException("Training Class not found");
            
            res.Name = request.Name;
            res.StartDate = request.StartDate;
            res.EndDate = request.EndDate;
            res.Capacity = request.Capacity;
            res.Status = request.Status;
            res.isActive = true;
            await _Context.SaveChangesAsync();
            _Logger.LogInformation("Training Class {TrainingClassId} updated successfully.", res.Id);
        }
    }
}