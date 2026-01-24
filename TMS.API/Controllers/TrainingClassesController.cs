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
    }
}