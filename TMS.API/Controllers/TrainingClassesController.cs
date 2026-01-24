using Microsoft.AspNetCore.Mvc;
using TMS.Application.TrainingClasses.DTOs;
using TMS.Application.TrainingClasses.Interfaces;

namespace TMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainingClassesController : ControllerBase
    {
        private readonly ITrainingClassService _services;
        public TrainingClassesController(ITrainingClassService services)
        {
            _services = services;
        }
        [HttpPost]
        public async Task<IActionResult> CreateTrainingClass([FromBody] CreateTrainingClassRequest request)
        {
            var trainingClassId = await _services.CreateAsync(request);
            var res = new CreateTrainingClassResponse
            {
                Id = trainingClassId,
                Message = "Training Class Created Successfully"
            };
            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrainingClassById(Guid id)
        {
            var trainingClass = await _services.GetByIdAsync(id);
            return Ok(trainingClass);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTrainingClasses()
        {
            var trainingClasses = await _services.GetAllAsync();
            return Ok(trainingClasses);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTrainingClassRequest request)
        {
            await _services.UpdateAsync(request);
            return Ok(new { Message = "Training Class Updated Successfully" });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _services.DeleteAsync(id);
            return Ok(new { Message = "Training Class Deleted Successfully" });
        }
    }
}