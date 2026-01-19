using TMS.Application.Courses.DTOs;

namespace TMS.Application.Courses.Interfaces
{
    public interface ICourseService
    {
        public Task<Guid> CreateAsync(CreateCourseRequest request);
        public Task<CourseResponse> GetByIdAsync(Guid id);
        public Task<List<CourseResponse>> GetAllAsync();
        public Task UpdateAsync(UpdateCourseRequest request);
        public Task DeleteAsync(Guid id);


    }
}
