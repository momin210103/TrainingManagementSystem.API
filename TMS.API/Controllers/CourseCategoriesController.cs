using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TMS.Application.CouresCategories.DTOs;
using TMS.Application.CouresCategories.Interfaces;

namespace TMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseCategoriesController: ControllerBase
    {
        private readonly ICourseCategoryService _service;

        public CourseCategoriesController(ICourseCategoryService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseCategoryRequest request)
        {
            var id = await _service.CreateAsync(request);
            return Ok(new
            {
                id,
                Message = "Created Course Category Succesfull"
            });
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var all = await _service.GetAllAsync();
            return Ok(all);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var courseCategory = await _service.GetByIdAsync(id);
            return Ok(courseCategory);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id,UpdateCourseCategoryRequest request)
        {
            request.Id = id;
            await _service.UpdateAsync(request);
            return Ok(new
            {
                Message = "Update Succesfully"
            });

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return Ok(new
            {
                id,
                message = "Delete Succesfull"
            });
        }
    }
}
