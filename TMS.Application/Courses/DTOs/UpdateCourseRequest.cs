namespace TMS.Application.Courses.DTOs
{
    public class UpdateCourseRequest
    {
        public Guid Id { get; set; }
        public Guid CourseCategoryId { get; init; }
        public string CourseCode { get; init; } = null!;
        public string Title { get; init; } = null!;
        public string Description { get; init; } = null!;
        public int DurationHours { get; init; }
    }
}
