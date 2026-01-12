using TMS.Application.Employees.DTOs;

namespace TMS.Application.Employees.Interfaces
{
    public interface IEmployeeService
    {
        Task<Guid> CreateAsync(CreateEmployeeRequest request);
        Task<EmployeeResponse> GetByIdAsync(Guid id);
        Task<List<EmployeeResponse>> GetAllAsync();
        Task UpdateAsync(UpdateEmployeeRequest request);
        Task DeleteAsync(Guid id);



    }
}
