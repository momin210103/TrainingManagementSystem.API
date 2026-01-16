using TMS.Application.CouresCategories.DTOs;

namespace TMS.Application.CouresCategories.Interfaces
{
    public interface ICourseCategoryService
    {
        public Task<Guid> CreateAsync(CreateCourseCategoryRequest request);
        public Task<List<CourseCategoryResponse>> GetAllAsync();
        public Task<CourseCategoryResponse> GetByIdAsync(Guid id);
        public Task UpdateAsync(UpdateCourseCategoryRequest request);
        public Task DeleteAsync(Guid id);

    }
}
