namespace TMS.Application.Enrollments.DTOs
{
    public class CreateEnrollmentRequest
    {
        public string Status { get; set; } = string.Empty;
        public Guid EmployeeId { get; set; }
        public Guid TrainingClassId { get; set; }
    }
}
