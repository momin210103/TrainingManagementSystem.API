
using Microsoft.AspNetCore.Mvc;
using TMS.Application.TrainingCategories.DTOs;
using TMS.Application.TrainingCategories.Interfaces;

namespace TMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainingCategoriesController : ControllerBase
    {
        private readonly ITrainingCategoryService _trainingCategoryService;
        public TrainingCategoriesController(ITrainingCategoryService trainingCategoryService)
        {
            _trainingCategoryService = trainingCategoryService;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTrainingCategoryRequest request)
        {
            var id = await _trainingCategoryService.CreateAsync(request);
            var res = new TrainingCategoryResponse
            {
                Id = id,
                Name = request.Name
            };
            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var trainingCategory = await _trainingCategoryService.GetByIdAsync(id);
            return Ok(trainingCategory);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var trainingCategories = await _trainingCategoryService.GetAllAsync();
            return Ok(trainingCategories);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTrainingCategoryRequest request)
        {
           await _trainingCategoryService.UpdateAsync(id, request);
           return Ok(new { Message = "Update Successfully" });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _trainingCategoryService.DeleteAsync(id);
            return Ok(new { Message = "Delete Successfully" });
        }


    }
}