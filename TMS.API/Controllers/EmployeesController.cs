using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TMS.Application.Common.Models;
using TMS.Application.Employees.DTOs;
using TMS.Application.Employees.Filters;
using TMS.Application.Employees.Interfaces;

namespace TMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController: ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        //Post : api/employees
        [HttpPost]

        public async Task<IActionResult> Create([FromBody] CreateEmployeeRequest request)
        {
          var id = await _employeeService.CreateAsync(request);
            var res = new CreateEmployeeResponse
            {
                Id = id,
                Message = "Employee Created Successfully",
            };
            return CreatedAtAction(nameof(GetById), new { id }, res);

        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            return Ok(employee);
        }
        // GET: api/employees
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest pagination,[FromQuery] EmployeeFilter filter)
        {
            var employees = await _employeeService.GetAllAsync(pagination,filter);
            return Ok(employees);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEmployeeRequest request)
        {
            request.Id = id;
            await _employeeService.UpdateAsync(request);

            return Ok(new
            {
                message = "Employee update successfully",
                id
            });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _employeeService.DeleteAsync(id);
            return Ok(new
            {
                message ="Employee Deleted successfully",
                id
            });
        }

    }
}
