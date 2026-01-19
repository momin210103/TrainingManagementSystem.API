using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TMS.Application.Enrollments.DTOs;
using TMS.Application.Enrollments.Interfaces;

namespace TMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController: ControllerBase
    {
        private readonly IEnrollmentService _service;

        public EnrollmentsController(IEnrollmentService service)
        {
            _service = service;
        }
        [HttpPost()]
        public async Task<IActionResult> Create(CreateEnrollmentRequest request)
        {
            var res = await _service.CreateAsync(request);
            return Ok(res);
        }

    }
}
