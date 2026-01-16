namespace TMS.Application.JobTitles.DTOs
{
    public class UpdateJobTitleRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
