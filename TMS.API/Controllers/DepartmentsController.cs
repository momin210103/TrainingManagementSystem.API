using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TMS.Application.Common.Models;
using TMS.Application.Departments.DTOs;
using TMS.Application.Departments.Interfaces;

namespace TMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController: ControllerBase
    {
        private readonly IDepartmentService _service;

        public DepartmentsController(IDepartmentService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentRequest req)
        {
            var id = await _service.CreateAsync(req);
            return Ok(new
            {
                message = "Department Created Successfully",
                id
            });

        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest pagination, [FromQuery] DepartmentFilter filter,[FromQuery] SortingRequest sorting)
        {
            var departments = await _service.GetAllAsync(pagination,filter,sorting);
            return Ok(departments);

        }
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var dep = await _service.GetByIdAsync(id);
            return Ok(dep);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id,UpdateDepartmentRequest req)
        {
            req.Id = id;
            await _service.UpdateAsync(req);
            return Ok(new
            {
                message = "Department updated successfully",
                id
            });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return Ok(new { 
            message = "Department deleted successfully",
            id
            });
        }
    }
}
