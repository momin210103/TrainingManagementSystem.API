using Microsoft.AspNetCore.Mvc;
using TMS.Application.TrainingPlans.DTOs;
using TMS.Application.TrainingPlans.Interfaces;

namespace TMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainingPlansController : ControllerBase
    {
        private readonly ITrainingPlanService _trainingPlanService;
        public TrainingPlansController(ITrainingPlanService trainingPlanService)
        {
            _trainingPlanService = trainingPlanService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateTrainingPlan([FromBody] CreateTrainingPlanRequest request)
        {
            var trainingPlanId = await _trainingPlanService.CreateAsync(request);
            return Ok(new { 
                Id = trainingPlanId ,
                Message = "Training Plan created successfully" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrainingPlanById(Guid id)
        {
            var trainingPlan = await _trainingPlanService.GetByIdAsync(id);
            return Ok(trainingPlan);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTrainingPlans()
        {
            var trainingPlans = await _trainingPlanService.GetAllAsync();
            return Ok(trainingPlans);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrainingPlan(Guid id, [FromBody] UpdateTrainingPlanRequest request)
        {
            await _trainingPlanService.UpdateAsync(id, request);
            return Ok(new { Message = "Training Plan updated successfully" }); 
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainingPlan(Guid id)
        {
            await _trainingPlanService.DeleteAsync(id);
            return Ok(new { Message = "Training Plan deleted successfully" });
        }
    }
}