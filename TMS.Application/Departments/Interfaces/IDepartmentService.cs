using TMS.Application.Common.Models;
using TMS.Application.Departments.DTOs;

namespace TMS.Application.Departments.Interfaces
{
    public interface IDepartmentService
    {
        Task<Guid> CreateAsync(CreateDepartmentRequest req);
        Task<PaginatedResponse<DepartmentResponse>> GetAllAsync(PaginationRequest pagination, DepartmentFilter filter);
        Task<DepartmentResponse> GetByIdAsync(Guid id);
        Task UpdateAsync(UpdateDepartmentRequest req);
        Task DeleteAsync(Guid id);

    }
}
