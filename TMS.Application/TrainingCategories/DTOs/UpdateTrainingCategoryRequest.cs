namespace TMS.Application.TrainingCategories.DTOs
{
    public class UpdateTrainingCategoryRequest
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}