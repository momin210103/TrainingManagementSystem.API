using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.Application.TrainingPlans.DTOs
{
    public class CreateTrainingPlanResponse
    {
        // Foreign Key
        

        // Business Fields
        public string PlanCode { get; set; } = string.Empty;
        public string PlanName { get; set; } = string.Empty;
        
        
    }
}