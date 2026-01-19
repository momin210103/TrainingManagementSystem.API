namespace TMS.Application.Courses.DTOs
{
    public record CreateCourseResponse
    (
         Guid Id,
         string Title,
         string Message
    );
}
