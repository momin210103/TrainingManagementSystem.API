namespace TMS.Application.TrainingClasses.DTOs
{
    public class TrainingClassResponse
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Capacity { get; set; }
        public string Status { get; set; } = string.Empty;
        
    }
}