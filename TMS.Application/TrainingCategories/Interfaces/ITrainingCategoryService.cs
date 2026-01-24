using TMS.Application.TrainingCategories.DTOs;

namespace TMS.Application.TrainingCategories.Interfaces
{
    public interface ITrainingCategoryService
    {
        public Task<Guid> CreateAsync(CreateTrainingCategoryRequest request);
        public Task<TrainingCategoryResponse> GetByIdAsync(Guid id);
        public Task<List<TrainingCategoryResponse>> GetAllAsync();
        public Task UpdateAsync(Guid id, UpdateTrainingCategoryRequest request);
        public Task DeleteAsync(Guid id);
        
    }
}