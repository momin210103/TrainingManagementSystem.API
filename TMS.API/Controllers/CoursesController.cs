using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TMS.Application.Courses.DTOs;
using TMS.Application.Courses.Interfaces;

namespace TMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController: ControllerBase
    {
        private readonly ICourseService _courseService;
        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCourseRequest request)
        {
            var id = await _courseService.CreateAsync(request);
            var res = new CreateCourseResponse(
                id,
                request.Title,
                "Course Created Successfully"
            );
            return Ok(res);
        }
        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _courseService.GetAllAsync();
            return Ok(courses);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var course = await _courseService.GetByIdAsync(id);
            return Ok(course);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCourseRequest request)
        {
            request.Id = id;
            await _courseService.UpdateAsync(request);
            return Ok(new
            {
                message = "Course updated successfully",
                id
            });
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _courseService.DeleteAsync(id);
            return Ok(new
            {
                message = "Course deleted successfully",
                id
            });
        }
    }
}
