using TMS.Application.Enrollments.DTOs;

namespace TMS.Application.Enrollments.Interfaces
{
    public interface IEnrollmentService
    {
        public Task<Guid> CreateAsync(CreateEnrollmentRequest request);
        public Task<List<ResponseEnrollment>> GetAllAsync();
        public Task<ResponseEnrollment> GetByIdAsync();

    }
}
