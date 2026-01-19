namespace TMS.Application.Courses.DTOs
{
    public class CreateCourseRequest
    {

        public Guid CourseCategoryId { get; set; }
        public string CourseCode { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int DurationHours { get; set; }
    }
}
