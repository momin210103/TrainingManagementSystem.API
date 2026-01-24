namespace TMS.Application.TrainingPlans.DTOs
{
    public class CreateTrainingPlanRequest
    {
        // Foreign Key
        public Guid TrainingCategoryId { get; set; }

        // Business Fields
        public string PlanCode { get; set; } = string.Empty;
        public string PlanName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TrainingCompany { get; set; } = string.Empty;
        public string TrainingPlace { get; set; } = string.Empty;
        public decimal TrainingCost { get; set; }
        public string ContactPerson { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
    }
}