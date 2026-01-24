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
                Status = request.Status
            };
            _Context.TrainingClasses.Add(TrainingClass);
            await _Context.SaveChangesAsync();
            return TrainingClass.Id;
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TrainingClassResponse>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TrainingClassResponse> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UpdateTrainingClassRequest request)
        {
            throw new NotImplementedException();
        }
    }
}