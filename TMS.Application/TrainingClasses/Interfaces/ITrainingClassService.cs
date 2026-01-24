using TMS.Application.TrainingClasses.DTOs;

namespace TMS.Application.TrainingClasses.Interfaces
{
    public interface ITrainingClassService
    {
        public Task<Guid> CreateAsync(CreateTrainingClassRequest request);
        public Task<TrainingClassResponse> GetByIdAsync(Guid id);
        public Task<List<TrainingClassResponse>> GetAllAsync();
        public Task UpdateAsync(UpdateTrainingClassRequest request);
        public Task DeleteAsync(Guid id);
        
    }
}