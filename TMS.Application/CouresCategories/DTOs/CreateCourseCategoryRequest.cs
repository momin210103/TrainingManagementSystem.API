namespace TMS.Application.CouresCategories.DTOs
{
    public class CreateCourseCategoryRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string  Description { get; set; } = string.Empty;
    }
}
