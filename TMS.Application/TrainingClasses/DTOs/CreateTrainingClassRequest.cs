using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.Application.TrainingClasses.DTOs
{
    public class CreateTrainingClassRequest
    {
        public Guid CourseId { get; set; }
        public Guid TrainingPlanId { get; set; }

        // Business Logic
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Capacity { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}