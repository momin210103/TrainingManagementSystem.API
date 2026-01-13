using TMS.Application.Common.Models;
using TMS.Application.Employees.DTOs;
using TMS.Application.Employees.Filters;

namespace TMS.Application.Employees.Interfaces
{
    public interface IEmployeeService
    {
        Task<Guid> CreateAsync(CreateEmployeeRequest request);
        Task<EmployeeResponse> GetByIdAsync(Guid id);
        Task<PaginatedResponse<EmployeeResponse>> GetAllAsync(PaginationRequest pagination, EmployeeFilter filter);
        Task UpdateAsync(UpdateEmployeeRequest request);
        Task DeleteAsync(Guid id);



    }
}
