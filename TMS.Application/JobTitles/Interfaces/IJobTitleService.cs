using TMS.Application.JobTitles.DTOs;

namespace TMS.Application.JobTitles.Interfaces
{
    public interface IJobTitleService
    {
        Task<Guid> CreateAsync(CreateJobTitleRequest request);
        Task<List<JobTitleResponse>> GetAllAsync();
        Task<JobTitleResponse> GetByIdAsync(Guid id);
        Task UpdateAsync(UpdateJobTitleRequest request);
        Task DeleteAsync(Guid id);
    }
}
