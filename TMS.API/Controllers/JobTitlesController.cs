using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TMS.Application.JobTitles.DTOs;
using TMS.Application.JobTitles.Interfaces;

namespace TMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTitlesController: ControllerBase
    {
        private readonly IJobTitleService _service;

        public JobTitlesController(IJobTitleService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateJobTitleRequest request)
        {
            var id = await _service.CreateAsync(request);
            return Ok(new {
                Id = id,
                Message = "JobTitle created successfully"
            });
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var jobTitles = await _service.GetAllAsync();
            return Ok(jobTitles);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var jobTitle = await _service.GetByIdAsync(id);
            return Ok(jobTitle);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateJobTitleRequest request)
        {
            request.Id = id;
            await _service.UpdateAsync(request);
            return Ok(new {
                Message = "Update Succesfully"
            });

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return Ok(new
            {
                Message = "Delete Succesfully"
            }
            );

        }
    
    }
}
