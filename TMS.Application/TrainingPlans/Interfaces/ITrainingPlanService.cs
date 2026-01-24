
using TMS.Application.TrainingPlans.DTOs;

namespace TMS.Application.TrainingPlans.Interfaces
{
    public interface ITrainingPlanService
    {
        public Task<Guid> CreateAsync(CreateTrainingPlanRequest request);
        public Task<TrainingPlanResponse> GetByIdAsync(Guid id);
        public Task<List<TrainingPlanResponse>> GetAllAsync();
        public Task UpdateAsync(Guid id, UpdateTrainingPlanRequest request);
        public Task DeleteAsync(Guid id);
        
    }
}